using System.Collections;
using UnityEngine;

namespace Commander2D.Units.Skills {
  public abstract class HitIndicator : MonoBehaviour {
    public SkillID skillID;

    public abstract bool IsDone();
    public abstract void SetTarget(UnitID targetID);
  }
}