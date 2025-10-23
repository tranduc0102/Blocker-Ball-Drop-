using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PersistentManager : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CFadeCanvas_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CanvasGroup canvasGroup;

		public float targetAlpha;

		public float duration;

		private float _003CstartAlpha_003E5__2;

		private float _003Ctime_003E5__3;

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
		public _003CFadeCanvas_003Ed__17(int _003C_003E1__state)
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
	private sealed class _003CFadeOutAndDisable_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PersistentManager _003C_003E4__this;

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
		public _003CFadeOutAndDisable_003Ed__16(int _003C_003E1__state)
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
	private sealed class _003CLoadingRoutine_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PersistentManager _003C_003E4__this;

		public bool isInit;

		public string sceneName;

		private AsyncOperation _003Cop_003E5__2;

		private float _003CstartTime_003E5__3;

		private bool _003CactivationRequested_003E5__4;

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
		public _003CLoadingRoutine_003Ed__14(int _003C_003E1__state)
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
	private sealed class _003CLogFirstTimeGameOpen_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CLogFirstTimeGameOpen_003Ed__18(int _003C_003E1__state)
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

	[Header("Loading UI")]
	[SerializeField]
	public GameObject loadingScreen;

	[SerializeField]
	private Image progressFill;

	[SerializeField]
	private GameObject initloadingScreen;

	[SerializeField]
	private GameObject midlevelloadingScreen;

	[Header("Timing")]
	[Tooltip("Minimum time (in seconds) to display the loading screen, even if the scene loads faster.")]
	[SerializeField]
	private float minLoadingTime;

	[Tooltip("How fast the progress bar moves (units per second).")]
	[SerializeField]
	private float fillSpeed;

	[SerializeField]
	private CanvasGroup midLevelCanvasGroup;

	[SerializeField]
	private float fadeDuration;

	public static PersistentManager Instance { get; private set; }

	private void Start()
	{
	}

	public void LoadGameScene(string sceneName, bool isInit = false)
	{
	}

	[IteratorStateMachine(typeof(_003CLoadingRoutine_003Ed__14))]
	private IEnumerator LoadingRoutine(string sceneName, bool isInit)
	{
		return null;
	}

	public void HideLoading()
	{
	}

	[IteratorStateMachine(typeof(_003CFadeOutAndDisable_003Ed__16))]
	private IEnumerator FadeOutAndDisable()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CFadeCanvas_003Ed__17))]
	private IEnumerator FadeCanvas(CanvasGroup canvasGroup, float targetAlpha, float duration)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CLogFirstTimeGameOpen_003Ed__18))]
	private IEnumerator LogFirstTimeGameOpen()
	{
		return null;
	}

	private void FirebaseInit()
	{
	}
}
