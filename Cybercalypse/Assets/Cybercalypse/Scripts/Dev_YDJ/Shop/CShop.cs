using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CShop : MonoBehaviour {

    public GameObject equipmentPanel;
    public GameObject consumablePanel;

    public GameObject skillPanel;
    public GameObject abilityPanel;

    private GameObject currentPanel;

    private int money;

    private List<AItem> items;

    private void Awake()
    {
        equipmentPanel = GameObject.Find("Panel_Shop_Equipment");
        consumablePanel = GameObject.Find("Panel_Shop_Consumable");

        skillPanel = GameObject.Find("Panel_Shop_Skill");
        abilityPanel = GameObject.Find("Panel_Shop_Ability");
    }

    // Use this for initialization
    void Start()
    {
        consumablePanel.SetActive(false);
        skillPanel.SetActive(false);
        abilityPanel.SetActive(false);

        currentPanel = equipmentPanel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
    }

    /// <summary>
    /// Item을 구입했을때 처리할 것들을 모아놓은 함수
    /// </summary>
    /// <param name="_object"></param>
    public static void BuyItem(GameObject _object)
    {
        //!< 돈을 받고 아이템을 준다.
        
    }

    public static void SellItem<T>(T other) where T : AItem
    {
        //!< 가격 차감
        int price = 1000;
        CInventory.money -= price;

        //CInventory.AddItem(other.tag, other.gameObject);
    }

    public static void SellItem(GameObject _object)
    {
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
