using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestShopInventorySlot : MonoBehaviour
{
    private GameObject playerInventory;
    private Vector3 startPosition;
    private Vector3 mousePosition;

    private Dictionary<int, GameObject> slotDictionary;

    private void Awake()
    {
        playerInventory = GameObject.Find("Player Inventory").gameObject;
        slotDictionary = new Dictionary<int, GameObject>();
    }

    private void Start()
    {
        for(int i = 0; i < playerInventory.transform.childCount - 1; i++)
        {
            slotDictionary.Add(i, playerInventory.transform.GetChild(i).transform.GetChild(0).gameObject);
        }
    }

    private void OnMouseDown()
    {
       for (int i = 0; i < playerInventory.transform.childCount - 1; i++)
       {
            if (slotDictionary[i].GetComponent<Image>().sprite.name == "NullSkill" || slotDictionary[i].GetComponent<Image>().sprite.name == "NullAbility")
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
