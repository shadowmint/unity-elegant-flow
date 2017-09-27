using System;
using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Errors
{
  public class MaxHeirarchyDepthExceededException : Exception
  {
    public MaxHeirarchyDepthExceededException(IComponentIdentity identity) : base($"Max heirarchy depth exceeded rendering {identity}")
    {
      Identity = identity;
    }

    public IComponentIdentity Identity { get; set; }
  }
}