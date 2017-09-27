using System;
using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Samples.Simple
{
  [System.Serializable]
  public class SimpleSceneItem : IComponentState
  {
    public int Id;
    public string Value;

    public bool Equals(IComponentState other)
    {
      var state = other as SimpleSceneItem;
      if (state == null) return false;
      return state.Id == Id;
    }

    public override string ToString()
    {
      return $"<SimpleSceneItem: {Id} {Value}>";
    }

    public IComponentState Clone()
    {
      return MemberwiseClone() as SimpleSceneItem;
    }
  }
}