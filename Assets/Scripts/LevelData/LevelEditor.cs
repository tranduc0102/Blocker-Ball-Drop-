using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public enum EditorMode
{
    None,
    Obstacle,
    BallSpawner,
    Block,
    Delete
}

public class LevelEditorManager : MonoBehaviour
{
    [Header("⚙️ Level Settings")]
    [SerializeField, Min(1)] public int levelWidth = 8;
    [SerializeField, Min(1)] public int levelHeight = 8;
    [SerializeField, Min(5f)] public float levelTime = 60f;

    [Header("📦 References")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private List<Block> blockPrefabs;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject ballSpawnerPrefab;

    [Header("🎨 Current Editor State")]
    [SerializeField] public EditorMode mode = EditorMode.None;
    [SerializeField] public BlockShape currentShape = BlockShape.Single;
    [SerializeField] public ColorBlock currentColor = ColorBlock.Red;
    [SerializeField] public Rotation currentRotation = Rotation.Angle45;

    private Block previewBlock;
    private Dictionary<Vector2Int, GameObject> obstacleDict = new();
    private Dictionary<Vector2Int, GameObject> spawnerDict = new();
    private HashSet<Vector2Int> hiddenCells = new();

    private string savePath = "Assets/Resources/Levels/";

    // ================================================================
    // ====================== UNITY LOOP ==============================
    // ================================================================

    private void Update()
    {
        if (mode == EditorMode.Block && previewBlock != null)
        {
            HandleBlockEditingInput();
        }

        if (Input.GetMouseButtonDown(0))
            HandleCellClick();
    }

    // ================================================================
    // ======================= GRID SETUP =============================
    // ================================================================

    public void GenerateGrid()
    {
        if (gridManager == null)
        {
            Debug.LogError("❌ GridManager chưa được gán!");
            return;
        }

        gridManager.GenerateGrid(levelWidth, levelHeight);
    }

    // ================================================================
    // ======================== INPUT LOGIC ===========================
    // ================================================================

    private void HandleCellClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Cell cell = hit.collider.GetComponent<Cell>();
            if (cell == null) return;
            Vector2Int index = cell.Index;

            switch (mode)
            {
                case EditorMode.Obstacle:
                    ToggleObstacle(index);
                    break;
                case EditorMode.BallSpawner:
                    ToggleSpawner(index);
                    break;
                case EditorMode.Block:
                    PlaceBlock(index);
                    break;
                case EditorMode.Delete:
                    ToggleCellHidden(index);
                    break;
            }
        }
    }

    private void HandleBlockEditingInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentRotation = (Rotation)(((int)currentRotation + 1) % 4);
            previewBlock.Rotation = currentRotation;
            previewBlock.ApplyRotation();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) currentShape = BlockShape.Line3;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentShape = BlockShape.T;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentShape = BlockShape.Square4;
        if (Input.GetKeyDown(KeyCode.Alpha4)) currentShape = BlockShape.L3;

        if (Input.GetKeyDown(KeyCode.C))
        {
            currentColor = (ColorBlock)(((int)currentColor + 1) % System.Enum.GetValues(typeof(ColorBlock)).Length);
            previewBlock._Color = currentColor;
        }
    }

    // ================================================================
    // ======================= CELL TOGGLES ===========================
    // ================================================================

    private void ToggleObstacle(Vector2Int index)
    {
        if (obstacleDict.ContainsKey(index))
        {
            DestroyImmediate(obstacleDict[index]);
            obstacleDict.Remove(index);
        }
        else
        {
            Vector2 pos = gridManager.GetCellWorldPosition(index.x, index.y);
            var obj = Instantiate(obstaclePrefab, pos, Quaternion.identity, transform);
            obstacleDict[index] = obj;
        }
    }

    private void ToggleSpawner(Vector2Int index)
    {
        if (spawnerDict.ContainsKey(index))
        {
            DestroyImmediate(spawnerDict[index]);
            spawnerDict.Remove(index);
        }
        else
        {
            Vector2 pos = gridManager.GetCellWorldPosition(index.x, index.y);
            var obj = Instantiate(ballSpawnerPrefab, pos, Quaternion.identity, transform);
            spawnerDict[index] = obj;
        }
    }

    private void ToggleCellHidden(Vector2Int index)
    {
        Cell cell = gridManager.GetCellAt(index);
        if (cell == null) return;

        bool hidden = hiddenCells.Contains(index);
        cell.gameObject.SetActive(hidden);
        if (hidden)
            hiddenCells.Remove(index);
        else
            hiddenCells.Add(index);
    }

    // ================================================================
    // ========================= BLOCK SPAWN ==========================
    // ================================================================

    private void PlaceBlock(Vector2Int index)
    {
        if (previewBlock == null)
        {
            var prefab = blockPrefabs.Find(b => b.Shape == currentShape);
            if (prefab == null)
            {
                Debug.LogWarning("⚠️ Không tìm thấy prefab cho shape " + currentShape);
                return;
            }

            Vector2 pos = gridManager.GetCellWorldPosition(index.x, index.y);
            previewBlock = Instantiate(prefab, pos, Quaternion.identity, transform);
            previewBlock.Shape = currentShape;
            previewBlock.Rotation = currentRotation;
            previewBlock._Color = currentColor;
            previewBlock.ApplyRotation();
        }
        else
        {
            previewBlock.transform.position = gridManager.GetCellWorldPosition(index.x, index.y);
            previewBlock = null;
        }
    }

    // ================================================================
    // ========================== SAVE / LOAD =========================
    // ================================================================

    public void SaveLevel(string fileName)
    {
        LevelData data = new LevelData
        {
            width = levelWidth,
            height = levelHeight,
            timeLimit = levelTime
        };

        // Save cells
        foreach (var c in gridManager.GetComponentsInChildren<Cell>())
        {
            var d = new CellData
            {
                index = c.Index,
                isHidden = hiddenCells.Contains(c.Index),
                isObstacle = obstacleDict.ContainsKey(c.Index),
                isBallSpawner = spawnerDict.ContainsKey(c.Index)
            };
            data.cells.Add(d);
        }

        // Save blocks
        foreach (var b in GetComponentsInChildren<Block>())
        {
            Vector2Int nearest = gridManager.GetNearestCellPosition(b.transform.position);
            data.blocks.Add(new BlockData
            {
                baseIndex = nearest,
                shape = b.Shape,
                color = b._Color,
                rotation = b.Rotation
            });
        }

        Directory.CreateDirectory(savePath);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath + fileName + ".json", json);
        Debug.Log("💾 Level saved: " + fileName);
    }

    public void LoadLevel(string fileName)
    {
        string path = savePath + fileName + ".json";
        if (!File.Exists(path))
        {
            Debug.LogWarning("⚠️ File not found: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        LevelData data = JsonUtility.FromJson<LevelData>(json);

        ClearAll();

        levelWidth = data.width;
        levelHeight = data.height;
        levelTime = data.timeLimit;
        GenerateGrid();

        foreach (var c in data.cells)
        {
            if (c.isHidden) ToggleCellHidden(c.index);
            if (c.isObstacle) ToggleObstacle(c.index);
            if (c.isBallSpawner) ToggleSpawner(c.index);
        }

        foreach (var b in data.blocks)
        {
            var prefab = blockPrefabs.Find(p => p.Shape == b.shape);
            if (prefab == null) continue;

            Vector2 pos = gridManager.GetCellWorldPosition(b.baseIndex.x, b.baseIndex.y);
            var newBlock = Instantiate(prefab, pos, Quaternion.identity, transform);
            newBlock.Shape = b.shape;
            newBlock._Color = b.color;
            newBlock.Rotation = b.rotation;
            newBlock.ApplyRotation();
        }

        Debug.Log("📂 Level loaded: " + fileName);
    }

    // ================================================================
    // ========================= CLEAR ================================
    // ================================================================

    public void ClearAll()
    {
        foreach (Transform child in transform)
            DestroyImmediate(child.gameObject);

        obstacleDict.Clear();
        spawnerDict.Clear();
        hiddenCells.Clear();
        previewBlock = null;
    }
}
