using UnityEngine;

public class GameAnalytics_Manager : MonoBehaviour
{
	public static GameAnalytics_Manager Instance { get; private set; }

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void LogDesignEvent(string eventName)
	{
	}

	public void LogLevelStart(int level, int attempt)
	{
	}

	public void LogLevelComplete(int level, int attempt)
	{
	}

	private void LogLevelOnFB(int level)
	{
	}

	public void LogLevelFailed(int level, bool isOutOfTime)
	{
	}

	private void LogLevelFail(bool isOutOfTime)
	{
	}

	public void LogCoins(int amount, bool gain = true)
	{
	}

	public void LogInitialInventory()
	{
	}

	public void LogEventOnFB(string eventName)
	{
	}
}
