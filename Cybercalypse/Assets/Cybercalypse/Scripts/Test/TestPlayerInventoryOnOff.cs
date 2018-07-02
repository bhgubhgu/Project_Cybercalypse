using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerInventoryOnOff : MonoBehaviour
{
    private GameObject playerInventory;
    private GameObject skillInventory;
    private bool isOnInventory;
    private bool isOpenSkillInventory;

    private void Awake()
    {
        playerInventory = GameObject.Find("Player Inventory").gameObject;
        skillInventory = GameObject.Find("Skill Inventory").gameObject;
    }

    private void Start()
    {
        playerInventory.SetActive(false);
        skillInventory.SetActive(false);
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
        else if (Input.GetKeyDown(KeyCode.K) && !isOpenSkillInventory)
        {
            skillInventory.SetActive(true);
            isOpenSkillInventory = true;
        }
        else if (Input.GetKeyDown(KeyCode.K) && isOpenSkillInventory)
        {
            skillInventory.SetActive(false);
            isOpenSkillInventory = false;
        }
    }
}
