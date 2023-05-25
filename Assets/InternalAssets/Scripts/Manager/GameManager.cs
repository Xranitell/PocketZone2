using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;private set; }

    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        LoadProgress();
    }

    public void LoadProgress()
    {
        var loadedData = SaveAndLoadSystem.Instance.LoadData();
        SaveAndLoadSystem.ApplySaveData(loadedData);
    }
    
    public void SaveProgress()
    {
        SaveAndLoadSystem.Instance.SaveGame(false);
    }
    
}
