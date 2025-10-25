using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropBlock3D : MonoBehaviour
{
    private Camera mainCam;
    private bool isDragging;
    private Vector3 offset;
    private float zCoord;

    [Header("Movement Settings")]
    public float smoothSpeed = 10f;
    public bool isDragable = true;
    public float followSpeed = 10f;
    public float maxSpeed = 10f;

    [Header("Current Block In Hand")]
    public Block blockInHand;

    public GridManager _grid;

    [Header("Ball Detection")]
    [SerializeField] private LayerMask ballLayer;
    [SerializeField] private float cellHeightCheck = 0.1f;

    private Vector2 oldPosition;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (!isDragable || IsPointerOverUIElement())
        {
            if (blockInHand != null)
            {
                blockInHand.Rigidbody.velocity = Vector2.zero;
            }
            return;
        }

        if (Input.GetMouseButtonDown(0))
            TrySelectBlock();

        if (Input.GetMouseButton(0) && isDragging && blockInHand != null)
            Drag();

        if (Input.GetMouseButtonUp(0) && isDragging)
            StopDragging();
    }
    private void TrySelectBlock()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Block block = hit.collider.GetComponentInParent<Block>();
            if (block != null)
            {
                blockInHand = block;
                StartDragging(hit.point);
                if (GameplayManager.Instance.State == GameplayManager.GameState.Waiting)
                {
                    GameplayManager.Instance.State = GameplayManager.GameState.Playing;
                }
            }
        }
    }

    private List<Vector2Int> _register;
    private void StartDragging(Vector3 hitPoint)
    {
        if (blockInHand == null) return;

        isDragging = true;
        zCoord = mainCam.WorldToScreenPoint(blockInHand.transform.position).z;
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;

        Vector3 worldMouse = mainCam.ScreenToWorldPoint(mousePoint);
        offset = blockInHand.transform.position - worldMouse;
        offset.z = 0f;
        oldPosition = blockInHand.transform.position;
        blockInHand.Selected();
        _register = new List<Vector2Int>(blockInHand.OccupiedInGrid);
        foreach (var index in blockInHand.OccupiedInGrid)
        {
            _grid.SetOccupied(index, false);
        }
        blockInHand.OccupiedInGrid.Clear();
    }
    private void StopDragging()
    {
        if (blockInHand == null || blockInHand.gameObject == null)
        {
            isDragging = false;
            blockInHand = null;
            return;
        }

        isDragging = false;
        blockInHand.Deselected();

        if (_grid == null)
        {
            blockInHand = null;
            return;
        }

        Vector2Int baseIndex = _grid.GetNearestCellPosition(blockInHand.transform.position);

        var localCells = blockInHand.GetRotatedCells();
        bool canPlace = true;

        foreach (var c in localCells)
        {
            Vector2Int cellPos = baseIndex + c;
            if (!_grid.IsValidPosition(cellPos) ||
                _grid.IsOccupied(cellPos) ||
                HasBallOnCell(cellPos))
            {
                canPlace = false;
                break;
            }
        }

        if (canPlace)
        {
            foreach (var c in localCells)
            {
                Vector2Int pos = baseIndex + c;
                _grid.SetOccupied(pos, true);
                blockInHand.OccupiedInGrid.Add(pos);
            }

            Vector2 centroid = Vector2.zero;
            foreach (var c in localCells)
                centroid += _grid.GetCellWorldPosition(baseIndex.x + c.x, baseIndex.y + c.y);
            centroid /= localCells.Length;

            Vector2 localCentroid = blockInHand.GetLocalCentroid();
            Vector2 pivotOffset = localCentroid * _grid.CellSize;

            Vector3 target = new Vector3(
                centroid.x - pivotOffset.x,
                centroid.y - pivotOffset.y,
                blockInHand.transform.position.z
            );

            blockInHand.transform.DOMove(target, 0.15f).SetEase(Ease.OutQuad);
        }
        else
        {
            foreach (var index in _register)
                _grid.SetOccupied(index, true);

            blockInHand.OccupiedInGrid = new List<Vector2Int>(_register);
            blockInHand.transform.DOMove(oldPosition, 0.15f).SetEase(Ease.OutQuad);
        }

        blockInHand = null;
    }



    private bool HasBallOnCell(Vector2Int cellIndex)
    {
        Vector2 cellWorld = _grid.GetCellWorldPosition(cellIndex.x, cellIndex.y);
        Vector3 center = new Vector3(cellWorld.x, cellWorld.y, blockInHand.transform.position.z);

        Vector3 halfExtents = new Vector3(_grid.CellSize / 3f, _grid.CellSize / 3f, cellHeightCheck / 3f);

        Collider[] hits = Physics.OverlapBox(center, halfExtents, Quaternion.identity, ballLayer);
        return hits.Length > 0;
    }

    private void Drag()
    {
        if (blockInHand == null) return;

        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        Vector3 targetPos = mainCam.ScreenToWorldPoint(mousePoint) + offset;
        Vector3 currentPos = blockInHand.transform.position;
        targetPos.z = currentPos.z;

        Vector3 moveDir = targetPos - currentPos;
        if (moveDir.sqrMagnitude < 0.0001f)
            return;

        Vector2Int gridDir = Vector2Int.zero;
        Vector3 finalTarget = targetPos;

        switch (blockInHand.Direction)
        {
            case Direction.Horizontal:
                finalTarget.y = currentPos.y;
                if (Mathf.Abs(moveDir.x) > 0.02f)
                    gridDir = new Vector2Int((int)Mathf.Sign(moveDir.x), 0);
                break;

            case Direction.Vertical:
                finalTarget.x = currentPos.x;
                if (Mathf.Abs(moveDir.y) > 0.02f)
                    gridDir = new Vector2Int(0, (int)Mathf.Sign(moveDir.y));
                break;

            case Direction.FullDirection:
                if (moveDir.magnitude > 0.02f)
                {
                    int dirX = Mathf.Abs(moveDir.x) > _grid.CellSize * 0.1f ? (int)Mathf.Sign(moveDir.x) : 0;
                    int dirY = Mathf.Abs(moveDir.y) > _grid.CellSize * 0.1f ? (int)Mathf.Sign(moveDir.y) : 0;
                    gridDir = new Vector2Int(dirX, dirY);
                }
                break;
        }

        if (gridDir != Vector2Int.zero)
        {
            Vector2Int baseCell = _grid.GetNearestCellPosition(blockInHand.transform.position);
            var localCells = blockInHand.GetRotatedCells();
        }

        Vector3 direction = finalTarget - currentPos;
        Vector3 desiredVelocity = direction * followSpeed;
        desiredVelocity.z = 0f;

        if (desiredVelocity.magnitude > maxSpeed)
            desiredVelocity = desiredVelocity.normalized * maxSpeed;

        blockInHand.Rigidbody.velocity = Vector3.Lerp(
            blockInHand.Rigidbody.velocity,
            desiredVelocity,
            Time.deltaTime * smoothSpeed
        );
    }
    private bool IsPointerOverUIElement()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult unused in results)
        {
            if (unused.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                return true;
            }
        }

        return false;
    }

}