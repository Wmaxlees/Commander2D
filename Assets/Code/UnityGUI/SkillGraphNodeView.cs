using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

using Commander2D.Units.Skills.Effects;
using System;

namespace Commander2D.UnityGUI {
  public class SkillGraphNodeView : Node {

    public SkillGraphNode node;

    public Port controlFlowInPort;
    public Port controlFlowOutPort;

    public Port targetInPort1;
    public Port targetInPort2;
    public Port targetOutPort;

    public SkillGraphNodeView(SkillGraphNode node) {
      this.node = node;
      this.title = node.name;

      this.viewDataKey = node.guid;

      this.style.left = node.guiPosition.x;
      this.style.top = node.guiPosition.y;

      this.CreateControlFlowOutPort();

      if (this.node.GetType() == typeof(Start)) {
        this.style.backgroundColor = new StyleColor(new Color(0.2f, 0.4f, 0.2f));
        this.style.width = 120;
      } else if (this.node.GetType().IsSubclassOf(typeof(InputNode))) {
        if (this.node.GetType() != typeof(CasterTarget)) {
          this.CreateControlFlowInPort();
        } else {
          this.outputContainer.Remove(this.controlFlowOutPort);
        }
        this.CreateTargetOutPort();
        this.style.backgroundColor = new StyleColor(new Color(0.6f, 0.4f, 0.6f));
        this.style.width = 220;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Code/UnityGUI/InputNodeView.uxml");
        var vtI = visualTree.Instantiate();
        this.extensionContainer.Add(vtI);

        SerializedObject so = new SerializedObject(this.node);
        this.extensionContainer.Bind(so);

        this.RefreshExpandedState();

        this.expanded = true;
      } else if (this.node.GetType().IsSubclassOf(typeof(VisualEffect))) {
        this.CreateControlFlowInPort();
        if (this.node.GetType() == typeof(UnitAnimation) || this.node.GetType() == typeof(FlareVisual)) {
          this.CreateTargetInPort1("Unit");
        } else if (this.node.GetType() == typeof(ProjectileVisual)) {
          this.CreateTargetInPort1("Starting Unit");
          this.CreateTargetInPort2("Ending Unit");
        }
        this.style.backgroundColor = new StyleColor(new Color(0.2f, 0.5f, 0.5f));
        this.style.width = 300;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Code/UnityGUI/VisualEffectNodeView.uxml");
        var vtI = visualTree.Instantiate();
        this.extensionContainer.Add(vtI);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Code/UnityGUI/VisualEffectNodeView.uss");
        this.extensionContainer.styleSheets.Add(styleSheet);

        SerializedObject so = new SerializedObject(this.node);
        this.extensionContainer.Bind(so);

        this.RefreshExpandedState();

        this.expanded = true;

      } else if (this.node.GetType().IsSubclassOf(typeof(SkillEffect))) {
        this.CreateControlFlowInPort();
        this.CreateTargetInPort1("Target");
        this.style.width = 300;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Code/UnityGUI/SkillEffectNodeView.uxml");
        var vtI = visualTree.Instantiate();
        this.extensionContainer.Add(vtI);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Code/UnityGUI/SkillEffectNodeView.uss");
        this.extensionContainer.styleSheets.Add(styleSheet);

        SerializedObject so = new SerializedObject(this.node);
        this.extensionContainer.Bind(so);

        this.RefreshExpandedState();

        this.expanded = true;
      }
    }

    private void CreateControlFlowInPort() {
      this.controlFlowInPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
      this.controlFlowInPort.portName = "Control Flow";
      this.inputContainer.Add(this.controlFlowInPort);
    }

    private void CreateControlFlowOutPort() {
      this.controlFlowOutPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
      this.controlFlowOutPort.portName = "Control Flow";
      this.outputContainer.Add(this.controlFlowOutPort);
    }

    private void CreateTargetInPort1(string name) {
      this.targetInPort1 = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(int));
      this.targetInPort1.portName = name;
      this.inputContainer.Add(this.targetInPort1);
    }
    
    private void CreateTargetInPort2(string name) {
      this.targetInPort2 = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(int));
      this.targetInPort2.portName = name;
      this.inputContainer.Add(this.targetInPort2);
    }

    private void CreateTargetOutPort() {
      this.targetOutPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(int));
      this.targetOutPort.portName = "Target";
      this.outputContainer.Add(this.targetOutPort);
    }



    public override void SetPosition(Rect newPos) {
      base.SetPosition(newPos);
      this.node.guiPosition.x = newPos.xMin;
      this.node.guiPosition.y = newPos.yMin;
    }
  }
}
