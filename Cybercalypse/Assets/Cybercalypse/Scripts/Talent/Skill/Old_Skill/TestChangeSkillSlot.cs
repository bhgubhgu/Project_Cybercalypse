using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestChangeSkillSlot : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : 스킬슬롯에 등록된 스킬을 바꿀수 있게 하는 스크립트 -> 인터페이스 또는 추상클래스로 만들어서 변수들과 함수들을 오버라이딩 해서 사용할 예정
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    public CSkillLibrary skillOffset;
    public int slotIndex;
    public int skillIndex;

    public ASkill.Skill skill;
    public ASkill.Skill currentSlotSkill;
    private Vector3 position;
    private GameObject setSkillSlotObject;
    private SpriteRenderer sprite;
    private Sprite skillSprite;
    private bool isGrabItem;
    private bool isGetItem;
    private Sprite otherSprite;

    private void Start()
    {
        //sprite = GetComponent<SpriteRenderer>();
        //skillSprite = sprite.sprite; //현재 각 슬롯에 등록된 스프라이트의 정보를 저장 --> 나중에 모든 스킬들이 각각 등록될것이다.
        /*position = this.transform.localPosition;
        currentSlotSkill = skillOffset.CheckSlotSkill(slotIndex);
        skillIndex = skillOffset.CheckSkillIndex(currentSlotSkill);*/
        //currentSlotSkill = skillOffset.CheckSlotSkill(slotIndex);
        //skillIndex = skillOffset.CheckSkillIndex(currentSlotSkill);
    }

    //스킬을 먹고 난 후의 이벤트 처리함
    //스킬 획득 이벤트 메소드

}
