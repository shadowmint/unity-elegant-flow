using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Samples.Stateful.Components
{
  public class TrafficLightState : IComponentState
  {
    public TrafficLightColor Color { get; set; }

    public bool Equals(IComponentState other)
    {
      var state = other.As<TrafficLightState>();
      return state?.Color == Color;
    }

    public IComponentState Clone()
    {
      return MemberwiseClone() as IComponentState;
    }
  }
}