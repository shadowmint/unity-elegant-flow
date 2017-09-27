using Elegant.Flow.Abstractions;
using Elegant.Flow.Infrastructure;
using Elegant.Flow.Unity.Services;
using UnityEngine;

namespace Elegant.Flow.Unity
{
  public abstract class UnityFlowController<TState> : MonoBehaviour where TState : IComponentState
  {
    public TState State;

    public bool Active = true;

    public bool Sync = true;

    private FlowController<TState, UnityComponentContext> _controller;

    public void Start()
    {
      _controller = new FlowController<TState, UnityComponentContext>(ComponentFactory)
      {
        RootComponent = RootComponent,
        RootContext = new UnityComponentContext(gameObject)
      };
    }

    public void Update()
    {
      if (Active && !_controller.Mounted)
      {
        _controller.Mount(State);
        Sync = false;
      }
      else if (!Active && _controller.Mounted)
      {
        _controller.Unmount();
      }
      if (!Active) return;
      if (Sync)
      {
        _controller.SetState(State);
        Sync = false;
      }
      _controller.Update();
    }

    /// <summary>
    /// Api for child components to trigger state changes with. 
    /// </summary>
    /// <param name="state"></param>
    public void SetState(TState state)
    {
      State = state;
      Sync = Active;
    }

    protected abstract FlowComponent RootComponent { get; }

    protected abstract VirtualComponentFactory ComponentFactory { get; }
  }
}