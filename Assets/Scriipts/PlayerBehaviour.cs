using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    #region Private mambers

    [Header("Player Mouvement")]
    [SerializeField]
    private float playerSpeed = 1.9f;

    [Header("Player script camera ")]
    [SerializeField]
    Transform playerCamera;

    [Header("Player Animator and gravity")]

    [SerializeField]
    private CharacterController characterControler;

    [Header("Player Jumping and velocity")]

    [SerializeField]
    private float turnCalmTime = 0.1f;

    [SerializeField]
    private float turnCalmVelocity = 0.1f;

    #endregion

    #region Unity Methods

    /// <summary>
    ///  Unity methods Update 
    /// </summary>
    private void Update()
    {
        PlayerMove();
    }

     #endregion

    #region methods

    /// <summary>
    /// Methods for the pla;yer move
    /// </summary>
    void PlayerMove()
    {
        float horiontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horiontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterControler.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
    }

    #endregion


}
