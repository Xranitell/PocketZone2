using UnityEngine;

public interface IDroppable
{
    public float DropChance { get; }
    public float DropOffset { get; set; }

    public DropItem CreateDropItem(Vector3 position);
}