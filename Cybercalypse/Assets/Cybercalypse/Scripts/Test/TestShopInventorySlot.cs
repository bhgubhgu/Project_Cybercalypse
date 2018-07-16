using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestShopInventorySlot : MonoBehaviour
{
    private GameObject playerInventory;

    private Dictionary<int, GameObject> slotDictionary;

    private void Awake()
    {
        playerInventory = GameObject.Find("Player Inventory").gameObject;
        slotDictionary = new Dictionary<int, GameObject>();
    }

    private void Start()
    {
        for(int i = 0; i < 6 ; i++) //현재 아이템 슬롯 6개
        {
            slotDictionary.Add(i, playerInventory.transform.GetChild(i).transform.GetChild(0).gameObject);
        }
    }

    private void OnMouseDown()
    {
       for (int i = 0; i < 6 ; i++) //현재 아이템 슬롯 6개
        {
            if (slotDictionary[i].GetComponent<Image>().sprite.name == "NullSkill" || slotDictionary[i].GetComponent<Image>().sprite.name == "NullAbility" || slotDictionary[i].GetComponent<Image>().sprite.name == "NullWeapon" || slotDictionary[i].GetComponent<Image>().sprite.name == "NullArmor")
            {
                TestTradeSystem.instance.Trade(this.transform.GetComponent<Image>().sprite, 1);

                if(TestTradeSystem.instance.isCantBuy)
                {
                    return;
                }
                else
                {
                    slotDictionary[i].GetComponent<Image>().sprite = this.transform.GetComponent<Image>().sprite;
                    return;
                }
            }
            else
            {
                if (i == playerInventory.transform.childCount - 2)
                {
                    return;
                }
            }
       }
    }
}
