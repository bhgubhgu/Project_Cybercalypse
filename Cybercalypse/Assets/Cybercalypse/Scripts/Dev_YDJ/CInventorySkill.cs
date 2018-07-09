using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CInventorySkill : AInventoryTalent, IDropHandler, ISwappable
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
        var target = _object.GetComponent<CInventorySkill>();

        var temp = new CInventorySkill();

        //!< 멤버 변수 값만 복사해주는 방법을 찾아 볼 것. 지금은 이것이 최선
        temp.ItemCategory = ItemCategory;
        temp.ItemName = ItemName;
        temp.ItemIcon = ItemIcon;
        temp.TalentCategory = TalentCategory;
        temp.Tooltip = Tooltip;
        temp.CoolTime = CoolTime;

        ItemCategory = target.ItemCategory;
        ItemName = target.ItemName;
        ItemIcon = target.ItemIcon;
        TalentCategory = target.TalentCategory;
        Tooltip = target.Tooltip;
        CoolTime = target.CoolTime;
        
        target.ItemCategory = temp.ItemCategory;
        target.ItemName = temp.ItemName;
        target.ItemIcon = temp.ItemIcon;
        target.TalentCategory = temp.TalentCategory;
        target.Tooltip = temp.Tooltip;
        target.CoolTime = temp.CoolTime;

        transform.GetComponent<Image>().sprite = ItemIcon;
        _object.GetComponent<Image>().sprite = target.ItemIcon;
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