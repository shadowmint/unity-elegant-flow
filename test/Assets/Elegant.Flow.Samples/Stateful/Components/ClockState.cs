using System;
using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Samples.Stateful.Components
{
  public class ClockState : IComponentState
  {
    public DateTime StartTime { get; set; }

    public bool Equals(IComponentState other)
    {
      return false;
    }

    public IComponentState Clone()
    {
      return MemberwiseClone() as IComponentState;
    }
  }
}