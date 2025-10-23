using UnityEngine;

public class WallSpawner : Singleton<WallSpawner>
{
	public GameObject WallPrefab;

	public GameObject CornerWallPrefab;

	public GameObject InnerCornerWallPrefab;

	public GameObject GatePrefab;

	public GameObject CornerGatePrefab;

	public Transform VerticalWallsParent;

	public Transform HorizontalWallsParent;

	public Transform LowerPivot;

	public void SpawnWalls()
	{
	}

	private void SpawnVerticalWalls()
	{
	}

	private bool FindBgTileAt(int row, int col)
	{
		return false;
	}

	private bool IsHorizontalWallAt(int row, float col)
	{
		return false;
	}

	private void SpawnHorizontallWalls()
	{
	}

	private bool IsVerticalWallWallAt(float row, int col)
	{
		return false;
	}

	public void SpawnCornerGate()
	{
	}
}
