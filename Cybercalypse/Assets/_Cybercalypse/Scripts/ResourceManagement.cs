using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManagement : MonoBehaviour {

    private int StarSeedNum { get; set; }
    private int EverCoinNum { get; set; }
    private int BitNum { get; set; }

    public TextMesh starSeed;
    public TextMesh everCoin;
    public TextMesh bit;

	// Use this for initialization
	void Start () {
        starSeed = transform.Find("StarSeed").GetChild(0).GetComponent<TextMesh>();
        everCoin = transform.Find("EverCoin").GetChild(0).GetComponent<TextMesh>();
        bit = transform.Find("Bit").GetChild(0).GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {

	}
}
