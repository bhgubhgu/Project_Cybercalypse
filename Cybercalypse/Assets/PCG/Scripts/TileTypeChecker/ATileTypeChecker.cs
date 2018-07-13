using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATileTypeChecker : MonoBehaviour {
    // 플레이어가 다닐 수 있는 가상 공간좌표
    protected Dictionary<Vector2Int, ETileType> tileDict;
    // 게임 오브젝트가 추가된 가상 공간좌표
    protected Dictionary<Vector2Int, ETileType> objectTileDict;


    protected abstract void CheckTileType();

    public CDungeonGenerator.TileTypeCheckerMethod AddTileType(Dictionary<Vector2Int, ETileType> _tileDict, Dictionary<Vector2Int, ETileType> _objectTileDict)
    {
        tileDict = _tileDict;
        objectTileDict = _objectTileDict;

        return CheckTileType;
    }
}
