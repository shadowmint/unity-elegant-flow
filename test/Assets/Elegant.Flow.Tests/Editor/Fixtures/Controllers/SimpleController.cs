using Elegant.Flow.Infrastructure;
using Elegant.Flow.Mocks;
using Elegant.Flow.Tests.Editor.Fixtures.Components;
using Elegant.Flow.Tests.Fixtures.Components;
using Elegant.Flow.Tests.Fixtures.Controllers;

namespace Elegant.Flow.Tests.Editor.Fixtures.Controllers
{
  public class SimpleController : FlowController<SimpleControllerState, MockComponentContext>
  {
    public SimpleController(VirtualComponentFactory factory) : base(factory)
    {
      RootComponent = new SimpleStatefulComponentList(null);
    }
  }
}