using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Commander2D.Units;

namespace Commander2D.UI {
  /// <summary>
  /// Class <c>TurnOrderPane</c> provides ways of interacting with the turn order
  /// HUD element.
  /// </summary>
  public class TurnOrderPane : MonoBehaviour {
    /// <summary>
    /// Property <c>portraitPrefab</c> provides a handle to the PortraitPrefab which is
    /// duplicated to create the individual turn based portraits.
    /// </summary>
    [SerializeField]
    private TurnPortraitPrefab portraitPrefab;

    /// <summary>
    /// Property <c>portraits</c> contains the list of all portraits in the turn
    /// order panel.
    /// </summary>
    private Dictionary<UnitID, TurnPortraitPrefab> profiles = new Dictionary<UnitID, TurnPortraitPrefab>();

    /// <summary>
    /// Method <c>SetTurnOrder</c> sets the order of units to display in the HUD.
    /// </summary>
    /// <param name="unitIDs">The ordered list of units.</param>
    public void SetTurnOrder(ICollection<Tuple<float, UnitID>> unitIDs) {
      foreach (KeyValuePair<UnitID, TurnPortraitPrefab> go in this.profiles) {
        Destroy(go.Value);
      }

      this.profiles = new Dictionary<UnitID, TurnPortraitPrefab>();

      Vector3 loc = this.gameObject.transform.position;
      loc.x -= 1.0f;
      foreach (Tuple<float, UnitID> unitID in unitIDs) {
        TurnPortraitPrefab newPrefab = Instantiate(this.portraitPrefab, loc, Quaternion.identity, this.gameObject.transform);
        newPrefab.SetUnit(unitID.Item2, unitID.Item1);
        this.profiles[unitID.Item2] = newPrefab;

        loc.x += 1.0f;
      }
    }

    public void UpdateHealth(UnitID unitID, int currentHP, int maxHP) {
      if (!this.profiles.ContainsKey(unitID)) {
        return;
      }
      this.profiles[unitID].UpdateHealth(currentHP, maxHP);
    }

  }
}