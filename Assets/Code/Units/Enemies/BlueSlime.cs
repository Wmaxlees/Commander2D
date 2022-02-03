using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Commander2D.TurnBased;

namespace Commander2D.Units.Enemies {

  /// <summary>
  ///  Class <c>BlueSlime</c> defines a blue slime <c>EnemyUnit</c>.
  /// </summary>
  public class BlueSlime : EnemyUnit {
    private void Awake() {
      stats = new UnitStats.UnitStatsBuilder()
        .Haste(10)
        .Speed(10)
        .Tenacity(3)
        .Mastery(10)
        .Build();
    }

    /// <summary>
    /// Method <c>GetSprite</c> returns the on-field Sprite for the unit.
    /// </summary>
    /// <returns>On-Field <c>Sprite</c> of the unit.</returns>
    public override Sprite GetSprite() {
      return Resources.Load<Sprite>("Enemies/BlueSlime");
    }

    /// <summary>
    /// Method <c>GetPortrait</c> returns the UI portrait <c>Sprite</c> for the unit.
    /// </summary>
    /// <returns><c>Sprite</c> UI portrait for the unit.</returns>
    public override Sprite GetPortrait() {
      return Resources.Load<Sprite>("Enemies/BlueSlime");
    }

    protected override void Update() {
      base.Update();

      if (!isMyTurn()) {
        return;
      }

      Vector3 cl3 = UnitController.GetInstance().GetUnitLocation(this.unitID);
      Vector2 currentLocation = new Vector2(cl3.x, cl3.y);
      if (this.isMoving) {
        if (currentLocation == this.targetLocation) {
          this.isMoving = false;
        }
        return;
      }

      Vector3 tl3 = UnitController.GetInstance().GetUnitLocation(this.targetID);
      Vector2 targetLocation = new Vector2(tl3.x, tl3.y);
      float distanceToTarget = Vector2.Distance(currentLocation, targetLocation);
      if (distanceToTarget > 1 && TurnBasedController.GetInstance().GetAvailableActionPoints() > 0) {
        this.isMoving = true;
        this.targetLocation = TurnBasedController.GetInstance().GetClosestLocationCanMoveTowards(this.targetID, 0.9f);
        TurnBasedController.GetInstance().CurrentUnitMoved(this.targetLocation);
      } else if (distanceToTarget <= 1 && TurnBasedController.GetInstance().GetAvailableActionPoints() >= 2) {
        Debug.Log("BlueSlime unit attacks.");
        UnitController.GetInstance().ApplyDamage(this.targetID, 2, Skills.Effects.SkillEffect.DamageType.Physical);
        TurnBasedController.GetInstance().UseActionPoints(2);
      } else {
        Debug.Log("Ending my turn.");
        TurnBasedController.GetInstance().EndCurrentUnitTurn();
      }
    }
  }
}