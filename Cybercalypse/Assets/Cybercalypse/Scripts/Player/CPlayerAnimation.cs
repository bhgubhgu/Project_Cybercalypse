using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerAnimation : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : Player 객체의 애니메이션을 구현하는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.07.17
    /// </summary>

    private CSkillLibrary skillPool;
    private Animator ani;
    private CPlayerController control;
    private CPlayerController playerController;
    private CSkillLibrary.SkillOffsetDel[] skillSlots = new CSkillLibrary.SkillOffsetDel[6];
    private ASkill.Skill skills;
    private int skillIndex;
    private bool isRun;
    private bool isAttackNow;

    private void Awake()
    {
        playerController = GetComponent<CPlayerController>(); //스킬 애니메이션을 위함
        ani = GetComponent<Animator>();
        control = GetComponent<CPlayerController>(); //플레이어의 공격 및 스킬 애니메이션을 제외한 애니메이션 판정할때 쓰임
        skillPool = CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>();
    }

    private void Start()
    {
        /* 인풋매니저의 인스턴스가 먼저 생성된 다음에 델리게이트로 등록 가능하다. 그래서 Start에서 인풋매니저의 델리게이트에 등록한다.*/
        CInputManager.instance.PlayerHMove += HMoveAni;
        CInputManager.instance.PlayerHRun += HRunAni;
        CInputManager.instance.PlayerVMove += VMoveAni;
        CInputManager.instance.Jump += JumpAni;
        CInputManager.instance.Dash += DashAni;
        CInputManager.instance.Skill1 += SkillAni;
        CInputManager.instance.Skill2 += SkillAni;
        CInputManager.instance.Skill3 += SkillAni;
        CInputManager.instance.Skill4 += SkillAni;

        CInputManager.instance.NormalAttack += NormalAttackAni;
        CInputManager.instance.SpecialAttack += SpecialAttackAni;

        skillSlots[0] = playerController.CActSkill1;
        skillSlots[1] = playerController.CActSkill2;
        skillSlots[2] = playerController.CActSkill3;
        skillSlots[3] = playerController.CActSkill4;
    }

    private void Update()
    {
        ani.SetFloat("gravity", control.GravityValue);
        ani.SetBool("isGrounded", control.IsGrounded);
        ani.SetBool("isJumpNow", control.IsJumpNow);
        ani.SetBool("isDashNow", control.IsDashNow);
        ani.SetBool("isRun", isRun);
        ani.SetBool("isAttackNow", isAttackNow);
    }

    /* delegate 메소드 */
    public void HMoveAni(float inputMoveValueInputManager)
    {
        isRun = false;
        ani.SetFloat("moveSpeed", Mathf.Abs(control.HorizontalVelocity));
    }

    public void HRunAni(float inputMoveValueInputManager)
    {
        isRun = true;
        ani.SetFloat("moveSpeed", Mathf.Abs(control.HorizontalVelocity));
    }

    public void VMoveAni(float inputMoveValueInputManager)
    {
        if(control.IsDownPlatform)
        {
            ani.SetBool("isGrounded", false);
            ani.SetFloat("gravity", 0.01f);
        }
    }

    //점프
    public void JumpAni(bool isDownCharacterJumpKeyInputManager)
    {
        if (!control.IsGrounded)
        {
            return;
        }

        ani.SetTrigger("accelerateUpward");
    }

    //구르기
    public void DashAni(bool isDownCharacterBlinkKeyInputMananger)
    {
        if(control.IsDashNow)
        {
            return;
        }

        ani.SetTrigger("accelerateHorizontally");
    }

    //스킬
    public void NormalAttackAni(bool isDownNormalAttackKey)
    {
        ani.SetTrigger("SlashAttack1");
    }

    public void SpecialAttackAni(bool isDownSpecialAttackKey)
    {
        ani.SetTrigger("SlashAttack2");
    }

    //스킬
    public void SkillAni(bool isDownSkillKey)
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            skills = skillPool.FindSKillSlot(skillSlots[0]);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            skills = skillPool.FindSKillSlot(skillSlots[1]);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetButtonDown("XBoxLT"))
        {
            skills = skillPool.FindSKillSlot(skillSlots[2]);
        }
        else if (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("XBoxRT"))
        {
            skills = skillPool.FindSKillSlot(skillSlots[3]);
        }

        skillIndex = skillPool.CheckSkillIndex(skills); //해당 스킬의 애니메이션 인덱스

        if(Equals(skillIndex, 0))
        {
            //다른 스킬 애니메이션이 나올때까지 아무것도 없음
        }
        else if(Equals(skillIndex, 1))
        {
            //다른 스킬 애니메이션이 나올때까지 아무것도 없음
        }
        else if (Equals(skillIndex, 2))
        {
            //다른 스킬 애니메이션이 나올때까지 아무것도 없음
        }
        else if (Equals(skillIndex, 3))
        {
            //다른 스킬 애니메이션이 나올때까지 아무것도 없음
        }
        else if (Equals(skillIndex, 4))
        {
            //다른 스킬 애니메이션이 나올때까지 아무것도 없음
        }
        else if (Equals(skillIndex, 5))
        {

        }
    }
}
