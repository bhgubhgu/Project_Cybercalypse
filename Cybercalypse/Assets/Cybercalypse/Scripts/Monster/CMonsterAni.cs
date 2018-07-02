using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterAni : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : 일반 몬스터 애니메이션 구현 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    private Animator ani;
    private CMonsterInputCommand inputCommand;
    private CMonsterController control;
    private CMonster cMonster;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        inputCommand = GetComponent<CMonsterInputCommand>();
        control = GetComponent<CMonsterController>();
        cMonster = GetComponent<CMonster>();
    }

    private void Start()
    {
        if(control.monstetType == CMonsterController.EMonsterType.Ground)
        {
            inputCommand.HMove += HMoveAni;
        }
        
        if(control.monstetType == CMonsterController.EMonsterType.Fly)
        {
            inputCommand.FlyAttack += FlyAttackAni;
        }

        inputCommand.GroundAttack += GroundAttackAni;
    }

    private void Update()
    {
        ani.SetBool("isGrounded", control.IsGrounded);

        if(control.IsHit)
        {
            ani.SetTrigger("Hit");
        }

        if(cMonster.isDead)
        {
            ani.SetTrigger("Die"); 
            if(ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void HMoveAni(float hInputValue)
    {
        ani.SetFloat("moveSpeed", control.HorizontalVelocity);
    }

    public void GroundAttackAni()
    {
        ani.SetTrigger("GroundAttack");
    }

    public void FlyAttackAni()
    {
        ani.SetTrigger("FlyingAttack");
    }
}
