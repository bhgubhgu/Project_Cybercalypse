using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingleTonManager<LevelManager>
{
    public CGridDrivenContentsGenerator GridGenerator { get; private set; }

    new void Awake()
    {
        base.Awake();

        GridGenerator = GetComponentInChildren<CGridDrivenContentsGenerator>();
    }
}
