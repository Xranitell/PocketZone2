using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AttackEnemyState : EnemyState
{
    public override void Init()
    {
        isFinished = true;
        base.Init();
    }

    public override void Run()
    {
        base.Run();
    }
}
