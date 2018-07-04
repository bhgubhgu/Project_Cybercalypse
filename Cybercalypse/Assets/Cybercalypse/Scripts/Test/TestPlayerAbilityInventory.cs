using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestPlayerAbilityInventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    public EAbilitySlot slot;

    private Vector3 startPosition;
    private Vector3 mousePosition;

    private GameObject slotAbility1;
    private GameObject slotAbility2;
    private GameObject slotAbility3;

    private bool isAbilityChangeComplete;
    private bool isGetAnotherObject;

    private void Awake()
    {
        slotAbility1 = this.transform.parent.transform.parent.GetChild(2).transform.GetChild(0).gameObject;
        slotAbility2 = this.transform.parent.transform.parent.GetChild(3).transform.GetChild(0).gameObject;
        slotAbility3 = this.transform.parent.transform.parent.GetChild(4).transform.GetChild(0).gameObject;
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
        if (!CGameManager.instance.testAbilityList.Contains(eventData.pointerDrag.GetComponent<Image>().sprite))
        {
            return;
        }

        Sprite dragSprite = eventData.pointerDrag.transform.gameObject.GetComponent<Image>().sprite;
        Sprite enterSprite = eventData.pointerEnter.transform.gameObject.GetComponent<Image>().sprite;

        //Ability Change (Test 날림으로 만듬)
        if (slot == EAbilitySlot.Ability1 && this.GetComponent<Image>().sprite.name == "NullAbility")
        {
            isAbilityChangeComplete = true;
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(dragSprite), 0);
        }
        else if (slot == EAbilitySlot.Ability2 && this.GetComponent<Image>().sprite.name == "NullAbility")
        {
            Debug.Log("wow");
            isAbilityChangeComplete = true;
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(dragSprite), 1);
        }
        else if (slot == EAbilitySlot.Ability3 && this.GetComponent<Image>().sprite.name == "NullAbility")
        {
            isAbilityChangeComplete = true;
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(dragSprite), 2);
        }
        else if (slot == EAbilitySlot.Ability1 && slotAbility1.GetComponent<Image>().sprite.name != "NullAbility" && slotAbility2.GetComponent<Image>().sprite.name != "NullAbility" && slotAbility3.GetComponent<Image>().sprite.name != "NullAbility" && (eventData.pointerEnter.gameObject == slotAbility2 || eventData.pointerEnter.gameObject == slotAbility3))
        {
            isAbilityChangeComplete = true;
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(dragSprite), 0);
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(enterSprite), 1);
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(enterSprite), 2);
        }
        else if (slot == EAbilitySlot.Ability2 && slotAbility1.GetComponent<Image>().sprite.name != "NullAbility" && slotAbility2.GetComponent<Image>().sprite.name != "NullAbility" && slotAbility3.GetComponent<Image>().sprite.name != "NullAbility" &&  (eventData.pointerEnter.gameObject == slotAbility1 || eventData.pointerEnter.gameObject == slotAbility3))
        {
            isAbilityChangeComplete = true;
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(dragSprite), 1);
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(enterSprite), 0);
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(enterSprite), 2);
        }
        else if (slot == EAbilitySlot.Ability3 && slotAbility1.GetComponent<Image>().sprite.name != "NullAbility" && slotAbility2.GetComponent<Image>().sprite.name != "NullAbility" && slotAbility3.GetComponent<Image>().sprite.name != "NullAbility" && (eventData.pointerEnter.gameObject == slotAbility1 || eventData.pointerEnter.gameObject == slotAbility2))
        {
            isAbilityChangeComplete = true;
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(enterSprite), 2);
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(dragSprite), 1);
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(enterSprite), 0);
        }
        else if (slot == EAbilitySlot.Ability1 && slotAbility1.GetComponent<Image>().sprite.name != "NullAbility")
        {
            isAbilityChangeComplete = true;
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(dragSprite), 0);
        }
        else if (slot == EAbilitySlot.Ability2 && slotAbility2.GetComponent<Image>().sprite.name != "NullAbility")
        {
            isAbilityChangeComplete = true;
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(dragSprite), 1);
        }
        else if (slot == EAbilitySlot.Ability3 && slotAbility3.GetComponent<Image>().sprite.name != "NullAbility")
        {
            isAbilityChangeComplete = true;
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(dragSprite), 2);
        }
        else
        {
            isAbilityChangeComplete = false;
        }

        if (isAbilityChangeComplete)
        {
            this.GetComponent<Image>().sprite = dragSprite;
            eventData.pointerDrag.transform.gameObject.GetComponent<Image>().sprite = enterSprite;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Ability Check (Test 날림으로 만듬)
        if (slotAbility1.GetComponent<Image>().sprite.name == "NullAbility")
        {
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(slotAbility1.GetComponent<Image>().sprite), 0);
        }

        if (slotAbility2.GetComponent<Image>().sprite.name == "NullAbility")
        {
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(slotAbility2.GetComponent<Image>().sprite), 1);
        }

        if (slotAbility3.GetComponent<Image>().sprite.name == "NullAbility")
        {
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(slotAbility3.GetComponent<Image>().sprite), 2);
        }

        if (slotAbility1.GetComponent<Image>().sprite.name != "NullAbility")
        {
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(slotAbility1.GetComponent<Image>().sprite), 0);
        }

        if (slotAbility2.GetComponent<Image>().sprite.name != "NullAbility")
        {
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(slotAbility2.GetComponent<Image>().sprite), 1);
        }

        if (slotAbility3.GetComponent<Image>().sprite.name != "NullAbility")
        {
            CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().ChangeSlot(CGameManager.instance.abilityLibrary.GetComponent<CAbilityLibrary>().FindAbilityToAbilityIcon(slotAbility3.GetComponent<Image>().sprite), 2);
        }

        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.localPosition = new Vector3(0, 0);
    }

    public enum EAbilitySlot
    {
        Ability1,
        Ability2,
        Ability3,
        Nothing
    }
}
