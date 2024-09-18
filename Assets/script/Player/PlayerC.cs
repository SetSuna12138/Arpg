using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEditor.PackageManager;
using UnityEngine;

//   战斗结算， 包含产生伤害， 普攻，技能，地方的远程近战对我们的伤害，也包含今天技能的一些效果
//   硬直， 击退，
//   经验升级系统，以及掉落系统
//   道具系统，以及 背包 UI。

    // 逻辑与渲染分离    同步


// 训练营  ARPG  
//         小鸟课  菜鸟教程C#

//  算法 查找 排序 A* 噪声     数据结构 数组 list  字典   
//  优化  内存， DC，  CPU  

//    入行  进阶  
//  找完整商业的案例来学习。  P2  MMO  战斗系统  三大章   
//                            P2  XLua插件  XLua开发框架  lua 
//                            P3  多人实时PK 帧同步  
//                            Unet UnityClound  muitlplayer  H  L  奶油大乱斗   

    // 爱给网 // 6m5m   // unity商店免费。 元素网
public class PlayerC : PlayerAttribute , IFight
{
    private CharacterController controller;
    public float walkSpeed = 4.5f; 
    public static PlayerC instance;
    InputC m_Input;
    bool canAttack = true;
    bool canBlock = true;
    public float blockTime = 0;
    public PlayerAni Ani;
    float addSpeed = 1;
    float skillAdd = 3; 
    float skillAddMax = 3;
    float skillAddMin = 1;
    float skillLastTime = 3.5f;
    bool 技能不能移动 = false;
    bool 技能不能攻击 = false;
    bool 技能不能并发 = false;
    int 当前释放技能 = 0;
    bool 攻击不能移动 = true ;
    private void OnEnable()
    {
       // InputC.instance.InputEventUpdate += FuncUpdate;
    }
    private void OnDisable()
    {
       // InputC.instance.InputEventUpdate -= FuncUpdate;
    }

    private void Awake()
    {
        instance = this;
        Ani = FindObjectOfType<PlayerAni>();
        controller = GetComponent<CharacterController>();
        m_Input = FindObjectOfType<InputC>();
        this.atk = 50;
        this.hpmax = 200;
        this.hp = this.hpmax;
    }
    private void Move()
    {
        if (技能不能移动) return;
        if (攻击不能移动 &&(Ani.checkCurrentState("Atk1")|| Ani.checkCurrentState("Atk2")|| Ani.checkCurrentState("Atk3")))
        {
            return;
        }
        //float H = Input.GetAxis("Horizontal");
        //float V = Input.GetAxis("Vertical");
        Vector3 dir = transform.TransformDirection( new Vector3(m_Input.m_Movement.x, -1, m_Input.m_Movement.y)   );
        controller.Move(dir * walkSpeed * Time.deltaTime);
    }
   bool CheckState()
    {
        if (playerState == 主角状态.死亡)
        {
            return false;
        }
        return true;
    } 
    void Update()
    {
        if (!CheckState()) return;
        Ani.FuncUpdate();
        Atk();
        Skill();
        Move(); 
    } 
    void Atk()
    {
        if (技能不能攻击) return; 
        if (m_Input.m_Attack && canAttack)
        {
            PlayerAni.Instance.atk();
        }
        //  m_Animator.SetTrigger(m_HashMeleeAttack);

        if (m_Input.m_Block && canBlock)
        { 
               blockTime = Time.time;
            canBlock = false;
            PlayerAni.Instance.block();
            StartCoroutine(BlockWait());
        }
    }
    IEnumerator BlockWait()
    {
        yield return new WaitForSeconds(0.5f);
        canBlock = true;
    }
    //void FuncUpdate(Vector2 m_Movement)
    //{
    //    Vector3 dir = transform.TransformDirection(new Vector3(m_Movement.x, -1, m_Movement.y));
    //    controller.Move(dir * walkSpeed * Time.deltaTime);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MonsAtkTrigger")
        {
            getHit(other.gameObject);
        }else if (other.tag == "DropItem")
        {
            Debug.Log("捡起"+ other.gameObject.name);
            GameDataC.addItem(other.gameObject.name ,1);
            Destroy(other.gameObject);
        }
    }

    public void getHit(GameObject obj)
    {
        float time_ = Time.time;
        if (time_ - blockTime <= blockBounceTime) // 格挡成功
        {
            Destroy(obj);
            Debug.Log("格挡成功");
        }
    }
    public void Skill()
    {

        if (技能不能并发 && 当前释放技能 != 1001) return;
        if (m_Input.m_Charge)
        {
            skill1();
        }else if (skillAdd > 0)
        {
            skill1Use();
        }
    }
    public void skill1()
    { 
        当前释放技能 = 1001;
        技能不能攻击 = true;
        技能不能移动 = true;
        技能不能并发 = true;
        skillAdd += Time.deltaTime * addSpeed;
        skillAdd = Mathf.Clamp(skillAdd,skillAddMin, skillAddMax);
    }
    public void skill1Use()
    {   
        if (skillAdd <= 0) return; 
        skillAdd = 0;
        StartCoroutine(skillLast());
    }
    IEnumerator skillLast()
    {     
        yield return  new WaitForSeconds(skillLastTime);
        技能不能移动 = false;
        技能不能攻击 = false;
        技能不能并发 = false;
        当前释放技能 = 0;
    } 
    public void beHit(Global.FightInfo fightInfo)
    {
        if (this.hp <= 0) return;
        this.hp -= fightInfo.伤害;
        checkDie();
    }
    void checkDie()
    {
        if (this.hp <= 0)
        {
            playerState = 主角状态.死亡;
            
        }
    }
    
}
