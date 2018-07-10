using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CShopSkill : ASkill, IConvertInventorySkill
{
    public override string ItemName { get; set; }
    public override string ItemDesc { get; set; }
    public override Sprite ItemIcon { get; set; }
    public override Sprite ItemSubs { get; set; }
    public override EItemCategory ItemCategory { get; set; }
    public override ETalentCategory TalentCategory { get; set; }
    public override float SkillCastingTime { get; set; }
    public override float SkillCoolDown { get; set; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        var skill = transform.GetComponent<CShopSkill>().Clone();
        //CUIManager.instance.inventory.GetItem<Object>(skill);
        CUIManager.instance.inventory.GetItem(skill);
    }

    public ASkill Clone()
    {
        var skill = new FixInventorySkill();

        skill.ItemName = ItemName;
        skill.ItemDesc = ItemDesc;
        skill.ItemIcon = new Sprite();
        skill.ItemIcon = ItemIcon;
        skill.ItemSubs = new Sprite();
        skill.ItemSubs = ItemSubs;
        skill.ItemCategory = ItemCategory;
        skill.TalentCategory = TalentCategory;
        skill.SkillCastingTime = SkillCastingTime;
        skill.SkillCoolDown = SkillCoolDown;

        return skill;
    }

    public FixInventorySkill ConvertToInventorySkill()
    {
        var skill = new FixInventorySkill();

        skill.ItemName = ItemName;
        skill.ItemDesc = ItemDesc;
        skill.ItemIcon = new Sprite();
        skill.ItemIcon = ItemIcon;
        skill.ItemSubs = new Sprite();
        skill.ItemSubs = ItemSubs;
        skill.ItemCategory = ItemCategory;
        skill.TalentCategory = TalentCategory;
        skill.SkillCastingTime = SkillCastingTime;
        skill.SkillCoolDown = SkillCoolDown;

        return skill;
    }
}

