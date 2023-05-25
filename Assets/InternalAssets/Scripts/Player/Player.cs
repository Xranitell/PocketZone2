using UnityEngine;

public class Player: Entity,IWatcher
{
    [SerializeField] private float seeDistance;
    public float SeeDistance => seeDistance;
    public static Player Instance;
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}