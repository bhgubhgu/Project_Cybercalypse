using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAbility : ATalent
{
    /// <summary>
    /// 작성자 : 윤동준, 김현우
    /// 스크립트 : 어빌리티의 속성 및 슬롯 변경 과 등록을 할 수 있게하는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>
    /// 
    #region //!< override member
    public override string ItemName { get; set; }
    public override string ItemDesc { get; set; }
    public override SpriteRenderer ItemIcon { get; set; }
    public override SpriteRenderer ItemSubs { get; set; }
    public override EItemCategory ItemCategory { get; set; }
    public override ETalantCategory TalantCagegory { get; set; }
    #endregion
}