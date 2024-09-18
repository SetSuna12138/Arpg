using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackC : MonoBehaviour
{
    public Transform UiItemlist;
    string selectEquipId;
    public GameObject equipInfo;
    public GameObject EquipedWeaponCell;
    public void OnEnable()
    {
        refreshEquip(); 
    }
    public void clickItem(string name)
    {
        Debug.Log("name " +  name );
        selectEquipId = name;
        equipInfo.transform.Find("name").GetComponent<Text>().text = name;
        equipInfo.SetActive(true);
    }
    public void cancelEquip()
    {
        selectEquipId = "";
        equipInfo.SetActive(false);
    }
    public void wearEquip()
    {
        GameDataC.changeEquip(selectEquipId);
        EquipedWeaponCell.transform.GetChild(0).GetComponent<Text>().text = selectEquipId;
        selectEquipId = "";
        equipInfo.SetActive(false);
        refreshEquip();
    }


    public void OnDisable()
    {

    }

    void refreshEquip()
    {
        selectEquipId = "";
        for (int i = 0; i < UiItemlist.childCount; i++)
        {
            if (i < GameDataC.itemList.Count)
            {
                UiItemlist.GetChild(i).GetChild(0).GetComponent<Text>().text = GameDataC.itemList[i].id;
                UiItemlist.GetChild(i).GetChild(1).GetComponent<Text>().text = GameDataC.itemList[i].num + "";
                UiItemlist.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
                int index = i;
                UiItemlist.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    Debug.Log(index + "  : index");
                    clickItem(GameDataC.itemList[index].id);
                });
            }
            else
            {
                UiItemlist.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
                UiItemlist.GetChild(i).GetChild(0).GetComponent<Text>().text = "";
                UiItemlist.GetChild(i).GetChild(1).GetComponent<Text>().text = "";
            }

        }
    }
}
