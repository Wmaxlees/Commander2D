using Commander2D.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Commander2D.UI {
  /// <summary>
  /// Class <c>TurnPortraitPrefab</c> provides controls for interacting with the
  /// turn order portrait.
  /// </summary>
  public class TurnPortraitPrefab : MonoBehaviour {
    /// <summary>
    /// Properties <c>portrait</c> a handle to the UI portrait image.
    /// </summary>
    [SerializeField]
    private Image portrait;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private Text initiative;

    /// <summary>
    /// Method <c>Awake</c> is called automatically when the object is instantiated.
    /// </summary>
    private void Awake() {
      Debug.Assert(this.portrait != null, "TurnPortrait does not have a portrait image component as a child.");
      Debug.Assert(this.healthBar != null, "TurnPortrait does not have a health bar component as a child.");
      Debug.Assert(this.initiative != null, "TurnPortrait does not have an initiative text component as a child.");
    }

    /// <summary>
    /// Method <c>SetUnit</c> sets the unit for the turn portrait.
    /// </summary>
    /// <param name="unitID">The unit's ID.</param>
    public void SetUnit(UnitID unitID, float currentInitiative) {
      this.healthBar.fillAmount = 1;
      this.portrait.sprite = UnitController.GetInstance().GetUnitPortrait(unitID);
      this.healthBar.fillAmount = UnitController.GetInstance().GetUnitHealthPercentage(unitID);
      this.initiative.text = currentInitiative.ToString("n2");
    }

    public void UpdateHealth(int currentHP, int maxHP) {
      this.healthBar.fillAmount = (float)currentHP / (float)maxHP;
    }
  }
}