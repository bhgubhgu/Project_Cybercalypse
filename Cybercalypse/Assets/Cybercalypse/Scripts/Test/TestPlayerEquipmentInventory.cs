using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestPlayerEquipmentInventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    public EEquipmentSlot slot;

    private Vector3 startPosition;
    private Vector3 mousePosition;

    private GameObject slotEquipment1;
    private GameObject slotEquipment2;
    private GameObject slotEquipment3;

    private void Awake()
    {
        slotEquipment1 = GameObject.Find("Weapon Slot Image").gameObject; //weapon
        slotEquipment2 = GameObject.Find("Mask Slot Image").gameObject; //mask
        slotEquipment3 = GameObject.Find("Suit Slot Image").gameObject; //suit
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent.SetAsLastSibling();
        transform.parent.transform.parent.SetAsLastSibling();
        startPosition = this.transform.position;
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(mousePosition.x, mousePosition.y);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Sprite dragSprite = eventData.pointerDrag.transform.gameObject.GetComponent<Image>().sprite;
        Sprite enterSprite = eventData.pointerEnter.transform.gameObject.GetComponent<Image>().sprite;

        //Equipment Change (Test 날림으로 만듬)
        if(slot == EEquipmentSlot.Weapon && this.GetComponent<Image>().sprite.name == "NullWeapon")
        {
             if (!CGameManager.instance.testWeaponList.Contains(eventData.pointerDrag.GetComponent<Image>().sprite))
             {
                return;
             }

            CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 0);
        }
        else if (slot == EEquipmentSlot.Mask && this.GetComponent<Image>().sprite.name == "NullMask")
        {
            if (!CGameManager.instance.testMaskList.Contains(eventData.pointerDrag.GetComponent<Image>().sprite))
            {
                return;
            }

            CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 1);
        }
        else if (slot == EEquipmentSlot.Suit && this.GetComponent<Image>().sprite.name == "NullSuit")
        {
            if (!CGameManager.instance.testSuitList.Contains(eventData.pointerDrag.GetComponent<Image>().sprite))
            {
                return;
            }

            CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 2);
        }

        this.GetComponent<Image>().sprite = dragSprite;
        eventData.pointerDrag.transform.gameObject.GetComponent<Image>().sprite = enterSprite;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Sprite dragSprite = eventData.pointerDrag.transform.gameObject.GetComponent<Image>().sprite;
        Sprite enterSprite = eventData.pointerEnter.transform.gameObject.GetComponent<Image>().sprite;

        if (slot == EEquipmentSlot.Weapon && this.GetComponent<Image>().sprite.name == "NullWeapon")
        {
            if (!CGameManager.instance.testWeaponList.Contains(eventData.pointerDrag.GetComponent<Image>().sprite))
            {
                return;
            }

            CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 0);
        }
        else if (slot == EEquipmentSlot.Mask && this.GetComponent<Image>().sprite.name == "NullMask")
        {
            if (!CGameManager.instance.testMaskList.Contains(eventData.pointerDrag.GetComponent<Image>().sprite))
            {
                return;
            }

            CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 1);
        }
        else if (slot == EEquipmentSlot.Suit && this.GetComponent<Image>().sprite.name == "NullSuit")
        {
            if (!CGameManager.instance.testSuitList.Contains(eventData.pointerDrag.GetComponent<Image>().sprite))
            {
                return;
            }

            CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 2);
        }

        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.localPosition = new Vector3(0, 0);
    }

    public enum EEquipmentSlot
    {
        Weapon,
        Mask,
        Suit,
        Nothing
    }
}
