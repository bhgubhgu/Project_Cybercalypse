using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGroundBoxChecker : ATileTypeChecker {

    protected override void CheckTileType()
    {
        foreach(KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            // GroundBox가 배치되는 패턴 조건 검사
            if(!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)))
            {
                if(!tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y + 1)) ||
                    !tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y + 1)))
                {
                    objectTileDict.Add(new Vector2Int(tile.Key.x, tile.Key.y - 1), ETileType.GroundBox);
                }
            }
        }
    }
}
