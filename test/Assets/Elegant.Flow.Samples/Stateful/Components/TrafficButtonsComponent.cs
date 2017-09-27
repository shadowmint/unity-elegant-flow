using Elegant.Flow.Abstractions;
using Elegant.Flow.Unity;
using Elegant.Flow.Unity.Services;

namespace Elegant.Flow.Samples.Stateful.Components
{
  public class TrafficButtonsComponent : UnityFlowComponent
  {
    public TrafficButtonsComponent(IComponentState state) : base(state)
    {
    }

    public override void Render(UnityComponentIdentity identity)
    {
      var props = identity.Props<TrafficButtonsProps>();
      var state = State as TrafficButtonsState;
      props.OnChangeColor = state.OnChangeColor;
    }
  }
}