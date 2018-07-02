using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CResolutionMenu : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : 해상도 변경 UI를 띄워 해상도를 변경 할 수 있는 값들을 반환하는 함수가 있는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    [SerializeField]
    private List<GameObject> resolutionList = new List<GameObject>();

    private GameObject resolutionSet;

    private List<CMenuButton> resolutionButtonList = new List<CMenuButton>();

    [SerializeField]
    private int curResolutionNum;
    [SerializeField]
    private bool isWindowSize;

    private void Awake()
    {
        resolutionSet = this.transform.GetChild(0).gameObject;

        for (int i = 0; i < resolutionSet.transform.childCount; i++)
        {
            resolutionList.Add(resolutionSet.transform.GetChild(i).gameObject);
            resolutionButtonList.Add(resolutionList[i].GetComponent<CMenuButton>());
        }

        for (int i = 0; i < resolutionSet.transform.childCount; i++)
        {
            resolutionButtonList[i].GetResolutionData(resolutionList);
        }

       //resolutionSet.SetActive(false);
    }

    private void Start()
    {
        CInputManager.instance.GamePause += SetResolutionMenu;
    }

    private void SetResolutionMenu(bool isActiveResolutionMenu)
    {
        if(isActiveResolutionMenu)
        {
            resolutionSet.SetActive(true);
        }
        else
        {
            //resolutionSet.SetActive(false);
        }
    }

    public void GetCurrentResolutionNum(int curNum) //해상도 데이터(자식 오브젝트)의 현재 해상도 넘버를 받는다.
    {
        curResolutionNum = curNum;
    }

    public void GetCurrentWindowSize(bool isWindow) //해상도 데이터(자식 오브젝트)의 현재 윈도우창 상태인지를 받는다.
    {
        isWindowSize = isWindow;
    }

    public int ReturnResolutionNum() //해상도 데이터(자식 오브젝트)의 현재 해상도 넘버를 반환한다..
    {
        return curResolutionNum;
    }

    public bool IsReturnWindowSize() //해상도 데이터(자식 오브젝트)의 현재 윈도우창 상태인지를 반환한다.
    {
        return isWindowSize;
    }
}
