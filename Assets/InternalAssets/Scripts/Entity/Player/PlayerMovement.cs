using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 1.5f;

    private void Update()
    {
        rb.velocity = InputHandler.MoveVector * speed;
    }

}
