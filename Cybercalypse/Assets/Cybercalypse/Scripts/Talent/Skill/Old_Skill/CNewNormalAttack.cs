using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNewNormalAttack : ASkill
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

    public GameObject normalAttackPrefab;
    public Transform startPosition;
    /*public CursorControl control;*/
    private int normalAttackCount;

    private List<CAmmo> normalAttackComponent;

    private CSkillLibrary cSkillOffset_Instance;

    private int curAmmoIndex;
    private bool isFireBallCanUse;

    private void Awake()
    {
        normalAttackComponent = new List<CAmmo>();
        cSkillOffset_Instance = transform.parent.gameObject.GetComponent<CSkillLibrary>();
        normalAttackCount = 25;
        Init();
    }

<<<<<<< HEAD
    void Start()
=======
    public void Start()
>>>>>>> dev
    {
        cSkillOffset_Instance.fireBallDel += NormalAttack;
    }

    private void Init()
    {
        //오브젝트 풀 생성
        for (int i = 0; i < normalAttackCount; i++)
        {
            GameObject tempObj = Instantiate(normalAttackPrefab);
            tempObj.transform.parent = this.transform;
            tempObj.transform.localPosition = Vector3.zero;
            this.transform.GetChild(i).name = this.gameObject.name;
            normalAttackComponent.Add(this.transform.GetChild(i).gameObject.GetComponent<CAmmo>());
        }
    }

    public void NormalAttack()
    {
        if(startPosition.transform.parent.gameObject.GetComponent<CExecutor>().CurrentEnergy <= 0.0f)
        {
            return;
        }

        if(curAmmoIndex >= normalAttackCount)
        {
            curAmmoIndex = 0;
        }

        startPosition.transform.parent.gameObject.GetComponent<CExecutor>().ConsumeEnergy(0.5f);
        StartCoroutine(normalAttackComponent[curAmmoIndex++].OffTheControlMovement(Camera.main.ScreenToWorldPoint(Input.mousePosition), startPosition));

        /*for (int i = 0; i < 6; i++)
        {
            if (slotArray[i].skillIndex == 4)
            {
                StartCoroutine(SqrClockwiseAnim(1.0f, slotArray[i].gameObject.transform.parent.gameObject));
                break;
            }
        }*/

        //StartCoroutine(FireBallCool());
    }

    IEnumerator FireBallCool()
    {
        yield return new WaitForSeconds(1.5f);
        isFireBallCanUse = false;
    }
}
