using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataC
{
    public class ItemData
    {
        public string id;
        public int num;
        public ItemData(string id_, int num_)
        {
            id = id_; num = num_;
        }
    }
    public static string nowMap;
    // 道具列表
    public static List<ItemData> itemList = new List<ItemData>(30);// 如果可以扩充，就给可能的最大值
    public static string equipWeaponId="";

    public static void changeEquip(string id )
    {
        if (!equipWeaponId.EndsWith(""))
        {
            Debug.Log(equipWeaponId + " equipWeaponId 不是空");
            addItem(equipWeaponId,1);
        }
        removeItem(id);
        equipWeaponId = id;
    }

    public static void addItem(string id, int num )
    {

        Debug.Log("add 1 " + itemList.Count);
        Debug.Log("addItem " + id + " num "  + num );
        bool found = false;
        for (int i = 0;i < itemList.Count; i++)
        {
            if (itemList[i].id==id)
            {
                found = true;
                itemList[i].num += num;
                break;
            }
        }
        if (!found)
        {
            ItemData itemData = new ItemData(id,num);
            itemList.Add(itemData);
        }

        Debug.Log("add 2 " + itemList.Count);
    }
    public static void removeItem(string id)
    {
        Debug.Log("删除之前 " + itemList.Count);
        bool found = false;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].id == id)
            {

                found = true;
                Debug.Log("num 前" + itemList[i].num);
                itemList[i].num -= 1;
                Debug.Log("num 后" + itemList[i].num);
                if (itemList[i].num<=0)
                {
                    Debug.Log("remove   "  );
                    itemList.RemoveAt(i);
                }
                break;
            }
        }
        if (!found)
        {
            Debug.LogError("试图remove一个不存在的道具");
            return;
        }
        Debug.Log("删除之后 "  + itemList.Count);
    }

}
