using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMoneyCheck : MonoBehaviour
{
    public static float money;

    private void Start()
    {
        money = 5000f;    
    }

    private void Update()
    {
        this.GetComponent<Text>().text = money.ToString();
    }
}
