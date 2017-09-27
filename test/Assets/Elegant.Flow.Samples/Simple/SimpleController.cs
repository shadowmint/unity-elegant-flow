using Elegant.Flow.Infrastructure;
using Elegant.Flow.Samples.Simple.Components;
using Elegant.Flow.Unity;
using Elegant.Flow.Unity.Services;

namespace Elegant.Flow.Samples.Simple
{
  public class SimpleController : UnityFlowController<SimpleSceneState>
  {
    protected override FlowComponent RootComponent => new SimpleItemListComponent(State);

    protected override VirtualComponentFactory ComponentFactory => UnityVirtualComponentFactory.Default;
  }
}