using System;

namespace Elegant.Flow.Virtual
{
  public  interface IComponentDispatcher
  {
    /// <summary>
    /// Execute action on the render thread.
    /// </summary>
    void Execute(Action action, Action resolve = null, Action<Exception> reject = null);
  }
}