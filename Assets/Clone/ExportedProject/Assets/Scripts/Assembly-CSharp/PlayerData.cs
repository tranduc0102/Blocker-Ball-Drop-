using System;

[Serializable]
public class PlayerData
{
	public string playerId;

	public string playerName;

	public string imageUrl;

	public int currentLevel;

	public int lives;

	public int coins;

	public long heartUpdatetimestamp;

	public TimeSpan RemainingHeartTimer;

	public string lastTimeLoginIso;

	public int dataVersion;

	public bool MusicEnabled;

	public bool SfxEnabled;

	public bool HapticsEnabled;

	public bool? isFirstTimeEventLogged;

	public string installTimeIso;

	public string firstBuild;

	public string currentBuild;

	public int dayNumber;

	public int sessionNumberForTheDay;

	public string lastSessionLocalYMD;

	public int currentLevelTries;

	public bool isPremium;
}
