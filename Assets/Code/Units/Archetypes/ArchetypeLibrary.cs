using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Commander2D.Units.Archetypes {
  public class ArchetypeLibrary : MonoBehaviour, ISerializationCallbackReceiver {
    [SerializeField]
    public List<ArchetypeMap> serializedArchetypes;

    private Dictionary<ArchetypeID, Archetype> archetypes;

    public void OnAfterDeserialize() {
      this.archetypes = new Dictionary<ArchetypeID, Archetype>();
      foreach (ArchetypeMap m in this.serializedArchetypes) {
        if (this.archetypes.ContainsKey(m.archetypeID)) {
          Debug.LogWarning("ArchetypeLibrary has overlapping definition for archetypeID: " + m.archetypeID);
        }
        this.archetypes[m.archetypeID] = m.archetype;
      }
    }

    public void OnBeforeSerialize() {
      this.serializedArchetypes = new List<ArchetypeMap>();

      foreach (KeyValuePair<ArchetypeID, Archetype> archetype in this.archetypes) {
        ArchetypeMap m = new ArchetypeMap();
        m.archetype = archetype.Value;
        m.archetypeID = archetype.Key;
        this.serializedArchetypes.Add(m);
      }
    }
  }

  [Serializable]
  public struct ArchetypeMap {
    public ArchetypeID archetypeID;
    public Archetype archetype;
  }
}