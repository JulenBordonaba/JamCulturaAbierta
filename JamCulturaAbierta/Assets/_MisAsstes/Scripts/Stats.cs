using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float acceleration = 1f;


    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }
        set
        {
            movementSpeed = value;
        }
    }

    public float Acceleration
    {
        get
        {
            return acceleration;
        }
        set
        {
            acceleration = value;
        }
    }
}
