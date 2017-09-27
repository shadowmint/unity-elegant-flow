using Elegant.Flow.Infrastructure;
using UnityEditor;
using UnityEngine.VR.WSA.Persistence;

namespace Elegant.Flow.Unity.Services
{
  public class UnityVirtualComponentFactory : VirtualComponentFactory
  {
    private static UnityVirtualComponentFactory _instance;

    protected override void Initialize()
    {
      Loader = Loader ?? new UnityComponentLoader();
      Logger = Logger ?? new UnityLogger();
      Dispatcher = Dispatcher ?? new UnityComponentDispatcher();
      IdentityProvider = IdentityProvider ?? new UnityIdentityProvider();
    }

    public static UnityVirtualComponentFactory Default
    {
      get
      {
        if (_instance != null) return _instance;
        _instance = new UnityVirtualComponentFactory();
        return _instance;
      }
    }
    
    public static UnityVirtualComponentFactory DefaultNoLogger
    {
      get
      {
        if (_instance != null) return _instance;
        _instance = new UnityVirtualComponentFactory {Logger = new UnityNullLogger()};
        return _instance;
      }
    }
  }
}