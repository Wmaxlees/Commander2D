using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Commander2D.UI.PlayerUnit {
  /// <summary>
  /// Class <c>SelectedIndicator</c> provides methods for interacting with the player unit HUD selection indicator.
  /// </summary>
  public class SelectedIndicator : MonoBehaviour {
    /// <summary>
    /// Property <c>selectedIndicator</c> provides a handle to the actual HUD image element.
    /// </summary>
    private Image selectedIndicator;

    /// <summary>
    /// Method <c>Awake</c> is called automatically on object creation.
    /// </summary>
    void Awake() {
      selectedIndicator = gameObject.GetComponent<Image>();
      selectedIndicator.enabled = false;
    }

    /// <summary>
    /// Method <c>SetSelected</c> can set whether the player is selected or not.
    /// </summary>
    /// <param name="selected">Whether the indicator should be on or not.</param>
    public void SetSelected(bool selected) {
      selectedIndicator.enabled = selected;
    }
  }
}