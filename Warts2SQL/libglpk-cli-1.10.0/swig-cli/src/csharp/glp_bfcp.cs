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

public class glp_bfcp : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal glp_bfcp(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(glp_bfcp obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~glp_bfcp() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          GLPKPINVOKE.delete_glp_bfcp(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public int msg_lev {
    set {
      GLPKPINVOKE.glp_bfcp_msg_lev_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = GLPKPINVOKE.glp_bfcp_msg_lev_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int type {
    set {
      GLPKPINVOKE.glp_bfcp_type_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = GLPKPINVOKE.glp_bfcp_type_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int lu_size {
    set {
      GLPKPINVOKE.glp_bfcp_lu_size_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = GLPKPINVOKE.glp_bfcp_lu_size_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double piv_tol {
    set {
      GLPKPINVOKE.glp_bfcp_piv_tol_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = GLPKPINVOKE.glp_bfcp_piv_tol_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int piv_lim {
    set {
      GLPKPINVOKE.glp_bfcp_piv_lim_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = GLPKPINVOKE.glp_bfcp_piv_lim_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int suhl {
    set {
      GLPKPINVOKE.glp_bfcp_suhl_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = GLPKPINVOKE.glp_bfcp_suhl_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double eps_tol {
    set {
      GLPKPINVOKE.glp_bfcp_eps_tol_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = GLPKPINVOKE.glp_bfcp_eps_tol_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double max_gro {
    set {
      GLPKPINVOKE.glp_bfcp_max_gro_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = GLPKPINVOKE.glp_bfcp_max_gro_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int nfs_max {
    set {
      GLPKPINVOKE.glp_bfcp_nfs_max_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = GLPKPINVOKE.glp_bfcp_nfs_max_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double upd_tol {
    set {
      GLPKPINVOKE.glp_bfcp_upd_tol_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = GLPKPINVOKE.glp_bfcp_upd_tol_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int nrs_max {
    set {
      GLPKPINVOKE.glp_bfcp_nrs_max_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = GLPKPINVOKE.glp_bfcp_nrs_max_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int rs_size {
    set {
      GLPKPINVOKE.glp_bfcp_rs_size_set(swigCPtr, value);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = GLPKPINVOKE.glp_bfcp_rs_size_get(swigCPtr);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_double foo_bar {
    set {
      GLPKPINVOKE.glp_bfcp_foo_bar_set(swigCPtr, SWIGTYPE_p_double.getCPtr(value));
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = GLPKPINVOKE.glp_bfcp_foo_bar_get(swigCPtr);
      SWIGTYPE_p_double ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_double(cPtr, false);
      if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public glp_bfcp() : this(GLPKPINVOKE.new_glp_bfcp(), true) {
    if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
