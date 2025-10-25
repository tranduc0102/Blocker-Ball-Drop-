using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MeshRenderer _meshRenderer;

    [Header("Setup")]
    [SerializeField] private Direction _direction;
    [SerializeField] private Rotation _rotation;
    [SerializeField] private BlockShape _shape;

    private bool _initialized;
    [SerializeField] private Vector2Int[] _occupiedCells;
    [SerializeField] private Vector2[] _offsets;
    private List<Vector2Int> _occupiedInGrid = new List<Vector2Int>();

    public List<Vector2Int> OccupiedInGrid
    {
        get { return _occupiedInGrid; }
        set { _occupiedInGrid = value; }
    } 
    public Direction Direction => _direction;
    public Rotation Rotation
    {
        get =>  _rotation;
        set => _rotation = value;
    }
    public BlockShape Shape
    {
        get => _shape;
        set => _shape = value;
    }
    public Rigidbody Rigidbody => _rigidbody;
    public ColorBlock _Color;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        ApplyRotation();
        _occupiedCells = GetBaseCells(_shape);
        AlignVisual();
    }
#endif

    private void Awake()
    {
        _occupiedCells = GetBaseCells(_shape);
        _initialized = true;
    }
    private void OnDestroy()
    {
        DOTween.Kill(this);
    }

    public void Init(ColorBlock colorBlock, Direction direction, Material mat)
    {
        _direction = direction;
        this._Color = colorBlock;
        _meshRenderer.material = mat;
        AlignVisual();
    }

    public void Selected()
    {
        _rigidbody.isKinematic = false;
        transform.DOKill();

        DOTween.Sequence()
            .Append(transform.DOLocalMoveZ(0.015f, 0.25f).SetEase(Ease.OutBack))
            .Join(DOVirtual.Float(0f, 1.5f, 0.3f, v => _meshRenderer.material.SetFloat("_OutlineWidth", v)));
    }

    public void Deselected()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        
        transform.DOKill();


        DOTween.Sequence()
            .Append(transform.DOLocalMoveZ(0, 0.25f).SetEase(Ease.OutBack))
            .Join(DOVirtual.Float(1.5f, 0f, 0.25f, v =>  _meshRenderer.material.SetFloat("_OutlineWidth", v)));
    }

    public void ApplyRotation()
    {
        float angle = RotationAngle();
        _meshRenderer.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    private float RotationAngle()
    {
        return _rotation switch
        {
            Rotation.Angle45 => 45f,
            Rotation.Angle135 => 135f,
            Rotation.Angle225 => 225f,
            Rotation.Angle315 => 315f,
            _ => 0f
        };
    }

    private Vector2Int[] GetBaseCells(BlockShape shape)
    {
        switch (shape)
        {
            case BlockShape.Single: return new[] { new Vector2Int(0, 0) };

            case BlockShape.Line2: return new[] { new Vector2Int(0, 0), new Vector2Int(1, 0) };
            case BlockShape.Line3: return new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) };

            case BlockShape.L3: return new[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 0) };
            case BlockShape.L4: return new[] { new Vector2Int(1, 0), new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1) };
            case BlockShape.L5: return new[] { new Vector2Int(2, 0), new Vector2Int(1, 0), new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(0, -2) };

            case BlockShape.LReverse3: return new[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(-1, 0) };
            case BlockShape.LReverse4: return new[] { new Vector2Int(1, 0), new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1) };
            case BlockShape.LReverse5: return new[] { new Vector2Int(2, 0), new Vector2Int(1, 0), new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2) };

            case BlockShape.T: return new[] { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };

            case BlockShape.Z: return new[] { new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1) };
            case BlockShape.ZReverse: return new[] { new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1) };

            case BlockShape.U: return new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(-1, 1), new Vector2Int(1, 1) };
            case BlockShape.UReverse: return new[] { new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(-1, 0), new Vector2Int(1, 0) };

            case BlockShape.Square4: return new[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) };

            default: return new[] { new Vector2Int(0, 0) };
        }
    }
    public Vector2 GetLocalCentroid()
    {
        Vector2 sum = Vector2.zero;
        var cells = GetRotatedCells();
        foreach (var c in cells)
        {
            sum += new Vector2(c.x, c.y);
        }
        return sum / cells.Length;
    }
    public Vector2Int[] GetRotatedCells()
    {
        int logicAngle = _rotation switch
        {
            Rotation.Angle45 => 0,
            Rotation.Angle135 => 90,
            Rotation.Angle225 => 180,
            Rotation.Angle315 => 270,
            _ => 0
        };

        float rad = logicAngle * Mathf.Deg2Rad;
        Vector2Int[] rotated = new Vector2Int[_occupiedCells.Length];

        for (int i = 0; i < _occupiedCells.Length; i++)
        {
            var c = _occupiedCells[i];
            int x = Mathf.RoundToInt(c.x * Mathf.Cos(rad) - c.y * Mathf.Sin(rad));
            int y = Mathf.RoundToInt(c.x * Mathf.Sin(rad) + c.y * Mathf.Cos(rad));
            rotated[i] = new Vector2Int(x, y);
        }

        return rotated;
    }

    private void AlignVisual()
    {
      _meshRenderer.transform.localPosition = _offsets[(int)_rotation];
    }

    private void OnDrawGizmosSelected()
    {
        if (!_initialized) _occupiedCells = GetBaseCells(_shape);
        Gizmos.color = Color.yellow;
        foreach (var c in GetRotatedCells())
        {
            Gizmos.DrawWireCube(transform.position + new Vector3(c.x * 0.1f, c.y * 0.1f, 0), Vector3.one * 0.1f);
        }
    }
}

public enum Direction
{
    FullDirection,
    Vertical,
    Horizontal,
}

public enum ColorBlock
{
    Red,
    DarkBlue,
    Yellow,
    Green,
    Pink,
    Purple,
    Orange,
    LightBlue
}
public enum Rotation
{
    Angle45 = 0,
    Angle135 = 1,
    Angle225 = 2,
    Angle315 = 3
}
public enum BlockShape
{
    Single,             // 1 ô
    Line2,              // 2 ô liền
    Line3,              // 3 ô liền
    L3,                 // L 3 ô
    L4,                 // L 4 ô
    L5,                 // L 5 ô
    LReverse3,          // L ngược 3 ô
    LReverse4,          // L ngược 4 ô
    LReverse5,          // L ngược 5 ô
    T,
    Z,                  // Z
    ZReverse,           // Z ngược
    U,                  // U
    UReverse,           // U ngược
    Square4             // Vuông 2x2
}
