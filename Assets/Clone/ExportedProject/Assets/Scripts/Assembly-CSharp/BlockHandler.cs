using System.Collections.Generic;
using UnityEngine;

public class BlockHandler : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> blocksObjects;

	public Drag3D myDragger;

	public string myColor;

	public string movementDir;

	public void InitBlock(string movement, string color, int length, string orientation)
	{
	}

	public void UnShineBlock()
	{
	}

	public void ShineBlock()
	{
	}
}
