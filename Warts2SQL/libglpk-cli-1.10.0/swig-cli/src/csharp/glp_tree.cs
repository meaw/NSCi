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

public class glp_tree : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal glp_tree(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(glp_tree obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~glp_tree() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          GLPKPINVOKE.delete_glp_tree(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  
/**
 * Hides internal structure.
 *
 * The internal fields of this structure cannot be accessed
 * directly. Use the library methods.
 */
  public int hidden_internal {
  /**
   * Does nothing.
   */
  set {
    
    if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    return;
  }

  /**
   * Always returns zero.
   *
   * @return 0
   */
  get {
    
    if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
    return 0;
  }

  }

  public glp_tree() : this(GLPKPINVOKE.new_glp_tree(), true) {
    if (GLPKPINVOKE.SWIGPendingException.Pending) throw GLPKPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
