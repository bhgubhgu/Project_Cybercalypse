using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNewFrozenContinuam : ASkill
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

    public override Sprite ItemIcon
    {
        get;
        set;
    }

    public override string ItemName
    {
        get;
        set;
    }

    public override Sprite ItemSubs
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
    
    public override ETalentCategory TalentCategory
    {
        get;
        set;
    }
    #endregion

    public GameObject frozenContinuamPrefab;
    public Transform startPosition;

    private int frozenCountinuamCount;

    private CSkill_FrozenContinuam frozenContinuamComponent;
    private CSkillLibrary cSkillOffset_Instance;

    private bool isCanUseFrozenContinuam;

    private void Awake()
    {
        cSkillOffset_Instance = transform.parent.gameObject.GetComponent<CSkillLibrary>();
        frozenCountinuamCount = 1;
        Init();
    }

<<<<<<< HEAD
    private void Start()
=======
    public void Start()
>>>>>>> dev
    {
        cSkillOffset_Instance.FrozenContinuamDel += FrozenContinuam;
    }

    private void Init()
    {
        for (int i = 0; i < frozenCountinuamCount; i++)
        {
            GameObject tempObj = Instantiate(frozenContinuamPrefab);
            tempObj.transform.parent = this.transform;
            tempObj.transform.localPosition = Vector3.zero;
            this.transform.GetChild(i).name = this.gameObject.name;
            frozenContinuamComponent = this.transform.GetChild(0).gameObject.GetComponent<CSkill_FrozenContinuam>();
        }
    }

    public void FrozenContinuam()
    {
        StartCoroutine(FrozenContinuamCoroutine());
    }

    IEnumerator FrozenContinuamCoroutine()
    {
        if(isCanUseFrozenContinuam || startPosition.transform.parent.gameObject.GetComponent<CExecutor>().CurrentEnergy <= 0.0f)
        {
            yield break;
        }

        isCanUseFrozenContinuam = true;
        startPosition.transform.parent.gameObject.GetComponent<CExecutor>().ConsumeEnergy(5f);
        StartCoroutine(frozenContinuamComponent.FireFrozenContinuam(startPosition.transform));

        yield return new WaitUntil(() => !frozenContinuamComponent.isFrozenContinuamActive);

        //쿨타임
        for (int i = 0; i < 6; i++)
        {
            if (slotArray[i].skillIndex == 3)
            {
                StartCoroutine(SqrClockwiseAnim(6.0f, slotArray[i].gameObject.transform.parent.gameObject));
                break;
            }
        }

        yield return new WaitForSeconds(6.0f);

        isCanUseFrozenContinuam = false;
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(0).gameObject.transform.position = this.transform.position;
    }
}
