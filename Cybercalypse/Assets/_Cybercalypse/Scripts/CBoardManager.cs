using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBoardManager : SingleTonManager<CBoardManager>
{
    public GameObject bottomBG;
    public GameObject defaultBG;

    private CParallexing cParallex;
    private bool isCanAlloc;

    private new void Awake()
    {
        base.Awake();
        cParallex = GetComponent<CParallexing>();
    }

    public void GenerateBottomBG(Vector3[] bottomPosition)
    {
        if (isCanAlloc)
        {
            return;
        }

        for (int i = 0; i < cParallex.backgrounds.Length; i++)
        {
            bottomBG.transform.GetChild(i).position = bottomPosition[i];
        }

        isCanAlloc = true;
        bottomBG.SetActive(true);
    }

    public void GenerateDefaultBG(Vector3[] defaultPosition)
    {
        if (isCanAlloc)
        {
            return;
        }

        for (int i = 0; i < cParallex.backgrounds.Length; i++)
        {
            defaultBG.transform.GetChild(i).position = defaultPosition[i];
        }

        isCanAlloc = true;
        defaultBG.SetActive(true);
    }

    public void ExtinguishBottomBG()
    {
        if (isCanAlloc)
        {
            AllocBGElement(defaultBG);
        }

        isCanAlloc = false;
        bottomBG.SetActive(false);

        StartCoroutine(AllocElement(defaultBG)); // 처음 시작의 BackGround가 사라지면 이때부터
    }

    public void ExtinguishDefaulBG()
    {
        isCanAlloc = false;
        defaultBG.SetActive(false);

        StartCoroutine(AllocElement(bottomBG)); // 처음 시작의 BackGround가 사라지면 이때부터
    }

    public void AllocBGElement(GameObject backGroundObject)
    {
        for (int i = 0; i < cParallex.backgrounds.Length; i++)
        {
            cParallex.backgrounds[i] = backGroundObject.transform.GetChild(i);
        }
    }

    IEnumerator AllocElement(GameObject backGroundObject)
    {
        yield return null;
        AllocBGElement(backGroundObject);
    }
}
