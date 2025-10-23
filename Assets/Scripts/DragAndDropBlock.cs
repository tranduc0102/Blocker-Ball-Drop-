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
    }
    private void StopDragging()
    {
        if (blockInHand == null) return;

        isDragging = false;
        blockInHand.Deselected();

        if (_grid != null)
        {
            Vector2 targetDrop = _grid.GetNearestCellPosition(blockInHand.transform.position);
            blockInHand.transform.DOMove(new Vector3(targetDrop.x, targetDrop.y, blockInHand.transform.position.z),
                0.15f); 
        }
        blockInHand = null;
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
