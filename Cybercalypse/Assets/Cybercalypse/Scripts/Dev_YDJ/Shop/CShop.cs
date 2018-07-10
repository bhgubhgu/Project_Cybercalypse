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

    // Use this for initialization
    void Start()
    {
        equipmentPanel = GameObject.Find("Panel_Shop_Equipment");
        consumablePanel = GameObject.Find("Panel_Shop_Consumable");

        skillPanel = GameObject.Find("Panel_Shop_Skill");
        abilityPanel = GameObject.Find("Panel_Shop_Ability");
        
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

    public void ActivateTab(GameObject targetTab)
    {
        currentPanel.SetActive(false);
        currentPanel = targetTab;
        currentPanel.SetActive(true);
    }
}
