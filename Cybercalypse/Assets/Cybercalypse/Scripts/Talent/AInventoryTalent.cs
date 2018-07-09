using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInventoryTalent : AInventoryItem, ITalent {

    public ATalent.ETalentCategory TalentCategory { get; set; }
    public string Tooltip { get; set; }
}