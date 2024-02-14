using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
    #region Private Members

    /// <summary>
    /// Reference for the zombie Agent
    /// </summary>
    [SerializeField]
    private NavMeshAgent zombieAgent;

    /// <summary>
    /// Reference for the player layer 
    /// </summary>
    [SerializeField]
    private Transform loockPoint;

    /// <summary>
    /// Reference for the player transform
    /// </summary>
    [SerializeField]
    private Transform playerPosition;

    /// <summary>
    /// Reference for the player layer 
    /// </summary>
    [SerializeField]
    private LayerMask playerlayer;

    /// <summary>
    /// Reference for the walk point
    /// </summary>
    [SerializeField]
    private GameObject[] walkPoints;

    /// <summary>
    /// Reference for the zombie speed
    /// </summary>
    [SerializeField]
    private float zombieSpeed;

    /// <summary>
    /// Reference of current zombie position
    /// </summary>
    private int currentZombiePosition = 0;

    /// <summary>
    /// Reference of walking Point Radius
    /// </summary>
    private float walkingPointRadius = 2f ;

    #endregion

    #region Public Members 

    /// <summary>
    /// Reference for the vision radius 
    /// </summary>
    public float visionRadius;

    /// <summary>
    /// Reference for the attack radius 
    /// </summary>
    public float attackingRadius ;

    /// <summary>
    /// Reference for the player Invision Radius
    /// </summary>
    public bool playerInvisionRadius;

    /// <summary>
    /// Reference for the player Inattacking Radiui
    /// </summary>
    public bool playerInattackingRadius;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start Unity Methods
    /// </summary>
    private void Start()
    {
        // Cache agent component and destination
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    ///  Unity methods Update 
    /// </summary>
    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerlayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, playerlayer);
        if (!playerInvisionRadius && !playerInattackingRadius)
        {
            OnGuard();
        }
        if (playerInvisionRadius && !playerInattackingRadius)
        {
            PursuepPlayer();
        }
    }



    #endregion

    #region  Private Methods

    /// <summary>
    /// Reference for the player Onguard
    /// </summary>
    private void OnGuard()
    {
        if (Vector3.Distance(walkPoints[currentZombiePosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentZombiePosition = UnityEngine.Random.Range(0, walkPoints.Length);
            if (currentZombiePosition >= walkPoints.Length)
            {
                currentZombiePosition = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed);

        //Change zombie facing
        transform.LookAt(walkPoints[currentZombiePosition].transform.position);
    }

    /// <summary>
    /// Methods for the zombie pursuep the player
    /// </summary>
    private void PursuepPlayer()
    {
        zombieAgent.destination = playerPosition.position;
    }

    #endregion

}
