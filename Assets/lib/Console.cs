using UnityEngine;
using System;
using System.Collections.Generic;
using N.Tests;

namespace N {

  public class Console {

    /// Debugging mode for tests
    public static bool DEBUG = false;

    /// Debug a message (array)
    public static void log<T>(T[] msg) {
      var expanded = list(msg);
      Console.log(typeof(T).ToString() + "[] { " + String.Join(",", expanded) + " }");
    }

    /// Debug a message (list)
    public static void log<T>(List<T> msg) {
      var expanded = list(msg);
      Console.log("List<"+typeof(T).ToString() + "> { " + String.Join(",", expanded) + " }");
    }

    /// Debug a message (any)
    public static void log(object msg) {
      Console.log(as_str(msg));
    }

    /// Debug a message (string)
    public static void log(String msg) {
      if (DEBUG) {
        Debug.Log(" ------- DEBUG ------- " + msg);
      }
      else {
        Debug.Log(msg);
      }
    }

    /// Debug a message (any)
    public static void error(Exception msg) {
      Debug.LogException(msg);
    }

    /// Debug a message (any)
    public static void error<T>(T msg) {
      N.Console.error(as_str(msg));
    }

    /// Debug a message (string)
    public static void error(String msg) {
      Debug.LogError(msg);
    }

    /// Helper to convert objects to strings
    private static String as_str(object msg) {
      if (msg == null) {
        return "null";
      }
      else {
        try {
          return ("" + msg) as String;
        }
        catch(Exception) {
          return "[unrenderable function: " + msg.GetType() + "]";
        }
      }
    }

    /// Expand a list of values into an array repr
    public static String[] list<T>(T[] target) {
      int size = target.Length;
      var rtn = new String[size];
      for (var i = 0; i < size; ++i) {
        rtn[i] = as_str(target[i]);
      }
      return rtn;
    }

    /// Expand a list of values into an array repr
    public static String[] list<T>(List<T> target) {
      int size = target.Count;
      var rtn = new String[size];
      for (var i = 0; i < size; ++i) {
        rtn[i] = as_str(target[i]);
      }
      return rtn;
    }
  }

  /// Tests
  public class ConsoleTests : TestSuite {

    public void test_can_expand_array() {
      var x = new int[3] { 1, 2, 3 };
      var t = N.Console.list(x);
      this.assert(t[0] == "1");
      this.assert(t[1] == "2");
      this.assert(t[2] == "3");
    }

    public void test_can_expand_list() {
      var x = new List<int>();
      x.Add(1);
      x.Add(2);
      x.Add(3);
      var t = N.Console.list(x);
      this.assert(t[0] == "1");
      this.assert(t[1] == "2");
      this.assert(t[2] == "3");
    }

    public void test_log() {
      N.Console.DEBUG = true;
      N.Console.log("Console log string");
      N.Console.log(this);
      N.Console.log(new int[3] {1, 2, 3});
      var x = new List<int>();
      x.Add(1);
      x.Add(2);
      x.Add(3);
      N.Console.log(x);
      N.Console.DEBUG = false;
    }
  }
}
