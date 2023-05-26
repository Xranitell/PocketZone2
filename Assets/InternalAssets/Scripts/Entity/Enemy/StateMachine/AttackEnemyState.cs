using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AttackEnemyState : EnemyState
{
    private float _timer;

    public override void Init()
    {
        character.animator.SetTrigger("Attack");
        IsFinished = false;
        base.Init();
    }

    public override void Run()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = character.delayAttack;
            IsFinished = true;
        }
    }
}
