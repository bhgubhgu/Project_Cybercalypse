using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ASkill : ATalent
{
    /// <summary>
    /// 작성자 : 구용모, 윤동준, 김현우
    /// 스크립트 : 스킬들의 속성을 갖는 추상클래스
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    public abstract float SkillCastingTime
    {
        get;
        set;
    }
    public abstract float SkillCoolDown
    {
        get;
        set;
    }

    public TestChangeSkillSlot[] slotArray = new TestChangeSkillSlot[6];

    public delegate void Skill();
    public delegate void SkillOffsetDel(bool isDownKey);

    public virtual void Start()
    {
       /* slotArray[0] = GameObject.Find("Talent_Image 1").GetComponent<TestChangeSkillSlot>();
        slotArray[1] = GameObject.Find("Talent_Image 2").GetComponent<TestChangeSkillSlot>();
        slotArray[2] = GameObject.Find("Talent_Image 3").GetComponent<TestChangeSkillSlot>();
        slotArray[3] = GameObject.Find("Talent_Image 4").GetComponent<TestChangeSkillSlot>();
        slotArray[4] = GameObject.Find("Talent_Image 5").GetComponent<TestChangeSkillSlot>();
        slotArray[5] = GameObject.Find("Talent_Image 6").GetComponent<TestChangeSkillSlot>();*/
    }

    public IEnumerator SqrClockwiseAnim(float runningTime, GameObject Slot)
    {
        Image alpha = Slot.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        for (float f = 1.0f, interval = 0.01f; f > 0.0f; f -= interval)
        {
            alpha.fillAmount = f;
            yield return new WaitForSeconds(runningTime * interval);
        }
        alpha.fillAmount = 0.0f;
    }
}
