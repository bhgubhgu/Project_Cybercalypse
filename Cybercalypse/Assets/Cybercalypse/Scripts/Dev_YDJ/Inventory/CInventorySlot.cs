using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    private bool _isEmpty;
    private Transform _item;
    private Transform _transform;
    private int _siblingIndex;

    public bool IsEmpty
    {
        get { return _isEmpty; }
        set { _isEmpty = value; }
    }

    public Transform Item
    {
        get { return _item; }
        set { _item = value; }
    }

    public Transform Transform
    {
        get { return _transform; }
        private set { _transform = value; }
    }

    public int SiblingIndex
    {
        get { return _siblingIndex; }
        private set { _siblingIndex = value; }
    }

    private void Start()
    {
        Transform = GetComponent<Transform>();
        Item = Transform.GetChild(0);    //!< InventorySlot의 Item에 대한 참조
        if(Item.GetComponent<Image>().sprite.Equals(null))
        {
            IsEmpty = true;
        }
        else /*if(!Item.GetComponent<Image>().sprite.Equals(null))*/
        {
            IsEmpty = false;
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //if(Transform.parent.parent.name.Equals("Panel_Inventory"))
        SiblingIndex = Transform.GetSiblingIndex();
        Transform.SetAsLastSibling();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (IsEmpty)
            return;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Item.position = new Vector3(mousePosition.x, mousePosition.y);
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        //AItem draggingItem;
        //switch (eventData.pointerDrag.transform.GetChild(0).tag)
        //{
        //    case "Weapon":
        //        break;
        //    case "Armor":
        //        break;
        //    case "Consumable":
        //        break;
        //    case "Skill":
        //        draggingItem = eventData.pointerDrag.transform.GetChild(0).GetComponent<CSkill>();
        //        //SwapData(draggingItem, Item);
        //        //SwapData<CSkill>()
        //        Item.GetComponent<Image>().sprite = Item.GetComponent<CSkill>().ItemIcon;
        //        break;
        //    case "Ability":
        //        draggingItem = eventData.pointerDrag.transform.GetChild(0).GetComponent<CAbility>();
        //        //SwapData(draggingItem, Item);
        //        Item.GetComponent<Image>().sprite = Item.GetComponent<CAbility>().ItemIcon;
        //        break;
        //    default:
        //        break; 
        //}

        //!< draggingImage = 드래그중인 Item의 Image 컴포넌트
        //var draggingImage = eventData.pointerDrag.transform.GetChild(0).GetComponent<Image>();
        //!< origin = Drop을 감지한 슬롯 속 Item의 Image 컴포넌트

        var targetSlot = eventData.pointerDrag.GetComponent<CInventorySlot>();

        if (targetSlot.IsEmpty)
            return;

        var draggingImage = targetSlot.Item.GetComponent<Image>();
        var original = Item.GetComponent<Image>();
        var temp = original.sprite;
        
        original.sprite = draggingImage.sprite;
        draggingImage.sprite = temp;
        
        var empty = IsEmpty;
        IsEmpty = targetSlot.IsEmpty;
        targetSlot.IsEmpty = empty;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Item.localPosition = Vector3.zero;
        Transform.SetSiblingIndex(SiblingIndex);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        //!< 상점이 열려있을때만 아이템을 팔 수 있도록 조건문을 걸어줄 것
        CInventory.SellItem(this);
    }
}

/* 템플릿 형식의 스크립트 속성들을 Swap 하는 함수
 * void SwapData<T>(T original, Transform destination) where T : AItem
    {
        System.Type type = original.GetType();
        //T copy = new T();
        var copy = new CSkill();
        Debug.Log(copy.GetType());
        var target = destination.GetComponent(type);
        System.Reflection.PropertyInfo[] properties = type.GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            if (properties[i].DeclaringType.Equals(copy.GetType()))
            {
                properties[i].SetValue(copy, properties[i].GetValue(original));
                properties[i].SetValue(original, properties[i].GetValue(target));
                properties[i].SetValue(target, properties[i].GetValue(copy));
            }
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        var itemImage = Item.GetComponent<Image>();


        throw new System.NotImplementedException();
    }
 */
