using System.Collections;
using System.Collections.Generic; 
using System.Threading;
using UnityEngine; 

public class MonsterCreator : MonoBehaviour 
{
    GameObject[] creators ;
    GameObject[] monsterPrefabs;
    string lastMap = ""; 
    public void Awake()
    { 
        if (creators == null)
        {
            creators = GameObject.FindGameObjectsWithTag("SpawnPoint");
        }
        this.gameObject.SetActive(false);
    }

    public void OnEnable () // 初始化怪物列表
    { 
        if (lastMap == GameDataC.nowMap )
        {
            return;
        }
        lastMap = GameDataC.nowMap; 
        List<GameDataConfig.MapCreateMonsData>  mData = GameDataConfig.getMapMonster(lastMap); 
        monsterPrefabs = new GameObject[mData.Count]; 
        for (int i =0;i< mData.Count;i++)
        {
            Debug.Log("mData[i].monsname " + mData[i].monsname);
            monsterPrefabs[i] = Resources.Load<GameObject>("monster/"+mData[i].monsname); 
        } 
        StartCoroutine( makeMonsters());
        
    }

    public IEnumerator   makeMonsters()
    {
        yield return 0; 
        for (int i = 0;i< creators.Length;i++)
        {
            yield return 5; 
            Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], creators[i].transform.position,Quaternion.identity)  ;
        }
    }
 
} 

