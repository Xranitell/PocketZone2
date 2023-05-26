using UnityEngine;
using UnityEngine.Events;

public class Player: Entity,IWatcher
{
    [SerializeField] private float seeDistance;
    public float SeeDistance => seeDistance;
    public static Player Instance;

    protected void Awake()
    {
        Instance = this;
        OnCurrentHealthChanged += OnShot;
    }

    void OnShot()
    {
        Debug.Log("GetDamage");
    }
    
}