using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRiflePickUpBeahviour : MonoBehaviour
{

    #region Private Members

    /// <summary>
    /// Reference of the player rifle
    /// </summary>
    [SerializeField]
    private GameObject playerRifle;

    /// <summary>
    /// Reference of the pickup Rifle
    /// </summary>
    [SerializeField]
    private GameObject pickupRifle;

    /// <summary>
    /// Reference of the player script
    /// </summary>
    [SerializeField]
    private PlayerBehaviour player;

    /// <summary>
    /// Reference of the player Punch script
    /// </summary>
    [SerializeField]
    private PlayerPunchBehaviour playerPunch;

    /// <summary>
    /// Reference for the radios 
    /// </summary>
    private float radios = 2.5f;

    /// <summary>
    /// Reference of the Animator
    /// </summary>
    [SerializeField]
    Animator animator;

    /// <summary>
    /// Reference of the next Time To Punch
    /// </summary>
    [SerializeField]
    private float nextTimeToPunch = 0f;

    /// <summary>
    /// Reference of the punch Range
    /// </summary>
    [SerializeField]
    private float punchRange = 15f;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Awake methods 
    /// </summary>
    private void Awake()
    {
        playerRifle.SetActive(false);
    }

    /// <summary>
    /// update Methods 
    /// </summary>
    private void Update()
    {

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToPunch)
        {
            nextTimeToPunch = Time.time + 1f / punchRange;

            animator.SetBool("Idle", false);
            animator.SetBool("Punch", true);
            playerPunch.Punch();
          
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Punch", false);
        }
        if (Vector3.Distance(transform.position, player.transform.position) < radios)
        {
            if (Input.GetKeyDown("f"))
            {
                playerRifle.SetActive(true);
                pickupRifle.SetActive(false);
            }
        }   
    }

    #endregion
}
