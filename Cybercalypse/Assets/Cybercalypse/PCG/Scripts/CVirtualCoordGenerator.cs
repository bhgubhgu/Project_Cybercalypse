using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CVirtualCoordGenerator : MonoBehaviour
{
    // 해당 방향으로 경로를 몇번 생성할 것인지 결정
    private int numOfSimulation;
    private int chamberWidth, chamberHeight;

    // Tile을 생성하기 위해 필요한 추가 정보
    private Dictionary<Vector2Int, CChamber> chamberPosition;
    private Vector2Int startChamberPos, endChamberPos;
    // 각 Chamber에 배치된 타일 정보를 저장, 관리하는 Dictionary
    private Dictionary<Vector2Int, ETileType> tileDict;

    /// <summary>
    /// 실제 맵 생성을 요청하는 메소드
    /// </summary>
    public void GenerateVirtualCoord()
    {
        CGridDrivenContentsGenerator gridGenerator = LevelManager.instance.GridGenerator;
        // 초기화
        chamberPosition = gridGenerator.ChamberPosition;
        startChamberPos = gridGenerator.StartChamberPos;
        endChamberPos = gridGenerator.EndChamberPos;
        tileDict = gridGenerator.TileDict;
        numOfSimulation = gridGenerator.NumOfSimulation;
        chamberWidth = gridGenerator.ChamberWidth;
        chamberHeight = gridGenerator.ChamberHeight;

        operateSimulation();
    }

    private void operateSimulation()
    {
        Vector2Int currentChamber, gap; // 현재 시뮬레이션 중인 Chamber의 좌표, 다음 Chamber로 향하는 방향이 어디인지 표시
        Queue<Vector2Int> bfsQueue = new Queue<Vector2Int>(); // dfs 방식으로 Chamber를 조회하기 위한 큐
        Queue<Vector2Int> currentStartQueue = new Queue<Vector2Int>(); // 현재 Chamber에서 시뮬레이션을 할 start 좌표
        Queue<Queue<Vector2Int>> bfsStartQueue = new Queue<Queue<Vector2Int>>(); // dfs 방식으로 조회할 때, 생성된 Next Start 좌표들을 저장하기 위한 큐
        Vector2Int startPositionOfSimulation = new Vector2Int(startChamberPos.x * chamberWidth + chamberWidth / 2, startChamberPos.y * chamberHeight + chamberHeight / 2);

        //LevelManager.instance.GridGenerator.PlayerStartPosition = new Vector3(startPositionOfSimulation.x, startPositionOfSimulation.y);
        // 시뮬레이션 실행 횟수만큼 start Queue에 삽입
        for (int i = 0; i<numOfSimulation; i++)
        {
            currentStartQueue.Enqueue(new Vector2Int(startChamberPos.x * chamberWidth + chamberWidth / 2, startChamberPos.y * chamberHeight + chamberHeight / 2));
        }
        
        bfsStartQueue.Enqueue(currentStartQueue);
        bfsQueue.Enqueue(startChamberPos);

        while(bfsQueue.Count != 0)
        {
            currentStartQueue = bfsStartQueue.Dequeue();
            currentChamber = bfsQueue.Dequeue();
            
            chamberPosition[currentChamber].NextChamberPosition.ForEach(delegate (Vector2Int nextChamber)
            {
                bfsQueue.Enqueue(nextChamber);
                Queue<Vector2Int> nextStartQueue = new Queue<Vector2Int>();

                gap = nextChamber - currentChamber;

                for(int i=0; i<currentStartQueue.Count; i++)
                {
                    Vector2Int start = currentStartQueue.Dequeue();
                    currentStartQueue.Enqueue(start);
                    simulation(start, currentChamber, nextStartQueue, gap);
                }

                bfsStartQueue.Enqueue(nextStartQueue);
            });
        }
    }

    /// <summary>
    /// 시뮬레이션 수행 메소드
    /// </summary>
    /// <param name="startPos">시뮬레이션을 시작하는 좌표</param>
    /// <param name="nextStartQueue">다음 시뮬레이션의 출발점 큐</param>
    /// <param name="tilePos">해당 Chamber의 Tile위치를 저장</param>
    /// <param name="gap">어느 방향으로 시뮬레이션 해야 되는지의 기준</param>
    private void simulation(Vector2Int startPos, Vector2Int currentChamberPos, Queue<Vector2Int> nextStartQueue, Vector2Int gap)
    {
        Vector2Int[] adjacentPos = getAdjacentTilePosition(startPos, currentChamberPos, gap);

        // 도착점에 도착한 경우 (수정 필요 가능성 존재)
        if(Object.ReferenceEquals(adjacentPos, null))
        {
            nextStartQueue.Enqueue(startPos);
            return;
        }

        int index = (int)Random.Range(0.0f, adjacentPos.Length);

        if (!tileDict.ContainsKey(adjacentPos[index]))
        {
            tileDict.Add(adjacentPos[index], ETileType.Empty);
        }

        simulation(adjacentPos[index], currentChamberPos, nextStartQueue, gap);
    }

    /// <summary>
    /// start의 인접 좌표 배열을 반환
    /// </summary>
    /// <param name="start"></param>
    /// <param name="gap">nextChamberPos - startChamberPos</param>
    /// <returns></returns>
    private Vector2Int[] getAdjacentTilePosition(Vector2Int start, Vector2Int currentChamber, Vector2Int gap)
    {
        List<Vector2Int> adjTile = new List<Vector2Int>();
        List<Vector2Int> available = new List<Vector2Int>();

        int xMin = currentChamber.x * chamberWidth;
        int xMax = (currentChamber.x + 1) * chamberWidth;
        int yMin = currentChamber.y * chamberHeight;
        int yMax = (currentChamber.y + 1) * chamberHeight;

        // 가는 방향에 확률을 추가하기 위해 밑의 작업을 조금 변형할 수 있음
        if (gap.x != 0)
        {
            if (gap.x == 1)
            {
                xMax++;
                if (start.x == chamberWidth * (currentChamber.x + 1))
                {
                    return null;
                }

                adjTile.Add(new Vector2Int(start.x + 1, start.y));
            }
            else if (gap.x == -1)
            {
                xMin--;
                if (start.x == chamberWidth * currentChamber.x - 1)
                {
                    return null;
                }

                adjTile.Add(new Vector2Int(start.x - 1, start.y));
            }

            adjTile.Add(new Vector2Int(start.x, start.y + 1));
            adjTile.Add(new Vector2Int(start.x, start.y - 1));
        }
        else if (gap.y != 0)
        {
            if (gap.y == 1)
            {
                yMax++;
                if (start.y == chamberHeight * (currentChamber.y + 1))
                {
                    return null;
                }

                adjTile.Add(new Vector2Int(start.x, start.y + 1));
            }
            else if (gap.y == -1)
            {
                yMin--;
                if (start.y == chamberHeight * currentChamber.y)
                {
                    return null;
                }

                adjTile.Add(new Vector2Int(start.x, start.y - 1));
            }

            adjTile.Add(new Vector2Int(start.x + 1, start.y));
            adjTile.Add(new Vector2Int(start.x - 1, start.y));
        }

        adjTile.ForEach(delegate (Vector2Int adj)
        {
            if (adj.x >= xMin && adj.x < xMax && 
            adj.y >= yMin && adj.y < yMax)
            {
                available.Add(adj);
            }
        });

        return available.ToArray();
    }    
}
