using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AttackEnemyState : EnemyState
{
    private float timer;
    private IDamaged target;
    
    public override void Init()
    {
        Debug.Log( "Damage!");
        isFinished = false;
        base.Init();
        target= character.searchTargets.currentTarget.GetComponent<IDamaged>();
        
        target.GetDamage(character.damageAttack);
    }

    public override void Run()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = character.delayAttack;
            isFinished = true;
        }
    }
}
