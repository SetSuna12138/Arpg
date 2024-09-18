using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
public class MapTriggerC : MonoBehaviour
{
    public bool isDelete;
    public GameObject[] hideObjects;
    public GameObject[] showObjects;
    public string EvnentName;
    public string triggerTag;
    public static event UnityAction<string> MapEvent; 
    void Start()
    {
        
    }

    public void OnTriggerEnter( Collider coll )
    { 
        if (coll.tag == triggerTag)
        {
          //  Debug.Log(MapEvent);
            if (EvnentName != "" && MapEvent != null)
            {
                Debug.Log(EvnentName);
                MapEvent(EvnentName);
            }
            //showObjects[0].SetActive(false); 
            for (int i = 0;i < hideObjects.Length;i++)
            { 
                hideObjects[i].SetActive(false);
            }
            for (int i = 0; i < showObjects.Length; i++)
            { 
                showObjects[i].SetActive(true);
            } 
            if (isDelete)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
