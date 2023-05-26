using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    public UnityAction<Weapon> OnCurrentWeaponChanged;
    [SerializeField] private SearchTargets searchTargets;

    public List<Weapon> weapons = new List<Weapon>();
    
    private Weapon _currentWeapon;
    public Weapon CurrentWeapon
    {
        get => _currentWeapon;
        set
        {
            _currentWeapon = value;
            OnCurrentWeaponChanged.Invoke(_currentWeapon);
        }
    }
    public static bool ShootModeEnabled { get; private set; }
    
    private int _counter;

    private void Start()
    {
        CurrentWeapon = weapons[0];
    }

    //активирует режим нацеливания и стрельбы
    public void EnableShootMode(bool state)
    {
        ShootModeEnabled = state;
    }

    public void SetNextWeapon()
    {
        _counter++;
        CurrentWeapon.gameObject.SetActive(false);
        CurrentWeapon = weapons[_counter % weapons.Count];
        CurrentWeapon.gameObject.SetActive(true);
    }
    
    private void Update()
    {
        WeaponRotate();

        if (ShootModeEnabled)
        {
            CurrentWeapon.Attack(); //вызов переопределенного абстрактного метода класса Weapon
        }
    }

    private void WeaponRotate()
    {
        if (searchTargets.currentTarget && ShootModeEnabled)
        {
            var diff = searchTargets.GetDifferenceToTarget();
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            Vector3 scale = CurrentWeapon.transform.localScale;
            
            if (rotZ > 90 || rotZ < -90)
            {
                scale.y = MathF.Abs(scale.y) * -1;
                transform.localScale = scale;
            }
            else
            {
                scale.y = MathF.Abs(scale.y);
            }
            CurrentWeapon.transform.localScale = scale;
            CurrentWeapon.transform.rotation = Quaternion.Euler(0, 0, rotZ);
            
        }
    }
}

