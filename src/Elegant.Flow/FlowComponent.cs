using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Infrastructure;
using Elegant.Flow.Samples.Simple;
using UnityEngine;

namespace Elegant.Flow
{
  /// <summary>
  /// Base class for components.
  /// </summary>
  public abstract class FlowComponent
  {
    private IComponentState _state;

    private IComponentContextResolver[] _resolvers;

    private IEnumerable<IComponentContextResolver> Resolvers => _resolvers ?? (_resolvers = ContextResolvers().ToArray());

    public IComponentState State => _state;

    public bool ShouldUpdate { get; private set; }

    protected FlowComponent(IComponentState state)
    {
      _state = state?.Clone();
      ShouldUpdate = true;
    }

    /// <summary>
    /// Return the layout of flow components that are used to build this one.
    /// </summary>
    public virtual IEnumerable<FlowComponent> Layout()
    {
      yield break;
    }

    /// <summary>
    /// Invoked after the component has completed its render.
    /// </summary>
    public virtual void OnComponentRendered()
    {
      ShouldUpdate = false;
    }

    /// <summary>
    /// Every child component requires a specific context to be loaded from.
    /// This function should return the set of IComponentContextResolver instances
    /// that are used to resolve the context for child objects.
    /// </summary>
    protected virtual IEnumerable<IComponentContextResolver> ContextResolvers()
    {
      yield return new NullContextResolver();
    }

    /// <summary>
    /// Resolve the context for the given child, based on the held set of context resolvers.
    /// </summary>
    /// <param name="child"></param>
    /// <param name="identity"></param>
    public IComponentContext ContextFor(FlowComponent child, IComponentIdentity identity)
    {
      return Resolvers.Select(resolver => resolver.ContextFor(child, identity)).FirstOrDefault(context => context != null);
    }

    /// <summary>
    /// Apply the current state of this component to the real UI.
    /// This function is always executed on the render thread.
    /// </summary>
    public abstract void Render(IComponentIdentity identity);

    /// <summary>
    /// Update the state of this component and set it to the new value.
    /// </summary>
    /// <param name="state"></param>
    public bool SetState(IComponentState state)
    {
      if (_state?.Equals(state) == true) return false;
      _state = state?.Clone();
      ShouldUpdate = true;
      return true;
    }
  }
}