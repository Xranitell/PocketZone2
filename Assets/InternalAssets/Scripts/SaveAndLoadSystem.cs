using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

public class SaveAndLoadSystem: MonoBehaviour
{
    public static SaveAndLoadSystem Instance { get; set; }
    public SaveData defaultSaveData;

    public static SaveData Data4Save
    {
        get
        {
            List<SerializableItem> items = new List<SerializableItem>();
            var player =Player.Instance;

            foreach (var item in InventorySystem.Instance.allItemsList)
            {
                items.Add(new SerializableItem(item,item.CurrentCount));
            }

            return new SaveData(items, player.currentHealth, player.transform.position);
        }
    }
    private string dataFilePath;
    

    private void Awake()
    {
        Instance = this;
    }

    private static string savePath
    {
        get => Application.persistentDataPath + "/save.json";
    }

    public void SaveGame(bool useDefaultData)
    {
        SaveData save = useDefaultData? defaultSaveData: Data4Save;

        FileOptions options = new FileOptions();
        
        
        var file = File.Create(savePath);
        file.Close();
        
        var json = JsonConvert.SerializeObject(save, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        
        File.WriteAllText(savePath, json);
    }

    public SaveData LoadData()
    {
        if (File.Exists(savePath))
        {
            var json = File.ReadAllText(savePath);
            SaveData save = JsonConvert.DeserializeObject<SaveData>(json);
            if(save != null)
                return save;
        }
        
        return defaultSaveData;
    }


    //Apply saved data
    public static void ApplySaveData(SaveData saveData)
    {
        Player.Instance.currentHealth = saveData.health;
        Player.Instance.transform.position = saveData.lastPosition;
        
        foreach (var item in InventorySystem.Instance.allItemsList)
        {
            var savedItem = saveData.Inventory.Find(x => x.Name == item.name);
            
            item.CurrentCount = savedItem?.count ?? 0;
        }
        InventorySystem.Instance.UpdateInventory();
    }
}
[Serializable]
public class SaveData
{
    public List<SerializableItem> Inventory;
    public float health;
    public Vector2 lastPosition;

    public SaveData(List<SerializableItem> inventory, float health,Vector2 lastPosition)
    {
        this.Inventory = inventory;
        this.health = health;
        this.lastPosition = lastPosition;
    }
}

[Serializable]
public class SerializableItem
{
    private string _name;
    [JsonProperty]public string Name
    {
        get
        {
            if(item) 
                _name = item.name;
            return _name;
        }
        set => _name = value;
    }

    public int count;

    [JsonIgnore] public Item item;

    public SerializableItem(Item item, int count)
    {
        this.count = count;
        if (item)
            Name = item.name;
    }
}