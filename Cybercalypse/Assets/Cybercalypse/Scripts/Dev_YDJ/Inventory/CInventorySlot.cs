using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private bool _isEmpty;
    private Transform _item;
    private Transform _transform;
    private AItem _myItem;

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

    public AItem MyItem
    {
        get { return _myItem; }
        set { _myItem = value; }
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
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Item.position = new Vector3(mousePosition.x, mousePosition.y);
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        AItem draggingItem;
        switch (eventData.pointerDrag.transform.GetChild(0).tag)
        {
            case "Weapon":
                break;
            case "Armor":
                break;
            case "Consumable":
                break;
            case "Skill":
                draggingItem = eventData.pointerDrag.transform.GetChild(0).GetComponent<CSkill>();
                SwapData(draggingItem, Item);
                //SwapData<CSkill>()
                Item.GetComponent<Image>().sprite = Item.GetComponent<CSkill>().ItemIcon;
                break;
            case "Ability":
                draggingItem = eventData.pointerDrag.transform.GetChild(0).GetComponent<CAbility>();
                SwapData(draggingItem, Item);
                Item.GetComponent<Image>().sprite = Item.GetComponent<CSkill>().ItemIcon;
                break;
            default:
                break;
        }
        //eventData.pointerDrag.GetComponent
        //if(eventData.pointerDrag.GetType().Equals(typeof()))
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Item.localPosition = Vector3.zero;

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Item.GetComponent<Image>().sprite = Item.GetComponent<CSkill>().ItemIcon;
    }

    void SwapData<T>(T original, Transform destination) where T : AItem
    {
        //!< CWeapon, CArmor, CSKill, CAbility...
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
                //Debug.Log(properties[i].GetValue(original));
                properties[i].SetValue(original, properties[i].GetValue(target));
                //Debug.Log(properties[i].GetValue(target));
                properties[i].SetValue(target, properties[i].GetValue(copy));
                //Debug.Log(properties[i].GetValue(copy));
            }
        }
    }
}
