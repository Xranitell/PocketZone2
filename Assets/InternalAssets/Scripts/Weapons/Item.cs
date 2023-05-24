using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

public delegate void ItemUpdate();

[CreateAssetMenu(menuName = "Items/SimpleItem")]
[Serializable]
public class Item : ScriptableObject, IDroppable
{

    public event ItemUpdate OnItemDataUpdate;
    
    public string name;
    public string description;
    [JsonIgnore]public Sprite sprite;
    
    private int _currentCount;
    [ShowNativeProperty] public int CurrentCount
    {
        get => _currentCount;
        set
        {
            _currentCount = value > maxCount ? maxCount : value;
            OnItemDataUpdate?.Invoke();
        }
    }

    public bool EnableInInventory => _currentCount > 0 ? true : false;
    [JsonIgnore] public DropItem dropItemPrefab;

    [SerializeField] [Range(0,1)] protected float dropChance = 1f;
    
    public bool isStackable = true;
    public int maxCount = 1;
    public int dropCount = 1;
    
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
    
    
