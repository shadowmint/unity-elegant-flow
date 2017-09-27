using System;
using System.Collections.Generic;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Tests.Editor.Fixtures.Components;

namespace Elegant.Flow.Tests.Fixtures.Components
{
  public class LayoutComponent : FlowComponent
  {
    public override IEnumerable<FlowComponent> Layout()
    {
      yield return new SimpleComponent() {Value = DateTime.Now.ToShortDateString()};
      yield return new SimpleComponent() {Value = DateTime.Now.ToLongDateString()};
      yield return new ContainerComponent(new ContainerComponentState()
      {
        Children = new List<SimpleComponent>()
        {
          new SimpleComponent() {Value = "Hello"},
          new SimpleComponent() {Value = "World"},
        }
      });
    }

    public override void Render(IComponentIdentity identity)
    {
    }

    public LayoutComponent(IComponentState state = null) : base(state)
    {
    }
  }
}