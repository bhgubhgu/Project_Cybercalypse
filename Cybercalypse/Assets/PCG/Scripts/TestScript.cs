using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    private CGridDrivenContentsGenerator generator;
    // Use this for initialization

    float x;
    float y;

	void Start ()
    {
        generator = LevelManager.instance.GridGenerator;
        generator.StartGenerator();

        x = 0.9f;//(generator.StartChamberPos.x + 1) * 10 * generator.TILE_LENGTH / 2;
        y = 15.8f;//(generator.StartChamberPos.y + 1) * 10 * generator.TILE_LENGTH / 2;

        CGameManager.instance.playerObject.transform.position = new Vector3(x, y);
    }
}
