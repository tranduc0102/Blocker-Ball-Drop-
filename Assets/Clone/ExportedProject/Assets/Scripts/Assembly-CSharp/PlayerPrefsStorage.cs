public class PlayerPrefsStorage : IStorageProvider
{
	public void SaveString(string key, string value)
	{
	}

	public string LoadString(string key, string def = "")
	{
		return null;
	}

	public bool HasKey(string key)
	{
		return false;
	}

	public void DeleteKey(string key)
	{
	}
}
