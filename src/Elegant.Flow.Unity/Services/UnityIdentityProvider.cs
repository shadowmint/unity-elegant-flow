using System.Linq;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Infrastructure;
using Elegant.Flow.Virtual;

namespace Elegant.Flow.Unity.Services
{
  public class UnityIdentityProvider : IComponentIdentityProvider
  {
    public IComponentIdentity IdentityFor(FlowComponent component, VirtualComponent parent)
    {
      var type = component.GetType();
      return new UnityComponentIdentity()
      {
        Type = type,
        Offset = parent?.Children.Count(i => i.Type == type) ?? 0,
        Parent = parent
      };
    }
  }
}