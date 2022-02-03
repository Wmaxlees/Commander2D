using UnityEditor;
using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  public class FlareVisual : VisualEffect {
    [SerializeField]
    protected SkillID skillID;

    protected FlareNodeState state;
    protected Flare flare;

    public override void Initialize() {
      this.state = FlareNodeState.NotFired;
    }

    public override State Run(UnitID casterID) {
      switch (this.state) {
        case FlareNodeState.NotFired:
          this.flare = HitIndicatorLibrary.GetInstance().GetHitIndicator(this.skillID) as Flare;
          if (this.flare == null) {
            Debug.LogWarning("Trying to create a flare for a skill that has none: " + this.skillID);
            return State.Failure;
          }
          this.flare.SetTarget(this.targetProvider.GetUnitTarget());
          this.state = FlareNodeState.Fired;
          return State.Running;

        case FlareNodeState.Fired:
          if (this.flare.IsDone()) {
            Destroy(this.flare.gameObject);
            return State.Success;
          }

          return State.Running;
      }
      
      return State.Failure;
    }

    protected enum FlareNodeState {
      NotFired,
      Fired,
    }
  }
}