using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CShop : MonoBehaviour, I {

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
        equipmentPanel = GameObject.Find("Panel_Equipment");
        consumablePanel = GameObject.Find("Panel_Consumable");

        skillPanel = GameObject.Find("Panel_Skill");
        abilityPanel = GameObject.Find("Panel_Ability");

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

    public void OnInventoryActivate(GameObject targetTab)
    {
        currentPanel.SetActive(false);
        currentPanel = targetTab;
        currentPanel.SetActive(true);
    }
}
