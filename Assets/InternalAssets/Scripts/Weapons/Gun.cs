using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Gun : Weapon
{
    public ObjectPull<Bullet> bulletPull = new ObjectPull<Bullet>();

    [SerializeField] private float delayBetweenShots = 0.25f;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float shakeIntensity = 0.05f;
    public Ammo ammoType;
    [SerializeField] private int bulletsPerShot = 1;

    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Transform bulletContainer;

    private float timer;

    
    public override void Attack()
    {
        if (timer <= 0)
        {
            if (ammoType.CurrentCount - bulletsPerShot > 0)
            {
                ammoType.CurrentCount -= bulletsPerShot;
                GenerateBullet();
                timer = delayBetweenShots;
            }
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    
    private void GenerateBullet()
    {
        DataHolder.Camera.DOShakePosition(0.02f, shakeIntensity);
        var bullet = bulletPull.Pull.Count > 0 ? bulletPull.GetFromPull() : Instantiate(bulletPrefab,bulletSpawnPoint.position,bulletSpawnPoint.rotation ,bulletContainer);
        
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        
        bullet.parentGun = this;
        bullet.gameObject.SetActive(true);
    }
    
}
