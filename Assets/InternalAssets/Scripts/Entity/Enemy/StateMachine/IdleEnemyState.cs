using System;


[Serializable]
public class IdleEnemyState : EnemyState
{
    public override void Init()
    {
        base.Init();
        IsFinished = true;
    }
}
