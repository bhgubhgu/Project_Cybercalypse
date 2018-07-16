using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCeilingChecker : ATileTypeChecker {

    protected override void CheckTileType()
    {
        foreach(KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            if(!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 2)))
            {
                // Left Ceiling
                if(tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y + 1)) &&
                    !tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y + 1)))
                {
                    objectTileDict.Add(new Vector2Int(tile.Key.x, tile.Key.y + 1), ETileType.LeftCeiling);
                } // Right Ceiling
                else if(tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y + 1)) &&
                    !tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y + 1)))
                {
                    objectTileDict.Add(new Vector2Int(tile.Key.x, tile.Key.y + 1), ETileType.RightCeiling);
                }
                else
                {
                    objectTileDict.Add(new Vector2Int(tile.Key.x, tile.Key.y + 1), ETileType.Ceiling);
                }
            }
        }
    }
}
