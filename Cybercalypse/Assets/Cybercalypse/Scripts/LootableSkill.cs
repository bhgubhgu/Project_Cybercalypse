using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootableSkill : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 윤동준
    /// 스크립트 : 스킬을 획득 하게 할 수 있는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    public CSkillLibrary skillOffset;

    //
    //!< 스킬이 주워지면 스킬 슬롯에 자동 세팅하기 위한 것들
    private Transform skillGroup;
    private List<Image> skills;
    private bool isUp;
    //
    //!< 0.0003f 정도가 둥둥 떠다니는데에 적합함
    private float velocity;
    private string triggerTag;
    private Sprite sprite;
    private int emptySkillSlotIndex;

    Coroutine coroutine;

	// Use this for initialization
	void Start ()
    {
        skillOffset = GameObject.Find("New Skill Offset").gameObject.GetComponent<CSkillLibrary>();
        //skillGroup = CGameManager.instance.SkillGroup;
        skills = new List<Image>();
        triggerTag = CGameManager.instance.playerObject.tag;
        velocity = 5.0f;
        isUp = true;
        coroutine = StartCoroutine(HoverItem(velocity));

        sprite = transform.GetComponent<SpriteRenderer>().sprite;

        for (int index = 0; index < skillGroup.childCount; index++)
            skills.Add(skillGroup.GetChild(index).GetChild(0).GetComponent<Image>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals(triggerTag))
        {
            StopCoroutine(coroutine);
            ObtainedByPlayer();
        }
    }

    private void ObtainedByPlayer()
    {
        int i = FindEmptySlotIndex();
        if (i != -1)
        {
            /*CGameManager.skillSprites[i] = skills[i].sprite = sprite;*/
            int skillIndex = FindSkillIndex(i);
            skillOffset.RegistSkill(i,skillIndex);
            skillGroup.transform.GetChild(i).transform.GetChild(0).transform.gameObject.GetComponent<TestChangeSkillSlot>().currentSlotSkill = skillOffset.CheckSlotSkill(i);
            skillGroup.transform.GetChild(i).transform.GetChild(0).transform.gameObject.GetComponent<TestChangeSkillSlot>().skillIndex = skillIndex;
        }
        gameObject.SetActive(false);
    }

    private int FindEmptySlotIndex()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].sprite.name.Equals("black"))
                return i;
        }
            
        return -1;
    }

    private int FindSkillIndex(int i)
    {
        if(skills[i].sprite.name.Equals("Icon_Skill_LightningSphere"))
        {
            return 0;
        }
        else if (skills[i].sprite.name.Equals("Icon_Skill_CrimsonStrike"))
        {
            return 1;
        }
        else if (skills[i].sprite.name.Equals("Icon_Skill_BlackOut"))
        {
            return 2;
        }
        else if (skills[i].sprite.name.Equals("Icon_Skill_FrozenContinuum"))
        {
            return 3;
        }
        else if (skills[i].sprite.name.Equals("Icon_Skill_FireBall"))
        {
            return 4;
        }
        else if (skills[i].sprite.name.Equals("Icon_Skill_MoonlightSlash"))
        {
            return 5;
        }
        else
        {
            return -1;
        }
    }

    /// <summary>
    /// 둥둥 떠다니는듯한 느낌으로 애니메이션 해야 함
    /// </summary>
    /// <returns></returns>
    IEnumerator HoverItem(float velocity)
    {
        float total = 0.0f;
        float hovertime = 2.0f;
        while (true)
        {
            if (total >= hovertime)
            {
                isUp = !isUp;
                total = 0.0f;
            }

            if (isUp)
                transform.Translate(Vector3.up * 0.01f * velocity * Time.deltaTime);
            else
                transform.Translate(Vector3.down * 0.01f * velocity * Time.deltaTime);

            total += Time.deltaTime;
            yield return null;
        }
    }
}
