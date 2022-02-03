using Commander2D.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander2D.UI.PlayerUnit {
  public class PlayerUnitInfoPaneController : MonoBehaviour {
    /// <summary>
    /// Property <c>playerInfoPanes</c> contains handles to all the UI player info frames.
    /// </summary>
    private PlayerUnitInfoPane[] playerInfoPanes;

    /// <summary>
    /// Method <c>Awake</c> is called automatically when the object is created.
    /// </summary>
    private void Awake() {
      playerInfoPanes = gameObject.GetComponentsInChildren<PlayerUnitInfoPane>();
    }

    /// <summary>
    /// Method <c>SetPlayerUnitSelectorIndicator</c> sets the selection indicator for the particular unit ID.
    /// </summary>
    /// <param name="unitID">The player's unit ID.</param>
    /// <param name="on">Whether the unit selector should be shown.</param>
    public void SetPlayerUnitSelectorIndicator(UnitID unitID, bool on) {
      playerInfoPanes[(int)unitID].SetSelectorIndicator(on);
    }

    /// <summary>
    /// Method <c>SetPlayerUnitHealth</c> sets the health of the particular unit ID.
    /// </summary>
    /// <param name="unitID">The player's unit ID.</param>
    /// <param name="maxHP">The maximum health.</param>
    /// <param name="currentHP">The current health.</param>
    public void SetPlayerUnitHealth(UnitID unitID, int maxHP, int currentHP) {
      playerInfoPanes[(int)unitID].SetHP(currentHP, maxHP);
    }

    /// <summary>
    /// Method <c>SetPlayerUnitActive</c> sets whether the unit portrait frame should be active.
    /// </summary>
    /// <param name="unitID">The player's unit ID.</param>
    /// <param name="active">Whether the frame should be active.</param>
    public void SetPlayerUnitActive(UnitID unitID, bool active) {
      playerInfoPanes[(int)unitID].SetActive(active);
    }
  }
}