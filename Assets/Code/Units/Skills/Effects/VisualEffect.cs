using System;
using System.Collections;
using System.Threading.Tasks;

using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  public abstract class VisualEffect : SkillGraphNode {
    [SerializeField]
    protected InputNode targetProvider;

    [SerializeField]
    protected InputNode secondTargetProvider;

    internal void SetTarget(InputNode provider) {
      this.targetProvider = provider;
    }

    internal SkillGraphNode GetTargetProvider() {
      return this.targetProvider;
    }

    internal void SetSecondTarget(InputNode provider) {
      this.secondTargetProvider = provider;
    }

    internal SkillGraphNode GetSecondTargetProvider() {
      return this.secondTargetProvider;
    }
  }
}