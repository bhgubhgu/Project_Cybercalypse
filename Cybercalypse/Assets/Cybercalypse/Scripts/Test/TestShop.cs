using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestShop : MonoBehaviour
{
    private Vector3 startPosition;
    private GameObject shopInventory;
    private GameObject playerInventory;
    private GameObject playerTalentInventory;
    public static bool isShopOpen;

    private void Awake()
    {
        shopInventory = GameObject.Find("Shop Inventory").gameObject;
        playerInventory = GameObject.Find("Player Inventory").gameObject;
    }

    private void Start()
    {
        shopInventory.SetActive(false);
        startPosition = this.transform.localPosition;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isShopOpen = false;
            shopInventory.SetActive(false);
            playerInventory.SetActive(false);

            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnMouseEnter()
    {
       this.GetComponent<SpriteRenderer>().color = new Vector4(0, 1, 0, 1);
    }

    private void OnMouseDown()
    {
        isShopOpen = true;
        shopInventory.SetActive(true);
        playerInventory.SetActive(true);
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
    }
}
