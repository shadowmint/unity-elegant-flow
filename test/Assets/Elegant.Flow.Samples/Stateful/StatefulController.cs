using Elegant.Flow.Infrastructure;
using Elegant.Flow.Samples.Simple.Components;
using Elegant.Flow.Samples.Stateful.Components;
using Elegant.Flow.Unity;
using Elegant.Flow.Unity.Services;

namespace Elegant.Flow.Samples.Stateful
{
  public class StatefulController : UnityFlowController<StatefulControllerAppState>
  {
    protected override FlowComponent RootComponent => new StatefulControllerApp(State);

    protected override VirtualComponentFactory ComponentFactory => UnityVirtualComponentFactory.DefaultNoLogger;
  }
}