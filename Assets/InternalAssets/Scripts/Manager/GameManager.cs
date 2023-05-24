using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        var loadedData = SaveAndLoadSystem.LoadData<List<Item>>("Assets/SaveData/Inventory.json");
        LoadSavedInventory(loadedData, InventorySystem.Instance.allItemsList);
    }

    private void LoadSavedInventory(List<Item> loadedData, List<Item> originalItems)
    {
        foreach (var item in originalItems)
        {
            var it = loadedData.Find(x => x.name == item.name);
            if (it != null) item.CurrentCount = it.CurrentCount;
        }
        InventorySystem.Instance.LoadInventory();
    }
    
    public void SaveInventory()
    {
        SaveAndLoadSystem.SaveData("Assets/SaveData/Inventory.json", InventorySystem.Instance.allItemsList);
    }
    [Button("Save Start pack")]
    public void SaveStartInventory()
    {
        SaveAndLoadSystem.SaveData("Assets/SaveData/StartInventory.json", InventorySystem.Instance.allItemsList);
    }
}
