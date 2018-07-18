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
        AItem item;
        switch (eventData.pointerDrag.transform.GetChild(0).tag)
        {
            case "Weapon":
                break;
            case "Armor":
                break;
            case "Consumable":
                break;
            case "Skill":
                item = eventData.pointerDrag.transform.GetChild(0).GetComponent<CSkill>();
                SwapData(item, Item.gameObject);
                Item.GetComponent<Image>().sprite = Item.GetComponent<CSkill>().ItemIcon;
                break;
            case "Ability":
                item = eventData.pointerDrag.transform.GetChild(0).GetComponent<CAbility>();
                SwapData(item, Item.gameObject);
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

    void SwapData<T>(T original, GameObject destination) where T : AItem
    {
        System.Type type = original.GetType();
        //var boo = original;
        //Debug.Log(boo.GetType());
        var copy = new CSkill();
        var target = destination.GetComponent(type);
        System.Reflection.PropertyInfo[] properties = type.GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            if (properties[i].DeclaringType.Equals(copy.GetType()))
            {
                properties[i].SetValue(copy, properties[i].GetValue(original));
                Debug.Log(properties[i].GetValue(original));
                properties[i].SetValue(original, properties[i].GetValue(target));
                Debug.Log(properties[i].GetValue(target));
                properties[i].SetValue(target, properties[i].GetValue(copy));
                Debug.Log(properties[i].GetValue(copy));
            }
        }

        //for (int i = 0; i < properties.Length; i++)
        //{
        //    properties[i].SetValue(copy, properties[i].GetValue(original));
        //}
        //for (int i = 0; i < properties.Length; i++)
        //{
        //    properties[i].SetValue(destination, properties[i].GetValue(original));
        //}
        //for (int i = 0; i < properties.Length; i++)
        //{
        //    properties[i].SetValue(original, properties[i].GetValue(original));
        //}
    }
}
