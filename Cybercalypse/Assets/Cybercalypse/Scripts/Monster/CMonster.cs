using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonster : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모, 윤동준
    /// 스크립트 : 몬스터 객체의 내부 속성들과 HP 등등을 체크하고 스탯을 나타내는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    /// <summary>
    /// 클래스 순서
    /// 1. 필드
    /// 2. 필드 - 프로퍼티
    /// 3. 매직 함수
    /// 4. 커스텀 함수
    /// 5. 코루틴
    /// </summary>

    public float maximumHP;

    public string Name { get; set; }
    
    public float Maximum_Health { get; set; }
    public float Current_Health { get; set; }

    private Transform hpBar;
    private SpriteRenderer hpBarRenderer;
    private SpriteRenderer myRenderer;
    private BoxCollider2D boxCollider;


    private float SumVisibleTime { get; set; }
    private float BarVisibleTime { get; set; }
    private float maxHPBarSize;

    public bool isHit { get; set; }
    public bool isDead { get; set; }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Use this for initialization
    void Start () {
        Maximum_Health = maximumHP; //Boss 체력
        Current_Health = Maximum_Health;

        hpBar = transform.Find("HP_Bar").Find("Monster_HP_Center");
        hpBarRenderer = hpBar.GetChild(0).GetComponent<SpriteRenderer>();
        maxHPBarSize = hpBarRenderer.size.x;

        BarVisibleTime = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {

        //scale이 바뀌어도 HP Bar 뒤집어 지지 않게
        hpBar.transform.localScale = transform.localScale;

        if (isHit)
        {
            SumVisibleTime += Time.deltaTime;
            if (hpBar.gameObject.activeSelf == false)
                hpBar.gameObject.SetActive(true);

            //!< hpBar.activeSelf == true ||
            else if (SumVisibleTime >= BarVisibleTime)
            {
                hpBar.gameObject.SetActive(isHit = false);
                SumVisibleTime = 0.0f;
            }
        }
    }

    public void GetDamage(float damage)
    {
        Current_Health -= damage;
        if (Current_Health <= 0.0f)
        {
            Current_Health = 0.0f;
            isDead = true;
            boxCollider.enabled = false;
        }

        //!< SpriteSizeWidth / currentHealth
        hpBarRenderer.size = new Vector2(Current_Health * (maxHPBarSize / Maximum_Health), hpBarRenderer.size.y);
    }
}
