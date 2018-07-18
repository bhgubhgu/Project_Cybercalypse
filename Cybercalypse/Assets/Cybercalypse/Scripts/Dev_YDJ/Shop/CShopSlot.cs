using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CShopSlot : MonoBehaviour, IPointerClickHandler {

    private Transform _item;
    private Transform _transform;

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

    // Use this for initialization
    void Start ()
    {
        CachingTransform = GetComponent<Transform>();
        Item = transform.GetChild(0);
	}

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click the Slot of Shop");
        CShop.SellItem(gameObject);
        throw new System.Exception();
    }
}