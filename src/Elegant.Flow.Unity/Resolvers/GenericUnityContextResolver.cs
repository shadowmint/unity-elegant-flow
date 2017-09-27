using Elegant.Flow.Abstractions;
using Elegant.Flow.Unity.Services;

namespace Elegant.Flow.Unity.Resolvers
{
  public class GenericUnityContextResolver : IComponentContextResolver
  {
    public IComponentContext ContextFor(FlowComponent component, IComponentIdentity identity)
    {
      var ident = identity as UnityComponentIdentity;
      var instance = ident?.Instance;
      return new UnityComponentContext(instance);
    }
  }
}