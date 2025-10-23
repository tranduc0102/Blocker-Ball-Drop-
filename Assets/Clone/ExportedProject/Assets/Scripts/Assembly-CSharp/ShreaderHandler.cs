using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShreaderHandler : MonoBehaviour
{
	public Image shreaderColor;

	public string myColor;

	public GameObject particlesParent;

	public List<ParticleSystem> particles;

	public GameObject shredderTeeth;

	public void InitShreader(string direction, string color)
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}
}
