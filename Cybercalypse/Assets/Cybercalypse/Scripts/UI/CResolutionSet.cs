using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CResolutionSet : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : 해상도를 변경을 구현한 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    private List<GameObject> resolutionList = new List<GameObject>();
    private GameObject setBackground;

    private void Awake()
    {
        setBackground = transform.GetChild(0).gameObject;

        for(int i = 0; i < setBackground.transform.childCount; i++)
        {
            resolutionList.Add(setBackground.transform.GetChild(i).gameObject);
        }

        setBackground.SetActive(false);
    }

    private void Start()
    {
        CInputManager.instance.GamePause += SetResolution;
    }

    private void SetResolution(bool isSetResolution)
    {
        if(isSetResolution)
        {
            setBackground.SetActive(true);
        }
        else
        {
            setBackground.SetActive(false);
        }
    }
}
