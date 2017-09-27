using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Tests.Fixtures.Controllers
{
  public class SimpleControllerItem : IComponentState
  {
    public string Id { get; set; }
    public int Value { get; set; }

    public bool Equals(IComponentState other)
    {
      return ((SimpleControllerItem) other).Id == Id;
    }

    public IComponentState Clone()
    {
      return MemberwiseClone() as IComponentState;
    }
  }
}