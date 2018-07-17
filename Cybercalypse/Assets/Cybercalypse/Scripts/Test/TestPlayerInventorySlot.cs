using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestPlayerInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Vector3 mousePosition;
    private Sprite nullSprite;

    private void Awake()
    {
        nullSprite = this.GetComponent<Image>().sprite;
    }

    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && TestShop.isShopOpen)
        {
            if(this.GetComponent<Image>().sprite.name != "NullSkill" || this.GetComponent<Image>().sprite.name != "NullAbility")
            {
                TestTradeSystem.instance.Trade(this.GetComponent<Image>().sprite, 0);
                this.GetComponent<Image>().sprite = nullSprite;
            }
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

        if(sprite == null)
        {
            eventData.pointerDrag.transform.gameObject.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
            this.GetComponent<Image>().sprite = null;
        }
        else
        {

            eventData.pointerDrag.transform.gameObject.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
            this.GetComponent<Image>().sprite = sprite;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.localPosition = new Vector3(0, 0);
    }

    public void SellItemUseKeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.Z) && TestShop.isShopOpen)
        {
            if (this.GetComponent<Image>().sprite.name != "NullSkill" || this.GetComponent<Image>().sprite.name != "NullAbility")
            {
                TestTradeSystem.instance.Trade(this.GetComponent<Image>().sprite, 0);
                this.GetComponent<Image>().sprite = nullSprite;
            }
        }
    }
}
