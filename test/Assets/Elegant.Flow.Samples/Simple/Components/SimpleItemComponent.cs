using Elegant.Flow.Abstractions;
using Elegant.Flow.Unity;
using Elegant.Flow.Unity.Services;

namespace Elegant.Flow.Samples.Simple.Components
{
  public class SimpleItemComponent : UnityFlowComponent
  {
    public SimpleItemComponent(IComponentState state) : base(state)
    {
    }

    public override void Render(UnityComponentIdentity identity)
    {
      var state = State as SimpleSceneItem;
      if (state == null) return;

      var props = identity.Instance.GetComponent<SimpleItemProps>();
      if (props == null) return;

      props.Id.text = $"Id: {state.Id}";
      props.Value.text = $"Value: {state.Value}";
    }
  }
}