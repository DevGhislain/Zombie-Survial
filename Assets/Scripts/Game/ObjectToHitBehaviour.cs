using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToHitBehaviour : MonoBehaviour
{
    #region Private Members

    /// <summary>
    /// Reference of the health to object
    /// </summary>
    [SerializeField]
    private float objectHealth = 30f;

    #endregion

    #region Public Methods

    public void ObjectHitDamage(float damage)
    {
        objectHealth -= damage;
        if (objectHealth<= 0)
        {
            Die();
        }
    }

    #endregion

    #region private Methods

    /// <summary>
    /// Methods for the object die
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }

    #endregion
}
