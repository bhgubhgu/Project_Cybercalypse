using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkill_BlackOut : MonoBehaviour
{
    public static bool isMonsterHitBlackOut; //임시방편 무조건 지울것
    public bool isBlackOutActive;

    private ParticleSystem BlackOutParticleSystemOne;
    private ParticleSystem BlackOutParticleSystemTwo;
    private ParticleSystem BlackOutParticleSystemThree;

    private BoxCollider2D hitCollider;

    private Transform hitChecker;
    private bool isHitEnemy;
    private Collider2D enemyCollider;
    private LayerMask whatIsEnemy;

    private bool isHit;
    private bool isGroundHit;
    private GameObject monster;

    private void Awake()
    {
        BlackOutParticleSystemOne = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        BlackOutParticleSystemTwo = this.transform.GetChild(1).GetComponent<ParticleSystem>();
        BlackOutParticleSystemThree = this.transform.GetChild(2).GetComponent<ParticleSystem>();

        hitCollider = GetComponent<BoxCollider2D>();
        hitChecker = this.transform;
        whatIsEnemy = 1 << 25;

        BlackOutParticleSystemOne.Stop();
        BlackOutParticleSystemTwo.Stop();
        BlackOutParticleSystemThree.Stop();
    }

    private void OnEnable()
    {
        if (!isBlackOutActive)
        {
            BlackOutParticleSystemOne.Stop();
            BlackOutParticleSystemTwo.Stop();
            BlackOutParticleSystemThree.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 25)
        {
            isHit = true;
            monster = collision.gameObject;
        }
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 0 || collision.gameObject.layer == 20)
        {
            isGroundHit = true;
        }
    }


    public IEnumerator BlackOut(Transform startPosition, Vector3 cursorPosition)
    {
        //첫번째 자식인 FireBlackOut을 Play
        //FireBlackOut이 날아간다
        //적을 맞췄따
        //적을 맞추면 FireBlackOut은 Stop
        //두번째 자식인 ReadyExplosion을 Playe
        //1초동안 0.25초 마다 도트뎀 들어간다. (1초동안 0.25초 마다 데미지 들어감)
        //1초가 지나면 ReadyExplosion은 STOP
        //세번째 자식인 Explosion을 Play
        //큰 데미지와 함께 넉백을 시킨다.
        //Explosion의 파티클 재생 시간을 측정하여 한 Cycle이 돌면 바로 Stop한다.
        //BlackOut 종료
        isBlackOutActive = true;

        Vector3 previousPosition = Vector3.zero;
        Vector3 vectorNomalize = Vector3.zero;
        float dx = 0f;
        float dy = 0f;
        float seta = 0f;

        this.transform.position = startPosition.transform.position;
        previousPosition = this.transform.localPosition;
        vectorNomalize = (cursorPosition - this.transform.position).normalized;

        dx = vectorNomalize.x;
        dy = vectorNomalize.y;

        seta = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(0, 0, seta);

        BlackOutParticleSystemOne.Play();

        for( ; Mathf.Abs((transform.localPosition - previousPosition).magnitude) <= 5f ; )
        {
            if(isHit || isGroundHit)
            {
                break;
            }
            this.transform.Translate(new Vector2(4.5f * Time.deltaTime, 0.0f));
            yield return null;
        }
   
        BlackOutParticleSystemOne.Stop();

        if(isHit)
        {
            isMonsterHitBlackOut = true;
            monster.GetComponent<CMonster>().GetDamage(40.0f);
            StartCoroutine(ReadyExplosionAct());
        }
        else if(isGroundHit)
        {
            isGroundHit = false;
            isHit = false;
            isBlackOutActive = false;
            isMonsterHitBlackOut = false;
            this.transform.position = this.transform.parent.position;
            yield break;
        }

        yield return new WaitUntil(() => !isHit);
        isBlackOutActive = false;
        isMonsterHitBlackOut = false;
        this.transform.position = this.transform.parent.position;
    }

    private IEnumerator ReadyExplosionAct()
    {
        BlackOutParticleSystemTwo.Play();

        this.transform.position = monster.transform.position;

        for (float t = 0 ; t < 0.1f ; t += Time.deltaTime)
        {
            //도트데미지 들어간다.
            monster.GetComponent<CMonster>().GetDamage(8.0f);
            this.transform.position = monster.transform.position;
            yield return new WaitForSeconds(Time.deltaTime * 60 * 0.25f);
        }

        BlackOutParticleSystemTwo.Stop();
        StartCoroutine(ExplosionAct());
    }

    private IEnumerator ExplosionAct()
    {
        this.transform.position = monster.transform.position;
        BlackOutParticleSystemThree.Play();

        yield return new WaitForSeconds(0.1f);
        monster.GetComponent<CMonster>().GetDamage(70.0f);
        for (float t = 0; t < 0.3f; t += Time.deltaTime)
        {
            monster.transform.Translate(new Vector3(t * -monster.transform.localScale.x * 0.21f, 0));
            yield return null;
        }

        BlackOutParticleSystemThree.Stop();
        monster = null;
        isHit = false;
    }
}
