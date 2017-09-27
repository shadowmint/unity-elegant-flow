using System;
using Elegant.Flow.Virtual;

namespace Elegant.Flow.Mocks
{
  public class MockComponentLogger : IComponentLogger
  {
    public void Exception(string message, Exception error)
    {
      Log($"ERROR: {message}: {error.Message}");
    }

    public void Info(string message)
    {
      Log($"INFO: {message}");
    }

    private void Log(string message)
    {
      Console.WriteLine(message);
    }
  }
}