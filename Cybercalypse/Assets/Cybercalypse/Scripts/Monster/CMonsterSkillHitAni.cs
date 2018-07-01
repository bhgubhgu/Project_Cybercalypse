using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterSkillHitAni : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : 몬스터 객체들이 피격당할때 나타나게 하는 스크립트 -> 삭제 예정(CMonsterAni와 병합 될 예정)
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    private Animator ani;
    private CMonsterController control;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        control = this.gameObject.transform.parent.GetComponent<CMonsterController>();
    }

    private void Update()
    {
        if(control.IsHit)
        {
            ani.SetTrigger("SkillHit");
        }
    }
}
