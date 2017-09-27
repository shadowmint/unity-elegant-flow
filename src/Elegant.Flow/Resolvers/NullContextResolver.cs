using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Infrastructure
{
  public class NullContextResolver : IComponentContextResolver
  {
    public IComponentContext ContextFor(FlowComponent component, IComponentIdentity identity)
    {
      return null;
    }
  }
}