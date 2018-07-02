using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestShop : MonoBehaviour
{
    private Vector3 startPosition;
    private GameObject shopInventory;
    private GameObject playerInventory;
    public static bool isShopOpen;

    public static Dictionary<Sprite, float> skillChangeDictionary = new Dictionary<Sprite, float>(); // 1,2,3,4,5,6 번 슬롯은 스킬 (지울것)
    public static Dictionary<Sprite, float> abiliyChangeDictionary = new Dictionary<Sprite, float>(); // 7,8,9 번 슬롯은 어빌리티 (지울것)

    private void Awake()
    {
        shopInventory = GameObject.Find("Shop Inventory").gameObject;
        playerInventory = GameObject.Find("Player Inventory").gameObject;
    }

    private void Start()
    {
        shopInventory.SetActive(false);
        startPosition = this.transform.localPosition;

        skillChangeDictionary.Add(shopInventory.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite, 0);
        skillChangeDictionary.Add(shopInventory.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite, 1);
        skillChangeDictionary.Add(shopInventory.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite, 2);
        skillChangeDictionary.Add(shopInventory.transform.GetChild(3).transform.GetChild(0).GetComponent<Image>().sprite, 3);
        skillChangeDictionary.Add(shopInventory.transform.GetChild(4).transform.GetChild(0).GetComponent<Image>().sprite, 4);
        skillChangeDictionary.Add(shopInventory.transform.GetChild(5).transform.GetChild(0).GetComponent<Image>().sprite, 5);

        abiliyChangeDictionary.Add(shopInventory.transform.GetChild(6).transform.GetChild(0).GetComponent<Image>().sprite, 0);
        abiliyChangeDictionary.Add(shopInventory.transform.GetChild(7).transform.GetChild(0).GetComponent<Image>().sprite, 1);
        abiliyChangeDictionary.Add(shopInventory.transform.GetChild(8).transform.GetChild(0).GetComponent<Image>().sprite, 2);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("wow");
    }
}
