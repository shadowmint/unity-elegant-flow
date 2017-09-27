using System;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Unity;
using Elegant.Flow.Unity.Services;
using UnityEngine;

namespace Elegant.Flow.Samples.Stateful.Components
{
  public class TrafficLightComponent : UnityFlowComponent
  {
    public TrafficLightComponent(IComponentState state) : base(state)
    {
    }

    public override void Render(UnityComponentIdentity identity)
    {
      var props = identity.Props<TrafficLightProps>();
      var state = State.As<TrafficLightState>();
      switch (state.Color)
      {
        case TrafficLightColor.Green:
          props.Target.color = Color.green;          
          break;
        case TrafficLightColor.Red:
          props.Target.color = Color.red;
          break;
        case TrafficLightColor.Yellow:
          props.Target.color = Color.yellow;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}