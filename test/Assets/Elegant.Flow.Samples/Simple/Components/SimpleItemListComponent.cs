using System.Collections.Generic;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Unity;
using Elegant.Flow.Unity.Resolvers;
using Elegant.Flow.Unity.Services;
using UnityEngine;

namespace Elegant.Flow.Samples.Simple.Components
{
  public class SimpleItemListComponent : UnityFlowComponent
  {
    public SimpleItemListComponent(IComponentState state) : base(state)
    {
    }

    public override IEnumerable<FlowComponent> Layout()
    {
      var states = State as SimpleSceneState;
      if (states == null) yield break;
      foreach (var item in states.Items)
      {
        yield return new SimpleItemComponent(item);
      }
    }

    protected override IEnumerable<IComponentContextResolver> ContextResolvers()
    {
      yield return new UnityContextResolver<SimpleItemComponent>((item, self) =>
      {
        var props = self.Instance.GetComponent<SimpleItemListProps>();
        return props.Container;
      });
    }

    public override void Render(UnityComponentIdentity identity)
    {
    }
  }
}