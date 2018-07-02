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
    private Dictionary<Skill, string> resetNameDictionary = new Dictionary<Skill, string>(); // 처음 등록시 
    private Dictionary<Skill, int> skillIndexDictionary = new Dictionary<Skill, int>(); //스킬 슬롯 Change 때 skill index 값을 받아올때

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

        ResetRegistSkills();
        RegistAllSkill();
        ResetSlot();
        //EX) skillLIst[0] = 0번째 슬롯에 등록된 스킬 ...
    }

    #region 스킬 슬롯 검사 및 추가
    private void RegistAllSkill()
    {
        skillIndexDictionary.Add(LightningSphere, 0);
        skillIndexDictionary.Add(CrimsonStrike, 1);
        skillIndexDictionary.Add(BlackOut, 2);
        skillIndexDictionary.Add(FrozenContinuam, 3);
        skillIndexDictionary.Add(FireBall, 4);
        skillIndexDictionary.Add(MoonLightSlash, 5);
        skillIndexDictionary.Add(Null, 6);
        // .. 앞으로 더 늘어남
    }

    private void ResetRegistSkills()
    {
        skillsDictionary.Add("LightningSphere", LightningSphere);
        skillsDictionary.Add("CrimsonStrike", CrimsonStrike);
        skillsDictionary.Add("BlackOut", BlackOut);
        skillsDictionary.Add("FrozenContinuam", FrozenContinuam);
        skillsDictionary.Add("FireBall", FireBall);
        skillsDictionary.Add("MoonLightSlash", MoonLightSlash);
        skillsDictionary.Add("Null", Null);
        // .. 앞으로 더 늘어남
    }

    private void ResetSlot()
    {
        for (int i = 0; i < 6; i++)
        {
            skillSlotList[i] += skillsDictionary["Null"];
        }

        /*skillSlotList[4] += skillsDictionary["MoonLightSlash"];
        skillSlotList[5] += skillsDictionary["FireBall"];*/
    }

    public void RegistSkill(int i, int skillIndexs)
    {
        //스킬을 획득한 후 여기서 스킬 등록이 이루어진다(비어있는 슬롯에 등록됨)

        if (skillSlotList[i] == skillsDictionary["Null"])
        {
            skillSlotList[i] -= skillsDictionary["Null"];
        }

        if (skillIndexs == 0)
        {
            skillSlotList[i] += skillsDictionary["LightningSphere"];
        }
        else if (skillIndexs == 1)
        {
            skillSlotList[i] += skillsDictionary["CrimsonStrike"];
        }
        else if (skillIndexs == 2)
        {
            skillSlotList[i] += skillsDictionary["BlackOut"];
        }
        else if (skillIndexs == 3)
        {
            skillSlotList[i] += skillsDictionary["FrozenContinuam"];
        }
        else if (skillIndexs == 4)
        {
            skillSlotList[i] += skillsDictionary["FireBall"];
        }
        else if (skillIndexs == 5)
        {
            skillSlotList[i] += skillsDictionary["MoonLightSlash"];
        }

        skillsNameDictionary.Add(skillSlotList[i], skillSlotList[i].Method.Name);
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
        if (changeSkill == skillsDictionary["Null"])
        {
            skillSlotList[slotIndex] -= skillsDictionary["Null"];
        }
        else
        {
            if (skillSlotList[slotIndex] == skillsDictionary["Null"])
            {
                skillSlotList[slotIndex] -= skillsDictionary["Null"];
                skillSlotList[slotIndex] += skillsDictionary[changeSkill.Method.Name];
            }
            else
            {
                skillSlotList[slotIndex] -= skillsDictionary[skillsNameDictionary[skillSlotList[slotIndex]]];
                skillSlotList[slotIndex] += skillsDictionary[changeSkill.Method.Name];
            }
        }
    }

    public Skill CheckSlotSkill(int index)
    {
        return skillSlotList[index];
    }

    public int CheckSkillIndex(Skill skill)
    {
        if (skill == Null)
        {
            return 6;
        }
        else
        {
            return skillIndexDictionary[skill];
        }
    }

    //지울것
    public void ChangeSkillInPlayerInventory(Sprite skillSprite)
    {

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