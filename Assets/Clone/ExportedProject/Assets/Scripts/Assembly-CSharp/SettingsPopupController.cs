using UnityEngine;
using UnityEngine.UI;

public class SettingsPopupController : MonoBehaviour
{
	[Header("Toggles")]
	[SerializeField]
	private Toggle musicToggle;

	[SerializeField]
	private Toggle sfxToggle;

	[SerializeField]
	private Toggle hapticsToggle;

	[Header("Images")]
	[SerializeField]
	private Image musicImageOn;

	[SerializeField]
	private Image musicImageOff;

	[SerializeField]
	private Image sfxImageOn;

	[SerializeField]
	private Image sfxImageOff;

	[SerializeField]
	private Image hapticsImageOn;

	[SerializeField]
	private Image hapticsImageOff;

	private void Start()
	{
	}

	private void OnEnable()
	{
	}

	private void OnMusicChanged(bool isOn)
	{
	}

	private void OnSfxChanged(bool isOn)
	{
	}

	private void OnHapticsChanged(bool isOn)
	{
	}

	private void OnDestroy()
	{
	}

	private void onClickFeedback()
	{
	}

	private void UpdateToggleImages(Toggle toggle, Image imageOn, Image imageOff)
	{
	}
}
