using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CGridDrivenContentsGenerator : MonoBehaviour
{
    // 상대 좌표에 곱할 상수
    public float TILE_LENGTH = 0.16f;
    public int numOfChamberInHorizontal;
    public int numOfChamberInVertical;
    public float possibilityOfContinuousDummy;
    public float possibilityOfDividedDummy;
    
    // 그리드 사각형 내에 어떤 그리드에 어떤 Chamber가 위치하는지 저장
    public Dictionary<Vector2Int, CChamber> ChamberPosition { get; private set; }

    // 각 Chamber에 대한 공통 정보
    public int NumOfChamberInHorizontal { get; private set; }
    public int NumOfChamberInVertical { get; private set; }

    // 출발 지점의 Chamber 상대 좌표
    public Vector2Int StartChamberPos { get; private set; }
    // 도착 지점의 Chamber 상대 좌표
    public Vector2Int EndChamberPos { get; private set; }

    public AContentsGenerator generator;

    void Awake()
    {
        ChamberPosition = new Dictionary<Vector2Int, CChamber>();
    }

    /// <summary>
    /// 생성할 맵에 대한 최소 정보 입력(Chamber의 가로 개수, Chamber의 세로 개수, 맵 구성요소 생성기)
    /// </summary>
    private void initGenerator()
    {
        NumOfChamberInHorizontal = numOfChamberInHorizontal;
        NumOfChamberInVertical = numOfChamberInVertical;


    }

    /// <summary>
    /// 맵 구동기 가동
    /// </summary>
    public void StartGenerator()
    {
        initGenerator();
        makeEssentialPath();
        makeDummyPath(StartChamberPos);

        //Vector2Int current = StartChamberPos;
        //Queue<Vector2Int> tempQueue = new Queue<Vector2Int>();
        //tempQueue.Enqueue(StartChamberPos);
        //while(tempQueue.Count != 0)
        //{
        //    current = tempQueue.Dequeue();
        //    Debug.Log(current);
        //    ChamberPosition[current].NextChamberPosition.ForEach(delegate (Vector2Int next)
        //    {
        //        tempQueue.Enqueue(next);
        //    });
        //}

        //Debug.Log("Essential Path @@@");
        //foreach(KeyValuePair<Vector2Int, CChamber> pair in ChamberPosition)
        //{
        //    if (pair.Value.ChamberType == EChamberType.Essential)
        //    {
        //        Debug.Log(pair.Key);
        //    }
        //}
        //Debug.Log("Dummy Path @@@");
        //foreach (KeyValuePair<Vector2Int, CChamber> pair in ChamberPosition)
        //{
        //    if (pair.Value.ChamberType == EChamberType.Dummy)
        //    {
        //        Debug.Log(pair.Key);
        //    }
        //}
        // generator를 이용해여 맵 구성요소 생성
        generator.GenerateContents();
    }

    private void checkPossibility()
    {
        float sum = possibilityOfContinuousDummy + possibilityOfDividedDummy;
        if(possibilityOfContinuousDummy <= 0.0f || possibilityOfDividedDummy <= 0.0f ||
            possibilityOfContinuousDummy + possibilityOfDividedDummy >= 100.0f)
        {
            possibilityOfDividedDummy = 20.0f;
            possibilityOfContinuousDummy = 60.0f;
        }
    }

    /// <summary>
    /// 필수 경로를 생성
    /// </summary>
    private void makeEssentialPath()
    {
        Vector2Int currentPosition, nextPosition;
        Vector2Int[] adjacentPosition;
        // 필수 경로 시작 지점
        currentPosition = StartChamberPos = new Vector2Int(0, (int)Random.Range(0.0f, NumOfChamberInVertical));
        //ChamberPosition[currentPosition].ChamberType = EChamberType.Essential;
        ChamberPosition.Add(currentPosition, new CChamber(EChamberType.Essential, currentPosition));
        // 필수 경로 인접 지점
        adjacentPosition = getAdjacentPath(currentPosition, true);
        nextPosition = adjacentPosition[(int)Random.Range(0.0f, adjacentPosition.Length)];
        //ChamberPosition[nextPosition].ChamberType = EChamberType.Essential;
        ChamberPosition.Add(nextPosition, new CChamber(EChamberType.Essential, nextPosition));
        addFromCurrentToNextChamberPassage(currentPosition, nextPosition);
        currentPosition = nextPosition;
        // 그리드의 맨 오른쪽 위치 까지 진행
        while (currentPosition.x != NumOfChamberInHorizontal - 1)
        {
            adjacentPosition = getAdjacentPath(currentPosition, true);

            nextPosition = adjacentPosition[(int)Random.Range(0.0f, adjacentPosition.Length)];
            //ChamberPosition[nextPosition].ChamberType = EChamberType.Essential;
            ChamberPosition.Add(nextPosition, new CChamber(EChamberType.Essential, nextPosition));
            addFromCurrentToNextChamberPassage(currentPosition, nextPosition);
            currentPosition = nextPosition;
        }
        // 도착 지점 설정
        EndChamberPos = currentPosition;
    }

    /// <summary>
    /// 해당 좌표의 근접한 위치의 상대 좌표 배열을 반환
    /// </summary>
    /// <param name="path"> 근접한 위치의 좌표를 구할 기준 좌표 </param>
    /// <param name="isEssential"> 필수 경로의 근접좌표를 구하는가? </param>
    /// <returns> 근접한 좌표 배열 </returns>
    private Vector2Int[] getAdjacentPath(Vector2Int path, bool isEssential)
    {
        List<Vector2Int> adjacentList = new List<Vector2Int>();
        List<Vector2Int> availableList = new List<Vector2Int>();
        // 필수 경로가 아닌 경우에는 역으로 이동하는 경로도 고려
        if (!isEssential)
        {
            adjacentList.Add(new Vector2Int(path.x - 1, path.y)); // left
        }
        adjacentList.Add(new Vector2Int(path.x, path.y + 1)); //up
        adjacentList.Add(new Vector2Int(path.x, path.y - 1)); // down
        adjacentList.Add(new Vector2Int(path.x + 1, path.y)); // right

        adjacentList.ForEach(delegate (Vector2Int adjPath)
        {
        if (!ChamberPosition.ContainsKey(adjPath) && adjPath.x >= 0 && adjPath.x < NumOfChamberInHorizontal
        && adjPath.y >= 0 && adjPath.y < NumOfChamberInVertical)
            {
                availableList.Add(adjPath);
            }
        });

        return availableList.ToArray();
    }

    /// <summary>
    /// start Chamber와 end Chamber를 이어주는 메소드, 두 Chamber는 상대좌표 거리가 1만큼 차이나야 한다.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    private void addFromCurrentToNextChamberPassage(Vector2Int start, Vector2Int end)
    {
        ChamberPosition[start].NextChamberPosition.Add(end);
        ChamberPosition[end].PrevChamberPosition = start;
    }

    /// <summary>
    /// 생성된 필수 경로를 기준으로 더미 경로를 생성한다.
    /// </summary>
    /// <param name="start"></param>
    private void makeDummyPath(Vector2Int start)
    {
        if(start == EndChamberPos)
        {
            return;
        }

        // 해당 Chamber가 필수 경로 상의 Chamber인 경우
        if (ChamberPosition[start].ChamberType == EChamberType.Essential)
        {
            // 다음 필수경로를 대상으로 실행
            makeDummyPath(ChamberPosition[start].NextChamberPosition[0]);
        }

        int possibility = (int)Random.Range(0.0f, 100.0f);
        Vector2Int[] adjacentChambers = getAdjacentPath(start, false);

        // 인접한 Chamber가 존재하지 않는 경우
        if (adjacentChambers.Length == 0)
        {
            return;
        }
        int index = (int)Random.Range(0.0f, adjacentChambers.Length);

        ChamberPosition.Add(adjacentChambers[index], new CChamber(EChamberType.Dummy, adjacentChambers[index]));
        addFromCurrentToNextChamberPassage(start, adjacentChambers[index]);

        // 설정된 확률로 길이 확장
        if (possibility <= possibilityOfContinuousDummy)
        {
            makeDummyPath(adjacentChambers[index]);
        }  // 설정된 확률로 길이 분열
        else if (possibility > possibilityOfContinuousDummy && possibility <= possibilityOfDividedDummy + possibilityOfContinuousDummy)
        {
            makeDummyPath(adjacentChambers[index]);
            makeDummyPath(start);
        }
        // 이외의 확률로 길이 끊어짐
    }
}
