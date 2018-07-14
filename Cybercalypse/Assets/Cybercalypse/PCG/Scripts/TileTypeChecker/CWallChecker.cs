using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWallChecker : ATileTypeChecker {

    protected override void CheckTileType()
    {
        foreach (KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            // LeftWall 패턴 검사
            if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) &&  // 겹치는것을 방지
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)))
            {
                if(!tileDict.ContainsKey(new Vector2Int(tile.Key.x - 2, tile.Key.y)))
                {
                    objectTileDict.Add(new Vector2Int(tile.Key.x - 1, tile.Key.y), ETileType.LeftWall);
                }
                else
                {
                    objectTileDict.Add(new Vector2Int(tile.Key.x - 1, tile.Key.y), ETileType.BothWall);
                }
            }
            // RightWall 패턴 검사
            if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)))
            {
                if(!tileDict.ContainsKey(new Vector2Int(tile.Key.x + 2, tile.Key.y)))
                {
                    objectTileDict.Add(new Vector2Int(tile.Key.x + 1, tile.Key.y), ETileType.RightWall);
                }
                else
                {
                    objectTileDict.Add(new Vector2Int(tile.Key.x + 1, tile.Key.y), ETileType.BothWall);
                }
            }
        }
    }
}
