using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeHandler : Singleton<HomeHandler>
{
	public TMP_Text currentLevel;

	public List<TMP_Text> nextLevelsText;

	public Enums.HomeTab currentTab;

	public GameObject MainMenuTabIcon;

	public GameObject SettingsTabIcon;

	public GameObject ShopTabIcon;

	public GameObject MainMenuTab;

	public GameObject SettingsTab;

	public GameObject ShopTab;

	public void Start()
	{
	}

	public void PlayGame()
	{
	}

	public void SetTabToMenu()
	{
	}

	public void SetTabToSettings()
	{
	}

	public void SetTabToShop()
	{
	}

	public void SetHomeTab()
	{
	}
}
