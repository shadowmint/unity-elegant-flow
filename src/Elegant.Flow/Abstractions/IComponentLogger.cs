using System;

namespace Elegant.Flow.Virtual
{
  public interface IComponentLogger
  {
    void Exception(string message, Exception error);
    void Info(string message);
  }
}