using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    private Vector2Int _index;
    public Vector2Int Index => _index;
    public void SetGridIndex(Vector2Int index)
    {
        _index = index;
    }

    public void ShowHighlight(bool state)
    {
        if (_highlight != null)
            _highlight.SetActive(state);
    }

}
