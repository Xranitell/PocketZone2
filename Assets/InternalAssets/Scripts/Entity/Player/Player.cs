using UnityEngine;
using UnityEngine.Events;

public class Player: Entity,IWatcher
{
    [SerializeField] private float seeDistance;
    public float SeeDistance => seeDistance;
    public static Player Instance;
    public Animator animator;

    protected void Awake()
    {
        Instance = this;
    }

}