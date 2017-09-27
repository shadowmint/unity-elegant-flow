using System;

namespace Elegant.Flow.Abstractions
{
  public interface IComponentErrorHandler
  {
    void OnError(Exception error);
  }
}