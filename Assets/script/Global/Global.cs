using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public struct FightStateEffect
    {
        public bool 能否移动;
        public bool 能否攻击;
        public bool 能否施法;
        public bool 能否击退;
    }
   public enum FightState
    {
        硬直,
        冰冻,
        击退,
        禁言, 
    }
   public struct FightInfo
    { 
        public float 伤害;
        public bool 暴击;
        public FightState[] fightState;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
