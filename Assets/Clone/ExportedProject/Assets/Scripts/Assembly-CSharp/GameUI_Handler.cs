using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI_Handler : Singleton<GameUI_Handler>
{
	[CompilerGenerated]
	private sealed class _003CAnimateCoinLabelCR_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public float duration;

		public int start;

		public int end;

		public GameUI_Handler _003C_003E4__this;

		private float _003Ct_003E5__2;

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
		public _003CAnimateCoinLabelCR_003Ed__26(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CReloadSceneAfter_003Ed__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float waitTime;

		public GameUI_Handler _003C_003E4__this;

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
		public _003CReloadSceneAfter_003Ed__24(int _003C_003E1__state)
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

	[Header("GameObjects")]
	[SerializeField]
	private GameObject winPopup;

	[SerializeField]
	private GameObject featureUnlockPopup;

	[SerializeField]
	private GameObject Level1FTUE;

	[SerializeField]
	private GameObject coinsFlyObject;

	[SerializeField]
	private GameObject DoubleCoinsButton;

	[Header("Buttons")]
	[SerializeField]
	private Button retryButton;

	[SerializeField]
	private Button settingsButton;

	[SerializeField]
	private Button levelChangeButton;

	[SerializeField]
	private Button resultNextButton;

	[SerializeField]
	public Button coinBarButton;

	[Header("Labels")]
	[SerializeField]
	private TextMeshProUGUI coinsLabel;

	[SerializeField]
	private TextMeshProUGUI levelLabel;

	[SerializeField]
	private TextMeshProUGUI resultWinAmtLabel;

	[SerializeField]
	private TextMeshProUGUI winLevelLabel;

	public GameObject coinBarVfx;

	public PlayerData playerData;

	private void Start()
	{
	}

	public void OnStartLevel()
	{
	}

	private void checkAndShowFeatureUnloack(int currentLevel)
	{
	}

	public void DisableFtue()
	{
	}

	public void UpdateUI()
	{
	}

	public void ShowWin()
	{
	}

	private void OnLevelCompleted(int level, int attempt)
	{
	}

	public void onResultNextClick()
	{
	}

	[IteratorStateMachine(typeof(_003CReloadSceneAfter_003Ed__24))]
	private IEnumerator ReloadSceneAfter(float waitTime)
	{
		return null;
	}

	private void OnDestroy()
	{
	}

	[IteratorStateMachine(typeof(_003CAnimateCoinLabelCR_003Ed__26))]
	private IEnumerator AnimateCoinLabelCR(int start, int end, float duration, float delay)
	{
		return null;
	}

	private void AnimateCoinLabel(int from, int to, float duration, float delay = 0f)
	{
	}

	public void SimLoss()
	{
	}

	private void ShowRev(int level)
	{
	}

	public void On2xCoinsClick()
	{
	}
}
