using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Infrastructure;
using Elegant.Flow.Virtual;

namespace Elegant.Flow.Mocks
{
  public class MockComponentIdentityProvider : IComponentIdentityProvider
  {
    public IComponentIdentity IdentityFor(FlowComponent component, VirtualComponent parent)
    {
      var type = component.GetType();
      return new MockComponentIdentity()
      {
        Type = type,
        Offset = parent?.Children.Count(i => i.Type == type) ?? 0,
        Parent = parent?.Identity
      };
    }
  }
}