using System;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Tests.Fixtures.Controllers;

namespace Elegant.Flow.Tests.Fixtures.Components
{
  public class SimpleStatefulComponent : FlowComponent
  {
    public string Output { get; set; }

    private SimpleControllerItem Item => State as SimpleControllerItem;

    public override void Render(IComponentIdentity identity)
    {
      Output = $"{DateTime.Now.ToString()}: {Item.Value}";
    }

    public SimpleStatefulComponent(IComponentState state) : base(state)
    {
    }
  }
}