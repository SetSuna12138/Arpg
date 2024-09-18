using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    public static PlayerAni Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerAni>() ;
            }
            return instance;
        }

    }
    static PlayerAni instance;
    public Animator ani;
    InputC m_Input;

    void Awake()
    {
        instance = this;
        Debug.Log("awake*********");
        ani = this.transform.GetChild(0).gameObject.GetComponent<Animator>();
        m_Input = FindObjectOfType<InputC>();
    }
     
   public void FuncUpdate()
    { 
        ani.SetFloat("AniTime", Mathf.Repeat(ani.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));
        ani.SetFloat("horizontal" ,  Math.Abs( m_Input.m_Movement.x)  );
        ani.SetFloat("vertical", Math.Abs(m_Input.m_Movement.y)); 
        ani.ResetTrigger("atk");
        ani.SetBool("蓄力", m_Input.m_Charge);
    }
    public void atk()
    { 
        ani.SetTrigger("atk");
    }
    public void block()
    { 
       ani.SetTrigger("block");
    }

    public bool checkCurrentState(string name )
    {
        return ani.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
