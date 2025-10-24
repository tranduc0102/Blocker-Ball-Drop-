using DG.Tweening;
using UnityEngine;

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
    [SerializeField] private LayerMask ballLayer; // Gán layer của ball vào đây
    [SerializeField] private float cellHeightCheck = 0.1f; // Chiều cao box check (điều chỉnh nếu ball có kích thước khác)

    private Vector2Int? oldBaseIndex; // Lưu baseIndex cũ để unset/set lại nếu cần
    private Vector3 oldPosition; // Lưu position cũ để snap về nếu drop fail

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (!isDragable) return;

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
            }
        }
    }

    private void StartDragging(Vector3 hitPoint)
    {
        if (blockInHand == null) return;

        isDragging = true;
        blockInHand.Rigidbody.useGravity = false;

        zCoord = mainCam.WorldToScreenPoint(blockInHand.transform.position).z;
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;

        Vector3 worldMouse = mainCam.ScreenToWorldPoint(mousePoint);
        offset = blockInHand.transform.position - worldMouse;
        blockInHand.Selected();

        // Lưu position cũ
        oldPosition = blockInHand.transform.position;

        // Tính oldBaseIndex và unset occupied nếu block đã được đặt
        Vector2 worldPos = oldPosition;
        float fx = (worldPos.x - _grid.Origin.x) / _grid.CellSize;
        float fy = (worldPos.y - _grid.Origin.y) / _grid.CellSize;
        Vector2 floatIndex = new Vector2(fx, fy);

        Vector2 localCentroid = blockInHand.GetLocalCentroid();

        Vector2 baseFloat = floatIndex - localCentroid;
        Vector2Int potentialOldBase = new Vector2Int(
            Mathf.RoundToInt(baseFloat.x),
            Mathf.RoundToInt(baseFloat.y)
        );

        var localCells = blockInHand.GetRotatedCells();

        // Kiểm tra xem tất cả cells cũ có occupied không (để xác định đã đặt hay chưa)
        bool wasPlaced = true;
        foreach (var c in localCells)
        {
            Vector2Int cellPos = potentialOldBase + c;
            if (!_grid.IsValidPosition(cellPos) || !_grid.IsOccupied(cellPos))
            {
                wasPlaced = false;
                break;
            }
        }

        if (wasPlaced)
        {
            // Unset occupied cũ
            foreach (var c in localCells)
            {
                _grid.SetOccupied(potentialOldBase + c, false);
            }
            oldBaseIndex = potentialOldBase;
        }
        else
        {
            oldBaseIndex = null; // Chưa được đặt trước đó
        }
    }
    private void StopDragging()
    {
        if (blockInHand == null) return;

        isDragging = false;
        blockInHand.Deselected();

        if (_grid != null)
        {
            Vector2 worldPos = blockInHand.transform.position;
            float fx = (worldPos.x - _grid.Origin.x) / _grid.CellSize;
            float fy = (worldPos.y - _grid.Origin.y) / _grid.CellSize;
            Vector2 floatIndex = new Vector2(fx, fy);

            Vector2 localCentroid = blockInHand.GetLocalCentroid();

            Vector2 baseFloat = floatIndex - localCentroid;
            Vector2Int baseIndex = new Vector2Int(
                Mathf.RoundToInt(baseFloat.x),
                Mathf.RoundToInt(baseFloat.y)
            );

            var localCells = blockInHand.GetRotatedCells();

            bool canPlace = true;
            foreach (var c in localCells)
            {
                Vector2Int cellPos = baseIndex + c;
                if (!_grid.IsValidPosition(cellPos) || _grid.IsOccupied(cellPos) || HasBallOnCell(cellPos))
                {
                    canPlace = false;
                    break;
                }
            }
            if (canPlace)
            {
                foreach (var c in localCells)
                    _grid.SetOccupied(baseIndex + c, true);

                Vector2 sum = Vector2.zero;
                int count = localCells.Length;
                foreach (var c in localCells)
                {
                    Vector2 cellWorldPos = _grid.GetCellWorldPosition(baseIndex.x + c.x, baseIndex.y + c.y);
                    sum += cellWorldPos;
                }
                Vector2 target = sum / count;

                blockInHand.transform.DOMove(new Vector3(target.x, target.y, blockInHand.transform.position.z), 0.15f)
                    .SetEase(Ease.OutQuad);
            }
            else
            {
                blockInHand.transform.DOMove(oldPosition, 0.15f)
                    .SetEase(Ease.OutQuad);

                if (oldBaseIndex.HasValue)
                {
                    foreach (var c in localCells)
                    {
                        _grid.SetOccupied(oldBaseIndex.Value + c, true);
                    }
                }
            }
        }
        blockInHand = null;
        oldBaseIndex = null; // Reset
    }

    private bool HasBallOnCell(Vector2Int cellIndex)
    {
        Vector2 cellWorld = _grid.GetCellWorldPosition(cellIndex.x, cellIndex.y);
        Vector3 center = new Vector3(cellWorld.x, cellWorld.y, blockInHand.transform.position.z); 

        Vector3 halfExtents = new Vector3(_grid.CellSize / 2f, _grid.CellSize / 2f, cellHeightCheck / 2f);

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

        switch (blockInHand.Direction)
        {
            case Direction.Horizontal:
                targetPos.y = currentPos.y;
                targetPos.z = currentPos.z;
                break;

            case Direction.Vertical:
                targetPos.x = currentPos.x;
                targetPos.z = currentPos.z;
                break;

            case Direction.FullDirection:
                targetPos.z = currentPos.z;
                break;
        }

        Vector3 direction = targetPos - currentPos;
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
}