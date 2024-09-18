using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpConfig  
{
    public struct Data
    {
        public int lv;
        public int targetExp;
    }
    public static Data getInfo(int lv)
    {
        Data mData = new Data();
         
        switch (lv)
        {
            case 2: mData.targetExp = 10; break;
            case 3: mData.targetExp = 30; break;
            case 4: mData.targetExp = 70; break;
            case 5: mData.targetExp = 200; break;
            case 6: mData.targetExp = 800; break;
            // 按照配置表
        }
        return mData;
    }
}
