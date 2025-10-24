using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;
using System.Linq;
using TMPro;

public class LevelEditorUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Toggle obstacleToggle;
    [SerializeField] private Toggle spawnerToggle;
    [SerializeField] private Toggle blockToggle;
    [SerializeField] private Toggle wallToggle;  
    [SerializeField] private Toggle gateToggle;   
    [SerializeField] private Toggle binToggle;   
    [SerializeField] private Toggle deleteToggle;
    [SerializeField] private Toggle defaultToggle;

    [SerializeField] private Button clearButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button generateButton; 
    [SerializeField] private Button clearItem; 

    [Header("Level Settings Inputs")]
    [SerializeField] private TMP_InputField widthInput;
    [SerializeField] private TMP_InputField heightInput;
    [SerializeField] private TMP_InputField timeInput;
    [SerializeField] private TMP_InputField sizeCamInput;
    [SerializeField] private TMP_InputField levelInput;
    [SerializeField] private TMP_InputField spawnCountBall;

    [Header("Block Settings")]
    [SerializeField] private TMP_Dropdown shapeDropdown;
    [SerializeField] private TMP_Dropdown colorDropdown;
    [SerializeField] private TMP_Dropdown rotationDropdown;

    [Header("References")]
    [SerializeField] private LevelEditorManager editorManager;

    private void Start()
    {
        // --- Toggle modes ---
        obstacleToggle.onValueChanged.AddListener(isOn => { if (isOn) SetMode(EditorMode.Obstacle); });
        spawnerToggle.onValueChanged.AddListener(isOn => { if (isOn) SetMode(EditorMode.BallSpawner); });
        blockToggle.onValueChanged.AddListener(isOn => { if (isOn) SetMode(EditorMode.Block); });
        wallToggle.onValueChanged.AddListener(isOn => { if (isOn) SetMode(EditorMode.Wall); });   
        gateToggle.onValueChanged.AddListener(isOn => { if (isOn) SetMode(EditorMode.Gate); });  
        deleteToggle.onValueChanged.AddListener(isOn => { if (isOn) SetMode(EditorMode.Delete); });
        defaultToggle.onValueChanged.AddListener(isOn => { if (isOn) SetMode(EditorMode.None); });
        binToggle.onValueChanged.AddListener(isOn => { if (isOn) SetMode(EditorMode.Bin); });

        // --- Buttons ---
        clearButton.onClick.AddListener(OnClearClicked);
        saveButton.onClick.AddListener(OnSaveClicked);
        loadButton.onClick.AddListener(OnLoadClicked);
        generateButton.onClick.AddListener(OnGenerateClicked);
        clearItem.onClick.AddListener(editorManager.ClearItem);

        // --- Input Fields ---
        widthInput.text = editorManager.levelWidth.ToString();
        heightInput.text = editorManager.levelHeight.ToString();
        spawnCountBall.text = editorManager.countSpawnBall.ToString();
        timeInput.text = editorManager.levelTime.ToString();
        sizeCamInput.text = editorManager._cam.fieldOfView.ToString("F1");
        levelInput.text = "0";
        spawnCountBall.text = "0";

        widthInput.onEndEdit.AddListener(val => UpdateInt(ref editorManager.levelWidth, val, 1, 50));
        heightInput.onEndEdit.AddListener(val => UpdateInt(ref editorManager.levelHeight, val, 1, 50));
        spawnCountBall.onEndEdit.AddListener(val => UpdateInt(ref editorManager.countSpawnBall, val, 1, 50));
        timeInput.onEndEdit.AddListener(val => UpdateFloat(ref editorManager.levelTime, val, 5, 600));
        sizeCamInput.onEndEdit.AddListener(OnCameraSizeChanged);

        // --- Dropdowns ---
        SetupDropdown(shapeDropdown, typeof(BlockShape), editorManager.currentShape);
        SetupDropdown(colorDropdown, typeof(ColorBlock), editorManager.currentColor);
        SetupDropdown(rotationDropdown, typeof(Rotation), editorManager.currentRotation);

        shapeDropdown.onValueChanged.AddListener(i => editorManager.currentShape = (BlockShape)i);
        colorDropdown.onValueChanged.AddListener(i => editorManager.currentColor = (ColorBlock)i);
        rotationDropdown.onValueChanged.AddListener(i => editorManager.currentRotation = (Rotation)i);
    }

    private bool isClear = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isClear = !isClear;
            deleteToggle.isOn = isClear;
            blockToggle.isOn = !isClear;
        }
    }

    private void SetupDropdown(TMP_Dropdown dropdown, Type enumType, Enum selected)
    {
        dropdown.ClearOptions();
        var names = Enum.GetNames(enumType).ToList();
        dropdown.AddOptions(names);
        dropdown.value = Array.IndexOf(Enum.GetValues(enumType), selected);
        dropdown.RefreshShownValue();
    }

    private void UpdateInt(ref int target, string input, int min, int max)
    {
        if (int.TryParse(input, out int val))
        {
            target = Mathf.Clamp(val, min, max);
        }
    }

    private void UpdateFloat(ref float target, string input, float min, float max)
    {
        if (float.TryParse(input, out float val))
        {
            target = Mathf.Clamp(val, min, max);
        }
    }

    private void OnCameraSizeChanged(string input)
    {
        if (float.TryParse(input, out float val))
        {
            editorManager._cam.fieldOfView = Mathf.Clamp(val, 10f, 120f);
        }
    }

    private void SetMode(EditorMode mode)
    {
        // Tắt tất cả toggle khác
        obstacleToggle.isOn = (mode == EditorMode.Obstacle);
        spawnerToggle.isOn = (mode == EditorMode.BallSpawner);
        blockToggle.isOn = (mode == EditorMode.Block);
        wallToggle.isOn = (mode == EditorMode.Wall);
        gateToggle.isOn = (mode == EditorMode.Gate);
        deleteToggle.isOn = (mode == EditorMode.Delete);

        editorManager.mode = mode;
        Debug.Log("🧭 Editor mode: " + mode);
    }

    private void OnClearClicked()
    {
        editorManager.ClearAll();
    }

    private void OnGenerateClicked()
    {
        editorManager.GenerateGrid();
        Debug.Log("🧱 Grid generated!");
    }

    private void OnSaveClicked()
    {
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Levels/");
        string fileName = "Level " +textAssets.Length;
        if (string.IsNullOrEmpty(fileName))
        {
          Debug.LogError("⚠️ Thiếu tên file, Vui lòng nhập tên file để lưu level!");
            return;
        }

        editorManager.SaveLevel(fileName);
    }

    private void OnLoadClicked()
    {
        string fileName = "Level " + levelInput.text;
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("⚠️ Thiếu tên file, Vui lòng nhập tên file để tải level!");
            return;
        }

        editorManager.LoadLevel(fileName);

        widthInput.text = editorManager.levelWidth.ToString();
        heightInput.text = editorManager.levelHeight.ToString();
        timeInput.text = editorManager.levelTime.ToString();
        sizeCamInput.text = editorManager._cam.fieldOfView.ToString("F1");
    }
}
