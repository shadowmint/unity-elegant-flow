using Elegant.Flow.Abstractions;
using UnityEngine;

namespace Elegant.Flow.Unity.Services
{
  public class UnityComponentContext : IComponentContext
  {
    public GameObject GameObject { get; set; }

    public UnityComponentContext()
    {
    }

    public UnityComponentContext(GameObject gameObject)
    {
      GameObject = gameObject;
    }
  }
}