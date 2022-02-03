using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Commander2D.Units;

namespace Commander2D.Units.Skills {

  public class Projectile : HitIndicator {
    private Vector2 target;

    public override void SetTarget(UnitID unitID) {
      this.target = UnitController.GetInstance().GetUnitLocation(unitID);
    }

    public override bool IsDone() {
      Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
      position2D = Vector2.MoveTowards(position2D, this.target, 12 * Time.deltaTime);
      return (position2D - this.target).SqrMagnitude() == 0.0;
    }

    private void Update() {
      Debug.Log("Hello?");
      Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
      position2D = Vector2.MoveTowards(position2D, this.target, 12 * Time.deltaTime);

      Vector3 targetDirection = new Vector3 (position2D.x, position2D.y, transform.position.z) - transform.position;

      transform.position = new Vector3(position2D.x, position2D.y, transform.position.z);

      Vector3 newRotation = Vector3.RotateTowards(transform.position, targetDirection, float.MaxValue, float.MaxValue);
      this.gameObject.transform.right = targetDirection;

      Debug.Log("Me: " + this.gameObject.transform.position);

      Debug.Log(this.gameObject.transform.rotation);
      Debug.DrawRay(position2D, newRotation, Color.red);
    }
  }
}