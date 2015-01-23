using UnityEngine;
using System;

namespace N {

  public class Meta {

    // Instance of the object
    protected GameObject _g;
    private Meta2d __2d = null;
    private MetaUi _ui = null;

    /// Create a new gameobject api instance
    public static Meta _(MonoBehaviour m) {
      return new Meta(m.gameObject);
    }

    /// Create a new gameobject api instance
    public static Meta _(GameObject g) {
      return new Meta(g);
    }

    /// Return a list of objects matching the given component interface
    public static Meta[] _<T>() {
      var matches = GameObject.FindObjectsOfType(typeof(T));
      var rtn = new Meta[matches.Length];
      for (var i = 0; i < matches.Length; ++i) {
        rtn[i] = Meta._(matches[i] as GameObject);
      }
      return rtn;
    }

    /// Return a meta interface from a name for debugging purposes.
    public static Meta _(string name) {
      var obj = GameObject.Find(name);
      if (obj == null) {
        throw new Exception("Cannot find object '" + name + "' in scene");
      }
      else {
        N.Console.log("Warning! Debug usage of GameObject.Find()");
        return new Meta(obj);
      }
    }

    public Meta(GameObject g) {
      if (g == null) {
        throw new Exception("Cannot create a meta object for an empty target");
      }
      this._g = g;
    }


    /// Get the 2d interface
    public Meta2d _2d {
      get {
        if (this.__2d == null) {
          this.__2d = new Meta2d(this);
        }
        return this.__2d;
      }
    }

    /// Get the ui interface
    public MetaUi ui {
      get {
        if (this._ui == null) {
          this._ui = new MetaUi(this);
        }
        return this._ui;
      }
    }

    /// Get the raw held object
    public GameObject raw {
      get { return this._g; }
    }

    /// Set the shader on an object
    public Shader shader {
      set { this._g.renderer.material.shader = value; }
    }

    /// Turn rendering on or off for object
    public bool active {
      set { this._g.SetActive(value); }
    }

    /// Apply opacity value to given object instance
    public float opacity {
      set {
        var c = this._g.renderer.material.color;
        c.a = value;
        this._g.renderer.material.color = c;
      }
    }

    /// Apply the texture to this object's MeshRenderer
    public Texture texture {
      set {
        var mr = this.cmp<MeshRenderer>();
        if (mr != null) {
          mr.material.mainTexture = value;
        }
      }
    }

    /// Apply opacity value to given object instance
    public float brightness {
      set {
        var c = this._g.renderer.material.color;
        c.r = value;
        c.g = value;
        c.b = value;
        this._g.renderer.material.color = c;
      }
    }

    /// Apply opacity value to given object instance
    public Meta color(float r, float g, float b) {
      this.color(r, g, b, 1.0f);
      return this;
    }

    /// Apply opacity value to given object instance
    public Meta color(float r, float g, float b, float a) {
      var c = this._g.renderer.material.color;
      c.r = r;
      c.g = g;
      c.b = b;
      c.a = a;
      this._g.renderer.material.color = c;
      return this;
    }

    /// Add a component to the target
    public T add_component<T>() where T : UnityEngine.Component {
      if (!this.has_component<T>()) {
        this._g.AddComponent<T>();
      }
      return this.component<T>() as T;
    }

    /// Add a component to the target
    public void remove_component<T>() where T : UnityEngine.Component {
      this.remove_component<T>(false);
    }

    /// Add a component to the target
    public void remove_component<T>(bool editor) where T :  UnityEngine.Component {
      if (this.has_component<T>()) {
        var c = this.component<T>();
        if (editor) {
          UnityEngine.Object.DestroyImmediate(c);
        }
        else {
          UnityEngine.Object.Destroy(c);
        }
      }
    }

    /// Check if a component exists
    public bool has_component<T>() where T : UnityEngine.Component {
      return this._g.GetComponent(typeof(T)) != null;
    }

    /// Get a component instance from a game object, or any child
    /// usage: meta._(this).cmp(MeshRenderer) as MeshRenderer
    public UnityEngine.Component component(Type target, bool suppress_warnings) {
      var rtn = this._g.GetComponent(target);
      if (rtn == null) {
        rtn = this._g.GetComponentInChildren(target);
        if (!suppress_warnings && (rtn == null)) {
          N.Console.log("Warning: " + target.Name + " is not a component on " + this._g);
        }
      }
      return rtn;
    }

    /// Get a component instance from a game object, or any child
    /// usage: meta._(this).cmp(MeshRenderer) as MeshRenderer
    public T component<T>(bool suppress_warnings) where T : UnityEngine.Component {
      return component(typeof(T), suppress_warnings) as T;
    }

    /// Get a component instance from a game object or any child
    /// usage: meta._(this).cmp(MeshRenderer) as MeshRenderer
    public T component<T>() where T : UnityEngine.Component {
      return this.cmp<T>(false);
    }

    /// Shortcut to component()
    public T cmp<T>(bool suppress_warnings) where T : UnityEngine.Component {
      return this.component<T>(suppress_warnings);
    }

    /// Shortcut to component()
    public T cmp<T>() where T : UnityEngine.Component {
      return this.component<T>(false);
    }

    /// Resize to a specific x,y,z size on the transform
    public Meta scale(float dx, float dy, float dz) {
      this._g.transform.localScale = new Vector3(dx, dy, dz);
      return this;
    }

    /// Move to a specific x,y,z pos on the transform
    public Meta move(float x, float y, float z) {
      this._g.transform.position = new Vector3(x, y, z);
      return this;
    }

    /// Move to a specific x,y,z pos on the transform
    public Meta move(Vector3 p) {
      this._g.transform.position = p;
      return this;
    }

    /// Check if the object is within distance of the target
    public bool near(float x, float y, float z, float distance) {
      return this.near(new Vector3(x, y, z), distance);
    }

    /// Check if the object is within distance of the target
    public bool near(Vector3 target, float distance) {
      return Vector3.Distance(target, this._g.transform.position) < distance;
    }

    /// Destroy this game object
    public void destroy() {
      UnityEngine.Object.Destroy(this._g);
    }

    /// Get the size of this object
    public Vector3 get_size() {

      // Is this a mesh object?
      var filter = this.cmp<MeshFilter>(true);
      if (filter != null) {
        var min = Vector3.one * Mathf.Infinity;
        var max = Vector3.one * Mathf.NegativeInfinity;
        var mesh = this.cmp<MeshFilter>().mesh;
        foreach (var vert in mesh.vertices){
          min = Vector3.Min(min, vert);
          max = Vector3.Max(max, vert);
        }
        return Vector3.Scale(max-min, this._g.transform.localScale);
      }

      // Is this a sprite?
      else {
        var sprite = this.cmp<SpriteRenderer>(true);
        if (sprite != null) {
          var sp = sprite.sprite;
          var min = sp.bounds.min;
          var max = sp.bounds.max;
          return Vector3.Scale(max - min, this._g.transform.localScale);
        }
      }

      N.Console.error("Unable to determine size of unknown object type");
      return new Vector3(0f, 0f, 0f);
    }

    /// Get the maximal extents of this object
    public Vector3[] get_extents() {
      var rtn = new Vector3[2];
      var size = this.get_size();
      rtn[0] = this._g.transform.position - size / 2;
      rtn[1] = this._g.transform.position + size / 2;
      return rtn;
    }
  }
}
