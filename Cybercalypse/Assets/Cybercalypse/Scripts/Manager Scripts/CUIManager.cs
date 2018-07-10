using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIManager : SingleTonManager<CUIManager> {

    public CInventory inventory;

    protected override void Awake()
    {
        base.Awake();
        inventory = GameObject.Find("Panel_Inventory").GetComponent<CInventory>();
    }

    private void Start()
    {
        //for (int i = 0; i < maxSlotNum; i++)
        //{
        //    inventoryAbilities[i] = new CInventoryAbility();
        //}

        //for (int i = 0; i < maxSlotNum; i++)
        //{
        //    inventorySkills[i] = new CInventorySkill();
        //}
    }

    public void ActivateUI()
    {

    }
}