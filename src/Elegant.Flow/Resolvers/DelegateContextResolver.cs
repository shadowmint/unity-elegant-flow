using System;
using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Resolvers
{
  public class DelegateContextResolver<T> : IComponentContextResolver where T : FlowComponent
  {
    private readonly Func<T, IComponentIdentity, IComponentContext> _action;

    public DelegateContextResolver(Func<T, IComponentIdentity, IComponentContext> action)
    {
      _action = action;
    }

    public IComponentContext ContextFor(FlowComponent component, IComponentIdentity identity)
    {
      var comp = component as T;
      return comp == null ? null : _action(comp, identity);
    }
  }
}