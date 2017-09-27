using System;
using Elegant.Flow.Infrastructure;

namespace Elegant.Flow.Abstractions
{
  /// <summary>
  /// Allows various different component instances to share a single identity.
  /// </summary>
  /// <inheritdoc />
  public interface IComponentIdentity : IEquatable<IComponentIdentity>
  {
    VirtualComponent Self { get; set; }
  }
}