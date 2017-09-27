using System;
using UnityEngine;

namespace Elegant.Flow.Samples.Stateful.Components
{
  public class TrafficButtonsProps : MonoBehaviour
  {
    public Action<TrafficLightColor> OnChangeColor { get; set; }

    public void OnClickRed()
    {
      SetColor(TrafficLightColor.Red);
    }

    public void OnClickGreen()
    {
      SetColor(TrafficLightColor.Green);
    }

    public void OnClickYellow()
    {
      SetColor(TrafficLightColor.Yellow);
    }

    private void SetColor(TrafficLightColor color)
    {
      Debug.Log($"Setting color: {color} using {OnChangeColor}");
      OnChangeColor?.Invoke(color);
    }
  }
}