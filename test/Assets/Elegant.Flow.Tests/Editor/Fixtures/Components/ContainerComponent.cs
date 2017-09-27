using System.Collections.Generic;
using System.Linq;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Mocks;
using Elegant.Flow.Resolvers;
using Elegant.Flow.Tests.Fixtures.Components;

namespace Elegant.Flow.Tests.Editor.Fixtures.Components
{
  public class ContainerComponent : FlowComponent
  {
    public List<MockComponentContext> Compartments { get; set; } = new List<MockComponentContext>();

    public override IEnumerable<FlowComponent> Layout()
    {
      var children = State.As<ContainerComponentState>();
      return children.Children.Concat(children.BadChildren.Select(i => i as FlowComponent));
    }

    protected override IEnumerable<IComponentContextResolver> ContextResolvers()
    {
      yield return new DelegateContextResolver<SimpleComponent>((c, i) =>
        Compartments.FirstOrDefault(cp => cp.Container == c.Container));
    }

    public override void Render(IComponentIdentity identity)
    {
    }

    public ContainerComponent(IComponentState state = null) : base(state)
    {
    }
  }
}