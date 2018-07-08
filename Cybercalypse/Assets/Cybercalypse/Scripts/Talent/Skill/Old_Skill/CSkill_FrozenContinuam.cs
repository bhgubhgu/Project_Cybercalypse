using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkill_FrozenContinuam : MonoBehaviour
{
    public static bool isFireFrozenContinuam; //임시방편 무조건 지워야함
    public bool isFrozenContinuamActive;

    private ParticleSystem FrozenContinuamParticleSystem;
    private BoxCollider2D hitCollider;

    private Transform hitChecker;
    private bool isHitEnemy;
    private Collider2D enemyCollider;
    private LayerMask whatIsEnemy;

    private bool isHit;

    private void Awake()
    {
        FrozenContinuamParticleSystem = GetComponent<ParticleSystem>();
        hitCollider = GetComponent<BoxCollider2D>();
        hitChecker = this.transform;
        whatIsEnemy = 1 << 25;

        FrozenContinuamParticleSystem.Stop();
    }

    private void OnEnable()
    {
        if (!isFrozenContinuamActive)
        {
            FrozenContinuamParticleSystem.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 25)
        {
            StartCoroutine(HitEnemy(collision.gameObject));
        }
    }

    public IEnumerator FireFrozenContinuam(Transform startPosition)
    {
        hitCollider.enabled = false;
        isFrozenContinuamActive = true;
        isFireFrozenContinuam = true;
        isHit = false;

        this.transform.position = new Vector3(startPosition.transform.position.x + 0.07f * startPosition.transform.parent.transform.localScale.x, startPosition.transform.position.y - 0.03f);
        this.transform.localScale = startPosition.transform.parent.transform.localScale;

        for (int i = 0; i < 12; i++)
        {
            this.transform.GetChild(i).gameObject.transform.localScale = startPosition.transform.parent.transform.localScale;
        }

        FrozenContinuamParticleSystem.Play();

        yield return new WaitForSeconds(0.2f);
        hitCollider.enabled = true;

        for(float t = 0; t < 2.5f; t += Time.deltaTime)
        {
            startPosition.transform.parent.transform.localScale = new Vector3(this.transform.GetChild(0).localScale.x, this.transform.GetChild(0).localScale.y, this.transform.GetChild(0).localScale.z);
            this.transform.position = new Vector3(startPosition.transform.position.x + 0.07f * startPosition.transform.parent.transform.localScale.x, startPosition.transform.position.y - 0.03f);
            if (isHit)
            {
                break;
            }
            yield return null;
        }

        FrozenContinuamParticleSystem.Stop();
        isFrozenContinuamActive = false;
        isFireFrozenContinuam = false;
        isHit = false;
        
    }

    IEnumerator HitEnemy(GameObject monster)
    {
        int count = 0;

        while (!isHit)
        {
            isHitEnemy = Physics2D.OverlapCircle(hitChecker.position, 1.5f, whatIsEnemy);

            if (isHitEnemy)
            {
                count++;
                //몬스터 체력 감소시키기
                monster.GetComponent<CMonster>().GetDamage(20.0f);
                yield return new WaitForSeconds(Time.deltaTime * 60 * 0.25f);
            }

            if (count >= 8)
            {
                hitCollider.enabled = false;
                isHit = true;
            }

            yield return null;
        }
    }
}
