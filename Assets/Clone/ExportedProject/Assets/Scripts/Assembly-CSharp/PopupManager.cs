using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CShowFlyerRoutine_003Ed__49 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupManager _003C_003E4__this;

		private RectTransform _003Crt_003E5__2;

		private Vector2 _003CoriginalPos_003E5__3;

		private Vector2 _003CstartPos_003E5__4;

		private float _003Cduration_003E5__5;

		private float _003Celapsed_003E5__6;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CShowFlyerRoutine_003Ed__49(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		public bool MoveNext()
		{
			return false;
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[Header("Popup Roots")]
	[SerializeField]
	private GameObject overlay;

	[SerializeField]
	private GameObject settingsPopup;

	[SerializeField]
	private GameObject lossPopup;

	[SerializeField]
	private GameObject restartPopup;

	[SerializeField]
	private GameObject levelChangePopup;

	[SerializeField]
	private GameObject outOfTimePopup;

	[SerializeField]
	private GameObject outOfBallsPopup;

	[SerializeField]
	private GameObject outOfLifePopup;

	[SerializeField]
	private GameObject refillLifePopup;

	[SerializeField]
	private GameObject quitPopup;

	[SerializeField]
	private GameObject Flyer;

	[SerializeField]
	private TMP_InputField levelChangeInputField;

	[SerializeField]
	private TMP_InputField levelUploadInputField;

	[SerializeField]
	private GameObject outOfTimeRVBtn;

	[SerializeField]
	private GameObject outOfLifeRVBtn;

	[SerializeField]
	public GameObject outOfLifeRVHomeBtn;

	[Header("Popup Coinbar")]
	[SerializeField]
	private GameObject coinBar;

	[SerializeField]
	private TextMeshProUGUI coinLabel;

	[Header("OutofTime Popup")]
	[SerializeField]
	private Button rvButtonTime;

	[SerializeField]
	private TextMeshProUGUI extraTimeCostLabel;

	private GameObject _currentPopup;

	private Coroutine flyerCoroutine;

	private int extraLifeAttempt;

	private bool isExtraLifeForRestart;

	public static PopupManager Instance { get; private set; }

	public bool isSettingsPopupActive => false;

	private void Start()
	{
	}

	public void HideAllPopups()
	{
	}

	public void Show(Enums.PopupType type)
	{
	}

	public void Hide()
	{
	}

	public void RestartGame()
	{
	}

	public void OnOutOfLifeCloseClick()
	{
	}

	public void ShowGameLoss(bool isOutOfTime)
	{
	}

	public void ShowSettings()
	{
	}

	private void PauseUnpauseGame(bool pause)
	{
	}

	public void ShowLoss()
	{
	}

	public void ShowRestart()
	{
	}

	public void Close()
	{
	}

	public void ShowLevelChange()
	{
	}

	public void ShowQuit()
	{
	}

	public void ShowRefillLife()
	{
	}

	public void OnHomeClick()
	{
	}

	public void LevelChange()
	{
	}

	public void ShowFlyer(bool isAdFail)
	{
	}

	public void UploadCustomLevel()
	{
	}

	[IteratorStateMachine(typeof(_003CShowFlyerRoutine_003Ed__49))]
	private IEnumerator ShowFlyerRoutine()
	{
		return null;
	}

	public void ExtraTime(bool isViaAd)
	{
	}

	private void OnExtraTimeSuccessPay()
	{
	}

	public void ExtraLife(bool isViaAd)
	{
	}

	private void OnExtraLifeSuccessPay()
	{
	}

	public void ShowShop()
	{
	}
}
