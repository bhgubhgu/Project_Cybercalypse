using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    private CGridDrivenContentsGenerator generator;
	// Use this for initialization
	void Start () {
        generator = LevelManager.instance.GridGenerator;
        generator.InitGenerator(10, 10);
        generator.StartGenerator();
	}
}
