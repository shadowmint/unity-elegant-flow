using System;
using Elegant.Flow.Virtual;
using UnityEngine;

namespace Elegant.Flow.Unity
{
  public class UnityLogger : IComponentLogger
  {
    public void Exception(string message, Exception error)
    {
      Debug.LogError(message);
      Debug.LogException(error);
    }

    public void Info(string message)
    {
      Debug.Log(message);
    }
  }
}