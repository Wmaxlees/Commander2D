using UnityEditor;
using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  public class ProjectileVisual : VisualEffect {
    [SerializeField]
    protected SkillID skillID;

    protected ProjectileNodeState state;
    protected Projectile projectile;

    public override void Initialize() {
      this.state = ProjectileNodeState.NotFired;
    }

    public override State Run(UnitID casterID) {
      switch (this.state) {
        case ProjectileNodeState.NotFired:
          this.projectile = HitIndicatorLibrary.GetInstance().GetHitIndicator(this.skillID) as Projectile;
          if (this.projectile == null) {
            Debug.LogWarning("Trying to create a projectile for a skill that has none: " + this.skillID);
            return State.Failure;
          }
          Vector3 startPos = UnitController.GetInstance().GetUnitLocation(this.targetProvider.GetUnitTarget());
          this.projectile.transform.position = startPos;
          this.projectile.SetTarget(this.secondTargetProvider.GetUnitTarget());
          this.state = ProjectileNodeState.Fired;
          return State.Running;

        case ProjectileNodeState.Fired:
          if (this.projectile.IsDone()) {
            Destroy(this.projectile.gameObject);
            return State.Success;
          }

          return State.Running;
      }
      
      return State.Failure;
    }

    protected enum ProjectileNodeState {
      NotFired,
      Fired,
    }
  }
}