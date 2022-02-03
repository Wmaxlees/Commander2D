using UnityEngine;
using UnityEngine.EventSystems;
using Commander2D.Units;

namespace Commander2D.UI.PlayerUnit {
  /// <summary>
  /// Class <c>PlayerUnitInfoPane</c> is a wrapper around the whole player unit portrait UI element.
  /// </summary>
  public class PlayerUnitInfoPane : MonoBehaviour, IPointerClickHandler {
    /// <summary>
    /// Property <c>playerUnitID</c> is the player unit ID corresponding to this HUD element.
    /// </summary>
    [SerializeField]
    private UnitID playerUnitID;

    /// <summary>
    /// Property <c>isActive</c> tracks whether there is actually a PlayerUnit defined for this HUD element.
    /// </summary>
    private bool isActive;  // Is there a unit in this frame?

    /// <summary>
    /// Property <c>portrait</c> is a handle to the portrait image.
    /// </summary>
    private Portrait portrait;

    /// <summary>
    /// Property <c>healthBar</c> is a handle to the portrait health bar.
    /// </summary>
    private HealthBar healthBar;

    /// <summary>
    /// Property <c>selectedIndicator</c> is a handle to the marker for whether the player unit is selected.
    /// </summary>
    private SelectedIndicator selectedIndicator;

    /// <summary>
    /// Method <c>Awake</c> is called automatically on object creation.
    /// </summary>
    void Awake() {
      portrait = gameObject.GetComponentInChildren<Portrait>();
      healthBar = gameObject.GetComponentInChildren<HealthBar>();
      selectedIndicator = gameObject.GetComponentInChildren<SelectedIndicator>();
    }

    /// <summary>
    /// Method <c>SetActive</c> sets whether there is an active PlayerUnit for this portrait.
    /// </summary>
    /// <param name="active">Whether the player unit is active.</param>
    public void SetActive(bool active) {
      isActive = active;
    }

    /// <summary>
    /// Method <c>OnPointerClick</c> is automatically called when a user clicks on the portrait frame.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerClick(PointerEventData pointerEventData) {
      if (!isActive) {
        return;
      }

      SelectionController.GetInstance().PlayerUnitSelected(playerUnitID);
    }

    /// <summary>
    /// Method <c>SetHP</c> sets the visible health bar in the portrait frame.
    /// </summary>
    /// <param name="currentHP">Current health.</param>
    /// <param name="maxHP">Maximum health.</param>
    public void SetHP(int currentHP, int maxHP) {
      healthBar.SetHP(currentHP, maxHP);
    }

    /// <summary>
    /// Method <c>SetSelectorIndicator</c> sets whether the selection indicator should be visible or not.
    /// </summary>
    /// <param name="on">Whether the selector should be shown or not.</param>
    public void SetSelectorIndicator(bool on) {
      selectedIndicator.SetSelected(on);
    }
  }
}