using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CShopSkill : ASkill, IConvertInventorySkill, IPointerClickHandler
{
    public override string ItemName { get; set; }
    public override string ItemDesc { get; set; }
    public override Sprite ItemIcon { get; set; }
    public override Sprite ItemSubs { get; set; }
    public override EItemCategory ItemCategory { get; set; }
    public override ETalentCategory TalentCategory { get; set; }
    public override float SkillCastingTime { get; set; }
    public override float SkillCoolDown { get; set; }


    void Start()
    {
        ItemName = "";
        ItemDesc = "";
        ItemIcon = GetComponent<Image>().sprite;
        ItemCategory = EItemCategory.Talent;
        TalentCategory = ETalentCategory.Skill;
        SkillCastingTime = 1.0f;
        SkillCoolDown = 5.0f;

        //Debug.Log(ItemIcon);
    }

    private void OnMouseDown()
    {
        CUIManager.PasteComponentValues<CShopSkill>(GetComponent<CShopSkill>());
    }

    public FixInventorySkill ConvertToInventorySkill()
    {
        FixInventorySkill skill = new FixInventorySkill();

        skill.ItemName = ItemName;
        skill.ItemDesc = ItemDesc;
        //skill.ItemIcon = new Sprite();
        skill.ItemIcon = ItemIcon;
        //skill.ItemSubs = new Sprite();
        skill.ItemSubs = ItemSubs;
        //skill.ItemCategory = ItemCategory;
        skill.ItemCategory = EItemCategory.Talent;
        skill.TalentCategory = TalentCategory;
        skill.SkillCastingTime = SkillCastingTime;
        skill.SkillCoolDown = SkillCoolDown;

        //skill.InitSkill(GetComponent<CShopSkill>());

        return skill;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnMouseDown from CShopSkill");

        //CUIManager.instance.inventory.AddItem(ConvertToInventorySkill());
    }
}

