using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestPlayerSkillInventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    public ESlot slot;

    private Vector3 startPosition;
    private Vector3 mousePosition;
    private GameObject slotQ;
    private GameObject slotE;

    private void Awake()
    {
        slotQ = this.transform.parent.transform.parent.GetChild(0).transform.GetChild(0).gameObject;
        slotE = this.transform.parent.transform.parent.GetChild(1).transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (this.gameObject.GetComponent<Image>().sprite == null)
        {
            this.gameObject.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        }
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

            //Skill Change (Test 날림으로 만듬)
            if (slot == ESlot.Q && this.GetComponent<Image>().sprite.name == "NullSkill")
            {
                CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(dragSprite), 0);              
            }
            else if(slot == ESlot.E && this.GetComponent<Image>().sprite.name == "NullSkill")
            {
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(dragSprite), 1);
            }
            else if(slot == ESlot.Q && slotQ.GetComponent<Image>().sprite.name != "NullSkill" && slotE.GetComponent<Image>().sprite.name != "NullSkill")
            {
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(dragSprite), 0);
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(enterSprite), 1);
            }
            else if (slot == ESlot.E && slotQ.GetComponent<Image>().sprite.name != "NullSkill" && slotE.GetComponent<Image>().sprite.name != "NullSkill")
            {
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(dragSprite), 1);
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(enterSprite), 0);
            }

        this.GetComponent<Image>().sprite = dragSprite;
        eventData.pointerDrag.transform.gameObject.GetComponent<Image>().sprite = enterSprite;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (slotQ.GetComponent<Image>().sprite.name == "NullSkill")
        {
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(slotQ.GetComponent<Image>().sprite), 0);
        }

        if (slotE.GetComponent<Image>().sprite.name == "NullSkill")
        {
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSlot(CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().FindSkillToSkillIcon(slotE.GetComponent<Image>().sprite), 1);
        }

        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.localPosition = new Vector3(0, 0);
    }

    public enum ESlot
    {
        Q,
        E,
        Nothing
    }
}
