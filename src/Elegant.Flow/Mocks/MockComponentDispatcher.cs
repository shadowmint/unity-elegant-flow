using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elegant.Flow.Virtual;

namespace Elegant.Flow.Mocks
{
  public class MockComponentDispatcher : IComponentDispatcher
  {
    class MockTaskInfo
    {
      public Task OnResolve { get; set; }
      public Task OnReject { get; set; }
    }

    private readonly Queue<MockTaskInfo> _waiting = new Queue<MockTaskInfo>();

    public void Execute(Action action, Action resolve = null, Action<Exception> reject = null)
    {
      var task = new Task(action);
      var info = new MockTaskInfo()
      {
        OnResolve = task.ContinueWith((t) => { resolve?.Invoke(); }),
        OnReject = task.ContinueWith(failedTask => { reject?.Invoke(failedTask.Exception); }, TaskContinuationOptions.OnlyOnFaulted)
      };
      Add(info);
      task.Start();
    }

    private void Add(MockTaskInfo task)
    {
      lock (_waiting)
      {
        _waiting.Enqueue(task);
      }
    }

    /// <summary>
    /// Wait for all pending actions
    /// </summary>
    public void Wait()
    {
      Thread.Sleep(1);
      while (true)
      {
        Thread.Sleep(1);
        MockTaskInfo t;
        lock (_waiting)
        {
          t = _waiting.Any() ? _waiting.Dequeue() : null;
        }
        if (t == null)
        {
          break;
        }
        Task.WaitAny(t.OnResolve, t.OnReject);
      }
    }
  }
}