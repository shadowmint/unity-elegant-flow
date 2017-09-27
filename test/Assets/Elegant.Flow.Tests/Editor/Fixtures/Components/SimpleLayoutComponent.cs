using System;
using System.Collections.Generic;
using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Tests.Fixtures.Components
{
  public class SimpleLayoutComponent : FlowComponent
  {
    public override IEnumerable<FlowComponent> Layout()
    {
      yield return new SimpleComponent() {Value = DateTime.Now.ToShortDateString()};
      yield return new SimpleComponent() {Value = DateTime.Now.ToLongDateString()};
    }

    public override void Render(IComponentIdentity identity)
    {
    }

    public SimpleLayoutComponent(IComponentState state = null) : base(state)
    {
    }
  }
}