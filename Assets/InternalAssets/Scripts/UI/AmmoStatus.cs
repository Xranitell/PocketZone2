using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoStatus : MonoBehaviour
{
    private WeaponManager _weaponManager;
    [Header("Ammo status")]
    [SerializeField] private Image ammoImage;
    [SerializeField] private TMP_Text ammoCount;

    private Ammo _currentAmmo;
    private Ammo _cachedAmmo;
    private void Awake()
    {
        _weaponManager = FindObjectOfType<WeaponManager>();
        _weaponManager.OnCurrentWeaponChanged += UpdateAmmoStaus;
    }

    private void UpdateAmmoStaus(Weapon weapon)
    {
        if (weapon.GetType() == typeof(Gun))
        {
            gameObject.SetActive(true);
            _currentAmmo = (weapon as Gun)?.ammoType;
            ammoImage.sprite = _currentAmmo.sprite;
            
            UpdateBullet();
            
            _currentAmmo.OnLocalItemUpdated += UpdateBullet;
            
            if(_cachedAmmo) _cachedAmmo.OnLocalItemUpdated -= UpdateBullet;
            
            _cachedAmmo = _currentAmmo;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void UpdateBullet()
    {
        ammoCount.text = (_currentAmmo.CurrentCount + "/" + _currentAmmo.maxCount);
    }
}
