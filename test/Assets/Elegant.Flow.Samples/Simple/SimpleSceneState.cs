using System;
using System.Collections.Generic;
using System.Linq;
using Elegant.Flow.Abstractions;
using UnityEngine;

namespace Elegant.Flow.Samples.Simple
{
  [System.Serializable]
  public class SimpleSceneState : IComponentState
  {
    public List<SimpleSceneItem> Items;

    public bool Equals(IComponentState other)
    {
      if (other == null) return false;
      var state = other as SimpleSceneState;
      if (state == null) throw new Exception($"Invalid state {other}");
      if (Items.Count != state.Items.Count) return false;
      return Items.All(i => state.Items.Any(j => j.Equals(i)));
    }

    public override string ToString()
    {
      var x = string.Join(", ", Items.Select(i => i.ToString()));
      return $"<SimpleSceneState: {x} -->";
    }

    public IComponentState Clone()
    {
      var rtn = MemberwiseClone() as SimpleSceneState;
      rtn.Items = rtn.Items.Select(i => i.Clone() as SimpleSceneItem).ToList();
      return rtn;
    }
  }
}