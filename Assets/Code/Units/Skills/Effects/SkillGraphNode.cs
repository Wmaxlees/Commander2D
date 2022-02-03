using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

using Commander2D.Board;
using System;

namespace Commander2D.Units.Skills.Effects {
  public abstract class SkillGraphNode : ScriptableObject {
    [HideInInspector]
    public string guid;

    [HideInInspector]
    public Vector2 guiPosition;

    [SerializeField]
    protected List<SkillGraphNode> children = new List<SkillGraphNode>();

    public void AddChild(SkillGraphNode child) {
      this.children.Add(child);
    }

    public void RemoveChild(SkillGraphNode child) {
      this.children.Remove(child);
    }

    public List<SkillGraphNode> GetChildren() {
      return this.children;
    }

    public abstract State Run(UnitID casterID);
    public abstract void Initialize ();

    public enum State {
      Success,
      Failure,
      Running,
    }
  }
}