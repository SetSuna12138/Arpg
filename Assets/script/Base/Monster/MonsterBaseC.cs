using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;// DoTween  
using System;

public class MonsterBaseC : MonsterAttribute ,IFight
{ 
    private Image hpUI; 
    protected Animator ani;
    private GameObject 攻击碰撞体;
    protected CharacterController cc; 
    protected float 攻击cd_ = 0;
    protected float 硬直时间_ = 0.3f;
    //protected float 决策时间_ = 1; 
    protected float 发呆时间_ = 1; 
    protected float 后摇时间_ = 1;
    protected float 前摇时间_ = 1; 
    protected 怪物状态 状态;
    protected Transform 目标;
    GameObject 头上图标; 
    //int 限制移动;
    //int 限制攻击;
    //int 限制施法;
    EffectConfig.EffectData eData;
    float 击退速度 = 15;
    float 击退加速度;
    float 冰冻持续 = 5;
    float 解除冰冻时间 = -1;

    public enum 怪物状态
    {
        没有目标,
        锁定目标,
        死亡,
        发呆,
        追击,
        攻击决策,
        攻击前摇,
        攻击开始,
        攻击中,
        攻击后摇,   
        击退中,
        硬直, 
    }
   
    public virtual void Awake()
    {
        binding();
    }
   
