using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRifleBehaviour : MonoBehaviour
{
    #region Private Members

    [Header("Rifle thing")]

    /// <summary>
    /// Reference of the camera
    /// </summary>
    [SerializeField]
    private Camera camera;

    /// <summary>
    /// Reference for the damage
    /// </summary>
    [SerializeField]
    private float takeDamgeOf = 10f;

    /// <summary>
    /// Reference for the shooting
    /// </summary>
    [SerializeField]
    private float shootingRange= 100f;

    /// <summary>
    /// Reference for the fire Charge
    /// </summary>
    [SerializeField]
    private float fireCharge = 15f;

    /// <summary>
    /// Reference for the next Time To Shoot
    /// </summary>
    [SerializeField]
    private float nextTimeToShoot = 0f;

    [Header("Rifle Ammunition and shooting")]

    /// <summary>
    /// Reference for the next Time To Shoot
    /// </summary>
    [SerializeField]
    private int maximunAmmmnution = 32;

    /// <summary>
    /// Reference for the next Time To Shoot
    /// </summary>
    [SerializeField]
    private int presentAmmunition;

    /// <summary>
    /// Reference for the ReloadingTime
    /// </summary>
    [SerializeField]
    private float reloadingTime = 1.3f;

    /// <summary>
    /// Reference fto the set reloading 
    /// </summary>
    [SerializeField]
    private bool setReloading = false;

    #endregion

    #region Public Members

    [Header("Particule system")]

    /// <summary>
    /// Reference to the particule system
    /// </summary>
    public ParticleSystem muzzLeSpark;

    /// <summary>
    /// Refrence to wood effect
    /// </summary>
    public GameObject woodeffect;

    [Header("Rifle Ammunition and shooting")]

    /// <summary>
    /// Reference to the mag
    /// </summary>
    public int mag = 10;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Methods for the Unity 
    /// </summary>
    private void Awake()
    {
        
    }

    /// <summary>
    /// Update methods 
    /// </summary>
    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot += Time.time + 1f/ fireCharge;
            Shoot();
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Methods for the Shoot 
    /// </summary>
    void Shoot()
    {
        muzzLeSpark.Play();

        RaycastHit raycastHitInfo;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out raycastHitInfo, shootingRange))
        {
            Debug.Log(raycastHitInfo.transform.name);
            ObjectToHitBehaviour objectToHitBehaviour = raycastHitInfo.transform.GetComponent<ObjectToHitBehaviour>();
            if (objectToHitBehaviour != null)
            {
                objectToHitBehaviour.ObjectHitDamage(takeDamgeOf);
                GameObject woodGo = Instantiate(woodeffect, raycastHitInfo.point, Quaternion.LookRotation(raycastHitInfo.normal));
                Destroy(woodGo, 1f);
            }
        }
    }

    #endregion
}
