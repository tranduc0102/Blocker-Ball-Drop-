using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
	public int levelNumber;

	public GridSize gridSize;

	public WinConditions winConditions;

	public List<Wall> verticalWalls;

	public List<Wall> horizontalWalls;

	public List<CellContentData> cellContents;

	public List<CornerGate> cornerGates;

	public List<Wall> cornerWalls;

	public List<Position> floor;

	public float scale;
}
