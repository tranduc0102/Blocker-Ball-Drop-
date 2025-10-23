using UnityEngine;

public class AudioHapticsManager : MonoBehaviour
{
	[Header("Audio Sources")]
	[SerializeField]
	private AudioSource musicSource;

	[SerializeField]
	private AudioSource sfxSource;

	[Header("Background Music")]
	[SerializeField]
	public AudioClip bgmClip;

	[Header("SFX Sounds")]
	[SerializeField]
	public AudioClip buttonClickSfx;

	[SerializeField]
	public AudioClip gameLossSfx;

	[SerializeField]
	public AudioClip gameWinSfx;

	[SerializeField]
	public AudioClip resultEntrySfx;

	[SerializeField]
	public AudioClip resultPopupSfx;

	[SerializeField]
	public AudioClip toggleSfx;

	[SerializeField]
	public AudioClip popupSfx;

	[SerializeField]
	public AudioClip debries;

	[SerializeField]
	public AudioClip coinCollectSfx;

	[SerializeField]
	public AudioClip newFeatureSfx;

	[SerializeField]
	public AudioClip ballCollect;

	[SerializeField]
	public AudioClip ballRoll;

	[SerializeField]
	public AudioClip blockSlide;

	[SerializeField]
	public AudioClip blockTap;

	[SerializeField]
	public AudioClip timer;

	public static AudioHapticsManager I { get; private set; }

	public bool MusicEnabled { get; private set; }

	public bool SfxEnabled { get; private set; }

	public bool HapticsEnabled { get; private set; }

	private void Awake()
	{
	}

	public void InitAduioStates()
	{
	}

	public void PlayMusic(AudioClip clip, bool loop = true)
	{
	}

	public void StopMusic()
	{
	}

	public void SetMusicEnabled(bool on)
	{
	}

	public void PlaySfx(AudioClip clip, float volume = 1f)
	{
	}

	public void PlaySfxLooped(AudioClip clip, float volume = 1f)
	{
	}

	public void SetSfxEnabled(bool on)
	{
	}

	public void Vibrate(Enums.HapticType type = Enums.HapticType.Medium)
	{
	}

	public void SetHapticsEnabled(bool on)
	{
	}

	private void ApplyAudioStates()
	{
	}

	public void stopBGM()
	{
	}

	public void startBGM()
	{
	}

	public void ReduceBGMVolume()
	{
	}
}
