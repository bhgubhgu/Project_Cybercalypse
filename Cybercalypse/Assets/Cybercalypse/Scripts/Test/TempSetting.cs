using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSetting : MonoBehaviour
{
    public static bool isOnInventory;

    private GameObject playerInventory;

    private void Awake()
    {
        playerInventory = GameObject.Find("Canvas").gameObject;
    }

    private void Start()
    {
        playerInventory.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOnInventory)
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
