using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    private CGridDrivenContentsGenerator generator;
    private GameObject shop;

    float x;
    float y;

    private void Awake()
    {
        shop = GameObject.Find("Shop").gameObject;
    }

    void Start ()
    {
        generator = LevelManager.instance.GridGenerator;
        generator.StartGenerator();
        CGameManager.instance.playerObject.transform.position = generator.PlayerStartPosition;
        shop.transform.position = generator.PlayerStartPosition;
    }
}
