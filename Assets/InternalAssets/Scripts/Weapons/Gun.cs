using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public ObjectPull<Bullet> bulletPull = new ObjectPull<Bullet>();

    [SerializeField] private float delayBetweenShots = 0.25f;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Ammo ammoType;

    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Transform bulletContainer;

    private float timer;

    public override void Attack()
    {
        if (timer <= 0)
        {
            GenerateBullet();
            timer = delayBetweenShots;
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    
    private void GenerateBullet()
    {
        var bullet = bulletPull.Pull.Count > 0 ? bulletPull.GetFromPull() : Instantiate(bulletPrefab,bulletSpawnPoint.position,bulletSpawnPoint.rotation ,bulletContainer);
        
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        
        bullet.parentGun = this;
        Debug.Log("BulletActivate");
        bullet.gameObject.SetActive(true);
    }
    
}
