using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.PackageManager;
using System.Runtime.CompilerServices;

public class ExpC  :MonoBehaviour
{

      static ExpC instance;
    public static ExpC Instance
    {
        set
        {
            instance = value;
        }
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ExpC>();
            }
            return instance;
        }
    }
     
    public   int level;
    public   int exp;

    // 策划案正常情况下，打怪经验不会导致升2级。
    // 如果可以一次升2级或以上。则递归调用
    public void addExp(int exp_)
    {
        StartCoroutine(IAddExp(exp_));
    }
    public IEnumerator IAddExp( int exp_)
    { 
        exp += exp_;
        //检测升级
        if ( exp >= ExpConfig.getInfo(level + 1).targetExp)
        {   // 升级
            exp = exp - ExpConfig.getInfo(level + 1).targetExp;
            level++;
            Debug.LogError("升级到" + level);
            yield return new WaitForSeconds(1.5f);
            // 播放特效等
           
           StartCoroutine(IAddExp(0));
           
        }
        else
        {
            // 没有升级    
        }
    }
}
