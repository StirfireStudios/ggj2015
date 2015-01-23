using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;
using N;

namespace N.Tests {

  /// Runner for all the various tests
  public class TestRunner : MonoBehaviour {

    /// Run all tests
    public void Start() {

      // Find types
      var items = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
      var tests = new List<TestSuite>();
      for (var ts = 0; ts < items.Length; ++ts) {
        if (items[ts].IsSubclassOf(typeof(TestSuite))) {
          tests.Add(System.Activator.CreateInstance(items[ts]) as TestSuite);
        }
      }

      N.Console.log(DateTime.Now.ToString());

      var all = 0;
      for (var i = 0; i < tests.Count; ++i) {
        try {
          tests[i].run();
          all += tests[i].total;
        }
        catch(Exception e) {
          N.Console.log(e);
        }
      }

      for (var j = 0; j < tests.Count; ++j) {
        var t = tests[j];
        N.Console.log("Test: " + t + ": " + t.passed.Count + "/" + t.total);
      }

      for (var k = 0; k < tests.Count; ++k) {
        var t2 = tests[k];
        for (var l = 0; l < t2.failed.Count; ++l) {
          var f2 = t2.failed[l];
          N.Console.error(f2.error);
        }
      }
    }
  }
}
