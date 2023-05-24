using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float speed = 1.5f;

    private void Update()
    {
        rigidbody2D.velocity = InputHandler.MoveVector * speed;
    }

}
