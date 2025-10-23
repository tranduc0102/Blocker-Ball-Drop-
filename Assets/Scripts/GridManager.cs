using System.Collections.Generic;
using UnityEngine;

public enum GridPattern
{
    Normal,
    Offset
}

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private float _spacing = 0.1f;
    [SerializeField] private int _width = 7;
    [SerializeField] private int _height = 7;
    [SerializeField] private Vector2 _posStartLeftBottom = new Vector2(-0.07f, 0.07f);

    [Header("Pattern Settings")]
    [SerializeField] private GridPattern _pattern = GridPattern.Offset;
    [Range(0f, 1f)] [SerializeField] private float _offsetRatio = 0.5f;

    [Header("Cell Prefabs (so le)")]
    [SerializeField] private Cell _cellPrefabA;
    [SerializeField] private Cell _cellPrefabB;

    [SerializeField] private List<Cell> _cells = new();

    /*private void Awake()
    {
        CreateCells();
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
            CreateCells();
    }

    private void ClearChildren()
    {
        _cells.Clear();
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private void CreateCells()
    {
        if (_cellPrefabA == null || _cellPrefabB == null)
        {
            Debug.LogWarning("⚠️ GridManager: Chưa gán đủ 2 prefab Cell (A và B)!");
            return;
        }

        ClearChildren();

        for (int y = 0; y < _height; y++)
        {
            float xOffset = 0f;
            if (_pattern == GridPattern.Offset && y % 2 == 1)
            {
                xOffset = _spacing * _offsetRatio;
            }

            for (int x = 0; x < _width; x++)
            {
                float xPos = _posStartLeftBottom.x - (x * _spacing) - xOffset;
                float yPos = _posStartLeftBottom.y + (y * _spacing);

                Vector3 cellPos = new Vector3(xPos, yPos, 0f);

                bool useA = (x + y) % 2 == 0; // so le A–B–A–B
                Cell prefabToUse = useA ? _cellPrefabA : _cellPrefabB;

                Cell cell = Instantiate(prefabToUse, cellPos,prefabToUse.transform.rotation, transform);
                cell.name = $"{(useA ? "A" : "B")}_Cell_{x}_{y}";
                _cells.Add(cell);
            }
        }
    }*/

    public Vector2 GetNearestCellPosition(Vector2 worldPosition)
    {
        /*if (_cells == null || _cells.Count == 0)
            CreateCells();*/

        Vector2 nearest = _cells[0].transform.position;
        float minDist = float.MaxValue;

        foreach (var cell in _cells)
        {
            float dist = Vector2.Distance(worldPosition, cell.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = cell.transform.position;
            }
        }
        return nearest;
    }
}
