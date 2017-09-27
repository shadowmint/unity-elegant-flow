using Elegant.Flow.Abstractions;

namespace Elegant.Flow
{
  public interface IComponentContextResolver
  {
    IComponentContext ContextFor(FlowComponent component, IComponentIdentity identity);
  }
}