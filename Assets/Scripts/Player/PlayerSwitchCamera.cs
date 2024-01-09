 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchCamera : MonoBehaviour
{
    #region Private Members

    ///Header Camera to assign

    /// <summary>
    /// Reference of the  Aim CineMachine
    /// </summary>
    [SerializeField]
    private GameObject AimCineMachine;

    /// <summary>
    /// Reference of the  Third Person CineMachine
    /// </summary>
    [SerializeField]
    private GameObject ThirdPersonCineMachine;

    /// <summary>
    /// Reference of the  TP camera
    /// </summary>
    [SerializeField]
    private GameObject TPCanvas;

    /// <summary>
    /// Reference of the  Aim camera
    /// </summary>
    [SerializeField]
    private GameObject AimCanvas;

    #endregion

    #region Unity Mathods

    /// <summary>
    /// Update methods 
    /// </summary>
    private void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            ThirdPersonCineMachine.SetActive(false);
            TPCanvas.SetActive(false);
            AimCineMachine.SetActive(true);
            AimCanvas.SetActive(true);
        }
        else
        {
            ThirdPersonCineMachine.SetActive(true);
            TPCanvas.SetActive(true);
            AimCineMachine.SetActive(false);
            AimCanvas.SetActive(false);
        }
    }

    #endregion
}
