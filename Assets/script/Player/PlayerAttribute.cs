using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : Attribute
{
    public enum 主角状态
    { 
        正常,
        死亡,
        攻击前摇,
        攻击开始,
        攻击中,
        攻击后摇,
        击退开始,
        击退中,
        硬直,
    }
    protected float blockBounceTime =0.3f;
    protected 主角状态 playerState;
}
