using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerController : APhysics
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : Player 객체의 행동들을 입력을 받는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.07.16
    /// </summary>
    /// 

    private const float runAccel = 2f;

    private CSkillLibrary skillOffset_Instance;
    private List<CSkillLibrary.SkillOffsetDel> skillMethodList;
    private CExecutor executor;

    #region inputCheck --> 컨트롤러 부분
    private bool isDownSkill1;
    private bool isDownSkill2;
    private bool isDownSkill3;
    private bool isDownSkill4;
    private bool isDownSkillMouseLeft;
    private bool isDownSkillMouseRight;
    #endregion

    #region Physics Value
    private float m_hMoveVelocity;
    private float m_vMoveVelocity;

    private float m_moveForce;

    private float m_horizontalMoveAcceleration;

    private float m_jumpForce;

    private Vector3 mousePosition;
    #endregion

    #region Physics Value 접근
    public override float HInputValue
    {
        get
        {
            return m_hInputValue;
        }
    }

    public override float VInputValue
    {
        get
        {
            return m_vInputValue;
        }
    }

    public override float HorizontalVelocity
    {
        get
        {
            return m_hMoveVelocity;
        }
    }

    public override float VerticalVelocity
    {
        get
        {
            return m_vMoveVelocity;
        }
    }

    public override float MoveForce
    {
        get
        {
            return m_moveForce;
        }
    }

    public override float HorizontalMoveAcceleration
    {
        get
        {
            return m_horizontalMoveAcceleration;
        }
    }

    public override float KnockbackForce
    {
        get
        {
            return m_knockBackForce;
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

    public override bool IsDashNow
    {
        get
        {
            return m_isDashNow;
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
        whatIsGround = 1 << 8 | 1 << 20 | 1 << 0 | 1 << 21;
        whatIsPlayerHit = 1 << 25;
        executor = this.GetComponent<CExecutor>();
    }

    private void Start()
    {
        Physics2D.autoSimulation = false; //Unity 물리 사용안함
        Physics2D.callbacksOnDisable = false;

        skillOffset_Instance = CGameManager.instance.skillLibrary.GetComponent<CSkillLibrary>();

        skillMethodList = new List<CSkillLibrary.SkillOffsetDel>();

        #region regist event
        /* Physics Control */
        CInputManager.instance.PlayerHMove += HMoveControl;
        CInputManager.instance.PlayerHRun += HRunControl;
        CInputManager.instance.PlayerVMove += VMoveControl;
        CInputManager.instance.Jump += JumpControl;
        CInputManager.instance.Dash += HorizontalAccelControl;

        /* Attack */
        CInputManager.instance.NormalAttack += NormalAttackContorl;
        CInputManager.instance.SpecialAttack += SpecialAttackContorl;

        /* CastSkill */
        CInputManager.instance.Skill1 += CActSkill1;
        CInputManager.instance.Skill2 += CActSkill2;
        CInputManager.instance.Skill3 += CActSkill3;
        CInputManager.instance.Skill4 += CActSkill4;
        #endregion

        #region skillMethod Regist
        skillMethodList.Add(CActSkill1);
        skillMethodList.Add(CActSkill2);
        skillMethodList.Add(CActSkill3);
        skillMethodList.Add(CActSkill4);
        #endregion
        
        for(int i = 0; i < skillMethodList.Count; i++)
        {
            skillOffset_Instance.SetSkillSlot(skillMethodList[i]);
        }

        //Get Player Data
        m_moveForce = executor.MoveForce;
        m_jumpForce = executor.JumpForce;
        m_horizontalMoveAcceleration = executor.DodgeForce;
        m_knockBackForce = executor.KnockbackForce;
    }

    #region InputManager Event Method

    public void HMoveControl(float hInputValue)
    {
        if(hInputValue != 0 && m_isGrounded)
        {
            CAudioManager.instance.PlayEffectSoundMoveEvent(HMoveControl);
        }

        m_hInputValue = hInputValue;
        m_hMoveVelocity = HMove(m_moveForce, m_hInputValue);
    }

    public void HRunControl(float hInputValue)
    {
        if (hInputValue != 0 && m_isGrounded)
        {
            CAudioManager.instance.PlayEffectSoundMoveEvent(HMoveControl);
        }

        m_hInputValue = hInputValue;        
        m_hMoveVelocity = HMove(m_moveForce * runAccel , m_hInputValue);
    }

    public void VMoveControl(float vInputValue)
    {
        m_vMoveVelocity = VMove(vInputValue);
    }

    public void JumpControl(bool isJumpKeyDown)
    {
        if(m_isGrounded)
        {
            CAudioManager.instance.PlayEffectSoundUniqueEvent(JumpControl);
        }

        if(isJumpKeyDown)
        {
            Jump(m_jumpForce);
        }
    }

    public void HorizontalAccelControl(bool isHorizontalAccelKeyDown)
    {
        if(!m_isDashNow && m_isGrounded)
        {
            CAudioManager.instance.PlayEffectSoundUniqueEvent(HorizontalAccelControl);
            HorizontalAccel(m_horizontalMoveAcceleration);
        }
    }
    #endregion

    #region Virtual Override Method
    public override float HMove(float moveForce, float hInputValue)
    {
        return base.HMove(moveForce, hInputValue);
    }

    public override float VMove(float vInputValue)
    {
        if(vInputValue < 0 && m_isPlatform && Input.GetButtonDown("Accelerate Upward"))
        {
            m_isGrounded = false;
        }

        return base.VMove(vInputValue);
    }

    public override void HorizontalAccel(float moveForce)
    {
        base.HorizontalAccel(moveForce);
    }

    public override void Jump(float jumpForce)
    {
        if(CGameManager.instance.isDead)
        {
            return;
        }

        base.Jump(jumpForce);
    }
    #endregion

    #region 공격 키 메소드
    public void NormalAttackContorl(bool isDownNormalAttackKey)
    {
        //Debug.Log("Normal Attack!");
    }

    public void SpecialAttackContorl(bool isDownSpecialAttackKey)
    {
        Debug.Log("Special Attack!");
    }
#endregion

    #region 스킬 발사 키 메소드
    public void CActSkill1(bool isDownSkill1Manager)
    {
        isDownSkill1 = isDownSkill1Manager;
        //Skill Slot 1 Key

        skillOffset_Instance.FindSKillOffset(CActSkill1);
    }

    public void CActSkill2(bool isDownSkill2Manager)
    {
        isDownSkill2 = isDownSkill2Manager;

        //Skill Slot 2 Key
        skillOffset_Instance.FindSKillOffset(CActSkill2);
    }

    public void CActSkill3(bool isDownSkill3Manager)
    {
        isDownSkill3 = isDownSkill3Manager;

        //Skill Slot 3 Key
        skillOffset_Instance.FindSKillOffset(CActSkill3);
    }

    public void CActSkill4(bool isDownSkill4Manager)
    {
        isDownSkill4 = isDownSkill4Manager;

        //Skill Slot 4 Key
        skillOffset_Instance.FindSKillOffset(CActSkill4);
    }
#endregion
}












/* 아카이브 */
/*private void CheckCalypseAABBVertical(Collider2D colliders) //바닦, 머리 충돌
{
    if(colliders == null)
    {
        m_isGrounded = false;
        m_isPlatform = false;
        m_isHeadCollide = false;
        m_isSlope = false;
        m_angle = 0.0f;
        return;
    }

    CapsuleCollider2D test = GetComponent<CapsuleCollider2D>();

    Vector3 center = test.bounds.center;
    Vector3 size = test.bounds.size;

    float boundBottom = Mathf.Round((-size.y * 0.5f + center.y ) * 100) * 0.01f;
    float boundTop = Mathf.Round((boundBottom + size.y )* 100) * 0.01f;

    Vector3 Hcenter = colliders.bounds.center;
    Vector3 Hsize = colliders.bounds.size;


    float HboundBottom = Mathf.Round((-Hsize.y * 0.5f + Hcenter.y) * 100) * 0.01f;
    float HboundTop = Mathf.Round((HboundBottom + Hsize.y) * 100) * 0.01f;

    if(colliders.gameObject.layer == 21) //오직 정점 3개의 3각형 PolygonCollider만 Slope 사용 가능(RayCast 없이)
    {
        m_isGrounded = true;
        m_isSlope = true;

        PolygonCollider2D slopeCollider = colliders.GetComponent<PolygonCollider2D>(); //성능에 영향 x
        Vector2 firstPoint = slopeCollider.points[0]; //왼쪽 정점
        Vector2 lastPoint = slopeCollider.points[2]; //Top 정점(꼭대기 꼭짓점)
        Vector2 vectorAngle = lastPoint - firstPoint; //가장 높은곳에서 가장 낮고 플레이어가 가장 먼저 접하는 접점을 빼줌

        m_angle = Mathf.Atan2(vectorAngle.y, vectorAngle.x) * Mathf.Rad2Deg; //Slope에 해당하는 벡터와 밑변 벡터의 사잇각을 구함
        return;
    }
    else
    {
        m_angle = 0;
        m_isSlope = false;
    }

    //머리, 발닿기
    if (boundTop >= HboundTop && boundBottom <= HboundTop && colliders.gameObject.layer != 20)
    {
        m_isGrounded = true;
        m_isHeadCollide = false;
        m_isPlatform = false;

        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, Hcenter.y + Hsize.y * 0.5f), 15f * Time.deltaTime);
    }
    else if (boundBottom <= HboundBottom && boundTop <= HboundTop && colliders.gameObject.layer != 20)
    {
        m_isHeadCollide = true;
        m_isGrounded = false;
    }
    else if (boundTop >= HboundTop && boundBottom <= HboundTop && colliders.gameObject.layer == 20 && this.transform.position.y == previousPosition.y)
    {
        m_isGrounded = true;
        m_isHeadCollide = false;
        m_isPlatform = true;
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, Hcenter.y + Hsize.y * 0.5f), 15f * Time.deltaTime);
    }
    else
    {
        m_isGrounded = false;
        m_isPlatform = false;
        m_isHeadCollide = false;
    }
}

private void CheckCalypseAABBHorizontal(Collider2D colliders) //왼쪽, 오른쪽 전용
{
    if (colliders == null)
    {
        m_isLeftCheck = false;
        m_isRightCheck = false;
        return;
    }

    CapsuleCollider2D test = GetComponent<CapsuleCollider2D>();

    Vector3 center = test.bounds.center;
    Vector3 size = test.bounds.size;

    float boundLeft = Mathf.Round((-size.x * 0.5f + center.x) * 100) * 0.01f;
    float boundRight = Mathf.Round((boundLeft + size.x) * 100) * 0.01f;

    Vector3 Hcenter = colliders.bounds.center;
    Vector3 Hsize = colliders.bounds.size;

    float HboundLeft = Mathf.Round((-Hsize.x * 0.5f + Hcenter.x) * 100) * 0.01f;
    float HboundRight = Mathf.Round((HboundLeft + Hsize.x) * 100) * 0.01f;

    if (boundLeft >= HboundRight && boundRight >= HboundLeft && colliders.gameObject.layer != 20)
    {
        m_isLeftCheck = true;
        m_isRightCheck = false;

        Vector3 testVector = new Vector3(this.transform.position.x + center.x - (-size.x * 0.5f + center.x), transform.position.y);

        this.transform.position = Vector3.Lerp(this.transform.position, testVector, 2.5f * Time.deltaTime);
    }
    else if(boundRight >= HboundLeft && boundLeft <= HboundRight && colliders.gameObject.layer != 20)
    {
        m_isRightCheck = true;
        m_isLeftCheck = false;

        Vector3 testVector = new Vector3(this.transform.position.x + center.x - (+size.x * 0.5f + center.x), transform.position.y);

        this.transform.position = Vector3.Lerp(this.transform.position, testVector, 2.5f * Time.deltaTime);
    }
}

private void Update()
{
    CheckCalypseAABBVertical(Physics2D.OverlapBox(this.gameObject.GetComponent<CapsuleCollider2D>().bounds.center, new Vector2(0, this.GetComponent<CapsuleCollider2D>().size.y), 0, whatIsGround));
    CheckCalypseAABBHorizontal(Physics2D.OverlapBox(this.gameObject.GetComponent<CapsuleCollider2D>().bounds.center, new Vector2(this.GetComponent<CapsuleCollider2D>().size.x, 0), 0, whatIsGround));

    //중력
    m_gravity = base.Gravity(m_isGrounded);
    previousPosition = this.transform.position;
}*/