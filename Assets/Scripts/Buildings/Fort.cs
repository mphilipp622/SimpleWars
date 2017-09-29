using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fort : Building
{

    private int bonusRange;

    // Constructor
    public Fort()
    {
        this.bonusRange = 1;
    }

    public int GetBonusRange()
    {
        return this.bonusRange;
    }
}
