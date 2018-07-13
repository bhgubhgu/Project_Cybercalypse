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

    static public T PasteComponentAsNew<T>(T original, GameObject destination) where T : Component
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

    public static T PasteComponentValues<T>(T original, GameObject destination) where T : Component
    {

        System.Type type = original.GetType();
        Component copy = destination.GetComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }

        return copy as T;
    }

    public static T PasteComponentValues<T>(T original) where T : Component
    {
        GameObject destination = GameObject.Find("");
        System.Type type = original.GetType();
        Component copy = destination.GetComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }

        return copy as T;
    }
}