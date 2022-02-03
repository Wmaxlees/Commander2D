using UnityEditor;
using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  public class UnitAnimation : VisualEffect {
    [SerializeField]
    protected string animationName;

    protected UnitAnimationNodeState state;

    public override void Initialize() {
      this.state = UnitAnimationNodeState.NotStarted;
    }

    public override State Run(UnitID casterID) {
      switch (this.state) {
        case UnitAnimationNodeState.NotStarted:
          UnitController.GetInstance().SendAnimationRequest(this.targetProvider.GetUnitTarget(), this.animationName);
          this.state = UnitAnimationNodeState.Started;
          return State.Running;

        case UnitAnimationNodeState.Started:
          bool stillPlaying = UnitController.GetInstance().IsPlayingAnimation(this.targetProvider.GetUnitTarget(), this.animationName);

          if (!stillPlaying) {
            return State.Success;
          }
          
          return State.Running;
      }
      
      return State.Failure;
    }

    protected enum UnitAnimationNodeState {
      NotStarted,
      Started,
    }
  }
}