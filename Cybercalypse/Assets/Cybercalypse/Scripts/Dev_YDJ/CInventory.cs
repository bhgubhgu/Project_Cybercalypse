using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventory : MonoBehaviour {

    public GameObject equipmentPanel;
    public GameObject consumablePanel;

    public GameObject skillPanel;
    public GameObject abilityPanel;

    private GameObject currentInventoryTab;

    private int money;

    //!<
    //!< ---

    private CInventoryAbility[] inventoryAbilities;
    private CInventorySkill[] inventorySkills;

    public int InventoryAbilityIndex;
    public int InventorySkillIndex;

    public const int maxSlotCount = 32;

	// Use this for initialization
	void Start () {
        equipmentPanel = GameObject.Find("Panel_Equipment");
        consumablePanel = GameObject.Find("Panel_Consumable");

        skillPanel = GameObject.Find("Panel_Skill");
        abilityPanel = GameObject.Find("Panel_Ability");

        consumablePanel.SetActive(false);
        skillPanel.SetActive(false);
        abilityPanel.SetActive(false);

        currentInventoryTab = equipmentPanel;

        inventoryAbilities = new CInventoryAbility[maxSlotCount];
        inventorySkills = new CInventorySkill[maxSlotCount];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateTab(GameObject _object)
    {
        currentInventoryTab.SetActive(false);
        currentInventoryTab = _object;
        currentInventoryTab.SetActive(true);
        //DeActivateExcludeTab(_object);
    }

    public void DeActivateExcludeTab(GameObject _object)
    {

    }

    public void GetItem<T>(T _item) where T : AItem
    {
        
    }

    public void GetMoney()
    {

    }

    static public T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
}