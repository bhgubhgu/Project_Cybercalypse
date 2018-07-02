using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagement : MonoBehaviour
{
    private CExecutor executor;

    #region //!< 체력 관련
    private List<SpriteRenderer> healthSprites;
    private List<SpriteRenderer> shieldSprites;

    private IEnumerator coroutine;
    //!< 현재 체력
    float curHealth;
    float curShield;

    //
    //!< 한 칸당 체력 = 최대 체력 * 0.1f
    //
    float healthPerCell;
    float shieldPerCell;

    float blinkInr;
    /// <summary> 
    /// Interval of Blinking Health Object
    /// </summary>
    float healthIntvl;
    /// <summary>
    /// Interval of Blinking Shield Object
    /// </summary>
    float shieldIntvl;
    float totalTime;
    
    bool isHCoroutine;
    bool isSCoroutine;
    #endregion

    #region //!< UI Pivot 관련
    private Transform trLeftTop;
    private Transform trLeftBottom;
    private Transform trMiddleBottom;
    private Transform trRightTop;
    private Transform trRightBottom;

    public Camera _camera;
    #endregion

    /// <summary>
    /// 1. 매 프레임마다 현재 체력을 받아와서 표시할 UI 요소들을 계산한다.
    /// 2. 초기 세팅을 모두 완료하고, 이전 프레임의 체력과 현재 프레임의 체력을 비교해서 
    /// 달라진만큼 체력칸을 추가로 생성 또는 제거한다. (이전 프레임의 체력을 가지고 있으니
    /// index는 있음)
    /// </summary>

    // Use this for initialization
    void Start()
    {
        trLeftBottom = transform.Find("1_LeftBottom");
        trMiddleBottom = transform.Find("2_MiddleBottom");
        trRightBottom = transform.Find("3_RightBottom");
        trLeftTop = transform.Find("7_LeftTop");
        trRightTop = transform.Find("9_RightTop");

        Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);

        executor = GameObject.Find("Player").GetComponent<CExecutor>();

        //!< 자식 안에서 찾기로 바꾸기
        GameObject healthOFF = GameObject.Find("UI_Health");
        GameObject shieldOFF = GameObject.Find("UI_Shield");

        healthSprites = new List<SpriteRenderer>();
        shieldSprites = new List<SpriteRenderer>();

        //!< 각각의 HealthFrame Object를 List에 저장
        for (int i = 0; i < 10; i++)
        {
            healthSprites.Add(healthOFF.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>());
            //healthSprites[i].color = Color.clear;

            shieldSprites.Add(shieldOFF.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>());
            //shieldSprites[i].color = Color.clear;
        }

        isHCoroutine = isSCoroutine = true;
    }

    // Update is called once per frame
    void Update()
    {
        curHealth = executor.CurrentHealth;
        curShield = executor.CurrentShield;

        healthPerCell = executor.MaximumHealth * 0.1f;
        shieldPerCell = executor.MaximumShield * 0.1f;

        //!< 나머지(=점멸빈도) 계산
        healthIntvl = curHealth % healthPerCell;
        shieldIntvl = curShield % shieldPerCell;

        //!< 300 / 200 = 1.5 -> 1
        int healthCellNum = System.Convert.ToInt32(Mathf.Ceil(curHealth / healthPerCell));
            //System.Convert.ToInt32(
            //System.Math.Truncate(
            //    System.Convert.ToDouble(curHealth / healthPerCell)));

        int shieldCellNum = System.Convert.ToInt32(Mathf.Ceil(curShield / shieldPerCell)); 
        //System.Convert.ToInt32(
        //System.Math.Truncate(
        //    System.Convert.ToDouble(curShield / shieldPerCell)));

        //!< 현재 체력 / 한 칸당 체력 == 표시해야 할 칸의 인덱스(=개수-1)
        //!< 현재 프레임에 표시할 체력, 실드를 계산함
        for (int i = 0; i < 10; i++)
        {
            if (i < healthCellNum)
                healthSprites[i].gameObject.SetActive(true);
            else
                healthSprites[i].gameObject.SetActive(false);

            if (i < shieldCellNum)
                shieldSprites[i].gameObject.SetActive(true);
            else
                shieldSprites[i].gameObject.SetActive(false);
        }

        if (healthIntvl > 0.0f && isHCoroutine)
            StartCoroutine(BlinkHealthCell(healthSprites[healthCellNum-1]));

        if (shieldIntvl > 0.0f && isSCoroutine)
            StartCoroutine(BlinkShieldCell(shieldSprites[shieldCellNum-1]));
    }

    /// <summary>
    /// !-- 현재 체력이 한 칸당 체력으로 나누어 떨어지지 않으면 깜빡이게 한다.
    /// !-- 나머지가 작으면 자주. 나머지가 크면 가끔 깜빡이도록 한다.
    /// </summary>
    /// <param name="renderer">체력칸 오브젝트</param>
    IEnumerator BlinkHealthCell(SpriteRenderer renderer)
    {
        bool isClear = true;
        isHCoroutine = false;
        for (; healthIntvl != 0.0f; isClear = !isClear) //!< 번갈아가며 깜빡임
        {
            if (isClear)
                renderer.color = Color.clear;

            else
                renderer.color = Color.white;

            yield return new WaitForSeconds(healthIntvl % 200 * 0.001f);
        }
        isHCoroutine = true;
    }

    IEnumerator BlinkShieldCell(SpriteRenderer renderer)
    {
        bool isClear = true;
        isSCoroutine = false;
        for (; shieldIntvl != 0.0f; isClear = !isClear) //!< 번갈아가며 깜빡임
        {
            if (isClear)
                renderer.color = Color.clear;

            else
                renderer.color = Color.white;

            yield return new WaitForSeconds(shieldIntvl % 200 * 0.001f);
        }
        isSCoroutine = true;
    }

    /// <summary>
    /// !-- UI 위치 수정하는 함수
    /// </summary>
    void ModifyPivot()
    {
        trLeftTop.position = _camera.ViewportToWorldPoint(Vector2.up);
        trLeftBottom.position = _camera.ViewportToWorldPoint(Vector2.zero);
        trMiddleBottom.position = _camera.ViewportToWorldPoint(new Vector2(0.5f, 0));
        trRightTop.position = _camera.ViewportToWorldPoint(Vector2.one);
        trRightBottom.position = _camera.ViewportToWorldPoint(Vector2.right);
    }
}