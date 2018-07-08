using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventory : MonoBehaviour {

    public GameObject equipmentPanel;
    public GameObject consumablePanel;

    public GameObject skillPanel;
    public GameObject abilityPanel;

    private GameObject currentInventoryTab;

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInventoryActivate(GameObject targetTab)
    {
        currentInventoryTab.SetActive(false);
        currentInventoryTab = targetTab;
        currentInventoryTab.SetActive(true);
    }

    public void GetItem()
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