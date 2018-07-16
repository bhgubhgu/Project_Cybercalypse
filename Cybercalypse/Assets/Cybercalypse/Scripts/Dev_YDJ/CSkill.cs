using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkill : ASkill {
    [SerializeField]
    public override string ItemName { get; set; }
    public override string ItemDesc { get; set; }
    public override Sprite ItemIcon { get; set; }
    public override Sprite ItemSubs { get; set; }
    public override EItemCategory ItemCategory { get; set; }
    public override ETalentCategory TalentCategory { get; set; }
    public override float SkillCastingTime { get; set; }
    public override float SkillCoolDown { get; set; }
}
