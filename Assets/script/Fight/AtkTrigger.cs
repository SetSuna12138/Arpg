//#define TEST 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AtkTrigger : MonoBehaviour
{

    public Attribute attribute;

    string skillName;
    public string SkillName
    {
        set
        {
            skillName = value;
        } 
    }
    public enum RoleType
    {
        Player,
        Monster
    }
    public RoleType roleType;

    private void OnTriggerEnter(Collider other)
    { 
        if (other.tag == "Monster")
        { 
            Debug.Log("成功");
#if TEST
            other.GetComponent<TestHit>().beHit();
#else
             FightManager.fightLogic(skillName, attribute, other.gameObject.GetComponent<Attribute>());
             
#endif
         }
    }
}
