using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockBoard : Singleton<BlockBoard>
{
	public int currentLevel;

	public int currentJsonLevel;

	public float boardScale;

	public LevelData levelData;

	public int[][] pencilGrid;

	public List<Transform> playableAreapositions;

	public Vector2 posOffset;

	public int targetToWin;

	public int availableBalls;

	public float timerLeft;

	public bool isGameEnded;

	public bool isTimerStarted;

	public TMP_Text bucketBallCountText;

	public Animation BuckAnim;

	public GameObject BuckFX;

	public Transform BucketSurface;

	public bool isTimerPaused;

	public int extraTimeUsesLeft;

	private void Start()
	{
	}

	public void StartGame()
	{
	}

	private void CreateBoard()
	{
	}

	public void UnShineBlocks()
	{
	}

	public void ClaimBall()
	{
	}

	private void ShowWin()
	{
	}

	public void RemoveBallFromList()
	{
	}

	private void Update()
	{
	}

	public void AddExtraTime(int extraTime)
	{
	}
}
