using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public abstract class Entity : MonoBehaviour, IDamaged
{
    public float maxHealth;
    
    [ShowNonSerializedField]private float _currentHealth;
    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = CurrentHealth > maxHealth ? maxHealth : value;
            
            OnCurrentHealthChanged?.Invoke();
            
            if (CurrentHealth <= 0)
            {
                OnDead?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }

    public UnityAction OnCurrentHealthChanged;
    public UnityAction OnDead;

    public virtual void GetDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    protected virtual void OnEnable()
    {
        CurrentHealth = maxHealth;
    }
}
