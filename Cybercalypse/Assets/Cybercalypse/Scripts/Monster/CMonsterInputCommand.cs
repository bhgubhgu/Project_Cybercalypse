using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterInputCommand : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : 몬스터 객체의 InputManager와 같은 동작을 하게하는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    public float groundAttackInterval;
    public CMonsterController.EMonsterKind kind;

    public delegate void Move(float hInputValue);
    public delegate void Jump();
    public delegate void Dodge(bool isAttack);
    public delegate void Attack();

    public event Move HMove;
    public event Dodge BossDodge;
    public event Jump BossJump;

    public event Attack GroundAttack;
    public event Attack FlyAttack;

    public event Attack BossLightAttack;
    public event Attack BossHeavyAttack;

    private bool isCheckPlayer;
    private bool isChasePlayer;
    private bool isGroundAttackPlayer;
    private bool isFlyAttackPlayer;

    private CMonster cMonster;
    private CMonsterController control;

    private bool isCorner;
    private Transform cornerChecker;
    private LayerMask whatIsGround;

    private bool isPlayerAttack;
    private Collider2D playerAttackObject;
    private Transform bossPlayerAttackChecker;
    private LayerMask whatIsPlayerAttack;

    #region Player Checker
    private GameObject player;
    private Collider2D playerCheck;
    private Transform playerChecker;
    private LayerMask whatIsPlayer;
    private float checkerRadius;
