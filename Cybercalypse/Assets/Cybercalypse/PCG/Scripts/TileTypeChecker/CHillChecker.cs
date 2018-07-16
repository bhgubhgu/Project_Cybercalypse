using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHillChecker : ATileTypeChecker {

    protected override void CheckTileType()
    {
        foreach(KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            // Left Hill
            if(!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)) &&
                !objectTileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y - 1)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y - 1)) &&
                tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) &&
                tileDict.ContainsKey(new Vector2Int(tile.Key.x + 2, tile.Key.y - 1)) && 
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 2)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y - 2)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x + 2, tile.Key.y - 2)))
            {
                objectTileDict.Add(new Vector2Int(tile.Key.x, tile.Key.y - 1), ETileType.LeftHill_0);
                objectTileDict.Add(new Vector2Int(tile.Key.x + 1, tile.Key.y - 1), ETileType.LeftHill_1);
            } // Right Hill
            else if(!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)) &&
                !objectTileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y - 1)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y - 1)) &&
                tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) &&
                tileDict.ContainsKey(new Vector2Int(tile.Key.x - 2, tile.Key.y - 1)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 2)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y - 2)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x - 2, tile.Key.y - 2)))
            {
                objectTileDict.Add(new Vector2Int(tile.Key.x, tile.Key.y - 1), ETileType.RightHill_0);
                objectTileDict.Add(new Vector2Int(tile.Key.x - 1, tile.Key.y - 1), ETileType.RightHill_1);
            }

        }
    }
}
