using UnityEngine;

public interface IDroppable
{
    public float DropChance { get; }
    public float DropOffset { get; set; }

    public void CreateDropItem(Vector3 position);
}