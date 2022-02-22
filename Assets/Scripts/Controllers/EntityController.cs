using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
#region Variables
    #region Client Variables
    #if !UNITY_SERVER || UNITY_EDITOR

    #endif
    #endregion
    
    #region Server Variables
    #if UNITY_SERVER || UNITY_EDITOR

    #endif
    #endregion
#endregion
#region General Methods
    // Start is called when this object is first enabled
    void Start() 
    {
    #region Client Start Actions
    #if !UNITY_SERVER || UNITY_EDITOR

    #endif
    #endregion

    #region Server Start Actions
    #if UNITY_SERVER || UNITY_EDITOR

    #endif
    #endregion
    }

    // Update is called every frame
    #if !UNITY_SERVER || UNITY_EDITOR
    /*
    There's nothing we want to run every frame on the server, and frames are frankly irrelevant to something that doesn't have a graphics driver, so this entire method should be excluded from server builds.
    It will only be used for client-side framewise entity updates.
    */
    void Update() 
    {

    }
    #endif

    // FixedUpdate is called every tick (every 1/10th of a second)
    void FixedUpdate() 
    {
    #region Client FixedUpdate Actions
    #if !UNITY_SERVER || UNITY_EDITOR

    #endif
    #endregion

    #region Server FixedUpdate Actions
    #if UNITY_SERVER || UNITY_EDITOR

    #endif
    #endregion
    }

    // OnDestroy is called when this entity is destroyed
    void OnDestroy() 
    {
    #region Client OnDestroy Actions
    #if !UNITY_SERVER || UNITY_EDITOR

    #endif
    #endregion

    #region Server OnDestroy Actions
    #if UNITY_SERVER || UNITY_EDITOR

    #endif
    #endregion
    }
#endregion
#region Specific Methods
    #region Client Methods
    #if !UNITY_SERVER || UNITY_EDITOR

    #endif
    #endregion

    #region Server Methods
    #if UNITY_SERVER || UNITY_EDITOR

    #endif
    #endregion
    #region Commands

    #endregion
#endregion
}
