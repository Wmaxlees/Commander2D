using UnityEditor;
using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  public class UnitTarget : InputNode {
    protected UnitID target;
    protected UnitID caster;

    [SerializeField]
    protected UnitTargetNodeState state = UnitTargetNodeState.None;

    public override SkillEffect.TargetType GetTargetType() {
      return SkillEffect.TargetType.Unit;
    }

    public void SetTarget(UnitID unit) {
      if (!this.CanCastOn(this.caster, unit)) {
        Debug.Log("Cannot cast on that unit.");
        return;
      }

      this.state = UnitTargetNodeState.ReceivedUnitSelect;
      this.target = unit;
    }

    public override UnitID GetUnitTarget() {
      return target;
    }

    public override State Run(UnitID casterID) {
      switch (this.state) {
        case UnitTargetNodeState.None:
          this.caster = casterID;
          InputController.GetInstance().AddNotifyOnUnitClick(SetTarget);
          GameManager.GetInstance().SetGameState(GameManager.GameState.TargetSelect);
          this.state = UnitTargetNodeState.RegisteredForUnitSelect;
          return State.Running;

        case UnitTargetNodeState.RegisteredForUnitSelect:
          return State.Running;

        case UnitTargetNodeState.ReceivedUnitSelect:
          InputController.GetInstance().RemoveNotifyOnUnitClick(SetTarget);
          GameManager.GetInstance().SetGameState(GameManager.GameState.Normal);
          return State.Success;

        default:
          return State.Failure;
      }
    }

    public override void Initialize() {
      this.state = UnitTargetNodeState.None;
    }

    protected enum UnitTargetNodeState {
      None,
      RegisteredForUnitSelect,
      ReceivedUnitSelect,
    }
  }
}