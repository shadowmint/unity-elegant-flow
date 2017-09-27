using Elegant.Flow.Infrastructure;

namespace Elegant.Flow.Mocks
{
  public class MockVirtualComponentFactory : VirtualComponentFactory
  {
    public MockComponentLoader ComponentLoader { get; private set; }
    public MockComponentDispatcher ComponentDispatcher { get; private set; }

    protected override void Initialize()
    {
      Logger = new MockComponentLogger();
      IdentityProvider = new MockComponentIdentityProvider();
      ComponentDispatcher = new MockComponentDispatcher();
      ComponentLoader = new MockComponentLoader(Logger, ComponentDispatcher);
      Dispatcher = ComponentDispatcher;
      Loader = ComponentLoader;
    }
  }
}