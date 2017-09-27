using System;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Unity.Services;

namespace Elegant.Flow.Unity.Errors
{
  public class InvalidResourceException : Exception
  {
    public InvalidResourceException(UnityComponentIdentity identity, Exception exception = null) : base($"No resource matching {identity.PrefabResourceId}", exception)
    {
      Identity = identity;
    }

    public IComponentIdentity Identity { get; set; }
  }
}