public interface IStorageProvider
{
	void SaveString(string key, string value);

	string LoadString(string key, string defaultValue = "");

	bool HasKey(string key);

	void DeleteKey(string key);
}
