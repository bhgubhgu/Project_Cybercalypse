using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerInventoryOnOff : MonoBehaviour
{
    private GameObject playerInventory;
    private bool isOnInventory;
    private bool isOpenSkillInventory;

    private void Awake()
    {
        playerInventory = GameObject.Find("Player Inventory").gameObject;
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
            isOnInventory = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOnInventory)
        {
            playerInventory.SetActive(false);
            isOnInventory = false;
        }
    }
}
