using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    private int hp;
    private int def;
    private int goldPerTurn;

    // Constructor
    public Building()
    {
        this.hp = 100;
        this.def = 100;
        this.goldPerTurn = 10;
    }

    public int GetHP()
    {
        return this.hp;
    }

    public int GetDef()
    {
        return this.hp;
    }

    public int GetGoldPerTurn()
    {
        return this.goldPerTurn;
    }

}

public class Fort : Building{

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
