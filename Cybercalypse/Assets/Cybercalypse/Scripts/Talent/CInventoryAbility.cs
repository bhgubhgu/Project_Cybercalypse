using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CInventoryAbility : AInventoryTalent, IAbility, IDropHandler
{
    public bool IsActive { get; set; }

    public override string ItemName { get; set; }

    public override string ItemDesc { get; set; }
    public override Sprite ItemIcon { get; set; }
    public override Sprite ItemSubs { get; set; }

    // Use this for initialization
    void Awake() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    /// <summary>
    /// targetAbility와 데이터를 교환하는 함수
    /// </summary>
    /// <param name="target">target이 될 Ability</param>
    public void SwapData(IAbility target)
    {
        IAbility temp = target;

        temp.ItemCategory = ItemCategory;
        temp.ItemName = ItemName;
        temp.ItemIcon = ItemIcon;
        temp.TalentCategory = TalentCategory;
        temp.Tooltip = Tooltip;
        temp.IsActive = IsActive;

        ItemCategory = target.ItemCategory;
        ItemName = target.ItemName;
        ItemIcon = target.ItemIcon;
        TalentCategory = target.TalentCategory;
        Tooltip = target.Tooltip;
        IsActive = target.IsActive;

        target.ItemCategory = temp.ItemCategory;
        target.ItemName = temp.ItemName;
        target.ItemIcon = temp.ItemIcon;
        target.TalentCategory = temp.TalentCategory;
        target.Tooltip = temp.Tooltip;
        target.IsActive = temp.IsActive;

        Debug.Log("Data Swap Complete");
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!eventData.GetType().Equals(GetType())) {
            return;
        }

        var draggingItem = eventData.pointerDrag.GetComponent<CInventoryAbility>();

        SwapData(draggingItem);
    }

    //public override void SwapData<T>(T item)
    //{
    //    if(item.GetType().Equals(GetType()))
    //    {
            
    //    }
    //}
}
