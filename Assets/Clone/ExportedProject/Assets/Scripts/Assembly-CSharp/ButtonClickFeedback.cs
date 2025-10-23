using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickFeedback : MonoBehaviour
{
	[SerializeField]
	private Enums.HapticType hapticType;

	[SerializeField]
	private bool playSound;

	private AudioClip clickSfx;

	[SerializeField]
	private bool doAnimate;

	private void Awake()
	{
	}

	private void OnClick()
	{
	}
}
