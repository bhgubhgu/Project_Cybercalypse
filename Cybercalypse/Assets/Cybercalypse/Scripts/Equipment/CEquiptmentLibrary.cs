using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEquiptmentLibrary : AEquipment
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : 장비들을 등록, 해제 및 발사 객체를 등록시켜주는 일종의 장비 도서관 같은 기능을 하는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.07.04
    /// </summary>

    #region Override
    public override EItemCategory ItemCategory
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }
    public override string ItemDesc
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }
    public override Sprite ItemIcon
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }
    public override string ItemName
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }
    public override Sprite ItemSubs
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }
    #endregion

    private Equipment[] equipmentSlotList;
    public Equipment lightningSphereDel;
    public Equipment crimsonStrikeDel;
    public Equipment fireBallDel;
    public Equipment moonLightSlashDel;
    public Equipment FrozenContinuamDel;
    public Equipment BlackOutDel;

    private EquipmentDel[] equipmentOffsetArray;

    private Dictionary<string, Equipment> equipmentsDictionary = new Dictionary<string, Equipment>(); //장비 슬롯 Change를 위한 Dictionary
    private Dictionary<Equipment, string> equipmentsNameDictionary = new Dictionary<Equipment, string>(); //장비 슬롯 Change 때 Key 값이 Equipment일때
    private Dictionary<Equipment, int> equipmentIndexDictionary = new Dictionary<Equipment, int>(); //장비 슬롯 Change 때 Equipment index 값을 받아올때
    private Dictionary<int, Equipment> equipmentIndexReverseDictionary = new Dictionary<int, Equipment>(); //장비 슬롯 Change 때 Key 값을 index로 받아올때
    private Dictionary<Sprite, Equipment> registEquipmentSpriteDictionary = new Dictionary<Sprite, Equipment>(); //Equipment 아이콘을 키로 각 아이콘에 해당하는 실질적 스킬들을 저장

    private void Awake()
    {
        //이 스크립트는 Equipment 들의 각각의 Offset만 처리

        equipmentOffsetArray = new EquipmentDel[3];
        equipmentSlotList = new Equipment[3];
        //EX) EquipmentLIst[0] = 0번째 슬롯에 등록된 어빌리티 ...
    }

    private void Start()
    {
        ResetRegistEquipments();
        RegistAllEquipment();
        RegistEquipmentIconToEquipment();
        ResetSlot();
    }

    #region 스킬 슬롯 검사 및 추가
    private void RegistAllEquipment()
    {
        equipmentIndexDictionary.Add(Null, 0);
        equipmentIndexDictionary.Add(LightningSphere, 1); //weapon은 1
        equipmentIndexDictionary.Add(CrimsonStrike, 2); //mask는 2
        equipmentIndexDictionary.Add(BlackOut, 3); //suit는 3
        // .. 앞으로 더 늘어남

        equipmentIndexReverseDictionary.Add(0, Null);
        equipmentIndexReverseDictionary.Add(1, LightningSphere); //weapon은 1
        equipmentIndexReverseDictionary.Add(2, CrimsonStrike); //mask는 2
        equipmentIndexReverseDictionary.Add(3, BlackOut); //suit는 3
    }

    private void ResetRegistEquipments()
    {
        equipmentsDictionary.Add("Null", Null);
        equipmentsDictionary.Add("LightningSphere", LightningSphere);
        equipmentsDictionary.Add("CrimsonStrike", CrimsonStrike);
        equipmentsDictionary.Add("BlackOut", BlackOut);

        equipmentsNameDictionary.Add(Null, "Null");
        equipmentsNameDictionary.Add(LightningSphere, "LightningSphere");
        equipmentsNameDictionary.Add(CrimsonStrike, "CrimsonStrike");
        equipmentsNameDictionary.Add(BlackOut, "BlackOut");
        // .. 앞으로 더 늘어남
    }

    private void RegistEquipmentIconToEquipment()
    {
        //Test 실질적인 아이콘을 가진 스킬 오브젝트
        registEquipmentSpriteDictionary.Add(CGameManager.instance.testWeaponList[0], Null);
        registEquipmentSpriteDictionary.Add(CGameManager.instance.testMaskList[0], Null);
        registEquipmentSpriteDictionary.Add(CGameManager.instance.testSuitList[0], Null);
        registEquipmentSpriteDictionary.Add(CGameManager.instance.testWeaponList[1], LightningSphere); //weapon은 0
        registEquipmentSpriteDictionary.Add(CGameManager.instance.testMaskList[1], CrimsonStrike); //weapon은 1
        registEquipmentSpriteDictionary.Add(CGameManager.instance.testSuitList[1], BlackOut); //weapon은 2
    }

    private void ResetSlot() //스킬 셋팅 초기화
    {
        for (int i = 0; i < 3; i++)
        {
            equipmentSlotList[i] += equipmentsDictionary["Null"];
        }
    }
    #endregion

    #region Equipments
    private void LightningSphere()
    {
        Debug.Log("Weapon1");
    }

    private void CrimsonStrike()
    {
        Debug.Log("Mask");
    }

    private void BlackOut()
    {
        Debug.Log("Suit");
    }

    private void Null()
    {
        Debug.Log("equipment Null!!");
    }

    #endregion

    #region 장비 변경
    public void ChangeSlot(Equipment changeEquipment, int slotIndex)
    {
        equipmentSlotList[slotIndex] -= equipmentsDictionary[equipmentsNameDictionary[equipmentSlotList[slotIndex]]];
        equipmentSlotList[slotIndex] += equipmentsDictionary[changeEquipment.Method.Name];

        equipmentSlotList[slotIndex]();
    }

    public Equipment CheckSlotEquipment(int index)
    {
        return equipmentIndexReverseDictionary[index];
    }

    public Equipment CheckEquipmentIcon(Sprite equipment)
    {
        return registEquipmentSpriteDictionary[equipment];
    }

    public int CheckEquipmentIndex(Equipment equipment)
    {
        return equipmentIndexDictionary[equipment];
    }

    //지울것
    public Equipment FindEquipmentToEquipmentIcon(Sprite equipmentSprite)
    {
        return CheckSlotEquipment(CheckEquipmentIndex(CheckEquipmentIcon(equipmentSprite)));
    }
    #endregion

    public enum EEquipmentOffset
    {
        Weapon,
        Mask,
        Suit,
        Error
    }
}

