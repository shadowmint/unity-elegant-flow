using System;
using System.Collections.Generic;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Unity;
using Elegant.Flow.Unity.Resolvers;
using Elegant.Flow.Unity.Services;
using UnityEngine;

namespace Elegant.Flow.Samples.Stateful.Components
{
  public class StatefulControllerApp : UnityFlowComponent
  {
    public StatefulControllerApp(IComponentState state) : base(state)
    {
    }

    public override IEnumerable<FlowComponent> Layout()
    {
      var state = State as StatefulControllerAppState;
      yield return new TrafficLightComponent(new TrafficLightState()
      {
        Color = state.Color
      });
      yield return new TrafficButtonsComponent(new TrafficButtonsState()
      {
        OnChangeColor = OnChangeColor
      });
      yield return new ClockComponent(new ClockState()
      {
        StartTime = DateTime.Now.Date + new TimeSpan(state.StartHour, 0, 0)
      });
    }

    private void OnChangeColor(TrafficLightColor color)
    {
      var state = State.Clone() as StatefulControllerAppState;
      state.Color = color;
      state.Controller.SetState(state);
    }

    protected override IEnumerable<IComponentContextResolver> ContextResolvers()
    {
      yield return new UnityContextResolver<TrafficLightComponent>((component, self) =>
      {
        var props = self.Props<StatefulControllerAppProps>();
        return props.TrafficLightContainer;
      });
      yield return new UnityContextResolver<TrafficButtonsComponent>((component, self) =>
      {
        var props = self.Props<StatefulControllerAppProps>();
        return props.TrafficButtonsContainer;
      });
      yield return new UnityContextResolver<ClockComponent>((component, self) =>
      {
        var props = self.Props<StatefulControllerAppProps>();
        return props.TrafficButtonsContainer;
      });
    }

    public override void Render(UnityComponentIdentity identity)
    {
    }
  }
}