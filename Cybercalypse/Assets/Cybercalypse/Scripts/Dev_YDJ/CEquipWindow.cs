using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CEquipWindow : MonoBehaviour {

    public static GameObject weaponPanel;
    public static GameObject armorPanel;
    public static GameObject consumablePanel;
    public static GameObject skillPanel;
    public static GameObject abilityPanel;

    public static GameObject currentInventoryTab;

    public static Transform weaponHUD;
    public static Transform armorHUD;
    public static Transform skillHUD;
    public static Transform abilityHUD;

    private void Awake()
    {
        weaponPanel = transform.Find("Panel_Equipment_Weapon").gameObject;
        armorPanel = transform.Find("Panel_Equipment_Armor").gameObject;
        consumablePanel = transform.Find("Panel_Equipment_Consumable").gameObject;
        skillPanel = transform.Find("Panel_Equipment_Skill").gameObject;
        abilityPanel = transform.Find("Panel_Equipment_Ability").gameObject;

        weaponPanel.SetActive(false);
        armorPanel.SetActive(false);
        consumablePanel.SetActive(false);
        skillPanel.SetActive(false);
        abilityPanel.SetActive(false);

        currentInventoryTab = null;

        weaponHUD = GameObject.Find("Slot_Weapon").transform;
        armorHUD = GameObject.Find("Slot_Armor").transform;
        skillHUD = GameObject.Find("Panel_Skill").transform;
        abilityHUD = GameObject.Find("Panel_Ability").transform;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateTab(GameObject _object)
    {
        if(currentInventoryTab != null)
            currentInventoryTab.SetActive(false);
        currentInventoryTab = _object;
        currentInventoryTab.SetActive(true);
    }

    public static void EquipItem(CEquipSlot slot, int index)
    {
        //!< 장착된 아이템 종류의 / 장착된 슬롯 Index에 / 장착된 아이템 Sprite를 배치한다.
        var mytag = slot.tag;
        Debug.Log(mytag);
        switch(mytag)
        {
            case "Weapon":
                weaponHUD.GetChild(0).GetComponent<Image>().sprite 
                    = slot.Item.GetComponent<Image>().sprite;
                break;
            case "Armor":
                armorHUD.GetChild(0).GetComponent<Image>().sprite
                    = slot.Item.GetComponent<Image>().sprite;
                break;
            case "Skill":
                skillHUD.transform.GetChild(index).GetChild(0).GetComponent<Image>().sprite
                    = slot.Item.GetComponent<Image>().sprite;
                break;
            case "Ability":
                abilityHUD.transform.GetChild(index).GetChild(0).GetComponent<Image>().sprite
                    = slot.Item.GetComponent<Image>().sprite;
                break;
            default:
                return;
        }
    }
}