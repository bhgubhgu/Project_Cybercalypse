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
             else
            {
                CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 0);
            }
        }
        else if (slot == EEquipmentSlot.Armor && this.GetComponent<Image>().sprite.name == "NullArmor")
        {
            if (!CGameManager.instance.testArmorList.Contains(eventData.pointerDrag.GetComponent<Image>().sprite))
            {
                return;
            }
            else
            {
                CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 1);
            }
        }


        if (slot == EEquipmentSlot.Weapon && this.GetComponent<Image>().sprite.name != "NullWeapon")
        {
            if (!CGameManager.instance.testWeaponList.Contains(eventData.pointerDrag.GetComponent<Image>().sprite))
            {
                return;
            }
            else
            {
                CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 0);
            }
        }
        else if (slot == EEquipmentSlot.Armor && this.GetComponent<Image>().sprite.name != "NullArmor")
        {
            if (!CGameManager.instance.testArmorList.Contains(eventData.pointerDrag.GetComponent<Image>().sprite))
            {
                return;
            }
            else
            {
                CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 1);
            }
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
            else
            {
                CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 0);
            }
        }
        else if (slot == EEquipmentSlot.Armor && this.GetComponent<Image>().sprite.name == "NullArmor")
        {
            if (!CGameManager.instance.testArmorList.Contains(eventData.pointerDrag.GetComponent<Image>().sprite))
            {
                return;
            }
            else
            {
                CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 1);
            }
        }

        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.localPosition = new Vector3(0, 0);
    }

    public enum EEquipmentSlot
    {
        Weapon,
        Armor,
        Nothing
    }
}
