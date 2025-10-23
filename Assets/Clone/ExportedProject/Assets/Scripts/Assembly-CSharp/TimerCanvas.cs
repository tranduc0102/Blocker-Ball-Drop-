using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class TimerCanvas : Singleton<TimerCanvas>
{
	[CompilerGenerated]
	private sealed class _003CBlink_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TimerCanvas _003C_003E4__this;

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
		public _003CBlink_003Ed__8(int _003C_003E1__state)
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

	[SerializeField]
	private TMP_Text timerText;

	[SerializeField]
	public AudioSource timerAudioSource;

	private Coroutine blinkCoroutine;

	public void ResetTimerText(float timeLeft)
	{
	}

	public void SetTimerTextToRed()
	{
	}

	public void SetTimerText(float timeLeft)
	{
	}

	private void StartBlinking()
	{
	}

	public void StopBlinking()
	{
	}

	[IteratorStateMachine(typeof(_003CBlink_003Ed__8))]
	private IEnumerator Blink()
	{
		return null;
	}

	private void OnDestroy()
	{
	}
}
