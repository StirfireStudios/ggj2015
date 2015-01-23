using System.Collections.Generic;
using System.Reflection;
using System;

namespace N.Tests {

  /// An actual test instance
  public class Test {

    private MethodInfo _fp;
    private Object _base;
    public String name;
    public Exception error;

    /// Create from funciton pointer
    public Test(String name, MethodInfo fp, Object self) {
      this.error = null;
      this.name = name;
      this._fp = fp;
      this._base = self;
    }

    /// Run instance and return result
    public bool run() {
      try {
        this._fp.Invoke(this._base, null);
        return true;
      }
      catch(Exception e) {
        this.error = e;
        return false;
      }
    }
  }
}
