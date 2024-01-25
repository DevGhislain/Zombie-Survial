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
    private float shootingRange = 100f;

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

    /// <summary>
    /// Reference for the Hand player
    /// </summary>
    [SerializeField]
    private Transform handPlayer;

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

    /// <summary>
    /// Reference for the player script
    /// </summary>
    [SerializeField]
    private PlayerBehaviour player;

    /// <summary>
    /// Reference of the Animator
    /// </summary>
    public Animator animator;

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
    /// Reference to the cartouche mag
    /// </summary>
    public int mag = 10;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Methods for the Unity 
    /// </summary>
    private void Awake()
    {
        transform.SetParent(handPlayer);
        presentAmmunition = maximunAmmmnution;
    }

    /// <summary>
    /// Update methods 
    /// </summary>
    private void Update()
    {
        if (setReloading)
            return;

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
          
            nextTimeToShoot = Time.time + 1f/fireCharge;
            Shoot();
        }
        else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("FireWalk", true);
        }
        else if ((Input.GetButton("Fire2") && Input.GetButton("Fire1")))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Fire", false);
            animator.SetBool("FireWalk", false);
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Methods for the Shoot 
    /// </summary>
    void Shoot()
    {
        //check for mag
        if (mag == 0)
        {
            //show the text
            return;
        }

        presentAmmunition--;

        if (presentAmmunition == 0)
        {
            mag--;
        }

        if (presentAmmunition <= 0)
        {
            StartCoroutine(Reload());
        }

        // Update the UI

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


    IEnumerator Reload()
    {
        player.playerSpeed = 0;
        player.playerSprint = 0;
        setReloading = true;

        // animation
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadingTime);

        // animation
        animator.SetBool("Reloading", false);
        // play the anim
        presentAmmunition = maximunAmmmnution;
        player.playerSpeed = 1.9f;
        player.playerSprint = 3f;
        setReloading = false;
    }

    #endregion
}
