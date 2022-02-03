using UnityEngine;

using Commander2D.Units.Skills.Effects;

namespace Commander2D.Units.Skills.Buffs {
  public class DamageOverTime : Buff {
    private int intensity;
    private Effects.Damage.DamageType damageType;

    public DamageOverTime(int duration, int intensity, Effects.Damage.DamageType damageType) : base(duration) {
      this.intensity = intensity;
      this.damageType = damageType;
    }

    public override UnitStats ModifyStats(UnitStats stats) {
      return stats;
    }

    public override bool Tick(UnitID unitID) {
      UnitController.GetInstance().ApplyDamage(unitID, this.intensity, this.damageType);

      return UpdateTime();
    }
  }
}