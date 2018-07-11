using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDungeonGenerator : AContentsGenerator {

    private Dictionary<Vector2Int, ETileType> tileDict;
    private Transform tileHolder;
    private float tileLength;

    protected override void Awake()
    {
        base.Awake();
        tileHolder = new GameObject("tileHolder").transform;
    }

    public override void GenerateContents()
    {
        tileDict = LevelManager.instance.GridGenerator.TileDict;
        tileLength = LevelManager.instance.GridGenerator.TILE_LENGTH;

        generateGameObject_test();
    }

    private void generateGameObject_test()
    {
        foreach(KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            GameObject test = GameObject.Instantiate(elementDict[ETileType.Empty][0].content, new Vector3(tile.Key.x * tileLength, tile.Key.y * tileLength, 0.0f), Quaternion.identity);
            test.transform.SetParent(tileHolder);
        }
    }

    private void addTileType()
    {
        foreach(KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            
        }
    }

    private void generateGameObject()
    {
        foreach (KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {

        }
    }
}
