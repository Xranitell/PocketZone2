using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float speed = 1f;
    public int damage = 3;
    public float bulletLifeTime = 2f;
    private float _timer;

    public Gun parentGun;
    
    public string ObjectName { get; set; }
    private void Update()
    {
        Move();
        DeathTimer();
    }

    private void DeathTimer()
    {
        _timer -= Time.deltaTime;
        
        if (_timer <= 0)
        {
            ReturnToPull();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            ReturnToPull();
            other.gameObject.GetComponent<Entity>().GetDamage(damage);
        }
    }
    

    private void Move()
    {
        rb.velocity = transform.right * speed;
    }
    public void ReturnToPull()
    {
        parentGun.bulletPull.PushToPull(this);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _timer = bulletLifeTime;
    }

    
}


