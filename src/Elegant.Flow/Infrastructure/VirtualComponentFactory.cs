using System.ComponentModel;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Virtual;

namespace Elegant.Flow.Infrastructure
{
  public abstract class VirtualComponentFactory
  {
    protected IComponentLoader Loader;
    protected IComponentIdentityProvider IdentityProvider;
    protected IComponentDispatcher Dispatcher;
    protected IComponentLogger Logger;
    private bool _ready;

    /// <summary>
    /// Override this to set internal component provider instances.
    /// </summary>
    protected virtual void Initialize()
    {
    }

    private void GuardReady()
    {
      if (_ready) return;
      _ready = true;
      Initialize();
    }

    public VirtualComponent From(FlowComponent component, VirtualComponent parent = null)
    {
      GuardReady();
      return new VirtualComponent(component, parent, Loader, IdentityProvider, Dispatcher, Logger, this);
    }    
  }
}