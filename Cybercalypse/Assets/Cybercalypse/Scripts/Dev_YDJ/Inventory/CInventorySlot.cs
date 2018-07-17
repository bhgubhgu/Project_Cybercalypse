using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private bool _isEmpty;
    private Transform _item;
    private Transform _transform;

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

    public Transform CachingTransform
    {
        get { return _transform; }
        private set { _transform = value; }
    }

    private void Start()
    {
        CachingTransform = GetComponent<Transform>();
        Item = CachingTransform.GetChild(0);    //!< InventorySlot의 Item에 대한 참조
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //if(Transform.parent.parent.name.Equals("Panel_Inventory"))
            CachingTransform.SetAsLastSibling();
        Item.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        CachingTransform.position = new Vector3(mousePosition.x, mousePosition.y);
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        switch(eventData.pointerDrag.tag)
        {
            case "Weapon":
                break;
            case "Armor":
                break;
            case "Consumable":
                break;
            case "Skill":
                break;
            case "Ability":
                break;
            default:
                break;
        }
        //eventData.pointerDrag.GetComponent
        //if(eventData.pointerDrag.GetType().Equals(typeof()))
        //!< eventData는 드래그 되기 시작한 Item일 것.
        
        throw new System.NotImplementedException();
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Item.localPosition = Vector3.zero;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
