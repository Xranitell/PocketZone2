using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;private set; }
    [SerializeField] private UnityEvent eventsAfterDeath;
    private void Awake()
    {
        Instance = this;
    }
    private void EndGame() => eventsAfterDeath.Invoke();

    private void Start()
    {
        Player.Instance.OnDead += EndGame;
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
