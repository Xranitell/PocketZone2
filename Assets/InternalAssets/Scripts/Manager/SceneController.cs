using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Image fade;
    [SerializeField] private float animationDuration;

    private void Awake()
    {
        fade.color = Color.black;
        fade.DOFade(0, animationDuration);
    }

    public void SaveGame() => SaveAndLoadSystem.Instance.SaveGame(false);
    public void ResetToDefault() => SaveAndLoadSystem.Instance.SaveGame(true);

    
    public void LoadGame()
    {
        //Start fade animation and load scene
        DOTween.Sequence()
            .Append(fade.DOFade(1, animationDuration))
            .AppendCallback(()=>SceneManager.LoadScene("SampleScene"));
    }

    public void QuitGame()
    {
        //Start fade animation and quit game
        DOTween.Sequence()
            .Append(fade.DOFade(1, animationDuration))
            .AppendCallback(()=>Application.Quit());
    }
}
