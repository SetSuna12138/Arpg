using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleBase : Attribute 
{ 
  
    
  

    public void beHit(Global.FightInfo fightInfo)
    {
        if ( this.hp <= 0 ) return;
        this.hp -= fightInfo.伤害;
        checkDie();
    }
    public void checkDie()
    {
        if ( this.hp <= 0 )
        {
            Debug.Log("怪物死亡");
        }
    }
}
