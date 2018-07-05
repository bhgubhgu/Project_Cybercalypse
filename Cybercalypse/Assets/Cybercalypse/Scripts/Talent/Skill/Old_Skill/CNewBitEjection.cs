using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNewBitEjection : ASkill
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

    private float ShockwaveMagnitude;// (충격파 크기)
    private float ShockwaveDamage;// (충격파 데미지)

    public GameObject bitEjectionPrefab; //CrimsonStrikePrefab;
    public GameObject startPosition;
    private int bitEjectionCount;

    private List<CSkill_BitEjection> bitEjectionComponent;

    private CSkillLibrary cSkillOffset_Instance;

    private bool isBitEjectionCanUse;
    private bool isBitEjectionCoolTimeWork;

    private void Awake()
    {
        //오브젝트 준비
        ItemName = "Bit Ejection";
        ItemCategory = EItemCategory.Talent;
        TalantCagegory = ETalantCategory.Skill;

        bitEjectionCount = 1;
        bitEjectionComponent = new List<CSkill_BitEjection>();
        cSkillOffset_Instance = transform.parent.gameObject.GetComponent<CSkillLibrary>();

        Init();

        SkillCoolDown = 3.0f;
    }

    public override void Start()
    {
        base.Start();
        cSkillOffset_Instance.lightningSphereDel += BitEjectionFire;
    }

    private void Init()
    {
        //오브젝트 풀 생성
        for (int i = 0; i < bitEjectionCount; i++)
        {
            GameObject tempObj = Instantiate(bitEjectionPrefab);
            tempObj.transform.parent = this.transform;
            tempObj.transform.localPosition = Vector3.zero;
            this.transform.GetChild(i).name = this.gameObject.name;
            bitEjectionComponent.Add(this.transform.GetChild(i).gameObject.GetComponent<CSkill_BitEjection>());
        }
    }

    private void BitEjectionFire()
    {
        if(isBitEjectionCanUse || startPosition.transform.parent.gameObject.GetComponent<CExecutor>().CurrentEnergy <= 0.0f)
        {
            return;
        }
        startPosition.transform.parent.gameObject.GetComponent<CExecutor>().ConsumeEnergy(4f);
        StartCoroutine(CoroutineBitEjectionFire(startPosition));
    }

    private IEnumerator CoroutineBitEjectionFire(GameObject startPosition)
    {
        if (isBitEjectionCanUse)
        {
            yield break;
        }

        isBitEjectionCanUse = true;

        bitEjectionComponent[0].ReadyToBitEjectionFire(startPosition.transform, startPosition.gameObject.transform.parent.gameObject.transform.localScale.x);

        yield return new WaitUntil(() => !bitEjectionComponent[0].isBitEjectionInActive);

        //쿨타임
        for(int i = 0; i < 6; i++)
        {
            if(slotArray[i].skillIndex == 0)
            {
                StartCoroutine(SqrClockwiseAnim(3.0f, slotArray[i].gameObject.transform.parent.gameObject));
                break;
            }
        }

        yield return new WaitForSeconds(3.0f);

        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(0).gameObject.transform.position = this.transform.position;
        isBitEjectionCanUse = false;
        yield break;
    }
}
