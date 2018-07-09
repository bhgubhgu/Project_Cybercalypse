using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APhysics : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : 모든 움직이는 동적인 객체들의 움직일 수 있고 여러 행동들을 할 수 있게 하는 물리 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.07.09
    /// </summary>
    const float gravityVelocity = 9.8f;
    const float knockbackSmoother = 100.0f;
    const float playerMass = 1;
    const float playerGravity = 1;

    #region abstract Property
    public abstract float HInputValue
    {
        get;
    }

    public abstract float VInputValue
    {
        get;
    }

    public abstract float GravityValue
    {
        get;
    }
    public abstract float HorizontalVelocity
    {
        get;
    }
    public abstract float VerticalVelocity
    {
        get;
    }
    public abstract float MoveForce
    {
        get;
    }
    public abstract float JumpForce
    {
        get;
    }
    public abstract float HorizontalMoveAcceleration
    {
        get;
    }
    
    public abstract bool IsDashNow
    {
        get;
    }

    public abstract bool IsJumpNow
    {
        get;
    }

    public abstract bool IsGrounded
    {
        get;
    }

    public abstract bool IsPlatform
    {
        get;
    }

    public abstract bool IsHeadHit
    {
        get;
    }

    public abstract bool IsHit
    {
        get;
    }

    public abstract bool IsDownPlatform
    {
        get;
    }

    public abstract bool IsKnockback
    {
        get;
    }
    #endregion

    #region 각종 물리 상태에 필요한 변수들
    //충돌 체크 --> 충돌체크 부분 모두 삭제
    protected LayerMask whatIsGround;

    protected LayerMask whatIsPlatform;

    protected LayerMask whatIsHeadCheckLayer;

    protected LayerMask whatIsPlayerHit;
    protected LayerMask whatIsMonsterHit;
    //중력, 이동, 점프
    protected float m_hVelocity;
    protected float m_vVelocity;
    protected float m_vInputValue;
    protected float m_hInputValue;
    protected float m_jumpVelocity;
    protected float m_dashVelocity;
    protected float m_jumpTime;
    protected float m_gravity;

    //상태 체크 --> 안쓰는 충돌상태체크변수 삭제
    protected bool m_isJumpNow;
    protected bool m_isDashNow;
    protected bool m_isGrounded;
    protected bool m_isPlatform;
    protected bool m_isHeadHit;
    protected bool m_isKnockback;
    protected bool m_isHit;
    protected bool m_isDownPlatform;

    //New Collision --> 위와 같음
    protected bool m_isKnockBackNow;
    protected bool m_isHeadCollide;
    protected bool m_isLeftCheck;
    protected bool m_isRightCheck;

    protected Vector3 previousPosition;
    protected float m_angle;
    protected bool m_isSlope;
    #endregion

    private void Update()
    {
        CheckCalypseAABBVertical(Physics2D.OverlapBox(this.gameObject.GetComponent<CapsuleCollider2D>().bounds.center, new Vector2(0, this.GetComponent<CapsuleCollider2D>().size.y), 0, whatIsGround));
        CheckCalypseAABBHorizontal(Physics2D.OverlapBox(this.gameObject.GetComponent<CapsuleCollider2D>().bounds.center, new Vector2(this.GetComponent<CapsuleCollider2D>().size.x * 2f, 0), 0, whatIsGround));
        CheckCalypseAABBHit(Physics2D.OverlapBox(this.gameObject.GetComponent<CapsuleCollider2D>().bounds.center, new Vector2(this.GetComponent<CapsuleCollider2D>().size.x, this.GetComponent<CapsuleCollider2D>().size.y), 0, whatIsPlayerHit));

        m_gravity = Gravity(m_isGrounded);

        //Position Check
        previousPosition = this.transform.position;
    }

    private void CheckCalypseAABBHit(Collider2D colliders)
    {
        //아무런 콜리더 오브젝트를 감지하지 못하면 리턴 시킨다.
        if (colliders == null)
        {
            return;
        }

        CapsuleCollider2D playerCollider = GetComponent<CapsuleCollider2D>();

        Vector3 pCenter = playerCollider.bounds.center;
        Vector3 pSize = playerCollider.bounds.size;

        float pBoundLeft = Mathf.Round((-pSize.x * 0.5f + pCenter.x) * 100) * 0.01f;
        float pBoundRight = Mathf.Round((pBoundLeft + pSize.x) * 100) * 0.01f;
        float pBoundBottom = Mathf.Round((-pSize.y * 0.5f + pCenter.y) * 100) * 0.01f;
        float pBoundTop = Mathf.Round((pBoundBottom + pSize.y) * 100) * 0.01f;

        Vector3 hCenter = colliders.bounds.center;
        Vector3 hSize = colliders.bounds.size;

        float hBoundLeft = Mathf.Round((-hSize.x * 0.5f + hCenter.x) * 100) * 0.01f;
        float hBoundRight = Mathf.Round((hBoundLeft + hSize.x) * 100) * 0.01f;
        float hBoundBottom = Mathf.Round((-hSize.y * 0.5f + hCenter.y) * 100) * 0.01f;
        float hBboundTop = Mathf.Round((hBoundBottom + hSize.y) * 100) * 0.01f;

        ///<summary>
        ///매 프레임 마다 맞으면 안된다.
        ///만약 Hit 했을 경우 바로 무적이 되고 무적 될 동안은 Hit 되서는 안된다.
        ///플레이어와 몬스터의 경계를 나눠야 한다.
        ///</summary>
        
        //플레이어 왼쪽 넉백
        if((pBoundRight >= hBoundLeft && pBoundLeft <=hBoundLeft && (pBoundTop <= hBoundBottom || pBoundBottom <= hBboundTop) && !CGameManager.instance.isPlayerInvincible) /*|| (pBoundLeft <= hBoundRight && ((pBoundTop <= hBoundBottom || pBoundBottom <= hBboundTop)) && !CGameManager.instance.isPlayerInvincible)*/)
        {
            //플레이어가 아닐시 리턴
            if (colliders.gameObject.layer == 9)
            {
                return;
            }
            else
            {
                CGameManager.instance.PlayerHasInvincible(); // 플레이어 만의 무적 판정
                Hit(0.01f, -1f);
            }
        }
        //플레이어 오른쪽 넉백
        else if ((pBoundLeft <= hBoundRight && pBoundLeft >= hBoundLeft && ((pBoundTop <= hBoundBottom || pBoundBottom <= hBboundTop)) && !CGameManager.instance.isPlayerInvincible))
        {
            if (colliders.gameObject.layer == 9)
            {
                return;
            }
            else
            {
                CGameManager.instance.PlayerHasInvincible(); // 플레이어 만의 무적 판정
                Hit(0.01f, +1f);
            }
        }

        ///<summary>
        ///매 프레임 마다 맞으면 안된다.
        ///만약 Hit 했을 경우 바로 무적이 되고 무적 될 동안은 Hit 되서는 안된다.
        ///플레이어와 몬스터의 경계를 나눠야 한다.
        ///</summary>
        //몬스터 왼쪽 넉백
        if ((pBoundRight >= hBoundLeft && pBoundLeft <= hBoundLeft && (pBoundTop <= hBoundBottom || pBoundBottom <= hBboundTop)))
        {
            //몬스터가 아닐시 리턴
            if(colliders.gameObject.layer == 25)
            {
                return;
            }
            else
            {
                Hit(0.01f, -1f); //Test
                return;
            }
        }
        //몬스터 오른쪽 넉백
        else if ((pBoundLeft <= hBoundRight && pBoundLeft >= hBoundLeft && ((pBoundTop <= hBoundBottom || pBoundBottom <= hBboundTop))))
        {
            if (colliders.gameObject.layer == 25)
            {
                return;
            }
            else
            {
                Hit(0.01f, +1f); //Test
            }
        }
    }

    private void CheckCalypseAABBVertical(Collider2D colliders) //바닦, 머리 충돌
    {
        if (colliders == null)
        {
            m_isGrounded = false;
            m_isPlatform = false;
            m_isHeadCollide = false;
            m_isSlope = false;
            m_angle = 0.0f;
            return;
        }

        CapsuleCollider2D playerCollider = GetComponent<CapsuleCollider2D>();

        Vector3 pCenter = playerCollider.bounds.center;
        Vector3 pSize = playerCollider.bounds.size;

        float pBoundBottom = Mathf.Round((-pSize.y * 0.5f + pCenter.y) * 100) * 0.01f;
        float pBoundTop = Mathf.Round((pBoundBottom + pSize.y) * 100) * 0.01f;

        Vector3 hCenter = colliders.bounds.center;
        Vector3 hSize = colliders.bounds.size;


        float hBoundBottom = Mathf.Round((-hSize.y * 0.5f + hCenter.y) * 100) * 0.01f;
        float hBboundTop = Mathf.Round((hBoundBottom + hSize.y) * 100) * 0.01f;

        if (colliders.gameObject.layer == 21) //오직 정점 3개의 3각형 PolygonCollider만 Slope 사용 가능(RayCast 없이)
        {
            m_isGrounded = true;
            m_isSlope = true;

            PolygonCollider2D slopeCollider = colliders.GetComponent<PolygonCollider2D>();//colliders.GetComponent<PolygonCollider2D>(); //성능에 영향 x
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
        if (pBoundTop >= hBboundTop && pBoundBottom <= hBboundTop && colliders.gameObject.layer != 20)
        {
            m_isGrounded = true;
            m_isHeadCollide = false;
            m_isPlatform = false;

            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, hCenter.y + hSize.y * 0.5f), 15f * Time.deltaTime);
        }
        else if (pBoundBottom <= hBoundBottom && pBoundTop <= hBboundTop && colliders.gameObject.layer != 20)
        {
            m_isHeadCollide = true;
            m_isGrounded = false;
        }
        else if (pBoundTop >= hBboundTop && pBoundBottom <= hBboundTop && colliders.gameObject.layer == 20 && this.transform.position.y == previousPosition.y)
        {
            m_isGrounded = true;
            m_isHeadCollide = false;
            m_isPlatform = true;
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, hCenter.y + hSize.y * 0.5f), 15f * Time.deltaTime);
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

        CapsuleCollider2D playerCollider = GetComponent<CapsuleCollider2D>();

        Vector3 pCenter = playerCollider.bounds.center;
        Vector3 pSize = playerCollider.bounds.size;

        float pBoundLeft = Mathf.Round((-pSize.x * 0.5f + pCenter.x) * 100) * 0.01f;
        float pBoundRight = Mathf.Round((pBoundLeft + pSize.x) * 100) * 0.01f;

        Vector3 hCenter = colliders.bounds.center;
        Vector3 hSize = colliders.bounds.size;

        float hBoundLeft = Mathf.Round((-hSize.x * 0.5f + hCenter.x) * 100) * 0.01f;
        float hBoundRight = Mathf.Round((hBoundLeft + hSize.x) * 100) * 0.01f;

        if (pBoundLeft >= hBoundRight && pBoundRight >= hBoundLeft && colliders.gameObject.layer != 20)
        {
            m_isLeftCheck = true;
            m_isRightCheck = false;

            Vector3 interpolationVector = new Vector3(this.transform.position.x + pCenter.x - (-pSize.x * 0.5f + pCenter.x), transform.position.y);

            this.transform.position = Vector3.Lerp(this.transform.position, interpolationVector, 2.5f * Time.deltaTime);
        }
        else if (pBoundRight >= hBoundLeft && pBoundLeft <= hBoundRight && colliders.gameObject.layer != 20)
        {
            m_isRightCheck = true;
            m_isLeftCheck = false;

            Vector3 interpolationVector = new Vector3(this.transform.position.x + pCenter.x - (+pSize.x * 0.5f + pCenter.x), transform.position.y);

            this.transform.position = Vector3.Lerp(this.transform.position, interpolationVector, 2.5f * Time.deltaTime);
        }
    }


    #region Physics Virtual Method for Player
    public virtual float HMove(float moveForce, float hInputValue)
    {
        if (hInputValue > 0)
        {
            this.transform.localScale = new Vector3(+1, 1, 1);
        }
        else if (hInputValue < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        switch (MoveCondition(hInputValue, this.transform.localScale.x))
        {
            case ELiveMoveStatement.rightFoward:
                m_hVelocity = +moveForce * (0.25f * (playerMass + playerGravity)) * Time.deltaTime;
                break;
            case ELiveMoveStatement.leftFoward:
                m_hVelocity = -moveForce * (0.25f * (playerMass + playerGravity)) * Time.deltaTime;
                break;
            case ELiveMoveStatement.wait:
                m_hVelocity = 0.0f;
                break;
        }

       if ((m_isLeftCheck && hInputValue < 0.0f) || (m_isRightCheck && hInputValue > 0.0f))
        {
            return m_hVelocity;
        }

        Vector3 velocityVector = new Vector3(m_hVelocity, 0);

       if (m_isSlope)
        {
            float distance = velocityVector.x;
            velocityVector.y = Mathf.Sin(m_angle * Mathf.Deg2Rad) * Mathf.Abs(distance) * Mathf.Sign(velocityVector.x); 
            velocityVector.x = Mathf.Cos(m_angle * Mathf.Deg2Rad) * Mathf.Abs(distance) * Mathf.Sign(velocityVector.x);
        }

        this.transform.position = Vector3.Lerp(this.transform.position, velocityVector + this.transform.position, 4f * Time.deltaTime);
        return m_hVelocity;
    }

    public virtual float VMove(float vInputValue)
    {
        m_vInputValue = vInputValue;

        if ((m_isPlatform && m_vInputValue < 0.0f && Input.GetButtonDown("Accelerate Upward")) || (m_isPlatform && Input.GetAxisRaw("XBoxVertical") > 0.0f && Input.GetKeyDown(KeyCode.JoystickButton0))) //컨트롤러 키도 추가
        {
            StartCoroutine(DownPlatformMove());
        }

        return 0;
    }
    public virtual void HorizontalAccel(float moveForce)
    {
        if(m_isDashNow)
        {
            return;
        }

        StartCoroutine(ActionDash(moveForce));
    }
    #endregion

    #region For Player Method
    private ELiveMoveStatement MoveCondition(float moveValue, float playerDir)
    {
        if (playerDir > 0 && moveValue > 0.0f)
        {
            return ELiveMoveStatement.rightFoward;
        }
        else if (playerDir < 0 && moveValue < 0.0f)
        {
            return ELiveMoveStatement.leftFoward;
        }
        else
        {
            return ELiveMoveStatement.wait;
        }
    }
    #endregion

    #region Physics Virtual Method for Object
    public virtual void Jump(float jumpForce)
    {
        if ((m_isPlatform && m_vInputValue < 0.0f && Input.GetButtonDown("Accelerate Upward")) || (m_isPlatform && Input.GetAxisRaw("XBoxVertical") > 0.0f && Input.GetKeyDown(KeyCode.JoystickButton0))) //컨트롤러 키도 추가
        {
            return;
        }

        if (m_isGrounded || m_isPlatform && m_vInputValue >= 0.0f)
        {
            StartCoroutine(ActionJump(jumpForce));
        }
    }
    public virtual float Gravity(bool isGrounded)
    {
        if(!isGrounded || m_isDownPlatform)
        {
            m_gravity += 0.25f * gravityVelocity * 0.5f * Time.deltaTime;
            m_gravity = Mathf.Clamp(m_gravity, 0.0f, 1.5f);
        }
        else
        {
            m_gravity = 0.0f;
        }

        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y  - m_gravity, this.transform.position.z), 3.5f * Time.deltaTime); //2.5f
        return m_gravity;
    }

    public virtual void Hit(float knockBackForce, float hitDir)
    {
        StartCoroutine(Knockback(knockBackForce, hitDir));
    }
    #endregion

    #region Physics Virtual Method for Monster
    public virtual float AIHMove(float hInputValue, float moveForce)
    {
        moveForce = 0.02f;//수정

        if (hInputValue > 0)
        {
            this.transform.localScale = new Vector3(+1, 1, 1);
        }
        else if (hInputValue < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        switch (MoveCondition(hInputValue)) //up down 추가 예정
        {
            case ELiveMoveStatement.rightFoward:
                m_hVelocity += +moveForce * (0.25f * (playerMass + playerGravity)) * Time.deltaTime * 60 * 0.15f;
                break;
            case ELiveMoveStatement.leftFoward:
                m_hVelocity += -moveForce * (0.25f * (playerMass + playerGravity)) * Time.deltaTime * 60 * 0.15f;
                break;
            case ELiveMoveStatement.wait:
                m_hVelocity = 0.0f;
                break;
        }

        m_hVelocity = Mathf.Clamp(m_hVelocity, -0.008f, 0.008f);

        if (m_isLeftCheck || m_isRightCheck)
        {
            this.transform.Translate(new Vector2(0, 0));
            return m_hVelocity;
        }

        this.transform.Translate(new Vector2(m_hVelocity, 0));
        return m_hVelocity;
    }
    #endregion

    #region For Monster Method

    private ELiveMoveStatement MoveCondition(float moveValue)
    {
        if (moveValue > 0.0f)
        {
            return ELiveMoveStatement.rightFoward;
        }
        else if (moveValue < 0.0f)
        {
            return ELiveMoveStatement.leftFoward;
        }
        else
        {
            return ELiveMoveStatement.wait;
        }
    }

    private IEnumerator GroundAttack(GameObject groundAttack)
    {
        yield return new WaitForSeconds(0.1f);

        groundAttack.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        groundAttack.SetActive(false);
    }
    #endregion

    #region physics for coroutine
    public IEnumerator ActionJump(float jumpForce)
    {
        for (float lessTime = 1f; m_jumpVelocity >= -Mathf.Epsilon ; m_jumpTime += Time.deltaTime , lessTime -= Time.deltaTime)
        {
            if((m_isKnockback || Input.GetButtonUp("Accelerate Upward") || Input.GetKeyUp(KeyCode.Joystick1Button0) || m_isHeadCollide))
            {
                m_isJumpNow = false;
                m_jumpVelocity = 0.0f;
                m_jumpTime = 0.0f;
                yield break;
            }

            m_gravity = 0.0f;
            m_isJumpNow = true;
            m_jumpVelocity = (Mathf.Pow(m_jumpTime, 2f) * -gravityVelocity * 0.5f) + (m_jumpTime * jumpForce);

            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y + m_jumpVelocity, this.transform.position.z), 8.5f * Mathf.Abs(lessTime) * Time.deltaTime);
            yield return null;
        }

        m_isJumpNow = false;
        m_jumpVelocity = 0.0f;
        m_jumpTime = 0.0f;
    }

    public IEnumerator ActionDash(float dashForce)
    {
        m_dashVelocity = 0.0f;
        dashForce = 4f;

        for (float dashTime = 0, lessTime = 1f; m_dashVelocity >= -Mathf.Epsilon ; dashTime += Time.deltaTime, lessTime -= Time.deltaTime)
        {
            if (m_isLeftCheck || m_isRightCheck || m_isKnockback)
            {
                m_isDashNow = false;
                yield break;
            }

            m_isDashNow = true;
            m_dashVelocity = (Mathf.Pow(dashTime, 2f) * -gravityVelocity * 0.5f) + (dashTime * dashForce);

            Vector3 velocityVector = new Vector3(m_dashVelocity * this.transform.localScale.x, 0);

            if (m_isSlope)
            {
                float distance = velocityVector.x;
                velocityVector.y = Mathf.Sin(Mathf.Abs(m_angle) * Mathf.Deg2Rad) * Mathf.Abs(distance);
                velocityVector.x = Mathf.Cos(Mathf.Abs(m_angle) * Mathf.Deg2Rad) * Mathf.Abs(distance) * Mathf.Sign(velocityVector.x);
            }

            this.transform.position = Vector3.Lerp(this.transform.position, velocityVector + this.transform.position, 3.5f * lessTime * Time.deltaTime);
            yield return null;
        }

        m_dashVelocity = 0.0f;
        m_isDashNow = false;
        yield break;
    }

    public IEnumerator DownPlatformMove()
    {
        Vector3 testPosition = new Vector3(this.transform.position.x, this.transform.position.y - 0.3f, this.transform.position.z);

        while (this.transform.position.y >= testPosition.y)
        {
            m_isDownPlatform = true;
            m_isGrounded = false;
            this.transform.Translate(new Vector2(0, 0.7f * -Time.deltaTime));
            yield return null;
        }

        m_isDownPlatform = false;
    }

    public IEnumerator Knockback(float moveForce, float dir)
    {
        if (m_isKnockback)
        {
            yield  break;
        }

        float knockbackForce = 0.1f; //무기,스킬에 따라서 넉백 크기가 달라진다.(임시용) (1~3)
        float knockBackDistance = dir * (((7 * (knockbackForce + 2) * knockbackForce) / 101f) + 9) * 2 * moveForce;
        float knockback = 0.0f;
        float knockbackGoal = this.transform.position.x + knockBackDistance;

        m_isKnockback = true;

        while ((this.transform.position.x - knockbackGoal >= Mathf.Epsilon && dir < 0.0f) || (knockbackGoal - this.transform.position.x >= Mathf.Epsilon && dir > 0.0f))
        {
            if(m_isLeftCheck || m_isRightCheck)
            {
                m_isKnockback = false;
                yield break;
            }

            knockback += dir * (moveForce * 7 * (0.25f * (playerMass + playerGravity)));

            Vector3 velocityVector = new Vector3(knockback, 0);

            if (m_isSlope)
            {
                float distance = velocityVector.x;
                velocityVector.y = Mathf.Sin(Mathf.Abs(m_angle) * Mathf.Deg2Rad) * Mathf.Abs(distance);
                velocityVector.x = Mathf.Cos(Mathf.Abs(m_angle) * Mathf.Deg2Rad) * Mathf.Abs(distance) * Mathf.Sign(velocityVector.x);
            }

            this.transform.position = Vector3.Lerp(this.transform.position, velocityVector + this.transform.position, knockbackSmoother * Time.deltaTime);
            yield return null;
        }
        
        m_isKnockback = false;
    }
    #endregion

    public enum ELiveMoveStatement
    {
        rightFoward,
        leftFoward,
        wait
    }
}