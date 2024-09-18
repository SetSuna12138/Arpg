using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectConfig 
{
    public struct EffectData
    {
        public int 限制移动;
        public int 限制攻击;
        public int 限制施法;
        public int 是否击退;
        public int 暂停动画;
        public int 切换状态; 
    }


    public static EffectData getEffectInfo(string ID)
    {
        EffectData mData = new EffectData();
        switch (ID)
        {
            case "冰冻":
                mData.切换状态 = 0; mData.限制移动 = 1; mData.限制攻击 = 1; mData.限制施法 = 1; mData.暂停动画 = 1; mData.是否击退 = 0;
                break;
            case "禁言":
                mData.切换状态 = 0; mData.限制移动 = 0; mData.限制攻击 = 0; mData.限制施法 = 1; mData.暂停动画 = 0; mData.是否击退 = 0;
                break;
            case "硬直":
                mData.切换状态 = 1;  
                break;
             case "击退": // 考虑一下，击退是否是不能兼容的状态，比如冰冻是否可以 击退。禁言是否可以击退，硬直是否可以击退。
               mData.是否击退 = 1;   // 大胆发挥想象力，多尝试，找出适合当前游戏的方案。  区分动画状态和状态机状态。
               break; 
        }
        return mData;
    }
}
