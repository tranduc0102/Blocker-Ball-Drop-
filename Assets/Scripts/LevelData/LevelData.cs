using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LevelData
{
    public int width;
    public int height;
    public float timeLimit;
    public float sizeCam;

    public List<CellData> cells = new();
    public List<BlockData> blocks = new();

    public List<ObjectPosData> walls = new();
    public List<ObjectPosData> gates = new(); 
    public ObjectPosData binObject = new(); 
}
[System.Serializable]
public class ObjectPosData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}
[System.Serializable]
public class CellData
{
    public Vector2Int index;
    public bool isHidden;
    public bool isObstacle;
    public bool isBallSpawner;
    public int spawnCount;
}


[System.Serializable]
public class BlockData
{
    public Vector2Int baseIndex;
    public BlockShape shape;
    public ColorBlock color;
    public Rotation rotation;
}
