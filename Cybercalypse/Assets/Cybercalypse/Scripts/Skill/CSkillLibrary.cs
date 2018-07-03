using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkillLibrary : ASkill
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : 스킬들을 등록, 해제 및 발사 객체를 등록시켜주는 일종의 스킬 도서관 같은 기능을 하는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.26
    /// </summary>

    #region Override

    public override EItemCategory ItemCategory
    {
        get;
        set;
    }

    public override string ItemDesc
    {
        get;
        set;
    }

    public override SpriteRenderer ItemIcon
    {
        get;
        set;
    }

    public override string ItemName
    {
        get;
        set;
    }

    public override SpriteRenderer ItemSubs
    {
        get;
        set;
    }

    public override float SkillCastingTime
    {
        get;
        set;
    }

    public override float SkillCoolDown
    {
        get;
        set;
    }

    public override ETalantCategory TalantCagegory
    {
        get;
        set;
    }
    #endregion

    private int delegateIndex = 0;

    private Skill[] skillSlotList;
    public Skill lightningSphereDel;
    public Skill crimsonStrikeDel;
    public Skill fireBallDel;
    public Skill moonLightSlashDel;
    public Skill FrozenContinuamDel;
    public Skill BlackOutDel;

    private SkillOffsetDel[] skillOffsetArray;

    private Dictionary<string, Skill> skillsDictionary = new Dictionary<string, Skill>(); //스킬 슬롯 Change를 위한 Dictionary
    private Dictionary<Skill, string> skillsNameDictionary = new Dictionary<Skill, string>(); //스킬 슬롯 Change 때 Key 값이 SKill일때
    private Dictionary<Skill, int> skillIndexDictionary = new Dictionary<Skill, int>(); //스킬 슬롯 Change 때 skill index 값을 받아올때
    private Dictionary<int, Skill> skillIndexReverseDictionary = new Dictionary<int, Skill>(); //스킬 슬롯 Change 때 Key 값을 index로 받아올때
    private Dictionary<Sprite, Skill> registSkillSpriteDictionary = new Dictionary<Sprite, Skill>(); //Skill 아이콘을 키로 각 아이콘에 해당하는 실질적 스킬들을 저장

    private void Awake()
    {
        //Override = null;
        //이 스크립트는 Skill 들의 각각의 Offset만 처리
        ItemDesc = null;
        ItemIcon = null;
        ItemName = null;
        ItemSubs = null;
        SkillCastingTime = 0.0f;
        SkillCoolDown = 0.0f;

        skillOffsetArray = new SkillOffsetDel[6];
        skillSlotList = new Skill[6];
        //EX) skillLIst[0] = 0번째 슬롯에 등록된 스킬 ...
    }

    private new void Start()
    {
        ResetRegistSkills();
        RegistAllSkill();
        RegistSkillIconToSkill();
        ResetSlot();
    }

    #region 스킬 슬롯 검사 및 추가
    private void RegistAllSkill()
    {
        skillIndexDictionary.Add(Null, 0);
        skillIndexDictionary.Add(LightningSphere, 1);
        skillIndexDictionary.Add(CrimsonStrike, 2);
        skillIndexDictionary.Add(BlackOut, 3);
        skillIndexDictionary.Add(FrozenContinuam, 4);
        skillIndexDictionary.Add(FireBall, 5);
        skillIndexDictionary.Add(MoonLightSlash, 6);
        // .. 앞으로 더 늘어남

        skillIndexReverseDictionary.Add(0, Null);
        skillIndexReverseDictionary.Add(1, LightningSphere);
        skillIndexReverseDictionary.Add(2, CrimsonStrike);
        skillIndexReverseDictionary.Add(3, BlackOut);
        skillIndexReverseDictionary.Add(4, FrozenContinuam);
        skillIndexReverseDictionary.Add(5, FireBall);
        skillIndexReverseDictionary.Add(6, MoonLightSlash);
    }

    private void ResetRegistSkills()
    {
        skillsDictionary.Add("Null", Null);
        skillsDictionary.Add("LightningSphere", LightningSphere);
        skillsDictionary.Add("CrimsonStrike", CrimsonStrike);
        skillsDictionary.Add("BlackOut", BlackOut);
        skillsDictionary.Add("FrozenContinuam", FrozenContinuam);
        skillsDictionary.Add("FireBall", FireBall);
        skillsDictionary.Add("MoonLightSlash", MoonLightSlash);

        skillsNameDictionary.Add(Null, "Null");
        skillsNameDictionary.Add(LightningSphere, "LightningSphere");
        skillsNameDictionary.Add(CrimsonStrike, "CrimsonStrike");
        skillsNameDictionary.Add(BlackOut, "BlackOut");
        skillsNameDictionary.Add(FrozenContinuam, "FrozenContinuam");
        skillsNameDictionary.Add(FireBall, "FireBall");
        skillsNameDictionary.Add(MoonLightSlash, "MoonLightSlash");
        // .. 앞으로 더 늘어남
    }

    private void RegistSkillIconToSkill()
    {
        //Test 실질적인 아이콘을 가진 스킬 오브젝트
        registSkillSpriteDictionary.Add(CGameManager.instance.testSkillList[0].GetComponent<SpriteRenderer>().sprite, Null);
        registSkillSpriteDictionary.Add(CGameManager.instance.testSkillList[1].GetComponent<SpriteRenderer>().sprite, LightningSphere);
        registSkillSpriteDictionary.Add(CGameManager.instance.testSkillList[2].GetComponent<SpriteRenderer>().sprite, CrimsonStrike);
        registSkillSpriteDictionary.Add(CGameManager.instance.testSkillList[3].GetComponent<SpriteRenderer>().sprite, BlackOut);
        registSkillSpriteDictionary.Add(CGameManager.instance.testSkillList[4].GetComponent<SpriteRenderer>().sprite, FrozenContinuam);
        registSkillSpriteDictionary.Add(CGameManager.instance.testSkillList[5].GetComponent<SpriteRenderer>().sprite, FireBall);
        registSkillSpriteDictionary.Add(CGameManager.instance.testSkillList[6].GetComponent<SpriteRenderer>().sprite, MoonLightSlash);
    }

    private void ResetSlot() //스킬 셋팅 초기화
    {
        for (int i = 0; i < 6; i++)
        {
            skillSlotList[i] += skillsDictionary["Null"];
        }
    }

    public void SetSkillSlot(SkillOffsetDel offsetDel)
    {
        skillOffsetArray[delegateIndex++] += offsetDel;
    }
    #endregion

    public Skill FindSKillSlot(SkillOffsetDel offsetDel) //애니메이션용
    {
        ESkillOffset skillOffsetKind = SkillOffsetKind(offsetDel);
        switch (skillOffsetKind)
        {
            case ESkillOffset.Slot1:
                return skillSlotList[0];
            case ESkillOffset.Slot2:
                return skillSlotList[1];
            case ESkillOffset.Slot3:
                return skillSlotList[2];
            case ESkillOffset.Slot4:
                return skillSlotList[3];
            case ESkillOffset.SlotMouseLeft:
                return skillSlotList[4];
            case ESkillOffset.SlotMouseRight:
                return skillSlotList[5];
            case ESkillOffset.Error:
                break;
        }
        return null;
    }

    public void FindSKillOffset(SkillOffsetDel offsetDel) // 스킬발사 용
    {
        ESkillOffset skillOffsetKind = SkillOffsetKind(offsetDel);
        switch (skillOffsetKind)
        {
            case ESkillOffset.Slot1:
                skillSlotList[0]();
                CAudioManager.instance.SelectSkillSound(skillSlotList[0]);
                break;
            case ESkillOffset.Slot2:
                skillSlotList[1]();
                CAudioManager.instance.SelectSkillSound(skillSlotList[1]);
                break;
            case ESkillOffset.Slot3:
                skillSlotList[2]();
                CAudioManager.instance.SelectSkillSound(skillSlotList[2]);
                break;
            case ESkillOffset.Slot4:
                skillSlotList[3]();
                CAudioManager.instance.SelectSkillSound(skillSlotList[3]);
                break;
            case ESkillOffset.SlotMouseLeft:
                skillSlotList[4]();
                CAudioManager.instance.SelectSkillSound(skillSlotList[4]);
                break;
            case ESkillOffset.SlotMouseRight:
                skillSlotList[5]();
                CAudioManager.instance.SelectSkillSound(skillSlotList[5]);
                break;
            case ESkillOffset.Error:
                break;
        }
    }

    private ESkillOffset SkillOffsetKind(SkillOffsetDel offsetDel) //스킬 체크 용
    {
        if (Equals(offsetDel, skillOffsetArray[0]))
        {
            return ESkillOffset.Slot1;
        }
        else if (Equals(offsetDel, skillOffsetArray[1]))
        {
            return ESkillOffset.Slot2;
        }
        else if (Equals(offsetDel, skillOffsetArray[2]))
        {
            return ESkillOffset.Slot3;
        }
        else if (Equals(offsetDel, skillOffsetArray[3]))
        {
            return ESkillOffset.Slot4;
        }
        else if (Equals(offsetDel, skillOffsetArray[4]))
        {
            return ESkillOffset.SlotMouseLeft;
        }
        else if (Equals(offsetDel, skillOffsetArray[5]))
        {
            return ESkillOffset.SlotMouseRight;
        }
        else
        {
            return ESkillOffset.Error;
        }
    }

    #region Skills
    private void LightningSphere()
    {
        //lightningSphereDel();
        Debug.Log("LightningSphere");
    }

    private void CrimsonStrike()
    {
        //crimsonStrikeDel();
        Debug.Log("CrimsonStrike");
    }

    private void FireBall()
    {
        //fireBallDel();
        Debug.Log("FireBall");
    }

    private void MoonLightSlash()
    {
        //moonLightSlashDel();
        Debug.Log("MoonLightSlash");
    }

    private void BlackOut()
    {
        //BlackOutDel();
        Debug.Log("BlackOut");
    }

    private void FrozenContinuam()
    {
        //FrozenContinuamDel();
        Debug.Log("FrozenContinuam");
    }

    private void Null()
    {
        Debug.Log("Nothing!!");
    }

    #endregion

    #region 스킬의 슬롯 변경
    public void ChangeSlot(Skill changeSkill, int slotIndex)
    {
        skillSlotList[slotIndex] -= skillsDictionary[skillsNameDictionary[skillSlotList[slotIndex]]];
        skillSlotList[slotIndex] += skillsDictionary[changeSkill.Method.Name];
    }

    public Skill CheckSlotSkill(int index)
    {
        return skillIndexReverseDictionary[index];
    }

    public Skill CheckSkillIcon(Sprite skill)
    {
        return registSkillSpriteDictionary[skill];
    }

    public int CheckSkillIndex(Skill skill)
    {
         return skillIndexDictionary[skill];
    }

    //지울것
    public Skill FindSkillToSkillIcon(Sprite skillSprite)
    {
        return CheckSlotSkill(CheckSkillIndex(CheckSkillIcon(skillSprite)));
    }
    #endregion
}


public enum ESkillOffset
{
    Slot1,
    Slot2,
    Slot3,
    Slot4,
    SlotMouseLeft,
    SlotMouseRight,
    Error
}