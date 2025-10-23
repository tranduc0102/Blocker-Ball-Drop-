using System;
using System.Collections.Generic;

public class DebugTenjin : BaseTenjin
{
	public override void Connect()
	{
	}

	public override void Connect(string deferredDeeplink)
	{
	}

	public override void Init(string apiKey)
	{
	}

	public override void InitWithSharedSecret(string apiKey, string sharedSecret)
	{
	}

	public override void InitWithAppSubversion(string apiKey, int appSubversion)
	{
	}

	public override void InitWithSharedSecretAppSubversion(string apiKey, string sharedSecret, int appSubversion)
	{
	}

	public override void SendEvent(string eventName)
	{
	}

	public override void SendEvent(string eventName, string eventValue)
	{
	}

	public override void Transaction(string productId, string currencyCode, int quantity, double unitPrice, string transactionId, string receipt, string signature)
	{
	}

	public override void TransactionAmazon(string productId, string currencyCode, int quantity, double unitPrice, string receiptId, string userId)
	{
	}

	public override void GetDeeplink(Tenjin.DeferredDeeplinkDelegate deferredDeeplinkDelegate)
	{
	}

	public override void GetAttributionInfo(Tenjin.AttributionInfoDelegate attributionInfoDelegate)
	{
	}

	public override void OptIn()
	{
	}

	public override void OptOut()
	{
	}

	public override void OptInParams(List<string> parameters)
	{
	}

	public override void OptOutParams(List<string> parameters)
	{
	}

	public override bool OptInOutUsingCMP()
	{
		return false;
	}

	public override void OptInGoogleDMA()
	{
	}

	public override void OptOutGoogleDMA()
	{
	}

	public override void DebugLogs()
	{
	}

	public override void AppendAppSubversion(int subversion)
	{
	}

	private void ImpressionHandler(string json)
	{
	}

	public override void SubscribeAppLovinImpressions()
	{
	}

	public override void AppLovinImpressionFromJSON(string json)
	{
	}

	private void AppLovinImpressionHandler(string json)
	{
	}

	public override void SubscribeIronSourceImpressions()
	{
	}

	public override void IronSourceImpressionFromJSON(string json)
	{
	}

	private void IronSourceImpressionHandler(string json)
	{
	}

	public override void SubscribeHyperBidImpressions()
	{
	}

	public override void HyperBidImpressionFromJSON(string json)
	{
	}

	private void HyperBidImpressionHandler(string json)
	{
	}

	public override void SubscribeAdMobBannerViewImpressions(object bannerView, string adUnitId)
	{
	}

	public override void SubscribeAdMobRewardedAdImpressions(object rewardedAd, string adUnitId)
	{
	}

	public override void SubscribeAdMobInterstitialAdImpressions(object interstitialAd, string adUnitId)
	{
	}

	public override void SubscribeAdMobRewardedInterstitialAdImpressions(object rewardedInterstitialAd, string adUnitId)
	{
	}

	public override void AdMobImpressionFromJSON(string json)
	{
	}

	private void AdMobBannerViewImpressionHandler(string json)
	{
	}

	private void AdMobRewardedAdImpressionHandler(string json)
	{
	}

	private void AdMobInterstitialAdImpressionHandler(string json)
	{
	}

	private void AdMobRewardedInterstitialAdImpressionHandler(string json)
	{
	}

	public override void RegisterAppForAdNetworkAttribution()
	{
	}

	public override void UpdateConversionValue(int conversionValue)
	{
	}

	public override void UpdatePostbackConversionValue(int conversionValue)
	{
	}

	public override void UpdatePostbackConversionValue(int conversionValue, string coarseValue)
	{
	}

	public override void UpdatePostbackConversionValue(int conversionValue, string coarseValue, bool lockWindow)
	{
	}

	public override void SubscribeTopOnImpressions()
	{
	}

	public override void TopOnImpressionFromJSON(string json)
	{
	}

	private void TopOnImpressionHandler(string json)
	{
	}

	public override void RequestTrackingAuthorizationWithCompletionHandler(Action<int> trackingAuthorizationCallback)
	{
	}

	public override void SetAppStoreType(AppStoreType appStoreType)
	{
	}

	public override void SetCustomerUserId(string userId)
	{
	}

	public override string GetCustomerUserId()
	{
		return null;
	}

	public override void SetSessionTime(int time)
	{
	}

	public override void SetCacheEventSetting(bool setting)
	{
	}

	public override void SetEncryptRequestsSetting(bool setting)
	{
	}

	public override void SetGoogleDMAParameters(bool adPersonalization, bool adUserData)
	{
	}

	public override string GetAnalyticsInstallationId()
	{
		return null;
	}

	public override void CASImpressionFromJSON(string json)
	{
	}

	public override void SubscribeCASImpressions(object casManager)
	{
	}

	public override void SubscribeCASBannerImpressions(object bannerView)
	{
	}

	public override void SubscribeTradPlusImpressions()
	{
	}

	public override void TradPlusImpressionFromJSON(string json)
	{
	}

	public override void TradPlusImpressionFromAdInfo(Dictionary<string, object> adInfo)
	{
	}
}
