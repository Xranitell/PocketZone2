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
    public float currentHealth;

    private UnityAction OnDamaged;
    protected UnityAction OnDead;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void GetDamage(int damage)
    {
        currentHealth -= damage;
        OnDamaged?.Invoke();

        if (currentHealth <= 0)
        {
            OnDead?.Invoke();
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
    }
}
