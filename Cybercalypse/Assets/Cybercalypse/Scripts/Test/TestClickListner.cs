using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClickListner : MonoBehaviour
{
    private GameObject weaponCategorySlot;
    private GameObject armorCategorySlot;
    private GameObject skillCategorySlot;
    private GameObject abilityCategorySlot;

    private void Awake()
    {
        weaponCategorySlot = GameObject.Find("Weapon Category Inventory").gameObject;
        armorCategorySlot = GameObject.Find("Armor Category Inventory").gameObject;
        skillCategorySlot = GameObject.Find("Skill Category Inventory").gameObject;
        abilityCategorySlot = GameObject.Find("Ability Category Inventory").gameObject;
    }

    private void Start()
    {
        if(!TestShop.isShopOpen)
        {
            weaponCategorySlot.SetActive(false);
            armorCategorySlot.SetActive(false);
            skillCategorySlot.SetActive(false);
            abilityCategorySlot.SetActive(false);
        }
    }

    private void Update()
    {
        if(TestShop.isShopOpen)
        {
            weaponCategorySlot.SetActive(false);
            armorCategorySlot.SetActive(false);
            skillCategorySlot.SetActive(false);
            abilityCategorySlot.SetActive(false);
        }
    }

    private void OnDisable()
    {
        weaponCategorySlot.SetActive(false);
        armorCategorySlot.SetActive(false);
        skillCategorySlot.SetActive(false);
        abilityCategorySlot.SetActive(false);
    }

    public void WeaponClick()
    {
        if (!TestShop.isShopOpen)
        {
            weaponCategorySlot.SetActive(true);
            armorCategorySlot.SetActive(false);
            skillCategorySlot.SetActive(false);
            abilityCategorySlot.SetActive(false);
        }
    }

    public void ArmorClick()
    {
        if (!TestShop.isShopOpen)
        {
            armorCategorySlot.SetActive(true);
            weaponCategorySlot.SetActive(false);
            skillCategorySlot.SetActive(false);
            abilityCategorySlot.SetActive(false);
        }
    }

    public void SkillClick()
    {
        if (!TestShop.isShopOpen)
        {
            skillCategorySlot.SetActive(true);
            weaponCategorySlot.SetActive(false);
            armorCategorySlot.SetActive(false);
            abilityCategorySlot.SetActive(false);
        }
    }

    public void AbilityClick()
    {
        if (!TestShop.isShopOpen)
        {
            abilityCategorySlot.SetActive(true);
            weaponCategorySlot.SetActive(false);
            armorCategorySlot.SetActive(false);
            skillCategorySlot.SetActive(false);
        }
    }

}
