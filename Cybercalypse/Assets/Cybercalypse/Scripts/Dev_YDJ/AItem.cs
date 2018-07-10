using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AItem : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 윤동준, 김현우,  구용모
    /// 스크립트 : 아이템,스킬,어빌리티 들의 속성을 갖고 있는 최상위 클래스(추상클래스)
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.07.04
    /// </summary>

    /// <summary>
    /// !-- 아이템의 범주를 설명한다
    /// </summary>
    public enum EItemCategory { None, Consumable, Resource, Equipment, Talent };

    public abstract string ItemName { get; set; }
    public abstract string ItemDesc { get; set; }
    public abstract Sprite ItemIcon { get; set; }
    public abstract Sprite ItemSubs { get; set; }
    public abstract EItemCategory ItemCategory { get; set; }
}
