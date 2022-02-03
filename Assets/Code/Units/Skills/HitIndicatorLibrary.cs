using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Commander2D.Units.Skills;

public class HitIndicatorLibrary : MonoBehaviour {
  [SerializeField]
  public List<HitIndicator> prefabs;

  public HitIndicator CreateHitIndicator(SkillID skillID) {
    HitIndicator hi = this.GetHitIndicator(skillID);
    if (hi == null) {
      Debug.LogWarning("Trying to create new HitIndicator for skill without HitIndicator prefab: " + skillID);
      return null;
    }

    return Instantiate<HitIndicator>(hi);
  }

  public HitIndicator GetHitIndicator(SkillID skillID) {
    foreach (HitIndicator hi in this.prefabs) {
      if (hi.skillID == skillID) {
        return Instantiate(hi);
      }
    }

    return null;
  }

  private static HitIndicatorLibrary s_ProjectilePrefabs;

  public static HitIndicatorLibrary GetInstance() {
    if (HitIndicatorLibrary.s_ProjectilePrefabs == null) {
      HitIndicatorLibrary.s_ProjectilePrefabs = new HitIndicatorLibrary();
    }

    return HitIndicatorLibrary.s_ProjectilePrefabs;
  }

  private void Awake() {
    HitIndicatorLibrary.s_ProjectilePrefabs = this;
  }
}