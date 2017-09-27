using System;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Unity.Errors;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Elegant.Flow.Unity.Services
{
  public class UnityComponentLoader : IComponentLoader
  {
    public void MakeRealInstance(IComponentIdentity identity, IComponentContext context)
    {
      var uident = (UnityComponentIdentity) identity;
      if (uident.Prefab == null)
      {
        uident.Prefab = LoadPrefabFor(uident);
      }
      if (uident.Instance != null)
      {
        throw new Exception($"Invalid attempt to make real component {identity} real again");
      }
      uident.Instance = SpawnInstance(uident, context as UnityComponentContext);
    }

    private GameObject SpawnInstance(UnityComponentIdentity uident, UnityComponentContext context)
    {
      try
      {
        var instance = Object.Instantiate(uident.Prefab);
        if ((context != null) && (context.GameObject != null))
        {
          instance.transform.SetParent(context.GameObject.transform, false);
        }
        return instance;
      }
      catch (Exception e)
      {
        throw new InvalidResourceException(uident, e);
      }
    }

    private GameObject LoadPrefabFor(UnityComponentIdentity uident)
    {
      try
      {
        var rtn = Resources.Load(uident.PrefabResourceId, typeof(GameObject)) as GameObject;
        if (rtn != null)
        {
          return rtn;
        }
        throw new InvalidResourceException(uident);
      }
      catch (Exception e)
      {
        throw new InvalidResourceException(uident, e);
      }
    }

    public void DestroyRealInstance(IComponentIdentity identity)
    {
      var uident = (UnityComponentIdentity) identity;
      if (uident.Instance == null)
      {
        throw new Exception($"Invalid attempt to destroy already null instance {identity}");
      }
      Object.Destroy(uident.Instance);
      uident.Instance = null;
    }
  }
}