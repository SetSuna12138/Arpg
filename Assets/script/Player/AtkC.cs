using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkC : MonoBehaviour
{
    public BoxCollider weapon;
    public static bool can;
    AtkTrigger atkTigger;
    public void Start()
    {
        atkTigger = weapon.GetComponent<AtkTrigger>();
    }
    public void AtkTriggerStart(string name)
    { 
        weapon.enabled = true;
        atkTigger.SkillName = name;
    }
    public void AtkTriggerEnd()
    {
        atkTigger.SkillName = "";
        weapon.enabled = false;  
    }
    public void tx(GameObject tx)
    {
        var instance = Instantiate(tx,this.transform.position,Quaternion.identity  );
        instance.transform.rotation = PlayerC.instance.transform.rotation;
        //instance.transform.parent = PlayerC.instance.transform;
        //instance.transform.localPosition = Vector3.zero;
        //instance.transform.localRotation = new Quaternion();

        Destroy(instance, 3);
    }
}
