using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FollowEnemyState : EnemyState
{
    public Transform target;
    public override void Init()
    {
        IsFinished = false;
        target = character.searchTargets.currentTarget.transform;
    }

    public override void Run()
    {
        if(IsFinished)
            return;
        MoveToTarget();
    }

    void MoveToTarget()
    {
        var targetPos = target.position;
        var distance = (targetPos - character.transform.position).magnitude;
        if (distance < character.stopDistance || distance > character.SeeDistance)
        {
            IsFinished = true;
        }
        else
        {
            character.MoveTo(targetPos);
        }
    }
}
