using System;
using Elegant.Flow.Virtual;

namespace Elegant.Flow.Unity.Services
{
  public class UnityNullLogger : IComponentLogger
  {
    public void Exception(string message, Exception error)
    {
    }

    public void Info(string message)
    {
    }
  }
}