using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour, IDamaged
{
    public string name;
    public float maxHealth;
    public float currentHealth;

    private UnityAction OnDamaged;
    private UnityAction OnDead;
    
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        OnDamaged?.Invoke();

        if (currentHealth <= 0)
        {
            OnDead?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
