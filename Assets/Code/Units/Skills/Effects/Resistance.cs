using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

using Commander2D.Units.Skills.Buffs;

namespace Commander2D.Units.Skills.Effects {
  /// <summary>
  /// Class <c>Resistance</c> provides the skill effect that causes a target to be more or less sensitive
  /// to a particular type of damage.
  /// 
  /// intensity: How much increase or decrease in resistance.
  /// range: How far away the target can be.
  /// duration: How long the target is more sensitive.
  /// targetType: The type of thing this effect can target.
  /// damageType: The type of damage the target becomes more sensitive to.
  /// </summary>
  public class Resistance : SkillEffect {
    public override void Initialize() {}

    public override State Run(UnitID casterID) {
      InputNode inputNode = this.targetProvider as InputNode;
      if (inputNode != null) {
        float modifiedDuration = this.duration * UnitController.GetInstance().GetBuffTimeMultiplier(casterID);
        Debug.Log("Applying resistance " + inputNode.GetTargetType() + " type: " + inputNode.GetUnitTarget() + " for " + this.intensity + " percent and " + modifiedDuration + " turns.");
        return State.Success;
      } else {
        return State.Failure;
      }
    }
  }
}