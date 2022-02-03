using UnityEditor;
using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  public class Start : SkillGraphNode {
    public override void Initialize() {}

    public override State Run(UnitID casterID) {
      return State.Success;
    }
  }
}