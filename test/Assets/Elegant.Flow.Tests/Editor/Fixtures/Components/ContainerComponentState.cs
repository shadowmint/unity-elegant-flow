using System.Collections.Generic;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Tests.Fixtures.Components;

namespace Elegant.Flow.Tests.Editor.Fixtures.Components
{
  public class ContainerComponentState : IComponentState
  {
    public List<SimpleComponent> Children { get; set; } = new List<SimpleComponent>();
    public List<RecursiveComponent> BadChildren { get; set; } = new List<RecursiveComponent>();
    
    public bool Equals(IComponentState other)
    {
      return false;      
    }

    public IComponentState Clone()
    {
      return MemberwiseClone() as IComponentState;
    }
  }
}