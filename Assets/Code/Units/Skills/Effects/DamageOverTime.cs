using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Commander2D.Units.Skills.Buffs;

namespace Commander2D.Units.Skills.Effects {
  /// <summary>
  /// Class <c>DamageOverTimee</c> provides the damage over time skill effect.
  /// 
  /// intensity: The base damage of the effect.
  /// range: How far away the target can be.
  /// duration: How many turns the damage will be applied to the target.
  /// targetType: The type of thing this effect can target.
  /// damageType: The type of damage that is dealt to the target.
  /// </summary>
  public class DamageOverTime : SkillEffect {
    public override void Initialize() {}

    public override State Run(UnitID casterID) {
      UnitController.GetInstance().ApplyBuff(this.targetProvider.GetUnitTarget(), new Buffs.DamageOverTime(this.duration, this.intensity, this.damageType));

      return State.Success;
    }
  }
}