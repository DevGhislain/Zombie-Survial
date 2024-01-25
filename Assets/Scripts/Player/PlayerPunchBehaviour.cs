using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunchBehaviour : MonoBehaviour
{
    #region Private Members

    /// <summary>
    /// Reference for the camera 
    /// </summary>
    [SerializeField]
    private Camera camera;

    /// <summary>
    /// Reference of the Player punch give a damage
    /// </summary>
    [SerializeField]
    private float giveDamage = 10f;

    /// <summary>
    /// Reference of the punch Range 
    /// </summary>
    [SerializeField]
    private float punchRange = 5f;

    #endregion

    #region Public region 

    /// <summary>
    /// Refrence to wood effect
    /// </summary>
    public GameObject woodeffect;

    #endregion

    #region Public Methods

    /// <summary>
    /// Methods for the punch Playerf
    /// </summary>
    public void Punch()
    {
        RaycastHit raycastHitInfo;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out raycastHitInfo, punchRange))
        {
            Debug.Log(raycastHitInfo.transform.name);
            ObjectToHitBehaviour objectToHitBehaviour = raycastHitInfo.transform.GetComponent<ObjectToHitBehaviour>();
            if (objectToHitBehaviour != null)
            {
                objectToHitBehaviour.ObjectHitDamage(giveDamage);
                GameObject woodGo = Instantiate(woodeffect, raycastHitInfo.point, Quaternion.LookRotation(raycastHitInfo.normal));
                Destroy(woodGo, 1f);
            }
        }
    }


    #endregion

}
