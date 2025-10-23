using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BallBucketCollector : MonoBehaviour
{
	[Header("UI")]
	public TextMeshProUGUI scoreText;

	private int score;

	private HashSet<int> collectedIds;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	private void UpdateUI()
	{
	}
}
