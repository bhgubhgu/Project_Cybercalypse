using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossAni : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : 보스 몬스터 애니메이션 구현 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    private Animator ani;
    private CMonsterInputCommand inputCommand;
    private CMonsterController control;
    private CMonster cMonster;

    private bool flipflop = true;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        inputCommand = GetComponent<CMonsterInputCommand>();
        control = GetComponent<CMonsterController>();
        cMonster = GetComponent<CMonster>();
    }

    private void Start()
    {
        inputCommand.HMove += HMoveAni;
        inputCommand.GroundAttack += GroundAttackAni;
        inputCommand.BossLightAttack += GroundAttackAni;
        inputCommand.BossHeavyAttack += GroundAttackAni;
        inputCommand.BossDodge += BossDodge;
        inputCommand.BossJump += BossJump;
    }

    private void Update()
    {
        ani.SetBool("Grounded", control.IsGrounded);

        if (control.IsHit)
        {
            ani.SetTrigger("Hurt");
        }

        if (cMonster.isDead)
        {
            ani.SetTrigger("Death");

            //if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            //{
                this.gameObject.SetActive(false);
            //}
        }
    }

    public void HMoveAni(float hInputValue)
    {
        ani.SetFloat("moveSpeed", Mathf.Abs(control.HInputValue));
    }

    public void GroundAttackAni()
    {
        if (flipflop)
        {
            ani.SetTrigger("AttackLight");
        }
        else
        {
            ani.SetTrigger("AttackHeavy");
        }

        flipflop = !flipflop;
    }

    public void BossDodge(bool isAttack)
    {
        if(!isAttack)
        {
            ani.SetTrigger("Dodge");
        }
        else
        {
            ani.ResetTrigger("Dodge");
        }
    }

    public void BossJump()
    {
        ani.SetTrigger("JumpStart");
    }
}
