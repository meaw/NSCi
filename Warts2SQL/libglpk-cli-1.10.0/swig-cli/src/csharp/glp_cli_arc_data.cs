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

public class glp_cli_arc_data : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal glp_cli_arc_data(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(glp_cli_arc_data obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~glp_cli_arc_data() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          GLPKPINVOKE.delete_glp_cli_arc_data(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public double cap {
    set {
      GLPKPINVOKE.glp_cli_arc_data_cap_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = GLPKPINVOKE.glp_cli_arc_data_cap_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double cost {
    set {
      GLPKPINVOKE.glp_cli_arc_data_cost_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = GLPKPINVOKE.glp_cli_arc_data_cost_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double low {
    set {
      GLPKPINVOKE.glp_cli_arc_data_low_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = GLPKPINVOKE.glp_cli_arc_data_low_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double rc {
    set {
      GLPKPINVOKE.glp_cli_arc_data_rc_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = GLPKPINVOKE.glp_cli_arc_data_rc_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double x {
    set {
      GLPKPINVOKE.glp_cli_arc_data_x_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = GLPKPINVOKE.glp_cli_arc_data_x_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public glp_cli_arc_data() : this(GLPKPINVOKE.new_glp_cli_arc_data(), true) {
    if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
