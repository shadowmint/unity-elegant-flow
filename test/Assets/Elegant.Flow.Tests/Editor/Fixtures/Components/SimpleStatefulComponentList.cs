using System.Collections.Generic;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Tests.Fixtures.Components;
using Elegant.Flow.Tests.Fixtures.Controllers;

namespace Elegant.Flow.Tests.Editor.Fixtures.Components
{
  public class SimpleStatefulComponentList : FlowComponent
  {
    public override IEnumerable<FlowComponent> Layout()
    {
      var state = State as SimpleControllerState;
      if (state == null) yield break;
      foreach (var item in state.Values)
      {
        yield return new SimpleStatefulComponent(item);
      }
    }

    public override void Render(IComponentIdentity identity)
    {
    }

    public SimpleStatefulComponentList(IComponentState state) : base(state)
    {
    }
  }
}