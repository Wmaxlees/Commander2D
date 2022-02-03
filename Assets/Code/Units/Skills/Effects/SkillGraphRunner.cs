using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  public class SkillGraphRunner : ScriptableObject {
    protected SkillGraph skillGraph;

    protected List<SkillGraphNode> currentNodes;

    protected UnitID casterID;

    public void SetSkillGraph(UnitID casterID, SkillGraph skillGraph) {
      this.skillGraph = skillGraph;
      this.casterID = casterID;

      this.currentNodes = new List<SkillGraphNode>();
      this.currentNodes.Add(this.skillGraph.rootNode);

      foreach (SkillGraphNode node in this.skillGraph.nodes) {
        node.Initialize();
      }
    }

    public int GetActionCost() {
      return this.skillGraph.GetActionCost();
    }

    public SkillGraphNode.State Run() {
      if (this.currentNodes.Count == 0) {
        return SkillGraphNode.State.Success;
      }

      SkillGraphNode.State returnState = SkillGraphNode.State.Running;
      List<SkillGraphNode> nodesToRemove = new List<SkillGraphNode>();
      List<SkillGraphNode> nodesToAdd = new List<SkillGraphNode>();
      foreach (SkillGraphNode node in this.currentNodes) {
        SkillGraphNode.State nodeState = node.Run(this.casterID);

        switch (nodeState) {
          case SkillGraphNode.State.Failure:
            return SkillGraphNode.State.Failure;

          case SkillGraphNode.State.Running:
            continue;

          case SkillGraphNode.State.Success:
            nodesToAdd.AddRange(node.GetChildren());
            nodesToRemove.Add(node);
            continue;
        }
      }

      this.currentNodes.AddRange(nodesToAdd);

      foreach (SkillGraphNode node in nodesToRemove) {
        this.currentNodes.Remove(node);
      }

      return returnState;
    }
  }
}