using System;
using UnityEngine;

public class RemoteConfigsHandler : MonoBehaviour
{
	[Serializable]
	private struct AdDetails
	{
		public int BannerUnlock;

		public int InterstitialUnlock;

		public int RewardedReviveUnlock;

		public int RewardedCoinsUnlock;

		public bool bannerPersist;

		public int adHolderUnlockLevel;

		public int[] adUnlockLevels;
	}

	[Serializable]
	private struct TierData
	{
		public string[] tier1;

		public string[] tier2;
	}

	[Serializable]
	private struct TierDataWrapper
	{
		public string[] tier1;

		public string[] tier2;
	}

	[Serializable]
	public struct ShopItem
	{
		public string type;

		public int coins;

		public string id;

		public float packPrice;

		public float amount;

		public bool? no_ads;

		public int? hammer;

		public int? time_freeze;

		public int? magnet;

		public bool? has_lives;
	}

	[Serializable]
	private struct ShopItemsWrapper
	{
		public ShopItem[] shop_items_tier1;

		public ShopItem[] shop_items_tier2;
	}

	public bool isRemoteConfigFetched;

	public static RemoteConfigsHandler Instance { get; private set; }

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnRemoteConfigsUpdated()
	{
	}

	private void ResetNow(TierData tierData, AdDetails adDetailsTier1, AdDetails adDetailsTier2)
	{
	}
}
