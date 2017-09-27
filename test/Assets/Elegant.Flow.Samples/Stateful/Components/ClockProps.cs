using System;
using UnityEngine;
using UnityEngine.UI;

namespace Elegant.Flow.Samples.Stateful.Components
{
  public class ClockProps : MonoBehaviour
  {
    private float _elapsed;
    
    public Text ClockOutput;

    public Action Trigger { get; set; }

    public void Update()
    {
      _elapsed += Time.deltaTime;
      if (!(_elapsed > 1f)) return;
      _elapsed = 0f;
      Trigger?.Invoke();
    }
  }
}