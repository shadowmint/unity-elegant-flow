using System;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Infrastructure;
using UnityEngine;

namespace Elegant.Flow
{
  /// <summary>
  /// FlowController is the top level state container for a heirarchy of components.
  /// </summary>
  public class FlowController<TState, TContext> where TState : IComponentState where TContext : IComponentContext
  {
    private readonly VirtualComponentFactory _factory;

    private VirtualComponent _virtualRoot;
    private bool _shouldUpdate;

    public bool Mounted => _virtualRoot != null;

    public FlowController(VirtualComponentFactory factory)
    {
      _factory = factory;
    }

    /// <summary>
    /// Return the root component for this display heirarchy.
    /// </summary>
    public FlowComponent RootComponent { private get; set; }

    /// <summary>
    /// Return the context required to mount the root component.
    /// eg. The containing element.
    /// </summary>
    public TContext RootContext { private get; set; }

    /// <summary>
    /// Invoke this to initialize and render the view heirarchy. 
    /// </summary>
    public void Mount(IComponentState initialState)
    {
      if (Mounted)
      {
        throw new Exception($"{this} is already mounted");
      }      
      _virtualRoot = _factory.From(RootComponent);
      RootComponent.SetState(initialState);
      _virtualRoot.Render(RootContext);
    }

    /// <summary>
    /// Invoke this to destroy this view heirarchy.
    /// </summary>
    public void Unmount()
    {
      if (!Mounted)
      {
        throw new Exception($"{this} is not mounted");
      }
      RootComponent.SetState(null);
      _virtualRoot.Dispose();
      _virtualRoot = null;
    }

    /// <summary>
    /// Render of the view hierarchy if required.
    /// </summary>
    public void Update()
    {
      if (!Mounted) return;
      if (!_shouldUpdate) return;
      try
      {
        _virtualRoot.Render(RootContext);
      }
      catch (Exception err)
      {
        Debug.LogException(err);
      }
      _shouldUpdate = false;
    }

    public void SetState(IComponentState state)
    {
      _shouldUpdate = _shouldUpdate || _virtualRoot == null || _virtualRoot.SetState(state);
    }
  }
}