#endregion

    private float hInputValue;

    private void Awake()
    {
        playerChecker = this.transform.GetChild(8);
        whatIsPlayer = 1 << 9;
        whatIsGround = 1 << 8 | 1 << 0;
        whatIsPlayerAttack = 1 << 13  | 1 << 15  | 1 << 17;
        checkerRadius = 1.7f;
        cMonster = GetComponent<CMonster>();
        control = this.GetComponent<CMonsterController>();

        if(kind == CMonsterController.EMonsterKind.Follower_Monster)
        {
            cornerChecker = this.transform.GetChild(7).transform;
        }
        else if(kind == CMonsterController.EMonsterKind.Apostle_Monster)
        {
            bossPlayerAttackChecker = this.transform.GetChild(12);
        }
    }

    private void Start()
    {
        StartCoroutine(DefaultStatement());
    }

    private void Update()
    {
        isCheckPlayer = Physics2D.OverlapCircle(playerChecker.position, checkerRadius, whatIsPlayer);
        playerCheck = Physics2D.OverlapCircle(playerChecker.position, checkerRadius, whatIsPlayer);

        if (kind == CMonsterController.EMonsterKind.Follower_Monster)
        {
            isCorner = Physics2D.OverlapCircle(cornerChecker.position, 0.01f, whatIsGround);
        }
        else if (kind == CMonsterController.EMonsterKind.Apostle_Monster)
        {
            isPlayerAttack = Physics2D.OverlapCircle(bossPlayerAttackChecker.position, 1.5f, whatIsPlayerAttack);
            playerAttackObject = Physics2D.OverlapCircle(bossPlayerAttackChecker.position, 1.5f, whatIsPlayerAttack);
        }

        HMove(hInputValue);
    }

    private IEnumerator DefaultStatement()
    {
        while(true)
        {
            if(kind == CMonsterController.EMonsterKind.Apostle_Monster)
            {
                this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, this.transform.position.x - 1.0f, this.transform.position.x + 1.0f), this.transform.position.y, this.transform.position.z);
            }

            if(cMonster.isDead)
            {
                hInputValue = 0.0f;
                yield break;
            }

            if (playerCheck != null)
            {
                player = playerCheck.gameObject;
            }

            if(isCheckPlayer)
            {
                isChasePlayer = true;

                if(kind == CMonsterController.EMonsterKind.Follower_Monster)
                {
                    StartCoroutine(ChaseStatement());
                }
                else
                {
                    StartCoroutine(ChasePlayerForBoss());
                }

                yield return new WaitUntil(() => !isChasePlayer);
            }

            hInputValue = Random.Range(-1, 2);

            if (!isCorner && kind == CMonsterController.EMonsterKind.Follower_Monster)
            {
                if (this.transform.localScale.x > 0.0f)
                {
                    hInputValue = Random.Range(-1, 0);
                }
                else if(this.transform.localScale.x < 0.0f)
                {
                    hInputValue = Random.Range(1, 2);
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator ChaseStatement()
    {
        while(isChasePlayer)
        {
            if (cMonster.isDead)
            {
                hInputValue = 0.0f;
                yield break;
            }

            if(!isCorner && kind == CMonsterController.EMonsterKind.Follower_Monster)
            {
                isChasePlayer = false;
                hInputValue = 0.0f;
                yield break;
            }

            if ((this.transform.position.x - player.transform.position.x > groundAttackInterval))
            {
                isGroundAttackPlayer = false;
                isFlyAttackPlayer = false;
                hInputValue = -1;
            }
            else if(Mathf.Abs(this.transform.position.x - player.transform.position.x) < groundAttackInterval)
            {
                isGroundAttackPlayer = true;
                hInputValue = 0;
                GroundAttack();
                yield return new WaitForSeconds(Time.deltaTime * 60 * 0.5f);
            }
            else
            {
                isGroundAttackPlayer = false;
                isFlyAttackPlayer = false;
                hInputValue = 1;
            }

            if(!isCheckPlayer)
            {
                StartCoroutine(CheckStopChase());
            }

            yield return null;
        }

        player = null;
    }

    private IEnumerator ChasePlayerForBoss()
    {
        while (isChasePlayer)
        {
            if (cMonster.isDead)
            {
                hInputValue = 0.0f;
                yield break;
            }

            if ((this.transform.position.x > player.transform.position.x))
            {
                int possbility = Random.Range(0, 10);

                if ((possbility != 7 && !control.m_isDodge) || (possbility != 8 && !control.m_isDodge) || (possbility != 9 && !control.m_isDodge) || (possbility != 5 && !control.m_isDodge))
                {
                    control.m_isBossInvincible = true;
                    BossDodge(false);
                }
                else
                {
                    control.m_isBossInvincible = false;
                    hInputValue = -1;
                }
            }
            else if (Mathf.Abs(this.transform.position.x - player.transform.position.x) <= groundAttackInterval)
            {
                if(this.transform.position.y < player.transform.position.y)
                {
                    BossJump();
                }

                StartCoroutine(control.TestBossInvincible());
                hInputValue = 0;
                BossLightAttack();
                yield return new WaitForSeconds(Time.deltaTime * 60 * 0.5f);
                BossHeavyAttack();
            }
            else
            {
                int possbility = Random.Range(0, 10);

                if ((possbility != 7 && !control.m_isDodge) || (possbility != 8 && !control.m_isDodge) || (possbility != 9 && !control.m_isDodge) || (possbility != 5 && !control.m_isDodge))
                {
                    StartCoroutine(control.TestBossInvincible());
                    BossDodge(false);
                }
                else
                {
                    hInputValue = 1;
                }
            }

            if (isPlayerAttack)
            {
                if ((this.transform.position - playerAttackObject.gameObject.transform.position).magnitude >= 1.3f)
                {
                    BossJump();
                }
                else if (Mathf.Abs(this.transform.position.x - player.transform.position.x) <= groundAttackInterval)
                {
                    StartCoroutine(control.TestBossInvincible());
                    hInputValue = 0;
                    BossLightAttack();
                    yield return new WaitForSeconds(Time.deltaTime * 60 * 0.5f);
                    BossHeavyAttack();

                    control.m_isBossInvincible = false;
                }
                else
                {
                    if(!control.m_isDodge)
                    {
                        StartCoroutine(control.TestBossInvincible());
                        BossDodge(false);
                    }
                }
            }

            if (!isCheckPlayer)
            {
                StartCoroutine(CheckStopChase());
            }

            yield return null;
        }
    }

    private IEnumerator CheckStopChase()
    {
        for(float t = 0; t < 8f; t += Time.deltaTime)
        {
            if(isCheckPlayer)
            {
                yield break;
            }
            yield return null;
        }

        isChasePlayer = false;
    }
}
