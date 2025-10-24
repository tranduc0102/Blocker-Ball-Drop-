using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    
    public LevelEditorManager _LevelEditor;
    public int level;

    [Header("UI Setting")] [SerializeField]
    private TextMeshPro _textTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        _LevelEditor.LoadLevel("Level " + level.ToString());
        foreach (var obj in _LevelEditor.GetBallSpawners())
        {
            obj.SpawnBalls();
        }
    }

    public void Win()
    {
        Debug.LogError("WIN");
    }

    public void Lose()
    {
        
    }
}
