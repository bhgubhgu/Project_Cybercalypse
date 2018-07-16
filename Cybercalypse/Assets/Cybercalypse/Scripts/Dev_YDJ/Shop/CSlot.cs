using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CSlot : MonoBehaviour, IPointerClickHandler {

    public bool IsEmpty { get; set; }

    private void Awake()
    {
        
    }
    private void Start()
    {
        //!< Slot의 자식에 Item이 없으면 empty상태, 있으면 false

        Transform itemTransform = transform.GetChild(0);
        string tag = itemTransform.tag;
        AItem item;
        switch (tag)
        {
            case "Skill":
                item = itemTransform.GetComponent<CSkill>();

                break;
            case "Ability":
                item = itemTransform.GetComponent<CAbility>();
                
                break;
            default:
                break;
        }

        if (itemTransform.GetComponent<UnityEngine.UI.Image>().sprite.name.Equals("UISprite"))
        { IsEmpty = true; }
        else
        { IsEmpty = false; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click Slot");
        CShop.SellItem(transform.GetChild(0).gameObject);
        //CInventory.AddItem(transform.tag, gameObject);
        //CShop.BuyItem(transform.GetChild(0).gameObject);
        //CInventory.AddItem(transform.GetChild(0).tag, transform.GetChild(0).gameObject);
        //CInventory.AddItem(GetComponent<AItem>().ItemCategory);
        //throw new System.NotImplementedException();

        //!< 
    }
}
