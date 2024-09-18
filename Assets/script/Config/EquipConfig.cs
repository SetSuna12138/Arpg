using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipConfig : MonoBehaviour
{
    public struct Data
    {
        public string id;
        public float atk;
    }
    public static Data getDropInfo(string ID)
    {
        Data mData = new Data();
        mData.id = ID;
        switch (ID)
        {
            case "item1001":
                mData.atk = 10; 
                break;
        }
        return mData;
    }
}
