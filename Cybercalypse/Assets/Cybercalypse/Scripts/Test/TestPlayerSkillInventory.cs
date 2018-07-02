using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestPlayerSkillInventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Vector3 mousePosition;

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
        Sprite sprite = eventData.pointerDrag.transform.gameObject.GetComponent<Image>().sprite;
        eventData.pointerDrag.transform.gameObject.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;

        if (sprite == null)
        {
            this.GetComponent<Image>().sprite = null;
            //Skll Change
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSkillInPlayerInventory(this.GetComponent<Image>().sprite);
        }
        else
        {
            this.GetComponent<Image>().sprite = sprite;
            //Skill Change
            CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>().ChangeSkillInPlayerInventory(this.GetComponent<Image>().sprite);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.localPosition = new Vector3(0, 0);
    }
}
