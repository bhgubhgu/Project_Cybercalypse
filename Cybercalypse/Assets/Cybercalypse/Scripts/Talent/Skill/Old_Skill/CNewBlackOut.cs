using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNewBlackOut : ASkill
{
    #region Override

    public override EItemCategory ItemCategory
    {
        get;
        set;
    }

    public override string ItemDesc
    {
        get;
        set;
    }

    public override SpriteRenderer ItemIcon
    {
        get;
        set;
    }

    public override string ItemName
    {
        get;
        set;
    }

    public override SpriteRenderer ItemSubs
    {
        get;
        set;
    }

    public override float SkillCastingTime
    {
        get;
        set;
    }

    public override float SkillCoolDown
    {
        get;
        set;
    }

    public override ETalantCategory TalantCagegory
    {
        get;
        set;
    }
    #endregion

    public CursorControl control;
    public GameObject blackOutPrefab;
    public Transform startPosition;

    private int blackOutCount;

    private CSkill_BlackOut blackOutComponent;

    private CSkillLibrary cSkillOffset_Instance;

    private bool isCanUseBlackOut;

    private void Awake()
    {
        cSkillOffset_Instance = transform.parent.gameObject.GetComponent<CSkillLibrary>();
        blackOutCount = 1;
        Init();
    }

    public override void Start()
    {
        base.Start();
        cSkillOffset_Instance.BlackOutDel += BlackOut;
    }

    private void Init()
    {
        for (int i = 0; i < blackOutCount; i++)
        {
            GameObject tempObj = Instantiate(blackOutPrefab);
            tempObj.transform.parent = this.transform;
            tempObj.transform.localPosition = Vector3.zero;
            this.transform.GetChild(i).name = this.gameObject.name;
            blackOutComponent = this.transform.GetChild(0).gameObject.GetComponent<CSkill_BlackOut>();
        }
    }

    public void BlackOut()
    {
        StartCoroutine(BlackOutCoroutine());
    }

    IEnumerator BlackOutCoroutine()
    {
        if (isCanUseBlackOut || startPosition.transform.parent.gameObject.GetComponent<CExecutor>().CurrentEnergy <= 0.0f)
        {
            yield break;
        }

        isCanUseBlackOut = true;
        startPosition.transform.parent.gameObject.GetComponent<CExecutor>().ConsumeEnergy(7f);
        StartCoroutine(blackOutComponent.BlackOut(startPosition.transform, control.gameObject.transform.position));

        yield return new WaitUntil(() => !blackOutComponent.isBlackOutActive);

        //쿨타임
        for (int i = 0; i < 6; i++)
        {
            if (slotArray[i].skillIndex == 2)
            {
                StartCoroutine(SqrClockwiseAnim(1.0f, slotArray[i].gameObject.transform.parent.gameObject));
                break;
            }
        }

        yield return new WaitForSeconds(1.0f);
        isCanUseBlackOut = false;
        this.transform.GetChild(0).gameObject.transform.position = this.transform.position;
    }

}
