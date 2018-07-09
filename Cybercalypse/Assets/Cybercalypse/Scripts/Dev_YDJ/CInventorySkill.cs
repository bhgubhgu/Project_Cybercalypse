using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CInventorySkill : AInventoryTalent, IDropHandler
{
    public int CoolTime { get; set; }

    public override EItemCategory ItemCategory { get; set; }
    public override string ItemName { get; set; }
    public override string ItemDesc { get; set; }
    public override Sprite ItemIcon { get; set; }
    public override Sprite ItemSubs { get; set; }

    // Use this for initialization
    void Awake () {
        ItemCategory = EItemCategory.Talent;
        ItemIcon = GetComponent<Image>().sprite;
        TalentCategory = ATalent.ETalentCategory.Skill;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void SwapData(GameObject _object)
    {
       var skill = _object.GetComponent<CInventorySkill>();

        var temp = new CInventorySkill();

        //!< 멤버 변수 값만 복사해주는 방법을 찾아 볼 것. 지금은 이것이 최선
        temp.ItemCategory = ItemCategory;
        temp.ItemName = ItemName;
        temp.ItemIcon = ItemIcon;
        temp.TalentCategory = TalentCategory;
        temp.Tooltip = Tooltip;
        temp.CoolTime = CoolTime;

        ItemCategory = skill.ItemCategory;
        ItemName = skill.ItemName;
        ItemIcon = skill.ItemIcon;
        TalentCategory = skill.TalentCategory;
        Tooltip = skill.Tooltip;
        CoolTime = skill.CoolTime;
        
        skill.ItemCategory = temp.ItemCategory;
        skill.ItemName = temp.ItemName;
        skill.ItemIcon = temp.ItemIcon;
        skill.TalentCategory = temp.TalentCategory;
        skill.Tooltip = temp.Tooltip;
        skill.CoolTime = temp.CoolTime;

        transform.GetComponent<Image>().sprite = ItemIcon;
        _object.GetComponent<Image>().sprite = skill.ItemIcon;
        //Debug.Log("Data Swap Complete");
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetType().Equals(GetType()))
        {
            return;
        }

        SwapData(eventData.pointerDrag);
    }
}