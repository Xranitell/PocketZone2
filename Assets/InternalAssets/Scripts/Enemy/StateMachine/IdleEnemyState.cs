using System;


[Serializable]
public class IdleEnemyState : EnemyState
{
    public override void Init()
    {
        base.Init();
        isFinished = true;
    }

    public override void Run()
    {
        base.Run();
    }
}
