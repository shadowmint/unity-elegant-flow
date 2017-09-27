using System.Collections.Generic;
using System.Linq;
using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Tests.Fixtures.Controllers
{
  public class SimpleControllerState : IComponentState
  {
    public List<SimpleControllerItem> Values { get; set; } = new List<SimpleControllerItem>();

    public bool Equals(IComponentState other)
    {
      var state = other.As<SimpleControllerState>();
      return state != null && Values.All(i => state.Values.Any(j => j.Id == i.Id));
    }

    public IComponentState Clone()
    {
      return MemberwiseClone() as IComponentState;
    }
  }
}