using UnityEngine;

public class Drag3D : MonoBehaviour
{
    public BlockHandler myHandler;

    public Material colorMaterial;
    public Material selectedMaterial;

    public GameObject arrow;
    public GameObject arrowInv;

    private Camera mainCam;
    private Rigidbody rb;
    private Rigidbody childRb;

    private bool isDragging;
    private Vector3 offset;
    private Vector3 parentOffset;
    private float zCoord;

    [Header("Movement Settings")]
    public float smoothSpeed = 10f;
    public float minMoveThreshold = 0.05f;
    public bool isDragable = true;
    public float followSpeed = 10f;
    public float maxSpeed = 5f;

    [Header("Collision Settings")]
    [SerializeField] private LayerMask solidMask;
    [SerializeField] private float contactSkin = 0.1f;

    private void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();

        if (transform.childCount > 0)
            childRb = transform.GetChild(0).GetComponent<Rigidbody>();
    }

    public void MouseDown()
    {
        if (!isDragable) return;

        isDragging = true;
        zCoord = mainCam.WorldToScreenPoint(transform.position).z;
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;

        offset = transform.position - mainCam.ScreenToWorldPoint(mousePoint);
        /*if (myHandler != null)
            myHandler.SetMaterial(selectedMaterial);*/

        if (arrow != null) arrow.SetActive(true);
        if (arrowInv != null) arrowInv.SetActive(false);
    }

    public void MouseUp()
    {
        isDragging = false;
        SnapToClosestGrid();

        /*if (myHandler != null)
            myHandler.SetMaterial(colorMaterial);*/

        if (arrow != null) arrow.SetActive(false);
        if (arrowInv != null) arrowInv.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (!isDragging) return;

        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;

        Vector3 targetPos = mainCam.ScreenToWorldPoint(mousePoint) + offset;

        // Optional: Check for collisions before moving
        if (!IsBlocked(targetPos))
        {
            Vector3 newPos = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * followSpeed);
            rb.MovePosition(Vector3.ClampMagnitude(newPos - transform.position, maxSpeed) + transform.position);
        }
    }

    private bool IsBlocked(Vector3 targetPos)
    {
        Collider[] hits = Physics.OverlapBox(targetPos, transform.localScale / 2 - Vector3.one * contactSkin, Quaternion.identity, solidMask);
        return hits.Length > 0;
    }

    public void SnapToClosestGrid()
    {
        Vector3 snappedPos = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            Mathf.Round(transform.position.z)
        );
        rb.MovePosition(snappedPos);
    }

    public bool ShredBlock(string color, Vector3 position)
    {
        // Example placeholder logic
        /*if (myHandler != null && myHandler.colorName == color)
        {
            Destroy(gameObject);
            return true;
        }*/
        return false;
    }
}
