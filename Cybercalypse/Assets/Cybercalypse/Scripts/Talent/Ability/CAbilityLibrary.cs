using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAbilityLibrary : AAbility
{
    /// <summary>
    /// 작성자 : 윤동준, 김현우, 구용모
    /// 스크립트 : 어빌리티의 속성 및 슬롯 변경 과 등록을 할 수 있게하는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.07.04
    /// </summary>
    /// 
    #region //!< override member
    public override string ItemName { get; set; }
    public override string ItemDesc { get; set; }
    public override Sprite ItemIcon { get; set; }
    public override Sprite ItemSubs { get; set; }
    public override EItemCategory ItemCategory { get; set; }
    public override ETalentCategory TalentCategory { get; set; }
    #endregion

    private Ability[] abilitySlotList;
    public Ability lightningSphereDel;
    public Ability crimsonStrikeDel;
    public Ability fireBallDel;
    public Ability moonLightSlashDel;
    public Ability FrozenContinuamDel;
    public Ability BlackOutDel;

    private AbilityOffsetDel[] abilityOffsetArray;

    private Dictionary<string, Ability> abilitysDictionary = new Dictionary<string, Ability>(); //스킬 슬롯 Change를 위한 Dictionary
    private Dictionary<Ability, string> abilitysNameDictionary = new Dictionary<Ability, string>(); //스킬 슬롯 Change 때 Key 값이 Ability일때
    private Dictionary<Ability, int> abilityIndexDictionary = new Dictionary<Ability, int>(); //스킬 슬롯 Change 때 Ability index 값을 받아올때
    private Dictionary<int, Ability> abilityIndexReverseDictionary = new Dictionary<int, Ability>(); //스킬 슬롯 Change 때 Key 값을 index로 받아올때
    private Dictionary<Sprite, Ability> registAbilitySpriteDictionary = new Dictionary<Sprite, Ability>(); //Ability 아이콘을 키로 각 아이콘에 해당하는 실질적 스킬들을 저장

    private void Awake()
    {
        //Override = null;
        //이 스크립트는 Ability 들의 각각의 Offset만 처리
        ItemDesc = null;
        ItemIcon = null;
        ItemName = null;
        ItemSubs = null;

        abilityOffsetArray = new AbilityOffsetDel[3];
        abilitySlotList = new Ability[3];
        //EX) AbilityLIst[0] = 0번째 슬롯에 등록된 어빌리티 ...
    }

    private void Start()
    {
        ResetRegistAbilitys();
        RegistAllAbility();
        RegistAbilityIconToAbility();
        ResetSlot();
    }

    #region 스킬 슬롯 검사 및 추가
    private void RegistAllAbility()
    {
        abilityIndexDictionary.Add(Null, 0);
        abilityIndexDictionary.Add(LightningSphere, 1);
        abilityIndexDictionary.Add(CrimsonStrike, 2);
        abilityIndexDictionary.Add(BlackOut, 3);
        // .. 앞으로 더 늘어남

        abilityIndexReverseDictionary.Add(0, Null);
        abilityIndexReverseDictionary.Add(1, LightningSphere);
        abilityIndexReverseDictionary.Add(2, CrimsonStrike);
        abilityIndexReverseDictionary.Add(3, BlackOut);
    }

    private void ResetRegistAbilitys()
    {
        abilitysDictionary.Add("Null", Null);
        abilitysDictionary.Add("LightningSphere", LightningSphere);
        abilitysDictionary.Add("CrimsonStrike", CrimsonStrike);
        abilitysDictionary.Add("BlackOut", BlackOut);

        abilitysNameDictionary.Add(Null, "Null");
        abilitysNameDictionary.Add(LightningSphere, "LightningSphere");
        abilitysNameDictionary.Add(CrimsonStrike, "CrimsonStrike");
        abilitysNameDictionary.Add(BlackOut, "BlackOut");
        // .. 앞으로 더 늘어남
    }

    private void RegistAbilityIconToAbility()
    {
        //Test 실질적인 아이콘을 가진 스킬 오브젝트
        registAbilitySpriteDictionary.Add(CGameManager.instance.testAbilityList[0], Null);
        registAbilitySpriteDictionary.Add(CGameManager.instance.testAbilityList[1], LightningSphere);
        registAbilitySpriteDictionary.Add(CGameManager.instance.testAbilityList[2], CrimsonStrike);
        registAbilitySpriteDictionary.Add(CGameManager.instance.testAbilityList[3], BlackOut);
    }

    private void ResetSlot() //스킬 셋팅 초기화
    {
        for (int i = 0; i < 3; i++)
        {
            abilitySlotList[i] += abilitysDictionary["Null"];
        }
    }
    #endregion

    //public void FindAbilityOffset(AbilityOffsetDel offsetDel) // 어빌리티 적용 용도
    //{
    //    EAbilityOffset abilityOffsetKind = AbilityOffsetKind(offsetDel);
    //    switch (abilityOffsetKind)
    //    {
    //        case EAbilityOffset.Slot1:
    //            abilitySlotList[0]();
    //            break;
    //        case EAbilityOffset.Slot2:
    //            abilitySlotList[1]();
    //            break;
    //        case EAbilityOffset.Slot3:
    //            abilitySlotList[2]();
    //            break;
    //        case EAbilityOffset.Error:
    //            break;
    //    }
    //}

    //private EAbilityOffset AbilityOffsetKind(AbilityOffsetDel offsetDel) //어빌리티 체크 용
    //{
    //    if (Equals(offsetDel, abilityOffsetArray[0]))
    //    {
    //        return EAbilityOffset.Slot1;
    //    }
    //    else if (Equals(offsetDel, abilityOffsetArray[1]))
    //    {
    //        return EAbilityOffset.Slot2;
    //    }
    //    else if (Equals(offsetDel, abilityOffsetArray[2]))
    //    {
    //        return EAbilityOffset.Slot3;
    //    }
    //    else
    //    {
    //        return EAbilityOffset.Error;
    //    }
    //}

    #region Abilitys
    private void LightningSphere()
    {
        Debug.Log("ability1");
    }

    private void CrimsonStrike()
    {
        Debug.Log("ability2");
    }

    private void BlackOut()
    {
        Debug.Log("ability3");
    }

    private void Null()
    {
        Debug.Log("abilit Null!!");
    }

    #endregion

    #region 어빌리티의 슬롯 변경
    public void ChangeSlot(Ability changeAbility, int slotIndex)
    {
        abilitySlotList[slotIndex] -= abilitysDictionary[abilitysNameDictionary[abilitySlotList[slotIndex]]];
        abilitySlotList[slotIndex] += abilitysDictionary[changeAbility.Method.Name];

        abilitySlotList[slotIndex]();
    }

    public Ability CheckSlotAbility(int index)
    {
        return abilityIndexReverseDictionary[index];
    }

    public Ability CheckAbilityIcon(Sprite ability)
    {
        return registAbilitySpriteDictionary[ability];
    }

    public int CheckAbilityIndex(Ability ability)
    {
        return abilityIndexDictionary[ability];
    }

    //지울것
    public Ability FindAbilityToAbilityIcon(Sprite abilitySprite)
    {
        return CheckSlotAbility(CheckAbilityIndex(CheckAbilityIcon(abilitySprite)));
    }
    #endregion
}

public enum EAbilityOffset
{
    Slot1,
    Slot2,
    Slot3,
    Error
}
