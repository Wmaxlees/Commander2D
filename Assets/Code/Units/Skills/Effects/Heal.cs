using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  public class Heal : SkillEffect {
    public override void Initialize() {}

    public override State Run(UnitID casterID) {
      UnitController.GetInstance().ApplyHeal(this.targetProvider.GetUnitTarget(), this.intensity);
      return State.Success;
    }
  }
}