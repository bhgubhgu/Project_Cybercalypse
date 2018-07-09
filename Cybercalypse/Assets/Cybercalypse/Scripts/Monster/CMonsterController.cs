using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterController : APhysics
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : 몬스터 객체의 행동들을 입력을 받는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    public EMonsterKind monsterKind;
    public EMonsterType monstetType;

    private CMonsterInputCommand inputCommand;
    private GameObject groundAttack;
    private CCameraController mainCamera;
    private CMonster cMonster;

    public bool m_isDodge;
    public bool m_isJump;
    public bool m_isBossInvincible;
    private SpriteRenderer bossSprite;

    private bool isHitNow;
    #region Physics Value
    private float m_inputHMoveValue;

    private float m_hMoveVelocity;

    private float m_moveForce;

    private float m_jumpForce;
    #endregion

    #region Physics Value 접근
    public override float HInputValue
    {
        get
        {
            return m_inputHMoveValue;
        }
    }

    public override float VInputValue //입력 안받음
    {
        get
        {
            return 0;
        }
    }

    public override float HorizontalVelocity
    {
        get
        {
            return m_hMoveVelocity;
        } 
    }

    public override float VerticalVelocity //입력 안받음
    {
        get
        {
            return 0;
        }
    }

    public override float MoveForce
    {
        get
        {
            return m_moveForce;
        }
    }

    public override float HorizontalMoveAcceleration //입력 안받음
    {
        get
        {
            return 0;
        }
    }

    public override float JumpForce
    {
        get
        {
            return m_jumpForce;
        }
    }

    public override float GravityValue
    {
        get
        {
            return m_gravity;
        }
    }

    public override bool IsDashNow //사용 안함
    { 
        get
        {
            return m_isDodge;
        }
    }

    public override bool IsJumpNow
    {
        get
        {
            return m_isJumpNow;
        }
    }

    public override bool IsGrounded
    {
        get
        {
            return m_isGrounded;
        }
    }

    public override bool IsPlatform
    {
        get
        {
            return m_isPlatform;
        }
    }

    public override bool IsHeadHit
    {
        get
        {
            return m_isHeadHit;
        }
    }

    public override bool IsHit
    {
        get
        {
            return m_isHit;
        }
    }

    public override bool IsDownPlatform
    {
        get
        {
            return m_isDownPlatform;
        }
    }

    public override bool IsKnockback
    {
        get
        {
            return m_isKnockback;
        }
    }
    #endregion

    private void Awake()
    {
        whatIsGround = 1 << 8 | 1 << 20 | 1 << 0;
        whatIsPlatform = 1 << 20;
        whatIsPlayerHit = 1 << 9;

        inputCommand = this.gameObject.GetComponent<CMonsterInputCommand>();
        cMonster = this.gameObject.GetComponent<CMonster>();

        mainCamera = Camera.main.gameObject.GetComponent<CCameraController>();

        if(monsterKind == EMonsterKind.Apostle_Monster)
        {
            bossSprite = this.gameObject.GetComponent<SpriteRenderer>();
        }
    }

    private void Start()
    {
        Physics2D.autoSimulation = false; //Unity 물리 사용안함

        inputCommand.HMove += HMoveControl;
        inputCommand.GroundAttack += AttackControl;

        inputCommand.BossHeavyAttack += AttackControl;
        inputCommand.BossLightAttack += AttackControl;

        inputCommand.BossDodge += BossDodge;
        inputCommand.BossJump += BossJumpControl;
    }

    private void MonsterHit(bool isDetectEnemy, bool isDetectWater, Collider2D collisionObject)
    {
        if (isDetectEnemy && collisionObject.gameObject != null && !IsHit)
        {
            if(!m_isBossInvincible && monsterKind == EMonsterKind.Apostle_Monster)
            {
                if(collisionObject.gameObject.layer != 13)
                {
                    cMonster.GetDamage(140.0f);
                }
                else
                {
                    cMonster.GetDamage(100.0f);
                }
            }
            else
            {
                if (collisionObject.gameObject.layer != 13)
                {
                    cMonster.GetDamage(140.0f);
                }
                else
                {
                    cMonster.GetDamage(100.0f);
                }
            }
        }

        if (isDetectWater)
        {
            cMonster.isDead = true;
        }

        if(m_isKnockBackNow && monsterKind == EMonsterKind.Apostle_Monster)
        {
            BossJumpControl();
        }
    }

    public void HMoveControl(float hInputValue)
    {
        m_inputHMoveValue = hInputValue;
        m_hMoveVelocity = AIHMove(m_inputHMoveValue, MoveForce);
    }

    public void AttackControl()
    {
        //Monster Attack
    }

    public void BossJumpControl()
    {
        if(!m_isJump)
        {
            Jump(JumpForce);
            StartCoroutine(JumpCool());
        }
    }

    IEnumerator JumpCool()
    {
        m_isJump = true;

        yield return new WaitForSeconds(1.5f);

        m_isJump = false;
    }

    public override float AIHMove(float hInputValue, float moveForce)
    {
        return base.AIHMove(hInputValue, moveForce);
    }

    public override void Jump(float jumpForce)
    {
        if(Equals(monstetType, EMonsterKind.Follower_Monster))
        {
            return;
        }
        else
        {
            StartCoroutine(DodgeCool());
            base.Jump(jumpForce);
        }
    }

    public void BossDodge(bool isAttack)
    {
        if(!this.m_isDodge)
        {
            HorizontalAccel(MoveForce);
            StartCoroutine(DodgeCool());
        }
    }

    private IEnumerator DodgeCool()
    {
        this.m_isDodge = true;
        yield return new WaitForSeconds(0.5f);
        this.m_isDodge = false;
    }

    public override void HorizontalAccel(float moveForce)
    {
        if (Equals(monsterKind, EMonsterKind.Apostle_Monster))
        {
            base.HorizontalAccel(moveForce);
        }
    }

    public enum EMonsterKind
    {
        Apostle_Monster,
        Follower_Monster
    }

    public enum EMonsterType
    {
        Fly,
        Ground
    }
}
