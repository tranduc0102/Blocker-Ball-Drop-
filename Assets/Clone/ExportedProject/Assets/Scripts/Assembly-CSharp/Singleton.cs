using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;

	private static bool isApplicationClosing;

	public static T Instance => null;

	protected virtual void Awake()
	{
	}
}
