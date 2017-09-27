using System;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Infrastructure;
using Elegant.Flow.Virtual;

namespace Elegant.Flow.Mocks
{
  public class MockComponentIdentity : IComponentIdentity
  {
    public Type Type { get; set; }
    public int Offset { get; set; }
    public IComponentIdentity Parent { get; set; }
    public IComponentContext Context { get; set; }
    public VirtualComponent Self { get; set; }

    public bool Equals(IComponentIdentity other)
    {
      var ident = (MockComponentIdentity) other;
      return (other.GetType() == typeof(MockComponentIdentity)) &&
             (Type == ident.Type) &&
             (Offset == ident.Offset) &&
             HasSameParent(ident);
    }

    private bool HasSameParent(MockComponentIdentity other)
    {
      return ((Parent == null) && (other.Parent == null)) || ((Parent != null) && (Parent.Equals(other.Parent)));
    }

    public override string ToString()
    {
      var rtn = $"{Type}";
      if (Offset > 0)
      {
        rtn += $", nth: {Offset}";
      }
      if (Parent != null)
      {
        rtn += $" parent -> {Parent}";
      }
      return rtn;
    }
  }
}