using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CTrySendNewPlayerEvent_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PlayerManager _003C_003E4__this;

		private float _003CtimeoutSeconds_003E5__2;

		private float _003CelapsedTime_003E5__3;

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
		public _003CTrySendNewPlayerEvent_003Ed__27(int _003C_003E1__state)
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
	private sealed class _003CUpdateHeartTimer_003Ed__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PlayerManager _003C_003E4__this;

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
		public _003CUpdateHeartTimer_003Ed__35(int _003C_003E1__state)
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

	private const string Prefix = "BallDrop";

	public static readonly string DeviceIdKey;

	public static readonly string PlayerDataKey;

	[Header("Save slot key")]
	[SerializeField]
	private string saveKey;

	public PlayerData Data;

	private const string ISO_FORMAT = "o";

	private const string DATE_YMD = "yyyy-MM-dd";

	public bool isDataLoaded;

	public static PlayerManager Instance { get; private set; }

	public IStorageProvider Storage { get; private set; }

	public bool IsNewPlayer { get; private set; }

	private void Awake()
	{
	}

	public static string GetDeviceId()
	{
		return null;
	}

	private void OnApplicationPause(bool pause)
	{
	}

	private void OnApplicationQuit()
	{
	}

	public void Save()
	{
	}

	public void LoadOrCreate()
	{
	}

	private void LogFirstTimeEvent()
	{
	}

	[IteratorStateMachine(typeof(_003CTrySendNewPlayerEvent_003Ed__27))]
	private IEnumerator TrySendNewPlayerEvent()
	{
		return null;
	}

	public void DeleteSave()
	{
	}

	public void AddCoins(int amount)
	{
	}

	public bool SpendCoins(int amount)
	{
		return false;
	}

	public void IncreaseLevel()
	{
	}

	public void InitLives()
	{
	}

	public bool UseLife()
	{
		return false;
	}

	public void AddLife()
	{
	}

	[IteratorStateMachine(typeof(_003CUpdateHeartTimer_003Ed__35))]
	private IEnumerator UpdateHeartTimer()
	{
		return null;
	}

	public void BeginSession()
	{
	}
}
