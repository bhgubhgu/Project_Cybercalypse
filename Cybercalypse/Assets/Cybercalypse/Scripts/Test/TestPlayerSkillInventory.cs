using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestPlayerSkillInventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    public ESkillSlot slot;

    private Vector3 startPosition;
    private Vector3 mousePosition;
    private GameObject select;

    private GameObject slotQ;
    private GameObject slotE;

    private void Awake()
    {
        select = GameObject.Find("Select").gameObject;

        slotQ = this.transform.parent.transform.parent.GetChild(0).transform.GetChild(0).gameObject;
        slotE = this.transform.parent.transform.parent.GetChild(1).transform.GetChild(0).gameObject;
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
        if(!CGameManager.instance.testSkillList.Contains(eventData.pointerDrag.GetComponent<Image>().sprite))
        {
            return;
        }

        Sprite dragSprite = eventData.pointerDrag.transform.gameObject.GetComponent<Image>().sprite;
        Sprite enterSprite = eventData.pointerEnter.transform.gameObject.GetComponent<Image>().sprite;

        //Skill Change (Test 날림으로 만듬)
            if (slot == ESkillSlot.Q && this.GetComponent<Image>().sprite.name == "NullSkill")
            {
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(dragSprite), 0);
            }
            else if (slot == ESkillSlot.E && this.GetComponent<Image>().sprite.name == "NullSkill")
            {
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(dragSprite), 1);
            }
            else if (slot == ESkillSlot.Q && slotQ.GetComponent<Image>().sprite.name != "NullSkill" && slotE.GetComponent<Image>().sprite.name != "NullSkill" && eventData.pointerEnter.gameObject == slotE)
            {
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(dragSprite), 0);
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(enterSprite), 1);
            }
            else if (slot == ESkillSlot.E && slotQ.GetComponent<Image>().sprite.name != "NullSkill" && slotE.GetComponent<Image>().sprite.name != "NullSkill" && eventData.pointerEnter.gameObject == slotQ)
            {
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(dragSprite), 1);
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(enterSprite), 0);
            }
            else if (slot == ESkillSlot.Q && slotQ.GetComponent<Image>().sprite.name != "NullSkill")
            {
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(dragSprite), 0);
            }
            else if (slot == ESkillSlot.E && slotQ.GetComponent<Image>().sprite.name != "NullSkill")
            {
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(dragSprite), 1);
            }

            this.GetComponent<Image>().sprite = dragSprite;
            eventData.pointerDrag.transform.gameObject.GetComponent<Image>().sprite = enterSprite;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Skill Check (Test 날림으로 만듬)
            if (slotQ.GetComponent<Image>().sprite.name == "NullSkill")
            {
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(slotQ.GetComponent<Image>().sprite), 0);
            }

            if (slotE.GetComponent<Image>().sprite.name == "NullSkill")
            {
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(slotE.GetComponent<Image>().sprite), 1);
            }

            if (slotQ.GetComponent<Image>().sprite.name != "NullSkill")
            {
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(slotQ.GetComponent<Image>().sprite), 0);
            }

            if (slotE.GetComponent<Image>().sprite.name != "NullSkill")
            {
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(slotE.GetComponent<Image>().sprite), 1);
            }

        select.transform.SetAsLastSibling();
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.localPosition = new Vector3(0, 0);
    }


    //키보드로 스킬 등록
    public void SetItemUseKeyBoard(GameObject inventoryItem)
    {
        Sprite dragSprite = inventoryItem.transform.GetChild(0).GetComponent<Image>().sprite;

        if (!CGameManager.instance.testSkillList.Contains(dragSprite))
        {
            return;
        }

        if (slotQ.GetComponent<Image>().sprite.name == "NullSkill")
        {
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(dragSprite), 0);
            inventoryItem.transform.GetChild(0).GetComponent<Image>().sprite = slotQ.GetComponent<Image>().sprite;
            slotQ.GetComponent<Image>().sprite = dragSprite;
            return;
        }
        else if(slotE.GetComponent<Image>().sprite.name == "NullSkill")
        {
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(dragSprite), 1);
            inventoryItem.transform.GetChild(0).GetComponent<Image>().sprite = slotE.GetComponent<Image>().sprite;
            slotE.GetComponent<Image>().sprite = dragSprite;
            return;
        }
    }

    public void ResetItemUseKeyBoard(GameObject emptyInventorySlot)
    {
        if(emptyInventorySlot == null)
        {
            return;
        }

        Sprite dragSprite;
        Sprite enterSprite = CGameManager.instance.testSkillList[0];

        if (slotQ.GetComponent<Image>().sprite.name != "NullSkill")
        {
            dragSprite = slotQ.GetComponent<Image>().sprite;
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(enterSprite), 0);
            emptyInventorySlot.transform.GetChild(0).GetComponent<Image>().sprite = dragSprite;
            slotQ.GetComponent<Image>().sprite = enterSprite;
            return;
        }
        else if (slotE.GetComponent<Image>().sprite.name != "NullSkill")
        {
            dragSprite = slotE.GetComponent<Image>().sprite;
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(enterSprite), 1);
            emptyInventorySlot.transform.GetChild(0).GetComponent<Image>().sprite = dragSprite;
            slotE.GetComponent<Image>().sprite = enterSprite;
            return;
        }
    }

    public enum ESkillSlot
    {
        Q,
        E,
        Nothing
    }
}