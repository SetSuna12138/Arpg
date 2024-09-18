using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GameDataConfig
{
    public struct MapCreateMonsData
    {
        public string monsname;
        public int minNum;
        public int maxNum; 
    }

    public static List<MapCreateMonsData> getMapMonster(string map)
    {
        MapCreateMonsData mData;
        List<MapCreateMonsData> rsl = new List<MapCreateMonsData>();
        switch (map)
        {
            case "map01":
                mData.monsname = "mons1001"; mData.minNum = 5; mData.maxNum = 10;
                rsl.Add(mData);
                break;
           default:
                mData.monsname = "mons1001"; mData.minNum = 5; mData.maxNum = 10;
                rsl.Add(mData);
                break;
        }
        return rsl;
    }


}
     
