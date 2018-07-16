using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrivableChecker : ATileTypeChecker
{

    protected override void CheckTileType()
    {
        List<Vector2Int> platformPos = new List<Vector2Int>();

        foreach (KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            // Platform이 배치될 최소 조건
            if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y)) &&
                tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y)) &&
                !objectTileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1)) &&
                tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1)))
            {
                platformPos.Clear();


                // Platform을 왼쪽에 배치하는 경우
                if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) &&
                    !objectTileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y + 1)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y + 1)))
                {
                    // Platform을 왼쪽 대각선 위에 배치하는 경우
                    if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 2)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 2)) &&
                    !objectTileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y + 2)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y + 2)))
                    {
                        platformPos.Add(new Vector2Int(tile.Key.x - 1, tile.Key.y + 1));
                    }
                    // Platform을 왼쪽에 배치하는 경우
                    if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x - 2, tile.Key.y)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x - 2, tile.Key.y)) &&
                    !objectTileDict.ContainsKey(new Vector2Int(tile.Key.x - 2, tile.Key.y + 1)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x - 2, tile.Key.y + 1)) &&
                    !objectTileDict.ContainsKey(new Vector2Int(tile.Key.x - 2, tile.Key.y - 1)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x - 2, tile.Key.y - 1)))
                    {
                        platformPos.Add(new Vector2Int(tile.Key.x - 2, tile.Key.y));
                    }
                }

                // Platform을 오른쪽에 배치하는 경우
                if(!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) &&
                    !objectTileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y + 1)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y + 1)))
                {
                    // Platform을 오른쪽 대각선 위에 배치하는 경우
                    if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 2)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 2)) &&
                    !objectTileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y + 2)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y + 2)))
                    {
                        platformPos.Add(new Vector2Int(tile.Key.x + 1, tile.Key.y + 1));
                    }
                    // Platform을 오른쪽에 배치하는 경우
                    if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x + 2, tile.Key.y)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x + 2, tile.Key.y)) &&
                    !objectTileDict.ContainsKey(new Vector2Int(tile.Key.x + 2, tile.Key.y + 1)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x + 2, tile.Key.y + 1)) &&
                    !objectTileDict.ContainsKey(new Vector2Int(tile.Key.x + 2, tile.Key.y - 1)) &&
                    tileDict.ContainsKey(new Vector2Int(tile.Key.x + 2, tile.Key.y - 1)))
                    {
                        platformPos.Add(new Vector2Int(tile.Key.x + 2, tile.Key.y));
                    }
                }

                // Platform이 배치 가능한 곳들 중 랜덤으로 한 곳을 정해서 배치
                if (platformPos.Count != 0)
                {
                    objectTileDict.Add(platformPos[(int)Random.Range(0.0f, platformPos.Count)], ETileType.Platform);
                }
            }
        }
    }
}
