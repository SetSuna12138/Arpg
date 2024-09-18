using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputC : MonoBehaviour
{

    public static InputC instance;
    public Vector2 m_Movement;
    public Vector3 m_Camera;
    public bool m_Attack;
    public bool m_Charge;
    int chargeFrame;
    public bool m_Block;
    public event UnityAction<Vector2> InputEventUpdate; 

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
       
    }
     
    void Update()
    {
        m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse ScrollWheel")); 
        if (Input.GetButtonDown("Fire1"))
        {
            m_Attack = true;
            StartCoroutine(AttackWait());
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("右键");
            m_Block = true;
            StartCoroutine(BlockWait());
        }
        
        if (Input.GetKey(KeyCode.Q))
        { 
            chargeFrame++;
            if (chargeFrame > 3)
            {
                m_Charge = true;
            }
        }
        else
        {
            m_Charge = false;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            UiC.Instance.openUI("backpack"); 
        }

            //if (InputEventUpdate!=null)
            //{
            //    InputEventUpdate(m_Movement);
            //}
        }
    IEnumerator AttackWait()
    { 
        yield return 0; 
        m_Attack = false;
    }
    IEnumerator BlockWait()
    {
        yield return 0;
        m_Block = false;
    }
}
