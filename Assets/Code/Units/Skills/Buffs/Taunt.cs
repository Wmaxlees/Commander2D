using UnityEngine;

using Commander2D.Units.Skills.Effects;

namespace Commander2D.Units.Skills.Buffs {
  public class Taunt : Buff {
    private UnitID forcedTarget;

    public Taunt(int duration, UnitID forcedTarget) : base(duration) {
      this.forcedTarget = forcedTarget;
    }

    public override UnitStats ModifyStats(UnitStats stats) {
      return stats;
    }

    public override bool Tick(UnitID unitID) {
      UnitController.GetInstance().SetTarget(unitID, this.forcedTarget);
      return UpdateTime();
    }
  }
}