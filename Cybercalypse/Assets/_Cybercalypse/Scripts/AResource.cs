using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AResource : AItem {
    
    public enum EResourceCategory { Bit, StarSeed, EverCoin };

    public abstract EResourceCategory ResourceCategory { get; set; }
}
