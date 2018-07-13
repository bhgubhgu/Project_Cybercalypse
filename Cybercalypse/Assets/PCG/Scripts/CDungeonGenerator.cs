using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDungeonGenerator : AContentsGenerator
{
    // 타일 패턴을 분석하여 적절한 타일을 배치하는 클래스 배치
    public ATileTypeChecker[] tileTypeChecker;

    // 플레이어가 다닐 수 있는 가상 공간좌표
    private Dictionary<Vector2Int, ETileType> tileDict;
    // 게임 오브젝트가 추가된 가상 공간좌표
    private Dictionary<Vector2Int, ETileType> objectTileDict;
    private Transform tileHolder;
    private float tileLength;

    // tileDict의 ETileType을 체크하고 objectTileDict에 해당하는 타일타입을 추가하는 델리게이트 변수
    public delegate void TileTypeCheckerMethod();
    private event TileTypeCheckerMethod TileTypeCheckerEvent;

    protected override void Awake()
    {
        base.Awake();
        tileHolder = new GameObject("tileHolder").transform;
        objectTileDict = new Dictionary<Vector2Int, ETileType>();
    }

    public override void GenerateContents()
    {
        tileDict = LevelManager.instance.GridGenerator.TileDict;
        tileLength = LevelManager.instance.GridGenerator.TILE_LENGTH;

        addTileType_0();
        generateGameObject_0();
        //addTileType_1();
        //generateGameObject_1();
    }

    private void generateGameObject_test()
    {
        foreach (KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            GameObject test = GameObject.Instantiate(elementDict[ETileType.Empty][0].content, new Vector3(tile.Key.x * tileLength, tile.Key.y * tileLength, 0.0f), Quaternion.identity);
            test.transform.SetParent(tileHolder);
        }
    }

    private void addTileType_0()
    {
        foreach (KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            // Ground의 경우
            if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)) &&  // 겹치는것을 방지
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)))
            {
                objectTileDict.Add(new Vector2Int(tile.Key.x, tile.Key.y - 1), ETileType.Ground);
            }

            // Ceiling의 경우
            if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 2)))
            {
                objectTileDict.Add(new Vector2Int(tile.Key.x, tile.Key.y + 1), ETileType.Ceiling);
            }

            // LeftWall의 경우
            if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y + 1)))
            {
                objectTileDict.Add(new Vector2Int(tile.Key.x - 1, tile.Key.y), ETileType.LeftWall);
            }

            // RightWall의 경우
            if (!objectTileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) &&
                !tileDict.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y + 1)))
            {
                objectTileDict.Add(new Vector2Int(tile.Key.x + 1, tile.Key.y), ETileType.RightWall);
            }
        } // Ceiling과 Ground가, LeftWall과 RIghtWall이 겹치는 문제가 발생할 수 있음

        // objectTileDict의 정보를 tileDict에 전달
        foreach (KeyValuePair<Vector2Int, ETileType> tile in objectTileDict)
        {
            tileDict.Add(tile.Key, tile.Value);
        }
    }

    private void addTileType_1()
    {
        // 이벤트 등록
        foreach (ATileTypeChecker checker in tileTypeChecker)
        {
            TileTypeCheckerEvent += checker.AddTileType(tileDict, objectTileDict);
        }


        TileTypeCheckerEvent();


        // objectTileDict의 정보를 tileDict에 전달
        foreach (KeyValuePair<Vector2Int, ETileType> tile in objectTileDict)
        {
            tileDict.Add(tile.Key, tile.Value);
        }
    }

    private void generateGameObject_0()
    {
        List<CContent> selectedList;
        GameObject instObject;

        foreach (KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            for (ETileType type = ETileType.Ground; type < ETileType.Empty; type++)
            {
                if (tile.Value == type && elementDict[type].Count != 0)
                {
                    selectedList = elementDict[type];
                    instObject = GameObject.Instantiate(selectedList[(int)Random.Range(0.0f, selectedList.Count)].content,
                        new Vector3(tile.Key.x * tileLength, tile.Key.y * tileLength, 0.0f), Quaternion.identity);
                    instObject.transform.SetParent(tileHolder);
                }
            }
        }
    }

    private void generateGameObject_1()
    {
        List<CContent> selectedList;
        GameObject instObject;

        foreach (KeyValuePair<Vector2Int, ETileType> tile in tileDict)
        {
            if (tile.Value == ETileType.Ground)
            {
                selectedList = elementDict[ETileType.Ground];
                instObject = GameObject.Instantiate(selectedList[(int)Random.Range(0.0f, selectedList.Count)].content,
                    new Vector3(tile.Key.x * tileLength, tile.Key.y * tileLength, 0.0f), Quaternion.identity);
                instObject.transform.SetParent(tileHolder);
            }
            else if (tile.Value == ETileType.Ceiling)
            {
                selectedList = elementDict[ETileType.Ceiling];
                instObject = GameObject.Instantiate(selectedList[(int)Random.Range(0.0f, selectedList.Count)].content,
                    new Vector3(tile.Key.x * tileLength, tile.Key.y * tileLength, 0.0f), Quaternion.identity);
                instObject.transform.SetParent(tileHolder);
            }
            else if (tile.Value == ETileType.LeftWall)
            {
                selectedList = elementDict[ETileType.LeftWall];
                instObject = GameObject.Instantiate(selectedList[(int)Random.Range(0.0f, selectedList.Count)].content,
                    new Vector3(tile.Key.x * tileLength, tile.Key.y * tileLength, 0.0f), Quaternion.identity);
                instObject.transform.SetParent(tileHolder);
            }
            else if (tile.Value == ETileType.RightWall)
            {
                selectedList = elementDict[ETileType.RightWall];
                instObject = GameObject.Instantiate(selectedList[(int)Random.Range(0.0f, selectedList.Count)].content,
                    new Vector3(tile.Key.x * tileLength, tile.Key.y * tileLength, 0.0f), Quaternion.identity);
                instObject.transform.SetParent(tileHolder);
            }
        }
    }
}
