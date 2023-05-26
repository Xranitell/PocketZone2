using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    [SerializeField] private SearchTargets searchTargets;

    public List<Weapon> weapons = new List<Weapon>();


    public UnityAction<Weapon> OnCurrentWeaponChanged;
        
    private Weapon _currentWeapon;
    public Weapon currentWeapon
    {
        get => _currentWeapon;
        set
        {
            _currentWeapon = value;
            OnCurrentWeaponChanged.Invoke(_currentWeapon);
        }
    }

    
    
    public static bool ShootModeEnabled { get; protected set; }
    
    private int counter;

    private void Start()
    {
        currentWeapon = weapons[0];
    }

    //активирует режим нацеливания и стрельбы
    public void EnableShootMode(bool state)
    {
        ShootModeEnabled = state;
    }

    public void SetNextWeapon()
    {
        counter++;
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = weapons[counter % weapons.Count];
        currentWeapon.gameObject.SetActive(true);
    }
    
    private void Update()
    {
        WeaponRotate();

        if (ShootModeEnabled)
        {
            currentWeapon.Attack(); //вызов переопределенного абстрактного метода класса Weapon
        }
    }

    private void WeaponRotate()
    {
        if (searchTargets.currentTarget && ShootModeEnabled)
        {
            var diff = searchTargets.GetDifferenceToTarget();
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            Vector3 scale = currentWeapon.transform.localScale;
            
            if (rotZ > 90 || rotZ < -90)
            {
                scale.y = MathF.Abs(scale.y) * -1;
                transform.localScale = scale;
            }
            else
            {
                scale.y = MathF.Abs(scale.y);
            }
            currentWeapon.transform.localScale = scale;
            currentWeapon.transform.rotation = Quaternion.Euler(0, 0, rotZ);
            
        }
    }
}

