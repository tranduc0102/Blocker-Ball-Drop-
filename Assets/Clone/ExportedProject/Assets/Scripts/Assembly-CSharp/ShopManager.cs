using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
	public static ShopManager Instance;

	public bool isShopInitialised;

	public GameObject overlay;

	public GameObject shopObj;

	public GameObject paymentSuccessObj;

	public GameObject paymentFailObj;

	public TextMeshProUGUI coinBarLabel;

	public Transform shopContentParent;

	public GameObject coinPrefab;

	public GameObject noAdsPrefab;

	public GameObject bundlePrefab;

	[Header("Coins Success")]
	public GameObject coinsObj;

	public TextMeshProUGUI coinsPackAmtLabel;

	[Header("No ads")]
	public GameObject noAdsObj;

	[Header("Bundle")]
	public TextMeshProUGUI magnetAmt;

	public TextMeshProUGUI hammerAmt;

	public TextMeshProUGUI timeFreezeAmt;

	public TextMeshProUGUI bundleCoinsAmt;

	public GameObject bundleAdsObj;

	public GameObject bundleObj;

	public GameObject shopEmptyPlaceholder;

	private void Awake()
	{
	}

	public void SetStore()
	{
	}

	public void OpenShop()
	{
	}

	private void HideNoAdsIfPrem()
	{
	}

	public void RefreshShop()
	{
	}

	public void CloseShop()
	{
	}

	public void ShowPaymentSuccess(RemoteConfigsHandler.ShopItem item)
	{
	}

	private void SetSuccessPop(RemoteConfigsHandler.ShopItem item)
	{
	}

	public void ShowPaymentFail()
	{
	}

	public void ClosePaymentFeedback()
	{
	}

	public void GrantItem(RemoteConfigsHandler.ShopItem item)
	{
	}
}
