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

    public void StartNewGame()
    {
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
            .AppendCallback(()=> GameManager.Instance.SaveProgress())
            .Append(fade.DOFade(1, animationDuration))
            .AppendCallback(()=>Application.Quit());
    }
}
