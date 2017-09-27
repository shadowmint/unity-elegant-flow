using System;
using System.Collections.Generic;
using System.Linq;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Virtual;

namespace Elegant.Flow.Mocks
{
  public class MockComponentLoader : IComponentLoader
  {
    private readonly IList<IComponentIdentity> _instances = new List<IComponentIdentity>();
    private readonly IComponentLogger _logger;
    private readonly MockComponentDispatcher _dispatcher;

    public IEnumerable<IComponentIdentity> Instances => _instances;

    /// <summary>
    /// Wait for any pending updates to finish, then return the list.
    /// </summary>
    public IList<IComponentIdentity> InstanceSnapshot
    {
      get
      {
        _dispatcher.Wait();
        return Instances.ToList();
      }
    }

    /// <summary>
    /// Wait for unresolved actions without doing things.
    /// </summary>
    public void Wait()
    {
      _dispatcher.Wait();
    }

    public MockComponentLoader(IComponentLogger logger, MockComponentDispatcher dispatcher)
    {
      _logger = logger;
      _dispatcher = dispatcher;
    }

    public void MakeRealInstance(IComponentIdentity identity, IComponentContext context)
    {
      lock (_instances)
      {
        if (_instances.Contains(identity))
        {
          throw new Exception($"Invalid attempt to recreate existing instance {identity}");
        }
        ((MockComponentIdentity) identity).Context = context;
        _instances.Add(identity);
        _logger.Info($"Made real instance {identity}");
      }
    }

    public void DestroyRealInstance(IComponentIdentity identity)
    {
      lock (_instances)
      {
        if (!_instances.Contains(identity))
        {
          throw new Exception($"Invalid attempt to destroy missing instance {identity}");
        }
        _instances.Remove(identity);
        _logger.Info($"Destroyed existing instance {identity}");
      }
    }
  }
}