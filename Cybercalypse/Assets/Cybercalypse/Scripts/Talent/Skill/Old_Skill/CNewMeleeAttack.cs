using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNewMeleeAttack : ASkill
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

    public override ETalantCategory TalentCategory
    {
        get;
        set;
    }
    #endregion

    public GameObject moonLightSlash;
    public GameObject startPosition;

    private GameObject moonLightSlashObject;
    private BoxCollider2D moonLightHitBox;
    private GameObject moonSlashTrail;
    private ParticleSystem moonSlashTrailParticle;

    private int moonLightSlashCount;

    private CSkillLibrary cSkillOffset_Instance;
    public static bool isDoingNow;

    private void Awake()
    {
        moonLightSlashCount = 1;
        cSkillOffset_Instance = transform.parent.gameObject.GetComponent<CSkillLibrary>();     
        Init();
    }

    public override void Start()
    {
        base.Start();
        cSkillOffset_Instance.moonLightSlashDel += MoonLightSlash;
        moonLightSlashObject = this.transform.GetChild(0).gameObject;
        moonSlashTrailParticle = moonLightSlashObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        moonSlashTrail = moonLightSlashObject.transform.GetChild(0).gameObject;
        moonLightHitBox = moonLightSlashObject.GetComponent<BoxCollider2D>();

        moonSlashTrailParticle.Stop();
    }

    private void Init()
    {
        //오브젝트 풀 생성
        for (int i = 0; i < moonLightSlashCount; i++)
        {
            GameObject tempObj = Instantiate(moonLightSlash);
            tempObj.transform.parent = this.transform;
            tempObj.transform.localPosition = Vector3.zero;
            this.transform.GetChild(i).name = this.gameObject.name;
        }
    }

    public void MoonLightSlash()
    {
        if(isDoingNow || startPosition.transform.parent.gameObject.GetComponent<CExecutor>().CurrentEnergy <= 0.0f)
        {
            return;
        }

        moonLightHitBox.enabled = true;
        startPosition.transform.parent.gameObject.GetComponent<CExecutor>().ConsumeEnergy(1f);
        moonLightSlashObject.transform.position = new Vector3(startPosition.transform.position.x + 0.297f * startPosition.transform.parent.gameObject.transform.localScale.x, startPosition.transform.position.y, startPosition.transform.position.z);
        StartCoroutine(ActMoonLightSlash());
        StartCoroutine(StopParticle(startPosition.transform.parent.transform));
    }

    private IEnumerator ActMoonLightSlash()
    {
        yield return new WaitForSeconds(0.1f);
        moonLightHitBox.enabled = false;
    }

    private IEnumerator StopParticle(Transform playerScale)
    {
        moonSlashTrailParticle.Play();

        yield return null;
        Vector3 previousPlayerScale = playerScale.localScale;

        if (startPosition.transform.parent.localScale.x > 0.0f)
        {
            moonSlashTrail.transform.rotation = Quaternion.Euler(59.49f, 0f, -112.22f);
        }
        else
        {
            moonSlashTrail.transform.rotation = Quaternion.Euler(59.49f, 180f, -112.22f);
        }

        for (float i = 0; i< 0.32; i += Time.deltaTime)
        {
            if (previousPlayerScale != startPosition.transform.parent.transform.localScale)
            {
                moonSlashTrailParticle.Stop();
                isDoingNow = false;
                StartCoroutine(StopParticle(startPosition.transform.parent.transform));
                yield break;
            }

            isDoingNow = true;
            moonLightSlashObject.transform.position = new Vector3(startPosition.transform.position.x + 0.297f * startPosition.transform.parent.gameObject.transform.localScale.x, startPosition.transform.position.y, startPosition.transform.position.z);
            yield return null;
        }

        isDoingNow = false;
        moonSlashTrailParticle.Stop();
    }

    /*private IEnumerator MoonLightSlashKi()
    {
        startPositions = position1.position;
        startTangetnPosition = position2.position;
        middleTangentPosition = position3.position;
        middlePosition = position4.position;
        endTangentPosition = position5.position;
        endPosition = position6.position;

        moonSlashTrailParticle.Play();
            for (float time = 0; time <= 1f; time += Time.deltaTime)
            {
                bezierCurvePosition = NewBezierCurvePosition(startPositions, startTangetnPosition, middleTangentPosition, middlePosition, endTangentPosition, endPosition, time);

                Vector3 dirNomal = (bezierCurvePosition - moonSlashTrail.transform.position).normalized;

                float dx = dirNomal.x;
                float dy = dirNomal.y;

                float seta = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
                moonSlashTrail.transform.rotation = Quaternion.Euler(0, 0, seta);

                moonSlashTrail.transform.position = CustomLerp(moonSlashTrail.transform.position, bezierCurvePosition, time);
                yield return null;
            }
    }

    private Vector3 NewBezierCurvePosition(Vector3 startPosition, Vector3 startTanget, Vector3 middleTanget, Vector3 middlePosition, Vector3 endTanget, Vector3 endPosition, float time)
    {
        Vector3 startCurve = CustomLerp(startPosition, startTanget, time);
        Vector3 middleTanCurve = CustomLerp(startTanget, middleTanget, time);
        Vector3 middleCurve = CustomLerp(middleTanget, middlePosition, time);
        Vector3 endTanCurve = CustomLerp(middlePosition, endTanget, time);
        Vector3 endCurve = CustomLerp(endTanget, endPosition, time);

        Vector3 bezierTanCurve1 = CustomLerp(startCurve, middleTanCurve, time);
        Vector3 bezierTanCurve2 = CustomLerp(middleTanCurve, middleCurve, time);
        Vector3 bezierTanCurve3 = CustomLerp(middleCurve, endTanCurve, time);
        Vector3 bezierTanCurve4 = CustomLerp(endTanCurve, endCurve, time);

        Vector3 bezierCurve1 = CustomLerp(bezierTanCurve1, bezierTanCurve2, time);
        Vector3 bezierCurve2 = CustomLerp(bezierTanCurve2, bezierTanCurve3, time);
        Vector3 bezierCurve3 = CustomLerp(bezierTanCurve3, bezierTanCurve4, time);

        Vector3 lastCurve1 = CustomLerp(bezierCurve1, bezierCurve2, time);
        Vector3 lastCurve2 = CustomLerp(bezierCurve2, bezierCurve3, time);

        return CustomLerp(lastCurve1, lastCurve2, time);
    }

    private Vector3 BezierCurvePosition(Vector3 startPosition, Vector3 middleTanget, Vector3 endPosition, float time)
    {
        Vector3 startCurve = CustomLerp(startPosition, middleTanget, time);
        Vector3 endCurve = CustomLerp(middleTanget, endPosition, time);

        return CustomLerp(startCurve, endCurve, time);
    }

    private Vector3 CustomLerp(Vector3 startPosition, Vector3 endPosition, float time)
    {
        return ((1f - time) * startPosition) + (time * endPosition);
    }*/
}
