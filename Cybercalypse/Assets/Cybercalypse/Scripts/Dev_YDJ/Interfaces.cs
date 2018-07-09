using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//!< 인터페이스를 통한 느슨한 결합(강한 결합 => 의존성)
public class Interfaces {
    
}

public interface IItem
{
    AItem.EItemCategory ItemCategory { get; set; }

    string ItemName { get; set; }
    string ItemDesc { get; set; }
    Sprite ItemIcon { get; set; }
    Sprite ItemSubs { get; set; }
}

public interface IEquipment : IItem
{

}

public interface IConsumable : IItem
{

}

public interface IOther : IItem
{

}

public interface ITalent : IItem
{
    ATalent.ETalentCategory TalentCategory { get; set; }

    string Tooltip { get; set; }
}

public interface ISkill : ITalent
{
    int CoolTime { get; set; }
}

public interface IAbility : ITalent
{
    bool IsActive { get; set; }
}

public interface IItemHolder
{
    void PutItem();
}