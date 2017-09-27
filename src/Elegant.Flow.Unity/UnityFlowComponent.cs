using System.Collections.Generic;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Unity.Resolvers;
using Elegant.Flow.Unity.Services;

namespace Elegant.Flow.Unity
{
  public abstract class UnityFlowComponent : FlowComponent
  {
    protected UnityFlowComponent(IComponentState state) : base(state)
    {
    }

    public override void Render(IComponentIdentity identity)
    {
      Render(identity as UnityComponentIdentity);
    }

    protected override IEnumerable<IComponentContextResolver> ContextResolvers()
    {
      yield return new GenericUnityContextResolver();
    }

    public abstract void Render(UnityComponentIdentity identity);
  }
}