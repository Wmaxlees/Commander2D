using System.Collections;
using UnityEngine;

namespace Commander2D.Units.Skills {
  public class Flare : HitIndicator {
    [SerializeField]
    protected float lifetimeMs;

    protected System.DateTime createdTime;

    public override bool IsDone() {
      return (System.DateTime.Now - createdTime).Milliseconds >= this.lifetimeMs;
    }

    public override void SetTarget(UnitID targetID) {
      this.createdTime = System.DateTime.Now;
      this.transform.position = UnitController.GetInstance().GetUnitLocation(targetID);
    }
  }
}