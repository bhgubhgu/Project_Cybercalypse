using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestShop : MonoBehaviour
{
    private GameObject shopInventory;
    private GameObject playerInventory;
    private GameObject playerSelect;
    public static bool isShopOpen;

    private void Awake()
    {
        shopInventory = GameObject.Find("Shop Inventory").gameObject;
        playerInventory = GameObject.Find("Player Inventory").gameObject;
        playerSelect = GameObject.Find("Select").gameObject;
    }

    private void Start()
    {
        shopInventory.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            isShopOpen = false;
            shopInventory.SetActive(false);
            playerInventory.SetActive(false);
            playerSelect.SetActive(false);

            TestShop.isShopOpen = false;
            TestPlayerInventoryOnOff.isOnInventory = false;

            this.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (Physics2D.OverlapCircle(this.transform.position, 0.1f, 1 << 9))
        {
            this.GetComponent<SpriteRenderer>().color = new Vector4(0, 1, 0, 1);

            if(Input.GetKeyDown(KeyCode.Z))
            {
                isShopOpen = true;
                shopInventory.SetActive(true);
                playerInventory.SetActive(true);
                playerSelect.SetActive(true);

                this.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        }
    }
}
