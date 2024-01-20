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
    /// Reference for the radios 
    /// </summary>
    private float radios = 2.5f;

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
