using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDropConfig 
{ 
    public struct Data
    {
        public int exp;
        public int money; 

        public string drop1Id;
        public float drop1Rate;
        public int drop1min;
        public int drop1max; 

        public string drop2Id;
        public float drop2Rate;
        public int drop2min;
        public int drop2max; // 横标 纵表  考虑到策划配表，这样做比较方便配表，如果有多个掉落，ID可以指向一个掉落包。需要一个掉落包表。
       
        public string drop3Id;
        public float drop3Rate;
        public int drop3min;
        public int drop3max;
    } 
    public static Data getDropInfo(string ID)
    {
        Data mData = new Data();
        switch (ID)
        {
            case "mons1001":
                mData.exp = 25; mData.money = 10; 
                mData.drop1Id = "item1001"; mData.drop1Rate = 50; mData.drop1min = 1; mData.drop1max = 1;
                mData.drop2Id = "item1002"; mData.drop2Rate = 30; mData.drop2min = 1; mData.drop2max = 1;
                mData.drop3Id = "item1003"; mData.drop3Rate = 10; mData.drop3min = 1; mData.drop3max = 1;
                break; 
        }
        return mData;
    }
}
