using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInventoryTalent : AInventoryItem {

    public abstract ATalent.ETalentCategory TalentCategory { get; set; }
}