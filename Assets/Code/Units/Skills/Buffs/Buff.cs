using UnityEditor;
using UnityEngine;

namespace Commander2D.Units.Skills.Buffs {
  public abstract class Buff {
    /// <summary>
    /// Property <c>duration</c> tracks how long the buff/debuff will last.
    /// -1 indicates that the duration is permanent.
    /// </summary>
    protected int duration;

    /// <summary>
    /// Property <c>timeSinceApplication</c> tracks how long it has been since
    /// the buff/debuff was applied.
    /// </summary>
    protected int timeSinceApplication;

    /// <summary>
    /// Method <c>Tick</c> is called once per turn on the buff/debuff and applies
    /// whatever turn based effects it has.
    /// </summary>
    /// <returns>Whether the buff has reached its time limit.</returns>
    public abstract bool Tick(UnitID unitID);

    /// <summary>
    /// Method <c>ModifyStats</c> is called any time a unit's stats are needed for
    /// a check. If the buff/debuff modifies stats, the effect will be applied.
    /// 
    /// E.G. If the debuff makes the target take more damage from fire based effects,
    /// this method will decrease the fire resistance value in the stat block
    /// by the appropriate amount.
    /// </summary>
    /// <param name="stats"></param>
    /// <returns></returns>
    public abstract UnitStats ModifyStats(UnitStats stats);

    public Buff(int duration) {
      this.duration = duration;
    }

    protected bool UpdateTime() {
      if (this.duration == -1) {
        return false;
      }

      this.timeSinceApplication += 1;
      return this.timeSinceApplication >= this.duration;
    }
  }
}