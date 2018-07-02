using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManagement : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 윤동준
    /// 스크립트 : CExecutor로부터 받은 Player의 HP, Shelid 등의 정보를 받아 UI로 표시하기 위한 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    public CExecutor executor;

    #region //!< 체력 관련
    private List<Image> healthSprites;
    private List<Image> shieldSprites;

    private List<Image> fillingImageList;

    private IEnumerator coroutine;
    
    //!< 표시 칸의 총 갯수
    int totalCellNumber;

    //!< 현재 체력
    float currentHealth;
    float currentShield;

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

    /// <summary>
    /// 1. 매 프레임마다 현재 체력을 받아와서 표시할 UI 요소들을 계산한다.
    /// 2. 초기 세팅을 모두 완료하고, 이전 프레임의 체력과 현재 프레임의 체력을 비교해서 
    /// 달라진만큼 체력칸을 추가로 생성 또는 제거한다. (이전 프레임의 체력을 가지고 있으니
    /// index는 있음)
    /// </summary>

    // Use this for initialization
    void Start()
    {
        if (executor.Equals(null))
            executor = GameObject.Find("Player").GetComponent<CExecutor>();

        totalCellNumber = transform.GetChild(0).childCount;

        healthSprites = new List<Image>();
        shieldSprites = new List<Image>();

        fillingImageList = new List<Image>();

        //!< 각각의 HealthFrame Object를 List에 저장
        for (int i = 0; i < totalCellNumber; i++)
        {
            healthSprites.Add(transform.Find("HP").GetChild(i).GetChild(0).GetComponent<Image>());
            shieldSprites.Add(transform.Find("SP").GetChild(i).GetChild(0).GetComponent<Image>());

            //fillingImageList.Add(transform.GetChild(i).GetChild(0).GetComponent<Image>());
        }

        isHCoroutine = isSCoroutine = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = executor.CurrentHealth;
        currentShield = executor.CurrentShield;

        healthPerCell = executor.MaximumHealth / totalCellNumber;
        shieldPerCell = executor.MaximumShield / totalCellNumber;

        //!< 나머지(=점멸빈도) 계산
        healthIntvl = currentHealth % healthPerCell;
        shieldIntvl = currentShield % shieldPerCell;

        //!< 300 / 200 = 1.5 -> 1
        int healthCellNum = System.Convert.ToInt32(Mathf.Ceil(currentHealth / healthPerCell));
        //System.Convert.ToInt32(
        //System.Math.Truncate(
        //    System.Convert.ToDouble(curHealth / healthPerCell)));

        int shieldCellNum = System.Convert.ToInt32(Mathf.Ceil(currentShield / shieldPerCell));
        //System.Convert.ToInt32(
        //System.Math.Truncate(
        //    System.Convert.ToDouble(curShield / shieldPerCell)));

        //!< 현재 체력 / 한 칸당 체력 == 표시해야 할 칸의 인덱스(=개수-1)
        //!< 현재 프레임에 표시할 체력, 실드를 계산함
        for (int i = 0; i < totalCellNumber; i++)
        {
            if (i < healthCellNum)
            {
                healthSprites[i].color = Color.white;
            }

            else
            {
                healthSprites[i].color = Color.clear;
            }
            

            if (i < shieldCellNum)
                shieldSprites[i].color = Color.white;
            else
                shieldSprites[i].color = Color.clear;
        }

        if (healthIntvl > 0.0f && isHCoroutine)
            StartCoroutine(BlinkHealthCell(healthSprites[healthCellNum - 1]));

        if (shieldIntvl > 0.0f && isSCoroutine)
            StartCoroutine(BlinkShieldCell(shieldSprites[shieldCellNum - 1]));
    }

    /// <summary>
    /// !-- 현재 체력이 한 칸당 체력으로 나누어 떨어지지 않으면 깜빡이게 한다.
    /// !-- 나머지가 작으면 자주. 나머지가 크면 가끔 깜빡이도록 한다.
    /// </summary>
    /// <param name="renderer">체력칸 오브젝트</param>
    IEnumerator BlinkHealthCell(Image image)
    {
        bool isClear = true;
        isHCoroutine = false;
        for (; healthIntvl != 0.0f; isClear = !isClear) //!< 번갈아가며 깜빡임
        {
            if (isClear)
                image.color = Color.clear;

            else
                image.color = Color.white;

            yield return new WaitForSeconds(healthIntvl % healthPerCell * 0.005f);
        }
        isHCoroutine = true;
    }

    IEnumerator BlinkShieldCell(Image image)
    {
        bool isClear = true;
        isSCoroutine = false;
        for (; shieldIntvl != 0.0f; isClear = !isClear) //!< 번갈아가며 깜빡임
        {
            if (isClear)
                image.color = Color.clear;

            else
                image.color = Color.white;

            yield return new WaitForSeconds(shieldIntvl % shieldPerCell * 0.005f);
        }
        isSCoroutine = true;
    }
}
