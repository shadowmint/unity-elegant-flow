using System;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Unity;
using Elegant.Flow.Unity.Services;
using UnityEngine;

namespace Elegant.Flow.Samples.Stateful.Components
{
  /// <summary>
  /// This component has internal state, so it triggers a render from its own virtual component.
  /// </summary>
  public class ClockComponent : UnityFlowComponent
  {
    private DateTime _rootTime;
    private float _initialTime;

    public ClockComponent(IComponentState state) : base(state)
    {
    }

    public override void Render(UnityComponentIdentity identity)
    {
      ResetInternalClock();
      var now = _rootTime + TimeSpan.FromSeconds(Time.timeSinceLevelLoad - _initialTime);
      var props = identity.Props<ClockProps>();
      props.ClockOutput.text = now.ToLongTimeString();
      props.Trigger = identity.Render;
    }

    private void ResetInternalClock()
    {
      var state = State.As<ClockState>();
      if (state.StartTime == _rootTime) return;

      _rootTime = state.StartTime;
      _initialTime = Time.timeSinceLevelLoad;
    }
  }
}