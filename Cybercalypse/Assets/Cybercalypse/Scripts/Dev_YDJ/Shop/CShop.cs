using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CShop : MonoBehaviour {
    
    public GameObject weaponPanel;
    public GameObject armorPanel;
    public GameObject consumablePanel;

    public GameObject skillPanel;
    public GameObject abilityPanel;

    private GameObject currentPanel;

    private int money;

    private List<AItem> items;

    private void Awake()
    {
        weaponPanel = GameObject.Find("Panel_Shop_Weapon");
        armorPanel = GameObject.Find("Panel_Shop_Armor");
        consumablePanel = GameObject.Find("Panel_Shop_Consumable");

        skillPanel = GameObject.Find("Panel_Shop_Skill");
        abilityPanel = GameObject.Find("Panel_Shop_Ability");
    }

    // Use this for initialization
    void Start()
    {
        armorPanel.SetActive(false);
        consumablePanel.SetActive(false);
        skillPanel.SetActive(false);
        abilityPanel.SetActive(false);

        currentPanel = weaponPanel;
    }

    /// <summary>
    /// Item을 구입했을때 처리할 것들을 모아놓은 함수
    /// </summary>
    /// <param name="_object"></param>
    public static void BuyItem(GameObject _object)
    {
        //!< 아이템을 받고 돈을 준다.
        int price = 1000;
        CInventory.money += price;
    }

    public static void SellItem(GameObject _object)
    {
        //!< 돈을 받고 아이템을 준다.
        int price = 1000;
        CInventory.money -= price;

        CInventory.AddItem(_object);
    }

    public void ActivateTab(GameObject targetTab)
    {
        currentPanel.SetActive(false);
        currentPanel = targetTab;
        currentPanel.SetActive(true);
    }
}
