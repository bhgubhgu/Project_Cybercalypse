using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkill_CrimsonStrike : MonoBehaviour
{
    private ParticleSystem trailParticle;

    private float homingForce = 1f;//얼마나 유도가 잘 되는지

    private Vector3 startPosition; //1 ( == this.transform.position)
    private Vector3 middlePosition; //2
    private Vector3 endPosition; //3
    private Vector3 bezierCurvePosition; //result

    private bool isHitMonster;

    private BoxCollider2D hitCollider;

    private void Awake()
    {
        trailParticle = GetComponent<ParticleSystem>();
        hitCollider = GetComponent<BoxCollider2D>();

        trailParticle.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isHitMonster)
        {
            return;
        }
        if(collision.gameObject.layer == 25)
        {
            isHitMonster = true;
        }
    }

    public void ReadyToCrimsonStrike(Transform startPosition, float bezierPosition , int missileDir, Collider2D target)
    {
        if(missileDir % 2 == 1)
        {
            missileDir = -1;
        }
        else
        {
            missileDir = +1;
        }

        GameObject targetObject = target.gameObject;
        StartCoroutine(CrimsonStrike(startPosition, bezierPosition, missileDir, targetObject));
    }

    private IEnumerator CrimsonStrike(Transform Skill2StartPosition, float bezierPosition, int missileDir, GameObject target)
    {
        this.transform.position = Skill2StartPosition.position;
        startPosition = this.transform.position;
        endPosition = target.transform.position;
        middlePosition = new Vector3(startPosition.x  + bezierPosition, startPosition.y + ((1.5f + bezierPosition) * missileDir) , startPosition.z);

        trailParticle.Play();

        this.hitCollider.enabled = true;

        for (float time = 0 ; time <= 1f ; time += Time.deltaTime)
        {
            if(isHitMonster)
            {
                this.hitCollider.enabled = false;
                break;
            }

            endPosition = target.transform.position;

            bezierCurvePosition = BezierCurvePosition(startPosition, middlePosition, endPosition, time * homingForce);

            Vector3 dirNomal = (bezierCurvePosition - this.transform.position).normalized;
        
            float dx = dirNomal.x;
            float dy = dirNomal.y;

            float seta = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, seta);

            this.transform.position = CustomLerp(this.transform.position, bezierCurvePosition, time * homingForce);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        isHitMonster = false;
        this.gameObject.SetActive(false);
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
    }
}
