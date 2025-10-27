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
    Wall,
    Gate,
    Bin,
    Delete
}

public class LevelEditorManager : MonoBehaviour
{
    [Header("Level Settings")]
    [Min(1)] public int levelWidth = 8;
    [Min(1)] public int levelHeight = 8;
    [Min(5f)] public float levelTime = 60f;
    [Min(5f)] public int countSpawnBall = 6;
    [Min(5f)] public float camsize = 6;

    [Header("References")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] public Camera _cam;
    [SerializeField] private List<Block> blockPrefabs;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private BallSpawner ballSpawnerPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject gatePrefab;
    [SerializeField] private Bin binObjectPrefab;
    [SerializeField] private Material[] _materialsColor;
    [SerializeField] private List<BallSpawner> ballSpawners;
    [Header("Current Editor State")]
    public EditorMode mode = EditorMode.None;
    public BlockShape currentShape = BlockShape.Single;
    public ColorBlock currentColor = ColorBlock.Red;
    public Rotation currentRotation = Rotation.Angle45;

    [Header("Current Editor State")] 
    public Transform _parentBlocks;
    public Transform _parentWalls;
    public Transform _parentGates;
    public Transform _parentObstacles;
    public Transform _parentSpawnBall;
    private Bin _binObject;
    public GridManager Grid => gridManager;

    private readonly Dictionary<Vector2Int, GameObject> obstacleDict = new();
    private readonly Dictionary<Vector2Int, GameObject> spawnerDict = new();
    private readonly List<Vector2Int> hiddenCells = new();

    private readonly List<GameObject> walls = new(); 
    private readonly List<GameObject> gates = new();

