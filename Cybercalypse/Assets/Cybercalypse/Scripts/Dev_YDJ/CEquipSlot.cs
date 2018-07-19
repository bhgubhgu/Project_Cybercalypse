using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CEquipSlot : MonoBehaviour, IDropHandler {

    private Transform _item;
    private Transform _transform;

    public Transform Item
    {
        get { return _item; }
        set { _item = value; }
    }

    public Transform Transform
    {
        get { return _transform; }
        set { _transform = value; }
    }

    // Use this for initialization
    void Start()
    {
        Transform = GetComponent<Transform>();
        Item = Transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        var targetSlot = eventData.pointerDrag.GetComponent<CInventorySlot>();
        var targetImage = targetSlot.Item.GetComponent<Image>();

        Item.GetComponent<Image>().sprite = targetImage.sprite;
        CInventory.RemoveItem(eventData.pointerDrag.GetComponent<CInventorySlot>());
        CEquipWindow.EquipItem(this, Transform.GetSiblingIndex());
    }
}