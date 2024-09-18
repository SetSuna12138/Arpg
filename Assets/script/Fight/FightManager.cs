using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{ 
    public static void fightLogic(string atkId, Attribute attacker , Attribute target)
    {
        Debug.Log(attacker);
        Debug.Log(target);
        if (attacker.hp<=0 ||target.hp<=0)
        {
            return;
        }
        Global.FightInfo fightInfo = new Global.FightInfo(); 
        caleDamage(  atkId, attacker, target ,ref fightInfo); 
        caleState(atkId, attacker, target, ref fightInfo); 
        IFight ifight = target as IFight;
        ifight.beHit(fightInfo);

        //ifight = attacker as IFight;
        //ifight.beHit(fightInfo);
    }

    static void caleState(string atkId, Attribute attacker, Attribute target, ref Global.FightInfo fInfo)
    {
        SkillConfig.SkillData skillData = SkillConfig.getSkillInfo(atkId); 
        fInfo.fightState = skillData.fightState;
    }
    static void caleDamage(string atkId, Attribute attacker, Attribute target, ref Global.FightInfo fInfo)
    {
        SkillConfig.SkillData skillData = SkillConfig.getSkillInfo(atkId);
        // 计算暴击减伤乱七八糟一堆
        // 
        float damage = (attacker.atk + EquipConfig.getDropInfo(GameDataC.equipWeaponId).atk) * skillData.伤害加成 + skillData.伤害值 - target.def;
        if (damage <= 0)
        {
            damage = 1;
        }  
        fInfo.伤害 = damage; 
    }

}
