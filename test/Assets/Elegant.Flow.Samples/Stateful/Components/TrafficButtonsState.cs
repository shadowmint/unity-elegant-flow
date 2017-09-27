using System;
using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Samples.Stateful.Components
{
  public class TrafficButtonsState : IComponentState
  {
    public Action<TrafficLightColor> OnChangeColor { get; set; }

    public bool Equals(IComponentState other)
    {
      var state = other.As<TrafficButtonsState>();
      return state.OnChangeColor == OnChangeColor;
    }

    public IComponentState Clone()
    {
      return MemberwiseClone() as IComponentState;
    }
  }
}