    private const string savePath = "Assets/Resources/Levels/";

    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleClick();
    }*/

    private void HandleClick()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        Cell clickedCell = hit.collider.GetComponent<Cell>();
        if (clickedCell != null && !clickedCell.gameObject.activeSelf)
            return;

        if (mode == EditorMode.Delete)
        {
            var block = hit.collider.GetComponentInParent<Block>();
            if (block != null)
            {
                DestroyImmediate(block.gameObject);
                return;
            }

            if (walls.Remove(hit.collider.gameObject))
            {
                DestroyImmediate(hit.collider.gameObject);
                return;
            }

            if (gates.Remove(hit.collider.gameObject))
            {
                DestroyImmediate(hit.collider.gameObject);
                return;
            }

            Cell cell = hit.collider.GetComponent<Cell>();
            if (cell != null)
                ClearCell(cell.Index);

            GameObject obstacleRoot = hit.collider.transform.parent.transform.parent.gameObject;

            if (obstacleDict.ContainsValue(obstacleRoot))
            {
                Vector2Int keyToRemove = Vector2Int.zero;
                foreach (var kvp in obstacleDict)
                {
                    if (kvp.Value == obstacleRoot)
                    {
                        keyToRemove = kvp.Key;
                        break;
                    }
                }

                if (obstacleDict.ContainsKey(keyToRemove))
                {
                    DestroyImmediate(obstacleDict[keyToRemove]);
                    obstacleDict.Remove(keyToRemove);
                    return;
                }
            }


            return;
        }


        if (mode == EditorMode.Wall)
        {
            ToggleFreeObject(hit.point, walls, wallPrefab);
            return;
        } 
        if (mode == EditorMode.Bin)
        {
            ToggleFreeObject(hit.point);
            return;
        }

        if (mode == EditorMode.Gate)
        {
            ToggleFreeObject(hit.point, gates, gatePrefab);
            return;
        }

        if (clickedCell == null) return;
        Vector2Int index = clickedCell.Index;

        if (hiddenCells.Contains(index)) return;

        switch (mode)
        {
            case EditorMode.Obstacle:
                ToggleObstacle(index);
                break; 
            case EditorMode.BallSpawner:
                ToggleSpawner(index, countSpawnBall);
                break;
            case EditorMode.Block:
                PlaceBlock(index);
                break;
        }
    }
    private void ToggleObstacle(Vector2Int index)
    {
        if (obstacleDict.TryGetValue(index, out GameObject obj))
        {
            DestroyImmediate(obj);
            obstacleDict.Remove(index);
        }
        else
        {
            Vector2 pos = gridManager.GetCellWorldPosition(index.x, index.y);
            var obstacle = Instantiate(obstaclePrefab, pos, Quaternion.identity, transform);
            obstacle.transform.SetParent(_parentObstacles);
            obstacleDict[index] = obstacle;
        }
    }

    private int _totalBall = 0;
    private void ToggleSpawner(Vector2Int index, int count)
    {
        if (spawnerDict.TryGetValue(index, out GameObject obj))
        {
            DestroyImmediate(obj);
            spawnerDict.Remove(index);
        }
        else
        {
            Vector2 pos = gridManager.GetCellWorldPosition(index.x, index.y);
            var spawner = Instantiate(ballSpawnerPrefab, pos, Quaternion.identity, transform);
            ballSpawners.Add(spawner);
            spawner.transform.SetParent(_parentSpawnBall);
            spawner.transform.localPosition = new Vector3(spawner.transform.position.x, spawner.transform.position.y, 0f);
            spawner.SetCountSpawn(count);
            _totalBall += count;
            spawnerDict[index] = spawner.gameObject;
        }
    }
    private void ToggleFreeObject(Vector3 pos, List<GameObject> list, GameObject prefab)
    {
        var objNew = Instantiate(prefab, pos, Quaternion.identity, transform);
        if (prefab == gatePrefab)
        {
            objNew.transform.SetParent(_parentGates);
        }
        else
        {
            objNew.transform.localPosition = new Vector3(objNew.transform.localPosition.x, objNew.transform.localPosition.y, 0.05f);
            objNew.transform.SetParent(_parentWalls);
        }
        list.Add(objNew);
    }
    private void ToggleFreeObject(Vector3 pos)
    {
        var objNew = Instantiate(binObjectPrefab, pos, Quaternion.identity, transform);
        _binObject = objNew;
    }
    private void PlaceBlock(Vector2Int index)
    {
        var prefab = blockPrefabs.Find(b => b.Shape == currentShape);
        if (prefab == null)
        {
            Debug.LogWarning($"⚠️ Không tìm thấy prefab cho shape {currentShape}");
            return;
        }

        Vector2 pos = gridManager.GetCellWorldPosition(index.x, index.y);
        var newBlock = Instantiate(prefab, pos, Quaternion.identity, transform);
        newBlock.transform.SetParent(_parentBlocks);
        newBlock.Rotation = currentRotation;
        newBlock._Color = currentColor;
        newBlock.ApplyRotation();

        int colorIndex = (int)currentColor;
        if (colorIndex >= 0 && colorIndex < _materialsColor.Length)
            newBlock.Init(currentColor, Direction.FullDirection, _materialsColor[colorIndex]);
    }

    private void ClearCell(Vector2Int index)
    {
        if (obstacleDict.ContainsKey(index))
        {
            DestroyImmediate(obstacleDict[index]);
            obstacleDict.Remove(index);
        }

        if (spawnerDict.ContainsKey(index))
        {
            DestroyImmediate(spawnerDict[index]);
            spawnerDict.Remove(index);
        }

        foreach (var block in GetComponentsInChildren<Block>())
        {
            var localCells = block.GetRotatedCells();
            Vector2Int nearest = gridManager.GetNearestCellPosition(block.transform.position);
            foreach (var c in localCells)
            {
                if (nearest + c == index)
                {
                    DestroyImmediate(block.gameObject);
                    break;
                }
            }
        }

        if (gridManager != null && gridManager.IsValidPosition(index))
            gridManager.SetOccupied(index, false);

        Cell cell = gridManager.GetCellAt(index);
        if (cell != null && cell.gameObject.activeSelf)
        {
            cell.gameObject.SetActive(false);
            hiddenCells.Add(cell.Index);
            gridManager.SetOccupied(cell.Index, true);
        }

        Debug.Log($"🧹 Cleared cell {index}");
    }
    public void ClearAll()
    {
        ClearObject(_parentWalls);
        ClearObject(_parentBlocks);
        ClearObject(_parentGates);
        ClearObject(_parentObstacles);
        ClearObject(_parentSpawnBall);
        if (_binObject != null)
        { 
            Destroy(_binObject.gameObject);
        }
        gridManager.ClearOldCells();
        obstacleDict.Clear();
        spawnerDict.Clear();
        hiddenCells.Clear();
        walls.Clear();
        gates.Clear();
        Debug.Log("Level cleared");
    }

    private void ClearObject(Transform t)
    {
        for (int i = t.childCount - 1; i >= 0; i--)
        {
            var child = t.GetChild(i); 
            if (Application.isPlaying)
                Destroy(child.gameObject);
            else
                DestroyImmediate(child.gameObject);
        }
    }

    public void SaveLevel(string fileName)
    {
        _totalBall = 0;
        LevelData data = new LevelData
        {
            width = levelWidth,
            height = levelHeight,
            timeLimit = levelTime,
            sizeCam = _cam.fieldOfView
        };

        foreach (var c in gridManager.GetComponentsInChildren<Cell>(true))
        {
            var cellData = new CellData
            {
                index = c.Index,
                isHidden = hiddenCells.Contains(c.Index),
                isObstacle = obstacleDict.ContainsKey(c.Index),
                isBallSpawner = spawnerDict.ContainsKey(c.Index),
                spawnCount = countSpawnBall
            };
            data.cells.Add(cellData);
        }
        if(_binObject != null) data.binObject = new ObjectPosData{position = _binObject.transform.position, rotation = _binObject.transform.rotation,  scale = _binObject.transform.localScale};

        foreach (var w in walls)
        {
            if(w == null) continue;
            data.walls.Add(new ObjectPosData { position = w.transform.position, rotation = w.transform.rotation, scale = w.transform.localScale});
        }

        foreach (var g in gates)
            data.gates.Add(new ObjectPosData { position = g.transform.position,  rotation = g.transform.rotation, scale = g.transform.localScale });
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
        Debug.Log($"💾 Level saved: {fileName}");
    }

    public List<BallSpawner> GetBallSpawners()
    {
        return ballSpawners;
    }
    public void LoadLevel(string fileName)
    {
        _totalBall = 0;
        ballSpawners = new List<BallSpawner>();
        string path = savePath + fileName + ".json";
        if (!File.Exists(path))
        {
            Debug.LogWarning($"⚠️ File not found: {path}");
            return;
        }

        string json = File.ReadAllText(path);
        LevelData data = JsonUtility.FromJson<LevelData>(json);

        ClearAll();

        levelWidth = data.width;
        levelHeight = data.height;
        levelTime = data.timeLimit;

        GenerateGrid();
        if (_cam != null && data.sizeCam > 0)
        {
            float targetAspect = 10.8f / 19.2f;
            float currentAspect = (float)Screen.width / Screen.height;

            float verticalFOV = data.sizeCam * (targetAspect / currentAspect);
            _cam.fieldOfView = verticalFOV;
            camsize = verticalFOV;
        }

        foreach (var c in data.cells)
        {
            if (c.isHidden)
            {
                Cell cell = gridManager.GetCellAt(c.index);
                if (cell != null)
                {
                    cell.gameObject.SetActive(false);
                    hiddenCells.Add(c.index);
                }
                if (gridManager != null && gridManager.IsValidPosition(c.index))
                    gridManager.SetOccupied(c.index, true);
            }
            if (c.isObstacle)
                ToggleObstacle(c.index);

            if (c.isBallSpawner)
                ToggleSpawner(c.index, c.spawnCount);
        }


        foreach (var w in data.walls)
        {
            var obj = Instantiate(wallPrefab, w.position, w.rotation, transform);
            obj.transform.localScale = w.scale;
            obj.transform.SetParent(_parentWalls);
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, 0.05f);
            walls.Add(obj);
        }

        foreach (var g in data.gates)
        {
            var obj = Instantiate(gatePrefab, g.position, g.rotation, transform);
            obj.transform.localScale = g.scale;
            obj.transform.SetParent(_parentGates);
            gates.Add(obj);
        }
        var objBin = Instantiate(binObjectPrefab, data.binObject.position, data.binObject.rotation, transform);
        objBin.transform.localScale = data.binObject.scale;
        _binObject = objBin;
        _binObject.SetTotal(_totalBall);
        foreach (var b in data.blocks)
        {
            var prefab = blockPrefabs.Find(p => p.Shape == b.shape);
            if (prefab == null) continue;

            var newBlock = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            newBlock.transform.SetParent(_parentBlocks);
            newBlock.Rotation = b.rotation;
            newBlock._Color = b.color;
            newBlock.ApplyRotation();

            int colorIndex = (int)b.color;
            if (colorIndex >= 0 && colorIndex < _materialsColor.Length)
                newBlock.Init(b.color, Direction.FullDirection, _materialsColor[colorIndex]);

            var localCells = newBlock.GetRotatedCells();
            Vector2 centroid = Vector2.zero;

            foreach (var c in localCells)
                centroid += gridManager.GetCellWorldPosition(b.baseIndex.x + c.x, b.baseIndex.y + c.y);

            centroid /= localCells.Length;

            Vector2 localCentroid = newBlock.GetLocalCentroid();
            Vector2 pivotOffset = localCentroid * gridManager.CellSize;

            Vector3 targetPos = new Vector3(
                centroid.x - pivotOffset.x,
                centroid.y - pivotOffset.y,
                0f
            );

            newBlock.transform.position = targetPos;

        }

        Debug.Log($"📂 Level loaded: {fileName}");
    }

    public void ClearItem()
    {
        ClearObject(_parentBlocks);
        ClearObject(_parentObstacles);
        ClearObject(_parentSpawnBall);
        gridManager.ClearOldCells();
        obstacleDict.Clear();
        spawnerDict.Clear();
    }
    public void GenerateGrid()
    {
        if (gridManager == null)
        {
            Debug.LogError("❌ GridManager chưa được gán!");
            return;
        }
        hiddenCells.Clear();
        gridManager.GenerateGrid(levelWidth, levelHeight);
        Debug.Log($"🧱 Generated grid {levelWidth}x{levelHeight}");
    }
}
