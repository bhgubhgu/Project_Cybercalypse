using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStairChecker : ATileTypeChecker {

    protected override void CheckTileType()
    {
        foreach(KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            if(!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)))
            { 
                // Left Stair
                if(tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y - 1)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x + 2, tile.Key.y - 1)) &&
                    !tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y - 2)))
                {
                    objectTileDict.Add(new Vector2Int(tile.Key.x, tile.Key.y - 1), ETileType.LeftStair);
                } // Right Stair
                else if(tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y - 1)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x - 2, tile.Key.y - 1)) &&
                    !tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y - 2)))
                {
                    objectTileDict.Add(new Vector2Int(tile.Key.x, tile.Key.y - 1), ETileType.RightStair);
                }
            }
        }
    }
}
