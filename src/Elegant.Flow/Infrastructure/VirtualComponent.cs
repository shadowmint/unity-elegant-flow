using System;
using System.Collections.Generic;
using System.Linq;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Errors;
using Elegant.Flow.Virtual;
using UnityEngine;

namespace Elegant.Flow.Infrastructure
{
  public class VirtualComponent : IDisposable
  {
    private readonly IComponentLoader _loader;
    private readonly IComponentLogger _logger;
    private readonly VirtualComponentFactory _factory;
    private readonly FlowComponent _component;
    private readonly IComponentIdentityProvider _identityProvider;
    private readonly IComponentDispatcher _dispatcher;
    private IList<FlowComponent> _layoutCache;

    /// <summary>
    /// Return the type of the underlying component instance.
    /// </summary>
    public Type Type => _component.GetType();

    /// <summary>
    /// Does this virtual component currently exist as a real instance?
    /// </summary>
    public bool Exists { get; private set; }

    /// <summary>
    /// Returns the identity marker for this component.
    /// </summary>
    public IComponentIdentity Identity { get; }

    /// <summary>
    /// VirtualComponent nodes to match the actual children of the associated components.
    /// </summary>
    public IList<VirtualComponent> Children { get; } = new List<VirtualComponent>();

    public VirtualComponent(FlowComponent component, VirtualComponent parent, IComponentLoader loader,
      IComponentIdentityProvider identityProvider, IComponentDispatcher dispatcher, IComponentLogger logger,
      VirtualComponentFactory factory)
    {
      Identity = identityProvider.IdentityFor(component, parent);
      Identity.Self = this;
      _identityProvider = identityProvider;
      _dispatcher = dispatcher;
      _component = component;
      _loader = loader;
      _logger = logger;
      _factory = factory;
    }

    /// <summary>
    /// This should only be executed on the render thread.
    /// </summary>
    private void Realize(IComponentContext context)
    {
      if (Exists) return;
      try
      {
        _loader.MakeRealInstance(Identity, context);
        Exists = true;
      }
      catch (Exception err)
      {
        _logger.Exception($"Failed to realize component {Identity}", err);
      }
    }

    /// <summary>
    /// Render this component; if maxDepth is specified, it is used as the maximum heriarchy depth.
    /// </summary>
    /// <param name="context">The context to render this component in</param>
    /// <param name="maxDepth">The maximum depth this tree supports</param>
    /// <param name="errorHandler">For custom error handling, pass in an error handler instance</param>
    public void Render(IComponentContext context = null, IComponentErrorHandler errorHandler = null, int maxDepth = 100)
    {
      _logger.Info($"Render requested on {Identity}");
      Action<Exception> onFailure = (err) => OnRenderFailed(errorHandler, err);
      Action performRender = () => _dispatcher.Execute(() => _component.Render(Identity), OnRenderedComponent, onFailure);
      Action updateChildren = () => _dispatcher.Execute(() => PerformComponentLayout(errorHandler, maxDepth), performRender, onFailure);
      _dispatcher.Execute(() => Realize(context), updateChildren, onFailure);
    }

    /// <summary>
    /// Identical to render, but if we are not currently realized, abort.
    /// This is useful for components that need to explicitly rerender themselves.
    /// </summary>
    public void RenderExisting(IComponentContext context = null, IComponentErrorHandler errorHandler = null, int maxDepth = 100)
    {
      if (!Exists) return;
      Render(context, errorHandler, maxDepth);
    }

    private void PerformComponentLayout(IComponentErrorHandler errorHandler, int maxDepth)
    {
      if (_component.ShouldUpdate)
      {
        _layoutCache = _component.Layout().ToList();
        _logger.Info($"Rebuild layout cache: {Identity} -> {_layoutCache.Count} children");
      }
      UpdateChildHeirarchy(_layoutCache, maxDepth - 1, errorHandler);
    }

    private void OnRenderedComponent()
    {
      _logger.Info($"Render: {Identity}");
      _component.OnComponentRendered();
    }

    private void OnRenderFailed(IComponentErrorHandler errorHandler, Exception err)
    {
      _logger.Exception($"Failed to render: {Identity}", err);
      errorHandler?.OnError(err);
    }

    private void UpdateChildHeirarchy(IEnumerable<FlowComponent> children, int maxDepth, IComponentErrorHandler errorHandler)
    {
      if (maxDepth < 0)
      {
        errorHandler.OnError(new MaxHeirarchyDepthExceededException(Identity));
        return;
      }
      var childCache = Children.ToList();
      Children.Clear();
      if (children != null)
      {
        foreach (var child in children)
        {
          var identity = _identityProvider.IdentityFor(child, this);
          var childInstance = childCache.FirstOrDefault(i => i.Identity.Equals(identity));
          if (childInstance == null)
          {
            _logger.Info($"Creating new component for {child} -> {identity}");
            childInstance = _factory.From(child, this);
          }
          else
          {
            _logger.Info($"Found existing instance of {child} -> {identity}");
            childInstance.SetState(child.State); // Pull state from the render step
            childCache.Remove(childInstance);
          }
          Children.Add(childInstance);
        }
      }
      _dispatcher.Execute(() =>
      {
        foreach (var removedChild in childCache)
        {
          _logger.Info($"Destroy existing child {removedChild.Identity}");
          removedChild.Destroy();
        }
      });
      foreach (var child in Children.ToArray())
      {
        _logger.Info($"Should update {child.Identity} -> {child._component.ShouldUpdate}");
        if (child._component.ShouldUpdate)
        {
          child.Render(_component.ContextFor(child._component, Identity), errorHandler, maxDepth);
        }
      }
    }

    public void Dispose()
    {
      _dispatcher.Execute(Destroy, null, (error) => { _logger.Exception($"Failed to destroy: {Identity}", error); });
    }

    /// <summary>
    /// This should only be executed on the thread thread
    /// </summary>
    private void Destroy()
    {
      if (!Exists) return;
      foreach (var child in Children)
      {
        child.Destroy();
      }
      _loader.DestroyRealInstance(Identity);
    }

    /// <summary>
    /// Push the state into the owned component instance.
    /// </summary>
    /// <param name="controllerState"></param>
    public bool SetState(IComponentState controllerState)
    {
      return _component.SetState(controllerState);
    }
  }
}