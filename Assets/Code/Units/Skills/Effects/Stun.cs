using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  /// <summary>
  /// Class <c>Stun</c> provides the stun skill effect.
  /// 
  /// intensity: No effect.
  /// range: How far away the user can be from the target.
  /// duration: How many turns the target is stunned.
  /// targetType: The type of thing this effect can target.
  /// damageType: No effect.
  /// </summary>
  public class Stun : SkillEffect {
    /// <summary>
    /// Method <c>ExecuteSkill</c> applies the stun effect to the target.
    /// </summary>
    public override IEnumerator ExecuteSkill() {
      switch (targetType) {
        default:
          Debug.LogError("In Stun, attempting to use targetType that isn't implemented.");
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