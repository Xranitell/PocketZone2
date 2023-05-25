using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


[CreateAssetMenu(menuName = "Items/SimpleItem")]
[Serializable]
public class Item : ScriptableObject, IDroppable
{
    public UnityAction OnLocalItemUpdated;
    public string description;
    public Sprite sprite;
    public bool EnableInInventory => _currentCount > 0 ? true : false;
    public DropItem dropItemPrefab;

    [SerializeField] [Range(0,1)] protected float dropChance = 1f;
    
    public bool isStackable = true;
    public int maxCount = 1;
    public int dropCount = 1;
    public float DropChance => dropChance;
    public float DropOffset { get; set; } = 0.2f;

    [ShowNativeProperty] public int CurrentCount
    {
        get => _currentCount;
        set
        {
            int previousValue = _currentCount;
            _currentCount = value > maxCount ? maxCount : value;
            
            //If this item was new or deleted, call global update
            //else if change only current count? call local event
            if (previousValue == 0 || CurrentCount == 0)
            {
                InventorySystem.Instance.OnGlobalItemUpdated?.Invoke();
            }
            else
            {
                OnLocalItemUpdated?.Invoke();
            }
            
        }
    }
    
    private int _currentCount;
    
    public DropItem CreateDropItem(Vector3 position)
    {
        var item = DropItem.DropPull.Count > 0 ? 
            DropItem.DropPull.GetFromPull() : 
            Instantiate(dropItemPrefab, position, dropItemPrefab.transform.rotation);

        item.itemData = this;
        Vector2 offset = new Vector2(Random.Range(-DropOffset, DropOffset), Random.Range(-DropOffset, DropOffset));
        
        item.transform.position = position + (Vector3)offset;
        item.Configure();
        return item;
    }
}
    
    
