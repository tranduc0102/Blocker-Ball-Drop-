using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider _slider;
    public float _timeLoading;

    private void Start()
    {
        _slider.DOValue(1f, _timeLoading).OnComplete(delegate
        {
            SceneManager.LoadSceneAsync("game");
        });
    }
}
