using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/SimpleItem")]
public class Item : ScriptableObject, IDroppable
{
    public delegate void ItemUpdate(Item item);

    public event ItemUpdate OnItemDataUpdate;
    
    public string name;
    public string description;
    public Sprite sprite;


    private int _currentCount;
    public int CurrentCount
    {
        get => _currentCount;
        set
        {
            if (value > maxCount)
            {
                _currentCount = maxCount;
            }
            else
            {
                _currentCount = value;
            }
            OnItemDataUpdate?.Invoke(this);
        }
    }

    public bool EnableInInventory => _currentCount > 0 ? true : false;
    public DropItem dropItemPrefab;

    [SerializeField] [Range(0,1)]protected float dropChance = 1f;
    
    public bool isStackable = true;
    [EnableIf("isStackable")] public int maxCount = 1;
    [EnableIf("isStackable")] public int dropCount = 1;
    public float DropChance => dropChance;
    
    public DropItem CreateDropItem(Vector3 position)
    {
        var item = DropItem.DropPull.Count > 0 ? 
            DropItem.DropPull.GetFromPull() : 
            Instantiate(dropItemPrefab, position, dropItemPrefab.transform.rotation);

        item.itemData = this;
        item.transform.position = position;
        item.Configure();
        return item;
    }
}
    
    
