using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmo : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모, 윤동준
    /// 스크립트 : 총알 발사 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    /// <summary>
    /// 초당 프레임(ex. 60)
    /// 초당속도 = fps x 이동값(float)
    /// 이동값 = 초당속도 / fps
    /// </summary>
    /// 

    /// <summary> 구용모 추가
    ///  총알이 AI 또는 벽에 부딪치면 총알이 해당 객체에 박혀서 터지면서(총알 애니메이션 실행)
    ///  총알 애니메이션이 모두 실행되면 총알 객체는 사라진다.(SetActive(false));
    /// </summary>
    /// 

    public float speedPerSecond;

    public GameObject muzzle;

    public enum Direction { left = -1, right = 1 };

    public enum MissileType { normal, special };

    private Vector3 prevPos;

    private bool isHit;
    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
    }

    //Collision 관한 함수들은 발동될때, OnTriggerEnter와 OnCollisionEnter 중 1개만 발동한다.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 25)
        {
            isHit = true;
        }
        else if (collision.gameObject.layer == 8 || collision.gameObject.layer == 0 || collision.gameObject.layer == 20)
        {
            isHit = true;
        }
    }

    public IEnumerator OffTheControlMovement(Vector3 mousePosition, Transform muzzle)
    {
        //!< 발사하면 총구 위치로 이동(GetChild는 총구인 Muzzle임)
        transform.position = muzzle.position;

        //!< 발사 시점의 위치 저장
        prevPos = transform.localPosition;

        //!< 발사 방향으로의 단위벡터
        Vector3 vNormal = (mousePosition - transform.position).normalized;

        //!< 발사 방향으로의 회전
        float dy = vNormal.y;
        float dx = vNormal.x;

        float seta = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, seta);

        particle.Play();

        for (; Mathf.Abs((transform.localPosition - prevPos).magnitude) <= 5;)
        {
            if(isHit)
            {
                break;
            }

            transform.Translate(speedPerSecond * Time.deltaTime, 0, 0);
            yield return null;
        }

        //!< 처음 발사 시의 위치와 현재 위치의 차이가 50m 보다 크면 
        //!< 또는 충돌되는 객체와 충돌했을 경우
        //!< Animation 처리를 위해 0.05초간 대기
        //yield return new WaitForSeconds(0.05f);
        isHit = false;
        this.transform.localPosition = Vector3.zero;
        //이 오브젝트가 사라지는 것들 연구
    }
}
