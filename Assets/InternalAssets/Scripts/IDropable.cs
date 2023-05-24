using UnityEngine;

public interface IDroppable
{
    public float DropChance { get; }

    public DropItem CreateDropItem(Vector3 position);
}