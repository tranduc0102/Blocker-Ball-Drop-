using System;

public class TenjinAdMobIntegration
{
	private static bool _subscribed_banner;

	private static bool _subscribed_rewarded;

	private static bool _subscribed_interstitial;

	private static bool _subscribed_rewarded_interstitial;

	public static void ListenForBannerViewImpressions(object bannerView, string adUnitId, Action<string> callback)
	{
	}

	public static void ListenForRewardedAdImpressions(object rewardedAd, string adUnitId, Action<string> callback)
	{
	}

	public static void ListenForInterstitialAdImpressions(object interstitialAd, string adUnitId, Action<string> callback)
	{
	}

	public static void ListenForRewardedInterstitialAdImpressions(object rewardedInterstitialAd, string adUnitId, Action<string> callback)
	{
	}
}
