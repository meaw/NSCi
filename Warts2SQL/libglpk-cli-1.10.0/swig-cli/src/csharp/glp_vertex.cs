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

public class glp_vertex : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal glp_vertex(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(glp_vertex obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~glp_vertex() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          GLPKPINVOKE.delete_glp_vertex(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public int i {
    set {
      GLPKPINVOKE.glp_vertex_i_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = GLPKPINVOKE.glp_vertex_i_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string name {
    set {
      GLPKPINVOKE.glp_vertex_name_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = GLPKPINVOKE.glp_vertex_name_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_void entry {
    set {
      GLPKPINVOKE.glp_vertex_entry_set(swigCPtr, SWIGTYPE_p_void.getCPtr(value));
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = GLPKPINVOKE.glp_vertex_entry_get(swigCPtr);
      SWIGTYPE_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_void data {
    set {
      GLPKPINVOKE.glp_vertex_data_set(swigCPtr, SWIGTYPE_p_void.getCPtr(value));
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = GLPKPINVOKE.glp_vertex_data_get(swigCPtr);
      SWIGTYPE_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_void temp {
    set {
      GLPKPINVOKE.glp_vertex_temp_set(swigCPtr, SWIGTYPE_p_void.getCPtr(value));
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = GLPKPINVOKE.glp_vertex_temp_get(swigCPtr);
      SWIGTYPE_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public glp_arc in_ {
    set {
      GLPKPINVOKE.glp_vertex_in__set(swigCPtr, global::System.Runtime.InteropServices.HandleRef.ToIntPtr(glp_arc.getCPtr(value)));
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = GLPKPINVOKE.glp_vertex_in__get(swigCPtr);
    if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return (cPtr == global::System.IntPtr.Zero) ? null : new glp_arc(cPtr, false);
    }
  
  }

  public glp_arc out_ {
    set {
      GLPKPINVOKE.glp_vertex_out__set(swigCPtr, global::System.Runtime.InteropServices.HandleRef.ToIntPtr(glp_arc.getCPtr(value)));
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = GLPKPINVOKE.glp_vertex_out__get(swigCPtr);
    if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return (cPtr == global::System.IntPtr.Zero) ? null : new glp_arc(cPtr, false);
    }
  
  }

  public glp_vertex() : this(GLPKPINVOKE.new_glp_vertex(), true) {
    if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
