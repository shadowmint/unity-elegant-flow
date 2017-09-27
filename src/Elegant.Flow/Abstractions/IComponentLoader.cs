namespace Elegant.Flow.Abstractions
{
  public interface IComponentLoader
  {
    void MakeRealInstance(IComponentIdentity identity, IComponentContext context);
    void DestroyRealInstance(IComponentIdentity identity);
  }
}