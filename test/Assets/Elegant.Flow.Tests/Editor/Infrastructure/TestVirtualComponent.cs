using System;
using System.Collections.Generic;
using System.Linq;
using Elegant.Flow.Errors;
using Elegant.Flow.Mocks;
using Elegant.Flow.Tests.Editor.Fixtures.Components;
using Elegant.Flow.Tests.Fixtures.Components;
using NUnit.Framework;
using UnityEngine;

namespace Elegant.Flow.Tests.Infrastructure
{
  public class TestVirtualComponent
  {
    [Test]
    public void TestSimpleComponentHeirarchy()
    {
      var factory = new MockVirtualComponentFactory();
      var simple = factory.From(new SimpleComponent() {Value = "Hello"});
      Assert.False(simple.Exists);
    }

    [Test]
    public void TestSimpleComponentHeirarchyExistsAfterRealize()
    {
      var factory = new MockVirtualComponentFactory();
      var simple = factory.From(new SimpleComponent() {Value = "Hello"});
      simple.Render(null);
      factory.ComponentDispatcher.Wait();
      Assert.True(simple.Exists);
    }

    [Test]
    public void TestComponentWithChildren()
    {
      var factory = new MockVirtualComponentFactory();
      var component = factory.From(new SimpleLayoutComponent());

      component.Render();
      var instances1 = factory.ComponentLoader.InstanceSnapshot;

      component.Render();
      var instances2 = factory.ComponentLoader.InstanceSnapshot;

      Assert.AreEqual(instances1.Count, 3);
      Assert.AreEqual(instances1.Count, instances2.Count);
      Assert.True(instances2.All(j => instances2.Any(i => i.Equals(j))));
    }

    [Test]
    public void TestComponentWithChildrenInValidCompartments()
    {
      var factory = new MockVirtualComponentFactory();
      var component = factory.From(new ContainerComponent(new ContainerComponentState()
      {
        Children = new List<SimpleComponent>()
        {
          new SimpleComponent() {Value = "1-1", Container = "1"},
          new SimpleComponent() {Value = "1-2", Container = "1"},
          new SimpleComponent() {Value = "2-1", Container = "2"}
        }
      })
      {
        Compartments = new List<MockComponentContext>()
        {
          new MockComponentContext() {Container = "1"},
          new MockComponentContext() {Container = "2"},
        }
      });

      component.Render(null);
      var instances = factory.ComponentLoader.InstanceSnapshot;
      var simples = instances.Select(i => (MockComponentIdentity) i).Where(id => id.Type == typeof(SimpleComponent));

      Assert.True(simples.All(i => i.Context != null));
    }

    [Test]
    public void TestComponentWithChildrenInInvalidCompartments()
    {
      var factory = new MockVirtualComponentFactory();
      var component = factory.From(new ContainerComponent(new ContainerComponentState()
      {
        Children = new List<SimpleComponent>()
        {
          new SimpleComponent() {Value = "3-1", Container = "3"},
          new SimpleComponent() {Value = "2-1", Container = "2"},
          new SimpleComponent() {Value = "2-2", Container = "2"}
        }
      })
      {
        Compartments = new List<MockComponentContext>()
        {
          new MockComponentContext() {Container = "1"},
          new MockComponentContext() {Container = "2"},
        }
      });

      component.Render(null);
      var instances = factory.ComponentLoader.InstanceSnapshot;
      var simples = instances.Select(i => (MockComponentIdentity) i).Where(id => id.Type == typeof(SimpleComponent)).ToList();

      Assert.AreEqual(simples.Count, 3);
      Assert.AreEqual(simples.Count(i => i.Context != null), 2);
    }

    [Test]
    public void TestComponentDisposalRemovesInstances()
    {
      var factory = new MockVirtualComponentFactory();
      var component = factory.From(new SimpleLayoutComponent());

      component.Render(null);
      factory.ComponentDispatcher.Wait();
      var instances1 = factory.ComponentLoader.InstanceSnapshot;

      component.Dispose();
      factory.ComponentDispatcher.Wait();
      var instances2 = factory.ComponentLoader.InstanceSnapshot;

      Assert.AreEqual(instances1.Count, 3);
      Assert.AreEqual(instances2.Count, 0);
    }

    [Test]
    public void TestComponentWithVaryingChildList()
    {
      var factory = new MockVirtualComponentFactory();

      var state = new ContainerComponentState();
      state.Children.AddRange(new[] {new SimpleComponent(), new SimpleComponent()});

      var component = new ContainerComponent(state);
      var virtualComponent = factory.From(component);

      virtualComponent.Render(null);
      var instancesTwoChildren = factory.ComponentLoader.InstanceSnapshot;

      state.Children.Clear();
      component.SetState(state);
      virtualComponent.Render(null);
      var instancesNoneChild = factory.ComponentLoader.InstanceSnapshot;

      state.Children = new List<SimpleComponent>() {new SimpleComponent()};
      component.SetState(state);
      virtualComponent.Render(null);
      var instancesOneChild = factory.ComponentLoader.InstanceSnapshot;

      Assert.AreEqual(instancesTwoChildren.Count, 3);
      Assert.AreEqual(instancesNoneChild.Count, 1);
      Assert.AreEqual(instancesOneChild.Count, 2);
    }

    [Test]
    public void TestComplexHeirarchyRendersCorrectly()
    {
      var factory = new MockVirtualComponentFactory();
      var virtualComponent = factory.From(new LayoutComponent());

      virtualComponent.Render(null);
      var instances = factory.ComponentLoader.InstanceSnapshot;

      foreach (var instance in instances)
      {
        Console.WriteLine(instance);
      }
      Assert.AreEqual(6, instances.Count);
    }

    [Test]
    public void TestNestedComponentFailsToRenderWithExplicitMaxDepth()
    {
      var factory = new MockVirtualComponentFactory();
      var errors = new MockErrorHandler();
      var virtualComponent = factory.From(new LayoutComponent());

      virtualComponent.Render(null, maxDepth: 2, errorHandler: errors);
      factory.ComponentDispatcher.Wait();

      Assert.AreEqual(2, errors.Errors.ToList().Count);
      Assert.True(errors.Errors.All(i => i is MaxHeirarchyDepthExceededException));
    }

    [Test]
    public void TestRecursiveComponentFailsToRender()
    {
      var factory = new MockVirtualComponentFactory();
      var errors = new MockErrorHandler();
      var virtualComponent = factory.From(new RecursiveComponent());

      virtualComponent.Render(null, errorHandler: errors);
      factory.ComponentDispatcher.Wait();

      Assert.True(errors.Errors.All(i => i is MaxHeirarchyDepthExceededException));
    }
  }
}