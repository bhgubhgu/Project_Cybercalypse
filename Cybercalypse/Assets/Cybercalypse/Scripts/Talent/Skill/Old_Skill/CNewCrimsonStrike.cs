using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNewCrimsonStrike : ASkill
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

<<<<<<< HEAD:Cybercalypse/Assets/Cybercalypse/Scripts/Skill/CNewCrimsonStrike.cs
    public override ETalentCategory TalentCategory
=======
    public override ETalantCategory TalentCategory
>>>>>>> dev:Cybercalypse/Assets/Cybercalypse/Scripts/Talent/Skill/Old_Skill/CNewCrimsonStrike.cs
    {
        get;
        set;
    }
    #endregion

    private float MinCastableDist;// (시전 가능 최소 거리)
    private float MaxCastableDist;// (시전 가능 최대 거리)
    private float MinProjectileNum;// (최소 투사체 개수)
    private float MaxProjectileNum;// (최대 투사체 개수)
    private float ProjectileDamage;// (투사체 데미지)
    private float ProjectileSpeed;//  (투사체 속도)

    public GameObject crimsonStrikePrefab; //CrimsonStrikePrefab;
    public GameObject startPosition;

    private int projectileCount; //CrimsonStrike 오브젝트 갯수
    private List<CSkill_CrimsonStrike> crimsonStrikeComponent;
    private CSkillLibrary cSkillOffset_Instance;

    public Transform skillTargetChecker;
    private Collider2D targetCollider;
    private float raderRadius;
    private LayerMask targetLayerMask;
    private bool isFindTarget;
    private bool isReadyToCrimsonFireTarget;

    private bool isChangeSkill;
    private bool isCrimsonStrikeCanUse;

    private void Awake()
    {
        //오브젝트 준비
        ItemName = "Crimson Strike";
        ItemCategory = EItemCategory.Talent;
<<<<<<< HEAD:Cybercalypse/Assets/Cybercalypse/Scripts/Skill/CNewCrimsonStrike.cs
        TalentCategory = ETalentCategory.Skill;
=======
        TalentCategory = ETalantCategory.Skill;
>>>>>>> dev:Cybercalypse/Assets/Cybercalypse/Scripts/Talent/Skill/Old_Skill/CNewCrimsonStrike.cs

        targetLayerMask = 1 << 25;

        projectileCount = 8;
        crimsonStrikeComponent = new List<CSkill_CrimsonStrike>();
        cSkillOffset_Instance = transform.parent.gameObject.GetComponent<CSkillLibrary>();

        Init();

        SkillCoolDown = 3.0f;
    }

    public override void Start()
    {
        base.Start();
        cSkillOffset_Instance.crimsonStrikeDel += CrimsonStrikeFire;
    }

    private void Init()
    {
        //오브젝트 풀 생성
        for(int i = 0; i < projectileCount; i++)
        {
            GameObject tempObj = Instantiate(crimsonStrikePrefab);
            tempObj.transform.parent = this.transform;
            tempObj.transform.localPosition = Vector3.zero;
            this.transform.GetChild(i).name = this.gameObject.name;
            crimsonStrikeComponent.Add(this.transform.GetChild(i).gameObject.GetComponent<CSkill_CrimsonStrike>());
        }
    }

    private void CrimsonStrikeFire()
    {
        StartCoroutine(CoroutineCrimsonStrikeFire(startPosition));
        //StartCoroutine(SqrClockwiseAnim(SkillCoolDown, gameObject));
    }

    private IEnumerator CoroutineCrimsonStrikeFire(GameObject startPosition)
    {
        if (isCrimsonStrikeCanUse || startPosition.transform.parent.gameObject.GetComponent<CExecutor>().CurrentEnergy <= 0.0f)
        {
            yield break;
        }

        startPosition.transform.parent.gameObject.GetComponent<CExecutor>().ConsumeEnergy(10f);

        //적 탐색, 적을 OverlapCircle에 닿을때까지 찾는다.(시간안에)
        for (float t = 0; t < 1.5f; t += Time.deltaTime * 1.5f)
        {
            isReadyToCrimsonFireTarget = true;
            raderRadius += t * 0.5f;
            raderRadius = Mathf.Clamp(raderRadius, 0, 1.5f);
            targetCollider = Physics2D.OverlapCircle(skillTargetChecker.position, raderRadius, targetLayerMask);

            if (targetCollider != null)
            {
                isReadyToCrimsonFireTarget = false;
                break;
            }
            yield return null;
        }

        if (targetCollider == null)
        {
            //쿨타임
            for (int i = 0; i < 6; i++)
            {
                if (slotArray[i].skillIndex == 1)
                {
                    StartCoroutine(SqrClockwiseAnim(6.0f, slotArray[i].gameObject.transform.parent.gameObject));
                    break;
                }
            }

            isReadyToCrimsonFireTarget = false;
            isCrimsonStrikeCanUse = false;
            raderRadius = 0;
            yield break;
        }

        isCrimsonStrikeCanUse = true;

        //8발의 유도 미사일
        for (int i = 0; i < projectileCount; i++)
        {
            float bezierPosition = UnityEngine.Random.Range(-2f, 2f);
            crimsonStrikeComponent[i].ReadyToCrimsonStrike(startPosition.transform, bezierPosition, i, targetCollider);
            yield return new WaitForSeconds(0.1f);
        }

        //쿨타임
        for (int i = 0; i < 6; i++)
        {
            if (slotArray[i].skillIndex == 1)
            {
                StartCoroutine(SqrClockwiseAnim(3.0f, slotArray[i].gameObject.transform.parent.gameObject));
                break;
            }
        }

        yield return new WaitForSeconds(5.2f); //쿨타임 3초

        for (int i = 0; i < projectileCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
            this.transform.GetChild(i).gameObject.transform.position = this.transform.position;
        }

        isReadyToCrimsonFireTarget = false;
        isCrimsonStrikeCanUse = false;
        targetCollider = null;
        raderRadius = 0;
        yield break;
    }
}
