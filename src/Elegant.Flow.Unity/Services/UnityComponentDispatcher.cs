using System;
using System.Collections.Generic;
using System.Linq;
using Elegant.Flow.Virtual;
using UnityEngine;

namespace Elegant.Flow.Unity
{
  public class UnityComponentDispatcher : IComponentDispatcher
  {
    private const int MaxTasksPerFrame = 100;

    class UiTask
    {
      public Action Action { get; set; }
      public Action Resolve { get; set; }
      public Action<Exception> Reject { get; set; }
    }

    private readonly Queue<UiTask> _tasks = new Queue<UiTask>();

    private UnityComponentDispatcherWorker _worker;

    public UnityComponentDispatcher()
    {
      RequireWorker();
    }

    public void Execute(Action action, Action resolve = null, Action<Exception> reject = null)
    {
      lock (_tasks)
      {
        _tasks.Enqueue(new UiTask()
        {
          Action = action,
          Resolve = resolve,
          Reject = reject
        });
      }
    }

    private void RequireWorker()
    {
      try
      {
        var _ = _worker.gameObject.transform.name;
      }
      catch (Exception)
      {
        _worker = null;
      }
      if (_worker != null) return;
      var worker = new GameObject();
      worker.transform.name = "FlowDispatchWorker";
      _worker = worker.AddComponent<UnityComponentDispatcherWorker>();
      _worker.Dispatcher = this;
      _worker.hideFlags = HideFlags.HideAndDontSave;
    }

    public void Flush()
    {
      var count = 0;
      while (count < MaxTasksPerFrame)
      {
        UiTask task;
        lock (_tasks)
        {
          task = _tasks.Any() ? _tasks.Dequeue() : null;
        }
        if (task == null)
        {
          break;
        }
        ExecuteTask(task);
        count += 1;
      }
    }


    private void ExecuteTask(UiTask task)
    {
      try
      {
        task.Action();
      }
      catch (Exception err)
      {
        task.Reject?.Invoke(err);
      }
      task.Resolve?.Invoke();
    }
  }
}