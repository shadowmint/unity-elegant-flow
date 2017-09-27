using System;
using System.IO;
using Elegant.Flow.Abstractions;
using Elegant.Flow.Infrastructure;
using UnityEngine;

namespace Elegant.Flow.Unity.Services
{
  public class UnityComponentIdentity : IComponentIdentity
  {
    public GameObject Prefab { get; set; }
    public GameObject Instance { get; set; }
    public Type Type { private get; set; }
    public int Offset { private get; set; }
    public VirtualComponent Parent { private get; set; }
    public VirtualComponent Self { get; set; }

    public bool Equals(IComponentIdentity other)
    {
      var ident = (UnityComponentIdentity) other;
      return (other.GetType() == typeof(UnityComponentIdentity)) &&
             (Type == ident.Type) &&
             (Offset == ident.Offset) &&
             HasSameParent(ident);
    }

    private bool HasSameParent(UnityComponentIdentity other)
    {
      return ((Parent == null) && (other.Parent == null)) || ((Parent != null) && (Parent.Equals(other.Parent)));
    }

    public override string ToString()
    {
      var rtn = $"{Type}";
      if (Offset > 0)
      {
        rtn += $", nth: {Offset}";
      }
      if (Parent != null)
      {
        rtn += $" parent -> {Parent}";
      }
      return rtn;
    }

    public string PrefabResourceId => Type == null || Type.Namespace == null ? "" : Path.Combine(Type.Namespace, Type.Name);
    
    public T Props<T>() where T : Component
    {
      return Instance.GetComponent<T>();
    }
  }
}