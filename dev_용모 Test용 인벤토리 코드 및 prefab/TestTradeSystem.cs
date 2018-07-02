using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTradeSystem : SingleTonManager<TestTradeSystem>
{
    private GameObject playerInventory;
    private GameObject shopInventory;

    private Dictionary<Sprite, float> shopObjectDictionary;

    private new void Awake()
    {
        base.Awake();
        shopObjectDictionary = new Dictionary<Sprite, float>();
        playerInventory = GameObject.Find("Player Inventory").gameObject;
        shopInventory = GameObject.Find("Shop Inventory").gameObject;
    }

    private void Start()
    {
        for (int i = 0; i < shopInventory.transform.childCount; i++)
        {
            shopObjectDictionary.Add(shopInventory.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<Image>().sprite, 500f);
        }
    }

    public void Trade(Sprite tradesprite, float checkObject)
    {
        if(checkObject == 0) //Player Sell
        {
            if(tradesprite == null)
            {
                return;
            }
            else
            {
                TestMoneyCheck.money += shopObjectDictionary[tradesprite];
            }
        }
        else //Player Buy
        {
            if (tradesprite == null)
            {
                return;
            }
            else
            {
                if(TestMoneyCheck.money <= 0.0f)
                {
                    return;
                }
                else
                {
                    TestMoneyCheck.money -= shopObjectDictionary[tradesprite];
                }
            }
        }
    }
}
