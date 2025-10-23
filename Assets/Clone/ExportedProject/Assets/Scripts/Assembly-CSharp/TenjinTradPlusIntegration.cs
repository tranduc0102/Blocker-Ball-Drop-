using System;
using System.Collections.Generic;

public class TenjinTradPlusIntegration
{
	private static bool _subscribed_tradplus;

	public static void ListenForImpressions(Action<string> callback)
	{
	}

	public static void ImpressionFromAdInfo(Action<string> callback, Dictionary<string, object> adInfo)
	{
	}

	private static TradPlusAdImpressionData MapAndroidAdData(Dictionary<string, object> adInfo)
	{
		return null;
	}

	private static TradPlusAdImpressionData MapIosAdData(Dictionary<string, object> adInfo)
	{
		return null;
	}

	private static string FormatFromNumber(int number)
	{
		return null;
	}
}
