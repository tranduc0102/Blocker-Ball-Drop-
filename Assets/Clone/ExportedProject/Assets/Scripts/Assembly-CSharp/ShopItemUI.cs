using TMPro;
using UnityEngine;

public class ShopItemUI : MonoBehaviour
{
	public RemoteConfigsHandler.ShopItem itemData;

	[Header("Coins")]
	[SerializeField]
	private TextMeshProUGUI coinsQuantityText;

	[SerializeField]
	private TextMeshProUGUI coinsAmountText;

	[Header("NoAds")]
	[SerializeField]
	private TextMeshProUGUI noAdsAmountText;

	[Header("Bundle")]
	[SerializeField]
	private TextMeshProUGUI bundleCoinsText;

	[SerializeField]
	private TextMeshProUGUI bundleHammerText;

	[SerializeField]
	private TextMeshProUGUI bundleTimeFreezeText;

	[SerializeField]
	private TextMeshProUGUI bundleMagnetText;

	[SerializeField]
	private TextMeshProUGUI bundleNameText;

	[SerializeField]
	private TextMeshProUGUI bndleAmountText;

	[SerializeField]
	private GameObject noadsIcon;

	public void SetItem(RemoteConfigsHandler.ShopItem item)
	{
	}

	public void OnBuyButtonClick()
	{
	}
}
