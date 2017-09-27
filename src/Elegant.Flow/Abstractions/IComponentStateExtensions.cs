using System;

namespace Elegant.Flow.Abstractions
{
  public static class ComponentStateExtensions
  {
    public static T As<T>(this IComponentState self) where T : class, IComponentState
    {
      if (self == null)
      {
        return null;
      }
      var rtn = self as T;
      if (rtn == null)
      {
        throw new Exception($"Invalid state {self}, expecting {typeof(T)}");
      }
      return rtn;
    }
  }
}