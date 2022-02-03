using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  /// <summary>
  /// Class <c>Grapple</c> provides the "Grapple" skill effect.
  /// 
  /// intensity: No effect.
  /// range: How far away the user can be from the target.
  /// duration: How many turns the target will be grappled.
  /// targetType: The target type.
  /// damageType: No effect.
  /// </summary>
  public class Grapple : SkillEffect {
    /// <summary>
    /// Method <c>ExecuteSkill</c> applies Grapple to the target.
    /// </summary>
    public override IEnumerator ExecuteSkill() {
      switch (targetType) {
        default:
          Debug.LogError("In Grapple, attempting to use targetType that isn't implemented.");
          break;
      }

      yield return null;
    }

    public override void Initialize() {
      throw new System.NotImplementedException();
    }

    public override State Run(UnitID casterID) {
      throw new System.NotImplementedException();
    }
  }
}