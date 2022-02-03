using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  /// <summary>
  /// Class <c>Damage</c> provides the damage skill effect.
  /// 
  /// intensity: The base damage of the effect.
  /// range: How far away the target can be.
  /// duration: No effect (for DOT, look at the <c>DamageOverTime</c> effect.
  /// targetType: The type of thing this effect can target.
  /// damageType: The type of damage that will be applied.
  /// </summary>
  public class Damage : SkillEffect {
    public override void Initialize() {}

    public override State Run(UnitID casterID) {
      InputNode inputNode = this.targetProvider as InputNode;
      if (inputNode != null) {
        Debug.Log("Damaging " + inputNode.GetTargetType() + " type: " + inputNode.GetUnitTarget() + " for " + this.intensity + " points.");
        UnitController.GetInstance().ApplyDamage(this.targetProvider.GetUnitTarget(), this.intensity, this.damageType);
        return State.Success;
      } else {
        return State.Failure;
      }
    }
  }
}