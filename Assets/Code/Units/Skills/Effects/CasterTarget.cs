using UnityEditor;
using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  public class CasterTarget : InputNode {
    public override SkillEffect.TargetType GetTargetType() {
      return SkillEffect.TargetType.Self;
    }

    public override UnitID GetUnitTarget() {
      return TurnBased.TurnBasedController.GetInstance().GetCurrentActorID();
    }

    public override void Initialize() {}

    public override State Run(UnitID casterID) {
      return State.Success;
    }
  }
}