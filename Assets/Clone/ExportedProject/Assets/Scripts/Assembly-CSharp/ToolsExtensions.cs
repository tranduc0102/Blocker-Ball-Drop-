using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public static class ToolsExtensions
{
	[CompilerGenerated]
	private sealed class _003CDoAction_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public UnityAction action;

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
		public _003CDoAction_003Ed__12(int _003C_003E1__state)
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
	private sealed class _003CDoAction_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UnityAction action;

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
		public _003CDoAction_003Ed__16(int _003C_003E1__state)
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
	private sealed class _003CDoIntervalAction_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float initialDelay;

		public UnityAction action;

		public float interval;

		public Func<bool> condition;

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
		public _003CDoIntervalAction_003Ed__14(int _003C_003E1__state)
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

	public static Color HexToColor(string hex)
	{
		return default(Color);
	}

	public static void ForEach<T>(this IEnumerable<T> ie, UnityAction<T, int> action)
	{
	}

	public static void ForEach<T>(this IEnumerable<T> ie, UnityAction<T> action)
	{
	}

	public static T FindChildByName<T>(this Transform i_Parent, string i_Name) where T : Component
	{
		return null;
	}

	public static void ActivateRandomChild(this Transform i_Parent, bool i_SingleActive = false)
	{
	}

	public static T GetRandomElement<T>(this List<T> i_List)
	{
		return default(T);
	}

	public static T GetRandomElement<T>(this T[] i_Array)
	{
		return default(T);
	}

	public static void SetChildActive(this Transform transform, int index = -1, bool value = false)
	{
	}

	public static int GetActiveChildCount(this Transform transform)
	{
		return 0;
	}

	public static void SetOnlyActive(this Transform transform, bool value = false)
	{
	}

	public static List<T> FindObjectsOfInterface<T>(this Transform rootObject) where T : class
	{
		return null;
	}

	public static void DelayedAction(this MonoBehaviour mono, UnityAction action, float delay)
	{
	}

	[IteratorStateMachine(typeof(_003CDoAction_003Ed__12))]
	private static IEnumerator DoAction(float delay, UnityAction action)
	{
		return null;
	}

	public static void DelayedIntervalAction(this MonoBehaviour mono, UnityAction action, float interval, float initialDelay, Func<bool> condition)
	{
	}

	[IteratorStateMachine(typeof(_003CDoIntervalAction_003Ed__14))]
	private static IEnumerator DoIntervalAction(MonoBehaviour mono, UnityAction action, float interval, float initialDelay, Func<bool> condition)
	{
		return null;
	}

	public static void SkipFrame(this MonoBehaviour mono, UnityAction action)
	{
	}

	[IteratorStateMachine(typeof(_003CDoAction_003Ed__16))]
	private static IEnumerator DoAction(UnityAction action)
	{
		return null;
	}

	public static void RemoveAllChildren(this GameObject gameObject)
	{
	}

	public static void DisabeAllChildren(this GameObject gameObject)
	{
	}

	public static void Off(this GameObject val)
	{
	}

	public static void On(this GameObject val)
	{
	}

	public static void SetOnlyActive(this Transform transform)
	{
	}

	public static void UnloadScene(string sceneNameToRemove)
	{
	}

	public static bool InRange(this int key, int start, int end)
	{
		return false;
	}

	public static bool InRange<T>(this int key, List<T> list)
	{
		return false;
	}

	public static bool InRange<T>(this int key, T[] array)
	{
		return false;
	}

	public static T GetLast<T>(this List<T> list)
	{
		return default(T);
	}

	public static Vector3 ParseVector3(string data)
	{
		return default(Vector3);
	}
}
