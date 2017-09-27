using System;

namespace Elegant.Flow.Abstractions
{
  public interface IComponentState : IEquatable<IComponentState>
  {
    IComponentState Clone();
  }
}