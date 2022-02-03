using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using Commander2D.Board;

namespace Commander2D.Units.Skills.Effects {
  public abstract class InputNode : SkillGraphNode {
    [SerializeField]
    protected int range;

    public abstract SkillEffect.TargetType GetTargetType();
    public abstract UnitID GetUnitTarget();

    /// <summary>
    /// Method <c>CanCastOn</c> determines whether the <c>targetUnit</c> can be the
    /// target of this.
    /// </summary>
    /// <param name="target">The <c>UnitID</c> in question.</param>
    /// <returns><c>true</c> if the <c>Unit</c> can be targeted, <c>false</c> otherwise.</returns>
    public bool CanCastOn(UnitID casterID, UnitID targetID) {
      if (this.range == -1) {
        return true;
      }

      Vector3 targetPos = UnitController.GetInstance().GetUnitLocation(targetID);
      targetPos.z = 0.0f;

      Vector3 casterPos = UnitController.GetInstance().GetUnitLocation(casterID);
      casterPos.z = 0.0f;
      Vector3 dist = GameBoard.GetInstance().GetBoardDistance(targetPos, casterPos);

      float mag = Vector3.SqrMagnitude(dist);

      return mag <= range * range;
    }

  }
}