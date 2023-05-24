using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class EnemyState
{
    public bool isFinished { get; protected set; } = false;
    public Enemy character;
    public virtual void Init(){}
    public virtual void Run(){}
}
