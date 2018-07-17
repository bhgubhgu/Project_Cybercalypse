using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGTestScript : MonoBehaviour
{
    private CGridDrivenContentsGenerator generator;

    float x;
    float y;

    void Start()
    {
        generator = LevelManager.instance.GridGenerator;
        generator.StartGenerator();
    }
}