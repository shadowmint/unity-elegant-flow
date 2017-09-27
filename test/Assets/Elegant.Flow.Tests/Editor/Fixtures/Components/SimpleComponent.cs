using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Tests.Fixtures.Components
{
  public class SimpleComponent : FlowComponent
  {
    public string Value { get; set; }
    public string Container { get; set; }

    public override void Render(IComponentIdentity identity)
    {
    }

    public SimpleComponent(IComponentState state = null) : base(state)
    {
    }
  }
}