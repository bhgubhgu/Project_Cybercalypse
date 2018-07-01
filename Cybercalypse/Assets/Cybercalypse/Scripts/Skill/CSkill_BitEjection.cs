using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkill_BitEjection : MonoBehaviour
{
    private float shockwaveMagnitude;
    private float shockwaveCount = 8;
    private float shockwaveDamage = 15;

    private ParticleSystem LightningSphereParticleSytem;
    private GameObject rotateParticle;
    private Vector3 rotateVector = new Vector3(0, 0, -200);

    private bool isHit;
    private CircleCollider2D circleCollider2D;

    private Transform hitChecker;
    private bool isHitEnemy;
    private Collider2D enemyCollider;
    private LayerMask whatIsEnemy;

    public bool isBitEjectionInActive;

    private void Awake()
    {
        LightningSphereParticleSytem = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        hitChecker = this.transform;
        whatIsEnemy = 1 << 25;
        LightningSphereParticleSytem.Stop();
    }

    private void OnEnable()
    {
        if (!isBitEjectionInActive)
        {
            LightningSphereParticleSytem.Stop();
        }
    }

    private void OnDisable()
    {
        isHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 0 || collision.gameObject.layer == 20 || collision.gameObject.layer == 15)
        {
            //총알이 충돌되는 오브젝트의 자식오브젝트로 들어가서 터지는 이펙트가 나와야한다.
            isHit = true;
        }

        else if(collision.gameObject.layer == 25)
        {
            StartCoroutine(HitEnemy(collision.gameObject));
        }
    }

    public void ReadyToBitEjectionFire(Transform startPosition, float dir)
   {
        this.transform.position = new Vector3(startPosition.position.x + 0.1f * dir, startPosition.position.y + 0.02f, startPosition.position.z);
        StartCoroutine(BitEjection(dir));
   }

    private IEnumerator BitEjection(float dir)
    {
        LightningSphereParticleSytem.Play();

        isBitEjectionInActive = true;

        while(!isHit)
        {
            this.transform.Translate(new Vector2(0.01f * dir, 0)); //방향
            yield return null;
        }

        isHit = false;
        isBitEjectionInActive = false;
        this.gameObject.SetActive(false);
    }

    IEnumerator HitEnemy(GameObject monster)
    {
        int count = 0;

        while (!isHit)
        {
            isHitEnemy = Physics2D.OverlapCircle(hitChecker.position, 1.5f, whatIsEnemy);

            if (isHitEnemy)
            {
                monster.GetComponent<CMonster>().GetDamage(20.0f); //수정
                count++;
                yield return new WaitForSeconds(Time.deltaTime * 60 * 0.25f);
            }

            if (count >= shockwaveCount)
            {
                isHit = true;
            }

            yield return null;
        }

        yield break;
    }
}
