using Elegant.Flow.Abstractions;
using Elegant.Flow.Infrastructure;

namespace Elegant.Flow.Virtual
{
  public interface IComponentIdentityProvider
  {
    IComponentIdentity IdentityFor(FlowComponent component, VirtualComponent parent);
  }
}