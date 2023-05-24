using UnityEngine;

public class Player: Entity,IWatcher
{
    [SerializeField] private float seeDistance;
    public float SeeDistance => seeDistance;
}