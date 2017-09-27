using System;
using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Samples.Stateful.Components
{
  [System.Serializable]
  public class StatefulControllerAppState : IComponentState
  {
    public StatefulController Controller;
    
    public TrafficLightColor Color;
    public int StartHour;

    public bool Equals(IComponentState other)
    {
      var state = other.As<StatefulControllerAppState>();
      return state.Color.Equals(Color) && state.StartHour.Equals(StartHour);
    }

    public IComponentState Clone()
    {
      return MemberwiseClone() as IComponentState;
    }
  }
}