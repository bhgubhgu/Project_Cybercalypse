using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FixInventorySkill : ASkill, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler {

    public override string ItemName { get; set; }
    public override string ItemDesc { get; set; }
    public override Sprite ItemIcon { get; set; }
    public override Sprite ItemSubs { get; set; }
    public override EItemCategory ItemCategory { get; set; }
    public override ETalentCategory TalentCategory { get; set; }
    public override float SkillCastingTime { get; set; }
    public override float SkillCoolDown { get; set; }

    public bool IsContain { get; set; }

    // Use this for initialization
    private void Start() {
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void InitSkill(ASkill other)
    {
        //other.ItemName = ItemName;
        //other.ItemDesc = ItemDesc;
        //other.ItemIcon = ItemIcon;

        ItemName = other.ItemName;
        ItemDesc = other.ItemDesc;
        //ItemIcon = new Sprite();
        ItemIcon = other.ItemIcon;
        //ItemSubs = new Sprite();
        ItemSubs = other.ItemSubs;
        ItemCategory = other.ItemCategory;
        TalentCategory= other.TalentCategory;
        SkillCastingTime = other.SkillCastingTime;
        SkillCoolDown = other.SkillCoolDown;
    }

    private void SwapSKillData(ASkill other)
    {
        ASkill temp = new FixInventorySkill();

        temp.ItemName = ItemName;
        temp.ItemDesc = ItemDesc;
        temp.ItemIcon = ItemIcon;
        temp.ItemSubs = ItemSubs;
        temp.ItemCategory = ItemCategory;
        temp.TalentCategory = TalentCategory;
        temp.SkillCastingTime = SkillCastingTime;
        temp.SkillCoolDown = SkillCoolDown;

        ItemName = other.ItemName;
        ItemDesc = other.ItemDesc;
        ItemIcon = other.ItemIcon;
        ItemCategory = other.ItemCategory;
        TalentCategory = other.TalentCategory;
        SkillCastingTime = other.SkillCastingTime;
        SkillCoolDown = other.SkillCoolDown;

        other.ItemName = temp.ItemName;
        other.ItemDesc = temp.ItemDesc;
        other.ItemIcon = temp.ItemIcon;
        other.ItemCategory = temp.ItemCategory;
        other.TalentCategory = temp.TalentCategory;
        other.SkillCastingTime = temp.SkillCastingTime;
        other.SkillCoolDown = temp.SkillCoolDown;
    }

    private void SwapData<T>(T other)
    {
        System.Type type = other.GetType();
        Component copy = new Component();
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(other));
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent.SetAsLastSibling();

        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
