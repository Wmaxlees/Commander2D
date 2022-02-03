using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

using Commander2D.Units.Skills.Effects;

namespace Commander2D.UnityGUI {
  public class SkillGraphView : GraphView {
    public new class UxmlFactory : UxmlFactory<SkillGraphView, GraphView.UxmlTraits> {}

    private SkillGraph skillGraph;

    public SkillGraphView() {
      Insert(0, new GridBackground());

      this.AddManipulator(new ContentZoomer());
      this.AddManipulator(new ContentDragger());
      this.AddManipulator(new SelectionDragger());
      this.AddManipulator(new RectangleSelector());

      var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Code/UnityGUI/SkillEffectEditor.uss");
      styleSheets.Add(styleSheet);
    }

    internal void PopulateView(SkillGraph graph) {
      this.skillGraph = graph;

      this.graphViewChanged -= OnGraphViewChanged;
      this.DeleteElements(this.graphElements.ToList());
      this.graphViewChanged += OnGraphViewChanged;

      if (this.skillGraph.rootNode) {
        this.CreateNodeView(this.skillGraph.rootNode);
      }
      this.skillGraph.nodes.ForEach(n => this.CreateNodeView(n));

      this.skillGraph.nodes.ForEach(n => {
        var parentView = (GetNodeByGuid(n.guid) as SkillGraphNodeView);

        var children = this.skillGraph.GetChildren(n);
        children.ForEach(c => {  
          var childView = (GetNodeByGuid(c.guid) as SkillGraphNodeView);
          AddElement(parentView.controlFlowOutPort.ConnectTo(childView.controlFlowInPort));
        });

        if (n.GetType().IsSubclassOf(typeof(SkillEffect))) {
          var targetProvider = this.skillGraph.GetTargetProvider(n as SkillEffect);
          if (targetProvider) {
            var tpView = GetNodeByGuid(targetProvider.guid) as SkillGraphNodeView;
            AddElement(parentView.targetInPort1.ConnectTo(tpView.targetOutPort));
          }
        }

        if (n.GetType().IsSubclassOf(typeof(VisualEffect))) {
          var targetProvider = this.skillGraph.GetTargetProvider(n as VisualEffect);
          if (targetProvider) {
            var tpView = GetNodeByGuid(targetProvider.guid) as SkillGraphNodeView;
            AddElement(parentView.targetInPort1.ConnectTo(tpView.targetOutPort));
          }

          var targetProvider2 = this.skillGraph.GetSecondTargetProvider(n as VisualEffect);
          if (targetProvider2) {
            var tpView = GetNodeByGuid(targetProvider2.guid) as SkillGraphNodeView;
            AddElement(parentView.targetInPort2.ConnectTo(tpView.targetOutPort));
          }
        }
      });

      if (this.skillGraph.rootNode) {
        this.skillGraph.rootNode.GetChildren().ForEach(c => {
          var parentView = (GetNodeByGuid(this.skillGraph.rootNode.guid) as SkillGraphNodeView);
          var childView = (GetNodeByGuid(c.guid) as SkillGraphNodeView);

          AddElement(parentView.controlFlowOutPort.ConnectTo(childView.controlFlowInPort));
        });
      }
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
      DropdownMenuAction.Status menuItemStatus = DropdownMenuAction.Status.Normal;
      if (this.skillGraph == null) {
        menuItemStatus = DropdownMenuAction.Status.Disabled;
      }

      if (this.skillGraph.rootNode == null) {
        evt.menu.AppendAction($"Start", (a) => CreateRootNode(), menuItemStatus);
      } else {
        var types = TypeCache.GetTypesDerivedFrom<SkillGraphNode>();
        foreach (var type in types) {
          if (type == typeof(Start)) {
            continue;
          }
          evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type), menuItemStatus);
        }
      }
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
      return this.ports.ToList().ToList().Where(endPort =>
        endPort.direction != startPort.direction &&
        endPort.node != startPort.node &&
        !typeof(InputNode).IsInstanceOfType(endPort.node) &&
        endPort.portType == startPort.portType
      ).ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange) {
      if (graphViewChange.elementsToRemove != null) {
        graphViewChange.elementsToRemove.ForEach(elem => {
          SkillGraphNodeView nodeView = elem as SkillGraphNodeView;
          if (nodeView != null) {
            this.skillGraph.DeleteNode(nodeView.node);
          }

          Edge edge = elem as Edge;
          if (edge != null) {
            SkillGraphNodeView child = edge.input.node as SkillGraphNodeView;

            SkillGraphNodeView parent = edge.output.node as SkillGraphNodeView;
            if (parent != null) {
              this.skillGraph.RemoveChild(parent.node, child.node);
            }
          }
        });
      }

      if(graphViewChange.edgesToCreate != null) {
        graphViewChange.edgesToCreate.ForEach(edge => {
          SkillGraphNodeView child = edge.input.node as SkillGraphNodeView;
          Type inputType = edge.input.portType;
          Type outputType = edge.output.portType;

          SkillGraphNodeView parent = edge.output.node as SkillGraphNodeView;
          if (parent != null) {
            if (outputType == typeof(int)) {
              var inputNode = parent.node as InputNode;

              var skillEffect = child.node as SkillEffect;
              if (skillEffect && inputNode) {
                this.skillGraph.SetTargetProvider(skillEffect, inputNode);
              }

              var visualEffect = child.node as VisualEffect;
              if (visualEffect && inputNode) {
                if (edge.input == child.targetInPort1) {
                  this.skillGraph.SetTargetProvider(visualEffect, inputNode);
                } else {
                  this.skillGraph.SetSecondTargetProvider(visualEffect, inputNode);
                }
              }
            } else if (outputType == typeof(bool)) {
              this.skillGraph.AddChild(parent.node, child.node);
            }
          }
        });
      }
      return graphViewChange;
    }

    private void CreateNode(Type type) {
      SkillGraphNode node = this.skillGraph.CreateNode(type);
      this.CreateNodeView(node);
    }

    private void CreateRootNode() {
      SkillGraphNode node = this.skillGraph.CreateRootNode();
      this.CreateNodeView(node);
    }

    private void CreateNodeView(SkillGraphNode node) {
      SkillGraphNodeView nodeView = new SkillGraphNodeView(node);
      this.AddElement(nodeView);
    }
  }
}