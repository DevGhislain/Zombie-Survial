using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    #region Private mambers

    /// <summary>
    /// Reference of the turn Calm Velocity
    /// </summary>
    [SerializeField]
    private float turnCalmVelocity = 0.1f;

    /// <summary>
    /// Reference of the jump Range
    /// </summary>
    [SerializeField]
    private float jumpRange = 1.0f;

    /// <summary>
    /// Reference of the surface Check
    /// </summary>
    [SerializeField]
    private Transform surfaceCheck;

    /// <summary>
    /// Reference of the check On surface
    /// </summary>
    [SerializeField]
    private bool Onsurface;

    /// <summary>
    /// Reference of the gravity
    /// </summary>
    Vector3 velocity;

    [Header("Player Mouvement")]

    /// <summary>
    /// Reference of the player Speed
    /// </summary>
    [SerializeField]
    private float playerSpeed = 1.9f;

    /// <summary>
    /// Reference of the player Speed
    /// </summary>
    [SerializeField]
    private float playerSprint = 3f;

    #endregion

    #region Public Members

    [Header("Player script camera ")]

    /// <summary>
    /// Reference of the player Camera
    /// </summary>
    public Transform playerCamera;

    [Header("Player Animator and gravity")]

    /// <summary>
    /// Reference of the character Controler
    /// </summary>
    public CharacterController characterControler;

    /// <summary>
    /// Reference of the gravity
    /// </summary>
    public float gravity = -9.81f;

    /// <summary>
    /// Reference of the Animator
    /// </summary>
    public Animator animator;

    [Header("Player Jumping and velocity")]

    /// <summary>
    /// Reference of the surface Distance
    /// </summary>
    public float surfaceDistance = 0.4f;

    /// <summary>
    /// Reference of the turn Calm Time
    /// </summary>
    public float turnCalmTime = 0.1f;

    /// <summary>
    /// Reference of the surface Mask
    /// </summary>
    public LayerMask surfaceMask;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Unity methods Start 
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    ///  Unity methods Update 
    /// </summary>
    private void Update()
    {
        Onsurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        if (Onsurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterControler.Move(velocity * Time.deltaTime);

        PlayerMove();

        PlayerJump();

        PlayerSprint();
    }

     #endregion

    #region private Methods

    /// <summary>
    /// Methods for the player move
    /// </summary>
    void PlayerMove()
    {
        float horiontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horiontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {

            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            animator.SetBool("Running", false);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("IdleAim", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterControler.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
        }
    }

    /// <summary>
    /// Method for the player jump
    /// </summary>
    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && Onsurface)
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jump");
        }
    }

    /// <summary>
    /// Method for the player sprint
    /// </summary>
    void PlayerSprint()
    {
        if (Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && Onsurface)
        {
            float horiontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horiontal_axis, 0f, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Running", true);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterControler.Move(moveDirection.normalized * playerSprint * Time.deltaTime);;
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Running", false);
            }
        }
    }

    #endregion
}

