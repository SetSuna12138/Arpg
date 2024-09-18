using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillConfig  
{
    public struct SkillData
    {
        public string ID;
        public float 伤害加成;
        public float 伤害值;
        public int level;
        public string txName;
        public Global.FightState[] fightState; 
    }
     

    public static SkillData getSkillInfo(string ID )
    {
        SkillData mData = new SkillData(); 
        switch (ID )
        {
            case "atk1":
                Debug.Log("atk1 ********* ");
                mData.伤害加成 = 1; mData.伤害值 = 0;
                mData.fightState = new Global.FightState[2];
                mData.fightState[0] = Global.FightState.击退;
                mData.fightState[1] = Global.FightState.冰冻;
                break;
            case "atk2":
                mData.伤害加成 = 1.1f; mData.伤害值 = 0;
                break;
            case "atk3":
                mData.伤害加成 = 1.5f; mData.伤害值 = 0;
                mData.fightState = new Global.FightState[1];
                mData.fightState[0] = Global.FightState.冰冻;
                break;
            case "skil1001":
                mData.伤害加成 = 1.5f; mData.伤害值 = 500;
                mData.fightState = new Global.FightState[2];
                mData.fightState[0] = Global.FightState.击退;
                mData.fightState[1] = Global.FightState.硬直;
                break;
            case "skil1002":
                mData.伤害加成 = 0; mData.伤害值 = 0;
                mData.fightState = new Global.FightState[1];
                mData.fightState[0] = Global.FightState.硬直; 
                break;
            case "skill003":
                mData.伤害加成 = 1; mData.伤害值 = 10;
                mData.fightState = new Global.FightState[1];
                mData.fightState[0] = Global.FightState.禁言;
                break;

        }
        return mData;
    }



}
