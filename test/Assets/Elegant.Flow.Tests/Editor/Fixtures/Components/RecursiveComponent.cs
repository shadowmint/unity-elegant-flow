using System.Collections.Generic;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Tests.Editor.Fixtures.Components;

namespace Elegant.Flow.Tests.Fixtures.Components
{
  public class RecursiveComponent : FlowComponent
  {
    public override IEnumerable<FlowComponent> Layout()
    {
      yield return new ContainerComponent(new ContainerComponentState()
      {
        Children = new List<SimpleComponent>()
        {
          new SimpleComponent()
        },
        BadChildren = new List<RecursiveComponent>()
        {
          new RecursiveComponent()
        }
      });
    }

    public override void Render(IComponentIdentity identity)
    {
    }

    public RecursiveComponent(IComponentState state = null) : base(state)
    {
    }
  }
}