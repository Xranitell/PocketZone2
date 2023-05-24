using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    public float speed = 1f;
    public float damage;
    public float bulletLifeTime = 2f;
    private float timer;

    public Gun parentGun;
    private void Update()
    {
        Move();
        DeathTimer();
    }

    private void DeathTimer()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            ReturnToPull();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            ReturnToPull();
        }
    }
    

    private void Move()
    {
        rigidbody2D.velocity = transform.right * speed;
    }
    public void ReturnToPull()
    {
        parentGun.bulletPull.PushToPull(this);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Debug.Log("BulletSpawn!");
        timer = bulletLifeTime;
    }
}


