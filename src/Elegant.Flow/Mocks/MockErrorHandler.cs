using System;
using System.Collections.Generic;
using Elegant.Flow.Abstractions;

namespace Elegant.Flow.Mocks
{
  public class MockErrorHandler : IComponentErrorHandler
  {
    private readonly IList<Exception> _errors = new List<Exception>();

    public IEnumerable<Exception> Errors => _errors;

    public void OnError(Exception error)
    {
      lock (_errors)
      {
        _errors.Add(error);
      }
    }
  }
}