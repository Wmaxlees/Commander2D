using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  /// <summary>
  /// Class <c>Wait</c> provides an empty skill effect that can be used
  /// for ordering skills properly.
  /// 
  /// intensity: No effect.
  /// range: No effect.
  /// duration: How many seconds to wait.
  /// targetType: No effect.
  /// damageType: No effect.
  /// </summary>
  public class Wait : SkillEffect {
    /// <summary>
    /// Method <c>ExecuteSkill</c> waits.
    /// </summary>
    public override IEnumerator ExecuteSkill() {
      // TBD

      yield return null;
    }

    public override void Initialize() {
      throw new System.NotImplementedException();
    }

    public override State Run(UnitID CasterID) {
      throw new System.NotImplementedException();
    }
  }
}