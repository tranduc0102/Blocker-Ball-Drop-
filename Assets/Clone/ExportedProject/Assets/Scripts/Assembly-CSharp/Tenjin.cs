using System;
using System.Collections.Generic;

public static class Tenjin
{
	public delegate void DeferredDeeplinkDelegate(Dictionary<string, string> deferredLinkData);

	public delegate void AttributionInfoDelegate(Dictionary<string, string> attributionInfoData);

	private static Dictionary<string, BaseTenjin> _instances;

	public static Action<int> authorizationStatusDelegate;

	public static BaseTenjin getInstance(string apiKey)
	{
		return null;
	}

	public static BaseTenjin getInstanceWithSharedSecret(string apiKey, string sharedSecret)
	{
		return null;
	}

	public static BaseTenjin getInstanceWithAppSubversion(string apiKey, int appSubversion)
	{
		return null;
	}

	public static BaseTenjin getInstanceWithSharedSecretAppSubversion(string apiKey, string sharedSecret, int appSubversion)
	{
		return null;
	}

	private static BaseTenjin createTenjin(string apiKey, string sharedSecret, int appSubversion)
	{
		return null;
	}
}
