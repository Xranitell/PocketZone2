using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image fade;
    [SerializeField] private float animationDuration;

    private void Awake()
    {
        fade.color = Color.black;
        fade.DOFade(0, animationDuration);
    }

    public void StartNewGame()
    {
        var content = File.ReadAllText("Assets/SaveData/StartInventory.json");
        File.WriteAllText("Assets/SaveData/Inventory.json",content);
        LoadExistGame();
    }

    public void LoadExistGame()
    {
        DOTween.Sequence()
            .Append(fade.DOFade(1, animationDuration))
            .AppendCallback(()=>SceneManager.LoadScene("SampleScene"));
    }

    public void QuitGame()
    {
        DOTween.Sequence()
            .Append(fade.DOFade(1, animationDuration))
            .AppendCallback(()=>Application.Quit());
    }
}
