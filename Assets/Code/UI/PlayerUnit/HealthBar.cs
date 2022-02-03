using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Commander2D.UI.PlayerUnit {
  /// <summary>
  /// Class <c>HealthBar</c> provides controls on the character portrait health bar.
  /// </summary>
  public class HealthBar : MonoBehaviour {
    /// <summary>
    /// Property <c>healthBar</c> provides a handle to the actual UI health bar.
    /// </summary>
    private Image healthBar;

    /// <summary>
    /// Method <c>Awake</c> is called automatically when the object is created.
    /// </summary>
    private void Awake() {
      healthBar = gameObject.GetComponent<Image>();
      healthBar.enabled = false;
    }

    /// <summary>
    /// Method <c>SetHP</c> sets the health amount to display.
    /// </summary>
    /// <param name="currentHP">The player unit's current health.</param>
    /// <param name="maxHP">The player unit's maximum health.</param>
    public void SetHP(int currentHP, int maxHP) {
      float pct = currentHP / (float)maxHP;
      healthBar.fillAmount = pct;
      healthBar.enabled = true;
    }
  }
}