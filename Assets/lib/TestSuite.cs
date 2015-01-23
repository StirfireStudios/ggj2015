using System.Collections.Generic;
using System;
using N.Tests;

namespace N.Tests {

  /// Basic genric test type
  public class TestSuite {

    private List<Test> _items = new List<Test>();

    /// Total test count run
    public int total = 0;

    /// Passing tests
    public List<Test> passed = new List<Test>();

    /// Failing tests
    public List<Test> failed = new List<Test>();

    /// Register self with runner
    public TestSuite() {
      foreach (var method in this.GetType().GetMethods())
      {
        var name = method.Name;
        if (name.StartsWith("test_")) {
          this._items.Add(new Test(method.Name, method, this));
        }
      }
    }

    /// Run this test suite
    public bool run() {
      for (var i = 0; i < this._items.Count; ++i) {
        this.total += 1;
        var t = this._items[i];
        if (t.run()) {
          this.passed.Add(t);
        }
        else {
          this.failed.Add(t);
        }
      }
      return this.failed.Count == 0;
    }

    /// Assert something is true in a test
    public void assert(bool value) {
      if (!value) {
        throw new Exception("Test failed");
      }
    }
  }
}
