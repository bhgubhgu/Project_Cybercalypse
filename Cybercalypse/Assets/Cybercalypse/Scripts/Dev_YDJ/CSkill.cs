using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkill : ASkill
{
    private string _itemName;
    private string _itemDesc;
    private Sprite _itemIcon;
    private Sprite _itemSubs;
    private float _skillCoolDown;
    private float _skillCastingTime;
    private ETalentCategory _talentCategory;
    private EItemCategory _itemCategory;

    public override string ItemName
    {
        get { return _itemName; }
        set { _itemName = value; }
    }
    public override string ItemDesc
    {
        get { return _itemDesc; }
        set { _itemDesc = value; }
    }
    public override Sprite ItemIcon
    {
        get { return _itemIcon; }
        set { _itemIcon = value; }
    }
    public override Sprite ItemSubs
    {
        get { return _itemSubs; }
        set { _itemSubs = value; }
    }
    public override EItemCategory ItemCategory
    {
        get { return _itemCategory; }
        set { _itemCategory = value; }
    }
    public override ETalentCategory TalentCategory
    {
        get { return _talentCategory; }
        set { _talentCategory = value; }
    }
    public override float SkillCastingTime
    {
        get { return _skillCastingTime; }
        set { _skillCastingTime = value; }
    }
    public override float SkillCoolDown
    {
        get { return _skillCoolDown; }
        set { _skillCoolDown = value; }
    }

    public void InitializeSkill()
    {

    }
}
