using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private float _cellSize = 0.1f;
    [SerializeField] private int _width = 7;
    [SerializeField] private int _height = 7;
    [SerializeField] private Vector2 _origin = new Vector2(-0.07f, 0.07f);

    [Header("Cell Prefabs")]
    [SerializeField] private Cell _cellPrefabA;
    [SerializeField] private Cell _cellPrefabB;

    [Header("Visual Options")]
    [SerializeField] private bool _showGrid = true;
    [SerializeField] private bool _checkerPattern = true;

    private readonly List<Cell> _cells = new();
    public bool[,] occupied;

    public float CellSize => _cellSize;
    public Vector2 Origin => _origin;
    public int Width => _width;
    public int Height => _height;

    // =================================================================
    // ===================== GRID CREATION =============================
    // =================================================================

    /*private void Awake()
    {
        GenerateGrid(_width, _height);
    }*/

    /// <summary>
    /// Tạo mới toàn bộ grid theo kích thước mới.
    /// </summary>
    public void GenerateGrid(int width, int height)
    {
        _width = width;
        _height = height;

        occupied = new bool[_width, _height];

        ClearOldCells();
        CenterGridToCamera();

        if (_showGrid)
            CreateCells();
    }

    private void CreateCells()
    {
        if (_cellPrefabA == null)
        {
            Debug.LogError("❌ Thiếu Cell Prefab A!");
            return;
        }

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Vector2 pos = GetCellWorldPosition(x, y);

                Cell prefabToUse = _checkerPattern && _cellPrefabB != null && (x + y) % 2 == 1
                    ? _cellPrefabB
                    : _cellPrefabA;

                Cell cell = Instantiate(prefabToUse, pos, prefabToUse.transform.rotation, transform);
                cell.name = $"Cell_{x}_{y}";
                cell.gameObject.SetActive(true);
                cell.SetGridIndex(new Vector2Int(x, y));
                _cells.Add(cell);
            }
        }
    }

    private void ClearOldCells()
    {
        foreach (var c in _cells)
        {
            if (c != null) DestroyImmediate(c.gameObject);
        }
        _cells.Clear();
    }

    // =================================================================
    // ====================== GRID UTILITIES ===========================
    // =================================================================

    public Vector2 GetCellWorldPosition(int x, int y)
    {
        return _origin + new Vector2(x * _cellSize, y * _cellSize);
    }

    public Vector2Int GetNearestCellPosition(Vector2 worldPosition)
    {
        if (_cells.Count == 0)
            return Vector2Int.zero;

        Cell nearest = _cells[0];
        float minDist = float.MaxValue;

        foreach (var cell in _cells)
        {
            float dist = Vector2.Distance(worldPosition, cell.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = cell;
            }
        }

        return nearest.Index;
    }

    public bool IsValidPosition(Vector2Int index)
    {
        return index.x >= 0 && index.x < _width && index.y >= 0 && index.y < _height;
    }

    public bool IsOccupied(Vector2Int index)
    {
        if (!IsValidPosition(index)) return true;
        return occupied[index.x, index.y];
    }

    public void SetOccupied(Vector2Int index, bool state)
    {
        if (IsValidPosition(index))
            occupied[index.x, index.y] = state;
    }

    public Cell GetCellAt(Vector2Int index)
    {
        if (!IsValidPosition(index)) return null;
        return _cells.Find(c => c.Index == index);
    }

    // =================================================================
    // ===================== CAMERA ALIGNMENT ==========================
    // =================================================================

    private void CenterGridToCamera()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("⚠️ Không tìm thấy Camera chính!");
            return;
        }

        Vector3 camPos = cam.transform.position;

        float gridWidth = _width * _cellSize;
        float gridHeight = _height * _cellSize;

        _origin = new Vector2(
            camPos.x - gridWidth / 2 + _cellSize / 2,
            camPos.y - gridHeight / 2 + _cellSize / 2
        );
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!_showGrid) return;

        Gizmos.color = Color.yellow;
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Vector2 pos = GetCellWorldPosition(x, y);
                Gizmos.DrawWireCube(pos, Vector3.one * _cellSize * 0.9f);
            }
        }
    }
#endif
}
