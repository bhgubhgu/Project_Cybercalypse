using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProfile : MonoBehaviour
{

    private SpriteRenderer playerRenderer;
    private SpriteRenderer childRenderer;

    // Use this for initialization
    void Start()
    {
        playerRenderer = GameObject.Find("UnityChan").GetComponent<SpriteRenderer>();
        childRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //childRenderer.sprite = playerRenderer.sprite;
    }
}