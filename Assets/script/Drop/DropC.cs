using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class DropC  
{ 
    public struct DropData
    {
        public string id;
        public int num; 
    } 
    public static  List<DropData>  doDrop(string monsId )
    {
        // 更好的魔法装备掉落。玩家身上的幸运值属性加成。 增加概率。
        List<DropData> dropDataList = new List<DropData>(25) ; // 设置一个max数 优化GC
        DropData dropData = new DropData();
        MonsterDropConfig.Data data = MonsterDropConfig.getDropInfo(monsId); 
        int i1 = ToolRandom.rand_100(); 
        if (i1 <= data.drop1Rate)
        { 
            dropData.id = data.drop1Id;
            dropData.num = Random.Range(data.drop1min, data.drop1max);
            dropDataList.Add(dropData); 
        }
        if (i1 <= data.drop2Rate)
        {
            dropData.id = data.drop2Id;
            dropData.num = Random.Range(data.drop2min, data.drop2max);
            dropDataList.Add(dropData);
        }
        if (i1 <= data.drop3Rate)
        {
            dropData.id = data.drop3Id;
            dropData.num = Random.Range(data.drop3min, data.drop3max);
            dropDataList.Add(dropData);
        }
        return dropDataList;
    }

    public static void doDrop(string monsId, Vector3 Pos)
    {
        // 更好的魔法装备掉落。玩家身上的幸运值属性加成。 增加概率。 
        DropData dropData = new DropData();
        MonsterDropConfig.Data data = MonsterDropConfig.getDropInfo(monsId);
        int i1 = ToolRandom.rand_100();
        GameObject item;
        if (i1 <= data.drop1Rate)
        { 
            //dropData.num = Random.Range(data.drop1min, data.drop1max);
            // 例子只掉落一个，需要掉落num个的时候，写一个处理方法，将掉落位置分散开
            item =  GameObject.Instantiate(Resources.Load<GameObject>("drop/"+ data.drop1Id));
            item.name = data.drop1Id;
        }
        if (i1 <= data.drop2Rate)
        {
            //dropData.num = Random.Range(data.drop2min, data.drop2max);
            item = GameObject.Instantiate(Resources.Load<GameObject>("drop/" + data.drop2Id));
            item.name = data.drop1Id;
        }
        if (i1 <= data.drop3Rate)
        {
            dropData.id = data.drop3Id;
            // dropData.num = Random.Range(data.drop3min, data.drop3max);
            item = GameObject.Instantiate(Resources.Load<GameObject>("drop/" + data.drop3Id));
            item.name = data.drop1Id;
        }
         
        ExpC.Instance.addExp(data.exp);
       
    }
}
