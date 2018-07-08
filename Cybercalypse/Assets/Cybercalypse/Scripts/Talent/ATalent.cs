using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATalent : AItem
{
    /// <summary>
    /// 작성자 : 김현우, 윤동준
    /// 스크립트 : 스킬, 어빌리티의 부모
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    public enum ETalentCategory { Skill, Ability }

<<<<<<< HEAD:Cybercalypse/Assets/Cybercalypse/Scripts/Skill/ATalent.cs
    public abstract ETalentCategory TalentCategory { get; set; }
=======
    public abstract ETalantCategory TalentCategory { get; set; }
>>>>>>> dev:Cybercalypse/Assets/Cybercalypse/Scripts/Talent/ATalent.cs
}