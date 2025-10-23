using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class DraggableBlock : MonoBehaviour
{
	public enum SlideAxis
	{
		Both = 0,
		Horizontal = 1,
		Vertical = 2
	}

	[Header("Allowed Slide Direction")]
	public SlideAxis allowed;

	[Header("Appearance")]
	public Color blockColor;

	private Rigidbody rb;

	private Camera cam;

	private bool dragging;

	private Vector3 grabOffset;

	private MeshRenderer meshRenderer;

	private void Awake()
	{
	}

	private void OnMouseDown()
	{
	}

	private void OnMouseUp()
	{
	}

	private void Update()
	{
	}

	private Vector3 ProjectPointerToPlane()
	{
		return default(Vector3);
	}
}
