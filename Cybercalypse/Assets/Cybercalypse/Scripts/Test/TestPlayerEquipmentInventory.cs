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
    private GameObject select;

    private void Awake()
    {
        select = GameObject.Find("Select").gameObject;
    }

    private void OnMouseOver()
    {
        select.transform.SetAsFirstSibling();
    }

    private void OnMouseExit()
    {
        select.transform.SetAsLastSibling();
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

        select.transform.SetAsLastSibling();
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.localPosition = new Vector3(0, 0);
    }


    public void SetItemUseKeyBoard(GameObject inventoryItem)
    {
        Sprite dragSprite = inventoryItem.transform.GetChild(0).GetComponent<Image>().sprite;

        if (slot == EEquipmentSlot.Weapon && this.GetComponent<Image>().sprite.name == "NullWeapon")
        {
            if (!CGameManager.instance.testWeaponList.Contains(dragSprite))
            {
                return;
            }

            CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 0);
            inventoryItem.transform.GetChild(0).GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
            this.GetComponent<Image>().sprite = dragSprite;
            return;
        }
        else if (slot == EEquipmentSlot.Armor && this.GetComponent<Image>().sprite.name == "NullArmor")
        {
            if (!CGameManager.instance.testArmorList.Contains(dragSprite))
            {
                return;
            }

            CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(dragSprite), 1);
            inventoryItem.transform.GetChild(0).GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
            this.GetComponent<Image>().sprite = dragSprite;
            return;
        }

    }

    public void ResetItemUseKeyBoard(GameObject emptyInventorySlot)
    {
        if (emptyInventorySlot == null)
        {
            return;
        }

        Sprite dragSprite;
        Sprite enterSprite;

        if (slot == EEquipmentSlot.Weapon && this.GetComponent<Image>().sprite.name != "NullWeapon")
        {
            dragSprite = this.GetComponent<Image>().sprite;
            enterSprite = CGameManager.instance.testWeaponList[0];

            if (!CGameManager.instance.testWeaponList.Contains(dragSprite))
            {
                return;
            }

            CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(enterSprite), 0);
            emptyInventorySlot.transform.GetChild(0).GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
            this.GetComponent<Image>().sprite = enterSprite;
            return;
        }
        else if (slot == EEquipmentSlot.Armor && this.GetComponent<Image>().sprite.name != "NullArmor")
        {
            dragSprite = this.GetComponent<Image>().sprite;
            enterSprite = CGameManager.instance.testArmorList[0];

            if (!CGameManager.instance.testArmorList.Contains(dragSprite))
            {
                return;
            }

            CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().ChangeSlot(CGameManager.instance.equipmentLibrary.GetComponent<CEquiptmentLibrary>().FindEquipmentToEquipmentIcon(enterSprite), 1);
            emptyInventorySlot.transform.GetChild(0).GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
            this.GetComponent<Image>().sprite = enterSprite;
            return;
        }
    }

    public enum EEquipmentSlot
    {
        Weapon,
        Armor,
        Nothing
    }
}
