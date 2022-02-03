using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  /// <summary>
  /// Class <c>Taunt</c> provides the taunt skill effect.
  /// 
  /// intensity: No effect.
  /// range: How far away the user can be from the target.
  /// duration: How many turns the target is taunted.
  /// targetType: The types of things that can be targeted by this effect.
  /// damageType: No effect.
  /// </summary>
  public class Taunt : SkillEffect {
    public override void Initialize() {}

    public override State Run(UnitID casterID) {
      UnitID target = this.targetProvider.GetUnitTarget();

      UnitController.GetInstance().ApplyBuff(target, new Buffs.Taunt(this.duration, casterID));

      return State.Success;
    }
  }
}