    public virtual void Update()
    {
        if (!状态检测()) return;
        
        if (状态 == 怪物状态.没有目标)
        {
            尝试发现目标();
        }
        else if (状态 == 怪物状态.锁定目标)
        {
            针对目标决策();
        }
        else if (状态 == 怪物状态.追击)
        {
            追击();
        }
        else if (状态 == 怪物状态.攻击决策)
        {
            针对攻击决策();
        }
        else if (状态 == 怪物状态.攻击开始)
        {
            攻击发动();
        }
        else if (状态 == 怪物状态.发呆)
        {
            发呆();
        }
        else if (状态 == 怪物状态.攻击后摇)
        {
            后摇();
        }
        else if (状态 == 怪物状态.攻击前摇)
        {
            前摇();
        }
        else if (状态 == 怪物状态.击退中)
        {
            击退();
        }
    }
    void 击退()
    { 
        if (击退速度 > 2f) // 处于击退中
        {
            击退加速度 += Time.deltaTime * 20;
            击退速度 -= 击退加速度;
            cc.Move(this.transform.forward * -击退速度 * Time.deltaTime); 
            return;
        }
        else
        {
            状态 = 怪物状态.没有目标;
            eData.是否击退  = 0;
            击退速度 = 15;
            击退加速度 = 0;
        }
    }
    void 尝试发现目标()
    {
        float distance = Vector3.Distance(this.transform.position, PlayerC.instance.transform.position);
        // 先判定听觉
        if (distance <= 听觉范围)
        {
            发现目标();
        }
        else
        {
            if (distance <= 视觉范围 && ToolMethod.获取与目标的夹角(PlayerC.instance.transform.position, this.transform) < GameConfig.怪物视野角度 / 2)
            {
                发现目标();
            }
            else
            {
                解除目标();
            }
        }
    }
    void 针对目标决策()
    {
        int R = UnityEngine.Random.Range(0, 100);
        if (R <= 追击概率)
        {
            this.transform.DOLookAt(new Vector3(目标.position.x, this.transform.position.y, 目标.position.z), 1.3f);
            状态 = 怪物状态.追击;
        }
        else
        { 
            状态 = 怪物状态.发呆;
        }
    }
    void 针对攻击决策()
    {
        int R = UnityEngine.Random.Range(0, 100);
        if (R <= 攻击概率)
        {
            ani.SetBool("beforeAtk", true);
            this.transform.DOLookAt(new Vector3(目标.position.x, this.transform.position.y, 目标.position.z), 0.6f);
            状态 = 怪物状态.攻击前摇;
        }
        else
        { 
            状态 = 怪物状态.发呆;
        }
    }
    void 发现目标()
    {
        状态 = 怪物状态.锁定目标;
        目标 = PlayerC.instance.transform;
        // 出现反应图标( 叹号，对白等)
        //if (!!头上图标)
        //{
        //    头上图标.SetActive(true);
        //    Invoke("关闭图标", 0.4f);
        //}
    }
    void 关闭图标()
    {
        头上图标.SetActive(false);
    } 
    void 解除目标()
    {
        状态 = 怪物状态.没有目标;
        目标 = null;
    }
    bool 状态检测()
    {
        if (状态 == 怪物状态.死亡)
        {
            return false;
        }
        //Debug.Log("eData.暂停动画 " + eData.暂停动画);
        if (eData.暂停动画 > 0)
        {
            ani.speed = 0;
        }
        else
        {
            ani.speed = 1;
        }
        if (解除冰冻时间!=-1 && Time.time > 解除冰冻时间)
        {
            // 动画解除冰冻状态

            // 读取配置数据，做相应的减法，这里偷懒直接写了。
            eData.限制移动 -= 1;
            eData.限制施法 -= 1;
            eData.限制攻击 -= 1;
            eData.暂停动画 -= 1;
        }
        if (eData.是否击退 > 0)
        {
            状态 = 怪物状态.击退中;
        } 

        if (硬直时间_ > 0)
        {
            硬直时间_ -= Time.deltaTime;
            return false;
        }
        else
        {
            if (状态 == 怪物状态.硬直)
            {
                状态 =  怪物状态.没有目标;
            }
        }
        return true;
    }
    void 追击()
    {
        if (检测是否可攻击())
        {
            状态 = 怪物状态.攻击决策;
            ani.SetBool("walking", false);
            return;
        }
        if (检测是否失去目标())
        {
            状态 = 怪物状态.没有目标;
            ani.SetBool("walking", false);
            return;
        }
        if (eData.限制移动 > 0) return;
        //Debug.Log("移动");
        this.transform.LookAt(new Vector3(目标.position.x, this.transform.position.y, 目标.position.z));
        cc.Move(this.transform.forward * Time.deltaTime * moveSpeed);
        ani.SetBool("walking", true);
    }
    bool 检测是否失去目标()
    {
        float distance = Vector3.Distance(this.transform.position, PlayerC.instance.transform.position);
        // 先判定听觉
        if (distance >= 失去目标距离)
        {
            return true;
        }
        return false;
    }
    bool 检测是否可攻击()
    {
        float distance = Vector3.Distance(this.transform.position, PlayerC.instance.transform.position);
        if (distance <= 攻击距离)
        {
            return true;
        }
        return false;
    }
    void 前摇()
    {
        if (eData.限制攻击 > 0) return;
        前摇时间_ -= Time.deltaTime;
        if (前摇时间_ <= 0)
        {
            ani.SetTrigger("atk"); // 没有前摇动作，用攻击动作代替
            状态 = 怪物状态.攻击开始;
            前摇时间_ = 前摇时间;
        }
    }
    void 后摇()
    {
        if (eData.限制攻击 > 0) return;
        后摇时间_ -= Time.deltaTime;
        if (后摇时间_ <= 0)
        {
            ani.SetBool("beforeAtk", false);
            状态 = 怪物状态.没有目标;
            后摇时间_ = 后摇时间;
        }
    }
    void 发呆()
    {
        if (发呆是否看向主角)
        {
            // this.transform.DOLookAt(new Vector3(目标.position.x, this.transform.position.y, 目标.position.z), 1.3f);
            this.transform.LookAt(new Vector3(目标.position.x, this.transform.position.y, 目标.position.z));
        }
        发呆时间_ -= Time.deltaTime;
        if (发呆时间_ <= 0)
        {
            状态 = 怪物状态.没有目标;
            发呆时间_ = 发呆时间;
        }
    }
    void 攻击发动()
    {
        if (eData.限制攻击 > 0) return;
        //攻击cd_ -= Time.deltaTime;
        //if (攻击cd_ <= 0)
        //{
        状态 = 怪物状态.攻击中; 
            //if (ani != null)
            //{
            //    ani.SetTrigger("atk");
          //  Debug.Log("攻击动画" + Time.realtimeSinceStartup);
            //}
            StartCoroutine(攻击()) ; 
        //    攻击cd_ = 攻击cd;
        //}
    }
    public  virtual IEnumerator 攻击()
    {
        yield return 0;
        this.transform.LookAt(new Vector3(目标.position.x, this.transform.position.y, 目标.position.z));
        Vector3 dir = this.transform.forward;
        yield return new WaitForSeconds(普攻生效时间);
        //  Debug.Log("攻击 " + Time.realtimeSinceStartup);

        if (eData.限制攻击 == 0)
        {
            GameObject bullet_ = Instantiate(bullet);
            bullet_.transform.LookAt(bullet_.transform.position + new Vector3(dir.x, 0, dir.z));
            bullet_.transform.position = this.transform.position + this.transform.forward;
            bullet_.GetComponent<Rigidbody>().velocity = bullet_.transform.forward * 5;
            状态 = 怪物状态.攻击后摇;
        }
        else
        {
            状态 = 怪物状态.攻击后摇;
        }
    }

