using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  /// <summary>
  /// Class <c>Push</c> provides the push skill effect.
  /// 
  /// intensity: How far the target will be pushed.
  /// range: How far away the user can be from the target.
  /// duration: No effect.
  /// targetType: The type of thing this effect can target.
  /// damageType: No effect.
  /// </summary>
  public class Push : SkillEffect {
    /// <summary>
    /// Method <c>ExecuteSkill</c> applies Push to the target.
    /// </summary>
    public override void Initialize() {
      throw new System.NotImplementedException();
    }

    public override State Run(UnitID casterID) {
      throw new System.NotImplementedException();
    }
  }
}