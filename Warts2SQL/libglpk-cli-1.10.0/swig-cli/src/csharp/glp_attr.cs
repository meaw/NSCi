//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace org.gnu.glpk {

public class glp_attr : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal glp_attr(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(glp_attr obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~glp_attr() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          GLPKPINVOKE.delete_glp_attr(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public int level {
    set {
      GLPKPINVOKE.glp_attr_level_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = GLPKPINVOKE.glp_attr_level_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int origin {
    set {
      GLPKPINVOKE.glp_attr_origin_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = GLPKPINVOKE.glp_attr_origin_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int klass {
    set {
      GLPKPINVOKE.glp_attr_klass_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = GLPKPINVOKE.glp_attr_klass_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_double foo_bar {
    set {
      GLPKPINVOKE.glp_attr_foo_bar_set(swigCPtr, SWIGTYPE_p_double.getCPtr(value));
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = GLPKPINVOKE.glp_attr_foo_bar_get(swigCPtr);
      SWIGTYPE_p_double ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_double(cPtr, false);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public glp_attr() : this(GLPKPINVOKE.new_glp_attr(), true) {
    if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
