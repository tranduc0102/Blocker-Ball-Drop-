using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    public enum GameState { Waiting, Playing, Win, Lose }
    public GameState State { get; set; }

    [Header("Level Setting")]
    public LevelEditorManager _LevelEditor;
    private int currentLevel;
    private int displayLevel;

    private const int MinLevel = 0;
    private const int MaxLevel = 19;
    private const int LoopStart = 9;
    private const int LoopEnd = 19;

    [Header("Timer Setting")]
    [SerializeField] private float maxTime = 60f;
    private float currentTime;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        displayLevel = PlayerPrefs.GetInt("DisplayLevel", 1);
    }

    private void Start()
    {
        SetState(GameState.Waiting);
        LoadLevel();
    }

    private void Update()
    {
        if (State == GameState.Playing)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                Lose();
            } 
            UIController.Instance.UpdateTime(currentTime);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            playeLevle10();
        }
    }

    private void SetState(GameState newState)
    {
        State = newState;
    }
    public void playeLevle10()
    {
        SetState(GameState.Waiting);
        _LevelEditor.ClearAll();
        _LevelEditor.LoadLevel("Level " + 9);
        maxTime = _LevelEditor.levelTime;
        currentTime = maxTime;
        UIController.Instance.UpdateViewLevel(displayLevel, currentTime);

        foreach (var obj in _LevelEditor.GetBallSpawners())
            obj.SpawnBalls();
    }
    private void LoadLevel()
    {
        SetState(GameState.Waiting);
        _LevelEditor.ClearAll();
        _LevelEditor.LoadLevel("Level " + currentLevel);
        maxTime = _LevelEditor.levelTime;
        currentTime = maxTime;
        UIController.Instance.UpdateViewLevel(displayLevel, currentTime);

        foreach (var obj in _LevelEditor.GetBallSpawners())
            obj.SpawnBalls();
      
    }
    public void Win()
    {
        if (State != GameState.Playing) return;
        SetState(GameState.Win);

        AudioManager.Instance.PlayWin(); 
        UIController.Instance.ShowResult(true, true);

        displayLevel++;

        currentLevel++;
        if (currentLevel > MaxLevel)
            currentLevel = LoopStart + (currentLevel - (MaxLevel + 1)) % (LoopEnd - LoopStart + 1);

        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("DisplayLevel", displayLevel);
        PlayerPrefs.Save();
    }

    public void Lose()
    {
        if (State != GameState.Playing) return;
        SetState(GameState.Lose);

        AudioManager.Instance.PlayLose();
        UIController.Instance.ShowResult(false, true);

    }

    public void ReplayLevel()
    {
        DOTween.KillAll();
        StopAllCoroutines();
        LoadLevel();
    }

    public void NextLevel()
    {
        DOTween.KillAll();
        StopAllCoroutines();
        LoadLevel();
    }
    [ContextMenu("Clear PlayerPrefs")]
    private void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
