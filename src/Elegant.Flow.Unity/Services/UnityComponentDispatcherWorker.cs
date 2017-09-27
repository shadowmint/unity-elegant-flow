using UnityEngine;

namespace Elegant.Flow.Unity
{
  public class UnityComponentDispatcherWorker : MonoBehaviour
  {
    public UnityComponentDispatcher Dispatcher { private get; set; }

    public void Update()
    {
      Dispatcher?.Flush();
    }
  }
}