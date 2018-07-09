using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class AInventoryItem : AItem, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void SwapData<T>(ref T origin, ref T target) where T : AInventoryItem
    {   
        System.Type type = origin.GetType();
    }

    #region #_Drag관련 함수들
    //
    //!< 여기서 구현하기(State가 Storage인 개체들만 Drag 가능하게 하기)
    //!< 그러려면, 이 밑에 State가 Storage인지, Loot인지 검사하는 구문이 들어가야 함
    //!< 그러지 않기 위해서는, AItem을 StorageItem과 LootItem으로 나누는 방법이 있음
    //!< 그렇게 되면, Loot의 장비/소비/특성, Storage의 장비/소비/특성은 별개가 됨
    //!< (과연?)그러나 그것보다, Drag는 '개체'보다는 '기능'으로써, 개별 스크립트로 부착하는 형태가 나을 수 있을듯함.
    //!< 이것은 기술명세서의 클래스 설계를 다시 보면서 생각할 것, 세 가지 원칙 공부할 것.

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent.SetAsLastSibling();

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y);

        
    }
    //public void OnDrop(PointerEventData eventData)
    //{
    //    Debug.Log("Called OnDrop");

    //    var targetColor = transform.GetComponent<Image>().color;
    //    var originImage = eventData.pointerDrag.GetComponent<Image>();

    //    transform.GetComponent<Image>().color = originImage.color;

    //    originImage.color = targetColor;

    //    //!< item 버전(자식 오브젝트를 바꾸면 안되기에 컴포넌트 데이터를 교환해서 스왑할 것)
    //    var _item = eventData.pointerDrag.GetComponent<AInventoryItem>();

    //    switch (_item.CategoryOfItem)
    //    {
    //        case AItem.ItemCategory.Equipment:
    //            break;
    //        case AItem.ItemCategory.Consumable:
    //            break;
    //        case AItem.ItemCategory.Talent:
    //            switch (eventData.pointerDrag.GetComponent<AInventoryTalent>().CategoryOfTalent)
    //            {
    //                case ATalent.TalentCategory.Skill:
    //                    var _skill = eventData.pointerDrag.GetComponent<CInventorySkill>();

    //                    break;
    //                case ATalent.TalentCategory.Ability:
    //                    var _ability = eventData.pointerDrag.GetComponent<CInventoryAbility>();

    //                    break;
    //            }
    //            break;
    //    }

    //    switch (CategoryOfItem)
    //    {
    //        case AItem.ItemCategory.Equipment:
    //            break;
    //        case AItem.ItemCategory.Consumable:
    //            break;
    //        case AItem.ItemCategory.Talent:
    //            break;
    //        default:
    //            break;
    //    }

    //    //eventData.pointerDrag.GetComponent<AInventoryItem>().CategoryOfItem
    //}

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    #endregion
}