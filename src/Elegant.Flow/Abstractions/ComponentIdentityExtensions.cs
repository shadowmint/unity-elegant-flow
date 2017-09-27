namespace Elegant.Flow.Abstractions
{
  public static class ComponentIdentityExtensions
  {
    /// <summary>
    /// Request this instance be re-rendered.
    /// Useful for components to explicitly trigger themselves.
    /// </summary>
    public static void Render(this IComponentIdentity self)
    {
      self?.Self?.Render();
    }
  }
}