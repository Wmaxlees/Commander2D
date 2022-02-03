using Commander2D.Board;
using Commander2D.UI;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Commander2D.Units {
  /// <summary>
  /// Class <c>EnemyUnitPrefab</c> provides ways to control the graphical representation of
  /// an enemy unit.
  /// </summary>
  public class EnemyUnitPrefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    /// <summary>
    /// Property <c>enemyUnit</c> is a handle to the <c>EnemyUnit</c> this prefab represents.
    /// </summary>
    private EnemyUnit enemyUnit;

    public void OnPointerEnter(PointerEventData eventData) {
      HUDManager.GetInstance().SetPointerOverEnemy(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
      HUDManager.GetInstance().SetPointerOverEnemy(false);
    }

    public void SetEnemyType<T>() where T : EnemyUnit {
      enemyUnit = gameObject.AddComponent<T>();
    }

    public void SetUnitID(UnitID unitID) {
      this.enemyUnit.SetUnitID(unitID);
    }

    public EnemyUnit GetEnemyUnit() {
      return enemyUnit;
    }
  }
}