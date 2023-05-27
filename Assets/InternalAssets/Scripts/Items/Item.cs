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
public class Item : ScriptableObject
{
    public static int MaxCountOfDrops = 10;
    public UnityAction OnLocalItemUpdated;

    [BoxGroup ("Item info")] public string itemName;
    [BoxGroup ("Item info")][ResizableTextArea] public string description;
    [ShowAssetPreview(128, 128)][BoxGroup ("Item info")] public Sprite sprite; 
    public bool EnableInInventory => _currentCount > 0 ? true : false;
    [BoxGroup ("Drop settings")] public DropItem dropItemPrefab;

    [BoxGroup ("Drop settings")][SerializeField] [Range(0,1)] protected float dropChance = 1f;
    
    [OnValueChanged("IsStackableChanged")][BoxGroup ("Counter")]public bool isStackable = true;
    private void IsStackableChanged() => maxCount = 1;
    
    [BoxGroup ("Counter")][EnableIf("isStackable")]public int maxCount = 1;
    [BoxGroup ("Drop settings")][EnableIf("isStackable")]public int dropCount = 1;
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
    
    public void CreateDropItem(Vector3 position)
    {
        if (DataHolder.ActiveDrop.Count >= MaxCountOfDrops) return;
        
        var item = DataHolder.DropPull.Count > 0 ? 
            DataHolder.DropPull.GetFromPull() : 
            Instantiate(dropItemPrefab, position, dropItemPrefab.transform.rotation);

        item.itemData = this;
        Vector2 offset = new Vector2(Random.Range(-DropOffset, DropOffset), Random.Range(-DropOffset, DropOffset));
        
        item.transform.position = position + (Vector3)offset;
        item.Configure();
    }
}
    
    
