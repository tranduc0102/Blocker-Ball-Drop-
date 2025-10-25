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
    private const int LoopStart = 10;
    private const int LoopEnd = 19;

    [Header("Timer Setting")]
    [SerializeField] private float maxTime = 60f;
    private float currentTime;

    [Header("UI Setting")]
    [SerializeField] private TextMeshProUGUI _textTime;
    [SerializeField] private TextMeshProUGUI _textLevel;
    [SerializeField] private CanvasGroup _win;
    [SerializeField] private CanvasGroup _lose;

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

            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            _textTime.text = $"{minutes:0}:{seconds:00}";
        }
    }

    private void SetState(GameState newState)
    {
        State = newState;
    }

    private void LoadLevel()
    {
        SetState(GameState.Waiting);
        _LevelEditor.ClearAll();
        _win.gameObject.SetActive(false);
        _lose.gameObject.SetActive(false);
        _win.alpha = 0;
        _lose.alpha = 0;
      

        _textLevel.text = "Level " + displayLevel;
        _LevelEditor.LoadLevel("Level " + currentLevel);
        maxTime = _LevelEditor.levelTime;
        currentTime = maxTime;
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        _textTime.text = $"{minutes:0}:{seconds:00}";

        foreach (var obj in _LevelEditor.GetBallSpawners())
            obj.SpawnBalls();
      
    }
    public void Win()
    {
        if (State != GameState.Playing) return;
        SetState(GameState.Win);

        AudioManager.Instance.PlayWin();
        _win.gameObject.SetActive(true);
        _win.DOFade(1, 0.5f).SetEase(Ease.OutQuad);

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
        _lose.gameObject.SetActive(true);
        _lose.DOFade(1, 0.5f).SetEase(Ease.OutQuad);
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
