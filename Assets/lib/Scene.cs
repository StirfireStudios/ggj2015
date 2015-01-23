using System;
using UnityEngine;

namespace N {

  /// Global functions to modify the current scene
  public class Scene {

    /// Add a new resource prefab instance to the current scene and return it
    public static GameObject add_prefab(String resource) {
      try {
        var factory = Resources.Load(resource, typeof(GameObject)) as GameObject;
        var instance = UnityEngine.Object.Instantiate(factory);
        return instance as GameObject;
      }
      catch(Exception e) {
        N.Console.error("Failed to load prefab path: " + resource + ": " + e);
      }
      return null;
    }
  }
}
