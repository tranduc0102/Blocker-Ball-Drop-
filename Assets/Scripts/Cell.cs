using UnityEngine;

public class Cell : MonoBehaviour
{
    private Vector2Int _index;
    public Vector2Int Index => _index;
    public void SetGridIndex(Vector2Int index)
    {
        _index = index;
    }
}
