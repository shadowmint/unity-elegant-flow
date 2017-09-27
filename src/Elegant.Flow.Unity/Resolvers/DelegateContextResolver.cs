using System;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Unity.Services;
using UnityEngine;

namespace Elegant.Flow.Unity.Resolvers
{
  public class UnityContextResolver<T> : IComponentContextResolver where T : UnityFlowComponent
  {
    private readonly Func<T, UnityComponentIdentity, GameObject> _action;

    public UnityContextResolver(Func<T, UnityComponentIdentity, GameObject> action)
    {
      _action = action;
    }

    public IComponentContext ContextFor(FlowComponent component, IComponentIdentity identity)
    {
      var comp = component as T;
      return comp == null ? null : new UnityComponentContext(_action(comp, identity as UnityComponentIdentity));
    }
  }
}