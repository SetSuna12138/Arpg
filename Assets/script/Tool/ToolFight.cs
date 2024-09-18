using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolFight  
{
    public static EffectConfig.EffectData ConvertState(Global.FightState[] fs)
    {
        if (fs == null || fs.Length == 0) return new EffectConfig.EffectData(); 
        EffectConfig.EffectData eData = new EffectConfig.EffectData(); 
        foreach (var fs_ in fs )
        {
            EffectConfig.EffectData ed = EffectConfig.getEffectInfo(fs_.ToString());
            eData.限制攻击 += ed.限制攻击;
            eData.限制施法 += ed.限制施法;
            eData.限制移动 += ed.限制移动;
            eData.是否击退 += ed.是否击退;
            eData.暂停动画 += ed.暂停动画;
        }
        return eData;
    }


}
