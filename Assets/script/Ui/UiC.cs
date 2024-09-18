using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiC : MonoBehaviour
{
    public static UiC Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UiC>();
            }
            return instance;
        }

    }
    static UiC instance;
    public Dictionary<string, GameObject> module = new Dictionary<string, GameObject>();
    public GameObject lastOpened;
    void Awake()
    { 
        Transform canvas = this.transform.Find("Canvas");
        foreach(Transform tr in canvas){
            module.Add(tr.name, tr.gameObject);
        }

    }
    public void openUI(string name) {

        if (lastOpened!=null && lastOpened.name == name && lastOpened.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            module[name].SetActive(false);
            lastOpened = null;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (lastOpened != null) lastOpened.SetActive(false);
            module[name].SetActive(true);
            lastOpened = module[name];
        }
       
    }

}
