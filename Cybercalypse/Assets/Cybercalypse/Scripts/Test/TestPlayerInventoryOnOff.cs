using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerInventoryOnOff : MonoBehaviour
{
    public static bool isOnInventory;

    private GameObject playerInventory;
    private GameObject playerSelect;

    private void Awake()
    {
        playerInventory = GameObject.Find("Player Inventory").gameObject;
        playerSelect = GameObject.Find("Select").gameObject;
    }

    private void Start()
    {
        playerInventory.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && !isOnInventory)
        {
            playerInventory.SetActive(true);
            playerSelect.SetActive(true);
            isOnInventory = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOnInventory)
        {
            playerInventory.SetActive(false);
            playerSelect.SetActive(false);
            isOnInventory = false;
        }
    }
}
