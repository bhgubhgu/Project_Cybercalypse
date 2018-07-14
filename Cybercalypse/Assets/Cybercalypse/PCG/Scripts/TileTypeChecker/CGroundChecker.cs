using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGroundChecker : ATileTypeChecker {

    protected override void CheckTileType()
    {
        foreach (KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            // Ground의 패턴과 같은지 검사
            if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)) &&  // 겹치는것을 방지
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)))
            {
                if(!tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 2)))
                {
                    objectTileDict.Add(new Vector2Int(tile.Key.x, tile.Key.y - 1), ETileType.Ground);
                }
                else
                {
                    objectTileDict.Add(new Vector2Int(tile.Key.x, tile.Key.y - 1), ETileType.GroundCeiling);
                }
            }
        } // Ceiling과 Ground가, LeftWall과 RIghtWall이 겹치는 문제가 발생할 수 있음
    }
}
