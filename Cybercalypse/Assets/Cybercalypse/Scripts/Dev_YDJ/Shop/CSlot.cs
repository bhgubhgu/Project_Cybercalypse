using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CSlot : MonoBehaviour, IPointerClickHandler {

    public bool IsEmpty { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click Slot");
        CInventory.AddItem(transform.tag, gameObject);
        //CInventory.AddItem(GetComponent<AItem>().ItemCategory);
        throw new System.NotImplementedException();
    }
}
