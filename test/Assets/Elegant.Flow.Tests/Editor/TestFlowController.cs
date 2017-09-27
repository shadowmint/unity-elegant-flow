using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elegant.Flow.Mocks;
using Elegant.Flow.Tests.Editor.Fixtures.Controllers;
using Elegant.Flow.Tests.Fixtures;
using Elegant.Flow.Tests.Fixtures.Controllers;
using NUnit;
using NUnit.Framework;
using UnityEngine;

namespace Elegant.Flow.Tests
{
  public class TestFlowController
  {
    [Test]
    public void TestSimpleController()
    {
      var factory = new MockVirtualComponentFactory();
      var controller = new SimpleController(factory);

      controller.Mount(new SimpleControllerState());

      var instances = factory.ComponentLoader.InstanceSnapshot;
      Assert.True(instances.Count == 1);
    }

    [Test]
    public void TestSimpleControllerWithChildren()
    {
      var factory = new MockVirtualComponentFactory();
      var controller = new SimpleController(factory);

      controller.Mount(new SimpleControllerState()
      {
        Values = new List<SimpleControllerItem>()
        {
          new SimpleControllerItem() {Id = "1", Value = 1},
          new SimpleControllerItem() {Id = "2", Value = 2}
        }
      });

      var instances = factory.ComponentLoader.InstanceSnapshot;
      Assert.True(instances.Count == 3);
    }
  }
}