    public virtual void 攻击帧事件()
    {
       
        Debug.Log("父物体攻击帧事件");
    }

    public void binding()
    {
        ID = "mons1001"; // 正常做怪物在预制体的脚本上设置该属性
        cc = this.GetComponent<CharacterController>(); 
        bullet = Resources.Load<GameObject>("tx/TX1001");
        ani = this.GetComponent<Animator>();
        if (ani == null)
        {
            ani = this.transform.GetChild(0).GetComponent<Animator>();
        }
        Debug.Log("ani " + ani); 
        this.atk = 100;
        this.hpmax = 200;
        this.hp = this.hpmax;
    }


    public virtual void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == "飞行道具")
        {
             hp -= 5;
            hpUI.fillAmount = hp / hpmax;
            this.transform.DOPunchRotation(Vector3.one * 15f, 0.1f, 1, 1).OnComplete(delegate ()
            {
                checkDie();
            }); 
        }
    }
    //public virtual void beHit(float demage_)
    //{
        

    //    硬直时间_ = 硬直时间;
    //    hp -= demage_;
    //    hpUI.fillAmount = hp / hpmax;
    //    this.transform.DOPunchRotation(Vector3.one * 15f, 0.1f, 1, 1).OnComplete(delegate ()
    //    {
    //        if (hp <= 0)
    //        {
    //            checkDie();
    //        }
    //    });
    //}
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "攻击碰撞体")
        {
          //    beHit(PlayerInputControllerC.instance.get攻击输出(name, other.name));
        }
    }
    // Update is called once per frame
    public virtual IEnumerator Die()
    {
        yield return new WaitForSeconds(2); 
        drop();
        Destroy(this.gameObject);
    }

    public void beHit(Global.FightInfo fightInfo)
    {
        if (this.hp <= 0) return;
        this.hp -= fightInfo.伤害; 
        eData = ToolFight.ConvertState(fightInfo.fightState);
        Debug.Log("eData.是否击退 "  + eData.是否击退);
        if (!checkDie())
        {
            foreach (var fs in fightInfo.fightState)
            {
                if (fs== Global.FightState.冰冻)
                {
                    // 播放冰冻动画
                    解除冰冻时间 = Time.time + 冰冻持续;
                }
            }
            //if (普攻打断动作)
            //{ 
            //    状态 = 怪物状态.硬直;
            //    硬直时间_ = 硬直时间;
            //    ani.SetTrigger("damage"); 
            //}

        }
    }
    bool checkDie()
    {
        if (this.hp <= 0)
        {
            状态 = 怪物状态.死亡;
            Debug.Log("怪物死亡");
            StartCoroutine(Die());
            return true;
        } 
        return false;
    }

    public void drop() // 掉落
    {
        DropC.doDrop(this.ID, this.transform.position);
    }

    
}
