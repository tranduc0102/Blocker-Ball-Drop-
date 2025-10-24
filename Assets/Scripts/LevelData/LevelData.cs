using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int width;
    public int height;
    public float timeLimit;

    public List<CellData> cells = new();
    public List<BlockData> blocks = new();
}

[System.Serializable]
public class CellData
{
    public Vector2Int index;
    public bool isHidden;
    public bool isObstacle;
    public bool isBallSpawner;
}

[System.Serializable]
public class BlockData
{
    public Vector2Int baseIndex;
    public BlockShape shape;
    public ColorBlock color;
    public Rotation rotation;
}
