using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField]
    int enemyType;//0=보스,1=검,2=할버드,3=해머,4=권총,5=화승총,99=소환사보스 소환물,98 네크로멘서 소환물
    [SerializeField]
    int bossType;//골렘=0,소환사=1,네크로멘서=2
    public int enemyStatus = 1;//0=이동중,1=대기중,2=공격중,3=사망,4=공격후 대기,5=원거리유닛의 근접공격,6=부활모션7=부활중,8=행동불가;
    int memore;//죽기전상태
    [SerializeField]
    GameObject AttackParticlePos;
    [SerializeField]
    GameObject AttackParticlePos2;
    public int thisPosX;
    public int thisPosY;
    [SerializeField]
    public int goingPosX;
    public int goingPosY;
    [SerializeField]
    int attackPosX, attackPosY;
    bool goUp, goDown;
    bool attackUp, attackDown;
    bool goforward;
    Animator ani;
    float redColorTimer = 0;
    float testTimer = 2;
    Renderer thisRenderer;
    Color Hit;
    public Door door;
    bool attacked=false;


    void Start()
    {
        Damage = 50;
        Hit = new Color(1, 0.5f, 0.5f);
        switch (enemyType)
        {
            case 0:
                if (bossType == 0)
                {
                    Max_Health = 500;
                    thisRenderer = this.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>();
                    this.transform.forward = Vector3.forward;
                }else if(bossType == 1)
                {
                    Max_Health = 1000;
                    thisRenderer = this.transform.GetChild(0).GetComponent<Renderer>();
                    this.transform.forward = Vector3.left;
                }
                else if(bossType == 2)
                {
                    Max_Health = 1000;
                }
                break;
            case 1://검
                thisRenderer = this.transform.GetChild(0).GetComponent<Renderer>();
                this.transform.forward = Vector3.left;
                Move_Speed = EnemyStatusArr.MoveSpeed[1];
                Current_Move_Speed = Move_Speed;
                Attack_range = EnemyStatusArr.AttackRange[1];
                Max_Health = EnemyStatusArr.MaxHp[1];
                Attack_Speed = EnemyStatusArr.AttackSpeed[1];
                enemyStatus = 1;
                ani = this.GetComponent<Animator>();
                break;
            case 2://할버드
                thisRenderer = this.transform.GetChild(0).GetComponent<Renderer>();
                this.transform.localEulerAngles = new Vector3(0, 0, 0);
                Move_Speed = EnemyStatusArr.MoveSpeed[2];
                Current_Move_Speed = Move_Speed;
                Attack_range = EnemyStatusArr.AttackRange[2];
                Max_Health = EnemyStatusArr.MaxHp[2];
                Attack_Speed = EnemyStatusArr.AttackSpeed[2];
                enemyStatus = 1;
                ani = this.GetComponent<Animator>();
                break;
            case 3://해머
                thisRenderer = this.transform.GetChild(0).GetComponent<Renderer>();
                this.transform.localEulerAngles = new Vector3(0, 0, 0);
                Move_Speed = EnemyStatusArr.MoveSpeed[3];
                Current_Move_Speed = Move_Speed;
                Attack_range = EnemyStatusArr.AttackRange[3];
                Max_Health = EnemyStatusArr.MaxHp[3];
                Attack_Speed = EnemyStatusArr.AttackSpeed[3];
                enemyStatus = 1;
                ani = this.GetComponent<Animator>();
                Damage = 100;
                break;
            case 4://권총
                thisRenderer = this.transform.GetChild(0).GetComponent<Renderer>();
                this.transform.forward = Vector3.left;
                Move_Speed = EnemyStatusArr.MoveSpeed[4];
                Current_Move_Speed = Move_Speed;
                Attack_range = EnemyStatusArr.AttackRange[4];
                Max_Health = EnemyStatusArr.MaxHp[4];
                Attack_Speed = EnemyStatusArr.AttackSpeed[4];
                enemyStatus = 1;
                ani = this.GetComponent<Animator>();
                break;
            case 5://총
                thisRenderer = this.transform.GetChild(0).GetComponent<Renderer>();
                this.transform.forward = Vector3.left;
                Move_Speed = EnemyStatusArr.MoveSpeed[5];
                Current_Move_Speed = Move_Speed;
                Attack_range = EnemyStatusArr.AttackRange[5];
                Max_Health = EnemyStatusArr.MaxHp[5];
                Attack_Speed = EnemyStatusArr.AttackSpeed[5];
                enemyStatus = 1;
                ani = this.GetComponent<Animator>();
                break;
            case 98://소환물 언데드
                thisRenderer = this.transform.GetChild(0).GetComponent<Renderer>();
                this.transform.forward = Vector3.left;
                Attack_range = 1;
                Current_Move_Speed = 1.5f;
                Move_Speed = 1.5f;
                enemyStatus = 6;
                Max_Health = 60;
                Attack_Speed = 3;
                ani = this.GetComponent<Animator>();
                this.transform.position -= new Vector3(0, 5, 0);

                break;
            case 99://소환물 골렘
                thisRenderer = this.transform.GetChild(0).GetComponent<Renderer>();
                this.transform.forward = Vector3.left;
                Attack_range = 1;
                Current_Move_Speed = 1.5f;
                Move_Speed = 1.5f;
                Attack_Speed = 3;
                Max_Health = 60;
                ani = this.GetComponent<Animator>();
                break;

        }
       
        Current_health = Max_Health;
        Attack_Speed = 1;
        if (enemyType == 0)//보스 로테이션 버그잡는용 임시
        {
            if (bossType == 0)
                this.transform.forward = Vector3.forward;
            if (bossType == 1)
                this.transform.forward = Vector3.left;
        }

    }

    void Update()
    {
        if(ani==null)
            ani = this.GetComponent<Animator>();
        if (enemyStatus == 6 || enemyStatus == 3 || enemyStatus == 7) { }
        else if (enemyType != 0)
        {
            if (this.transform.position.y < 0.1)
            {
                this.transform.Translate(0, -this.transform.position.y, 0);
            }
            else if (this.transform.position.y > 0.1)
            {
                this.transform.Translate(0, -this.transform.position.y, 0);
            }
        }
        if (enemyStatus == 4)
        {
            if (attackPosX != -1)
            {
                if (TileStatus.tileStatus[attackPosX, attackPosY, 1] == null || TileStatus.tileStatus[attackPosX, attackPosY, 1].tag != "TeamUnit")
                {
                    enemyStatus = 1;
                }
            }
        }
        if (Current_Move_Speed < Move_Speed)
        {
            Current_Move_Speed += 0.05f;
            if(Current_Move_Speed>Move_Speed)
            {
                Current_Move_Speed = Move_Speed;
            }
        }
        if (thisRenderer != null)//피격시 색변경
        {
            if (redColorTimer > 0)
            {
                thisRenderer.material.color = Hit;
                redColorTimer -= Time.deltaTime;
            }
            else if (redColorTimer < 0)
            {
                thisRenderer.material.color = Color.white;
            }
            else if (redColorTimer == 0)
            {
                if (thisRenderer.material.color == Hit)
                {
                    thisRenderer.material.color = Color.white;
                }
            }
        }

        if (enemyType != 0)
        {
            //attackDelayTime -= Time.deltaTime;
            //if (enemyStatus == 2)
            //{
                
            //    if (attackDelayTime > 0)
            //    {
            //        ani.SetInteger("EnemyStatus", 4);
            //        //ani.Play("attack delay");
            //        // ani.speed = 0;
            //    }
            //    else if (attackDelayTime > -1)//|| attacked ==false
            //    {
            //        if (!attacked)
            //        {
            //                    ani.SetInteger("EnemyStatus", 2);
                        
            //        }else
            //        {
            //            ani.SetInteger("EnemyStatus", 1);
            //        }

            //        if (attackDelayTime < -0.5)
            //        {
            //            if (!attacked)
            //            {
            //                if (attackPosX != -1)
            //                {

            //                    TileStatus.tileStatus[attackPosX, attackPosY, 1].gameObject.GetComponent<TeamUnit>().getDamage(Damage);
            //                    attacked = true;
            //                    if (gunFire != null)
            //                        gunFire.Play();

            //                }
            //                else
            //                {
            //                    if (door != null)
            //                    {
            //                        door.Hit(Damage / 10);
            //                        attacked = true;
            //                        if (gunFire != null)
            //                            gunFire.Play();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else if (attackDelayTime < -0.5f) 
            //    {
            //        attackDelayTime = 5 / Attack_Speed;
            //        attacked = false;
            //    }
            //    if (attackPosX!=-1) {
            //        if (TileStatus.tileStatus[attackPosX, attackPosY, 1] == null || TileStatus.tileStatus[attackPosX, attackPosY, 1].tag != "TeamUnit")
            //        {
            //            enemyStatus = 1;
            //        } 
            //    }else
            //    {
            //        enemyStatus = 2;
            //    }
            //}
            if (enemyStatus == 1)
            {

                goingCheck();
                if(enemyStatus==2)
                {
                    if(!attacked)
                    StartCoroutine(attack());
                    else
                    {
                        ani.SetInteger("EnemyStatus", 4);
                        enemyStatus = 4;
                    }
                }
                //Debug.Log(attackPosX);
                //Debug.Log(enemyStatus);
                if (enemyStatus==2)
                {
                    this.transform.forward = Vector3.left;
                }    
               
            }
            

            //이탈방지 목표지점을 넘어가면 목표지점으로 좌표수정
            if (this.transform.forward == Vector3.forward)
            {
                if (this.transform.position.z > (2.5f + goingPosY * 5))
                {
                    this.transform.position = new Vector3((goingPosX * 5 - 2.5f), 0, (2.5f + goingPosY * 5));
                }
            }
            if (goingPosX < thisPosX)
            {
                if (this.transform.position.x < (goingPosX * 5 - 2.5f))
                {
                    this.transform.position = new Vector3((goingPosX * 5 - 2.5f), 0, (2.5f + goingPosY * 5));
                }
            }
            if (this.transform.forward == Vector3.back)
            {
                if (this.transform.position.z < (2.5f + goingPosY * 5))
                {
                    this.transform.position = new Vector3((goingPosX * 5 - 2.5f), 0, (2.5f + goingPosY * 5));
                }
            }
            if (enemyStatus == 1)
            {
                this.transform.forward = Vector3.left;
                ani.SetInteger("EnemyStatus", 1);
            }else if (enemyStatus == 0)
            {

                ani.SetInteger("EnemyStatus", 0);
                //ani.speed = Move_Speed / 10;
                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * Current_Move_Speed;
                if (Vector3.Distance(this.transform.position, new Vector3((goingPosX * 5 - 2.5f), 0, (2.5f + goingPosY * 5))) < Current_Move_Speed * Time.deltaTime)
                {
                    this.transform.position = new Vector3((goingPosX * 5 - 2.5f), 0, (2.5f + goingPosY * 5));
                    thisPosX = goingPosX;
                    thisPosY = goingPosY;

                    enemyStatus = 1;
                }
            }else if(enemyStatus==6)
            {
                revive();
            }else if(enemyStatus==7)
            {
                this.transform.Translate(Vector3.up * Time.deltaTime * 4);
                if (this.transform.position.y > 0.05f)
                {
                    this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                }
                else if (this.transform.position.y == 0)
                {
                    this.transform.Translate(Vector3.up * -this.transform.position.y);
                }
            }
        }
    }
    

    //bool goingCheck()//앞타일 체크 함수 에너미1용
    //{
    //    goforward = false;
    //    goUp = false;
    //    goDown = false;
    //    if (thisPosX > 0)
    //    {
    //        if (TileStatus.tileStatus[thisPosX - 1, thisPosY, 1] == null)
    //        {
    //            goingPosX = thisPosX - 1;
    //            goingPosY = thisPosY;
    //            enemyStatus = 0;
    //            goforward = true;
    //            this.transform.forward = Vector3.left;
    //            TileStatus.tileStatus[goingPosX, goingPosY, 1] = this.gameObject;//목표지점에 자기의 정보 입력
    //            if (TileStatus.tileStatus[thisPosX, thisPosY, 1] == this.gameObject)//현제 위치 타일에 자기의 정보가 있으면 삭제
    //                TileStatus.tileStatus[thisPosX, thisPosY, 1] = null;
    //        }
    //        else
    //        {
    //            if (TileStatus.tileStatus[thisPosX - 1, thisPosY, 1].tag == "TeamUnit")
    //            {
    //                enemyStatus = 2;
    //                attackPosX = thisPosX - 1;
    //                attackPosY = thisPosY;
    //                this.transform.LookAt(TileStatus.tileStatus[thisPosX - 1, thisPosY, 1].transform);
    //            }
    //            else
    //            {
    //                enemyStatus = 1;
    //                TileStatus.tileStatus[thisPosX, thisPosY, 1] = this.gameObject;
    //            }
    //        }
    //    }else{
    //        enemyStatus = 1;
    //        TileStatus.tileStatus[thisPosX, thisPosY, 1] = this.gameObject;
    //    }
    //    //sidCheck();
    //    if (enemyStatus == 2)
    //    {
    //        TileStatus.tileStatus[goingPosX, goingPosY, 1] = null;
    //        TileStatus.tileStatus[thisPosX, thisPosY, 1] = this.gameObject;
    //    }
    //    return false;
    //} 
    bool goingCheck()//앞타일 체크 함수
    {
        //goforward = false;
        goUp = false;
        goDown = false;

        switch(enemyType)
        {
            case 1:
                break;
            case 2:
                if (thisPosX - 1 > 0)
                {
                    if (TileStatus.tileStatus[thisPosX - 2, thisPosY, 1] != null)
                    {
                        if (TileStatus.tileStatus[thisPosX - 2, thisPosY, 1].tag == "TeamUnit")
                        {
                            enemyStatus = 2;
                            attackPosX = thisPosX - 2;
                            attackPosY = thisPosY;
                            this.transform.LookAt(TileStatus.tileStatus[thisPosX - 2, thisPosY, 1].transform);
                            return true;
                        }
                    }
                }
                if (thisPosX == 1)
                {
                    enemyStatus = 2;
                    attackPosX = -1;
                    attackPosY = thisPosY;
                    this.transform.forward = Vector3.left;
                    return true;
                }
                break;
            case 3:
                break;
            case 4:
                if (rangedAttackCheck(0) == -1)
                {
                    if (thisPosX - Attack_range <= -1)
                    {
                        attackPosY = thisPosY;
                        attackPosX = -1;
                        enemyStatus = 2;
                        this.transform.forward = Vector3.left;
                        return true;
                    }
                }
                else
                {
                    if (thisPosX - Attack_range <= attackPosX)
                    {
                        attackPosY = thisPosY;
                        enemyStatus = 2;
                        this.transform.forward = Vector3.left;
                        return true;
                    }
                }
                break;
            case 5:
                
                if(rangedAttackCheck(0)==-1)
                {
                    if (thisPosX < 3)
                        if (thisPosX - Attack_range <= -1)
                    {
                        attackPosY = thisPosY;
                        attackPosX = -1;
                        enemyStatus = 2;
                        this.transform.forward = Vector3.left;
                        return true;
                    }
                }
                else
                {
                    if (thisPosX - Attack_range <= attackPosX)
                    {
                        attackPosY = thisPosY;
                        enemyStatus = 2;
                        this.transform.forward = Vector3.left;
                        return true;
                    }
                }
                break;

        }

        if (thisPosX == 0)
        {
            enemyStatus = 2;
            attackPosX = -1;
            attackPosY = thisPosY;
            this.transform.forward = Vector3.left;
            return true;
        }
        if (thisPosX > 0)
        {
            if (TileStatus.tileStatus[thisPosX - 1, thisPosY, 1] == null)
            {
                goingPosX = thisPosX - 1;
                goingPosY = thisPosY;
                enemyStatus = 0;
                //goforward = true;
                // this.transform.forward = Vector3.left;
                switch (enemyType)
                {
                    case 1:
                        this.transform.forward = Vector3.left;
                        break;
                    case 2:
                        this.transform.localEulerAngles = new Vector3(0, 0, 0);
                        break;
                    case 3:
                        this.transform.localEulerAngles = new Vector3(0, 0, 0);
                        break;
                }

                TileStatus.tileStatus[goingPosX, goingPosY, 1] = this.gameObject;//목표지점에 자기의 정보 입력
                if (TileStatus.tileStatus[thisPosX, thisPosY, 1] == this.gameObject)//현제 위치 타일에 자기의 정보가 있으면 삭제
                    TileStatus.tileStatus[thisPosX, thisPosY, 1] = null;
            }
            else
            {
                if (TileStatus.tileStatus[thisPosX - 1, thisPosY, 1].tag == "TeamUnit")
                {
                    enemyStatus = 2;
                    attackPosX = thisPosX - 1;
                    attackPosY = thisPosY;
                    this.transform.LookAt(TileStatus.tileStatus[thisPosX - 1, thisPosY, 1].transform);
                }
                else if(thisPosX>=2)
                {
                    if(TileStatus.tileStatus[thisPosX - 2, thisPosY, 1]==null)
                    {
                        goingPosX = thisPosX - 2;
                        goingPosY = thisPosY;
                        enemyStatus = 0;
                        TileStatus.tileStatus[goingPosX, goingPosY, 1] = this.gameObject;//목표지점에 자기의 정보 입력
                        if (TileStatus.tileStatus[thisPosX, thisPosY, 1] == this.gameObject)//현제 위치 타일에 자기의 정보가 있으면 삭제
                            TileStatus.tileStatus[thisPosX, thisPosY, 1] = null;
                    }
                    else
                    {
                        enemyStatus = 1;
                        TileStatus.tileStatus[thisPosX, thisPosY, 1] = this.gameObject;
                    }
                }else
                {
                    enemyStatus = 1;
                    TileStatus.tileStatus[thisPosX, thisPosY, 1] = this.gameObject;
                }
            }
        }
        else
        {
            enemyStatus = 1;
            TileStatus.tileStatus[thisPosX, thisPosY, 1] = this.gameObject;
        }
        if (enemyStatus == 2)
        {
            TileStatus.tileStatus[goingPosX, goingPosY, 1] = null;
            TileStatus.tileStatus[thisPosX, thisPosY, 1] = this.gameObject;
        }
        return false;
    }
    //bool sidCheck()//양옆타일 체크 함수
    //{
    //    goUp = false;
    //    goDown = false;
    //    attackUp = false;
    //    attackDown = false;
    //    if (thisPosY > 0)
    //    {
    //        if (TileStatus.tileStatus[thisPosX, thisPosY - 1, 1] == null)
    //        {
    //            goDown = true;
    //        }else if (TileStatus.tileStatus[thisPosX, thisPosY-1, 1].tag == "TeamUnit")
    //        {
    //            attackDown = true;
    //        }
    //    }
    //    if (thisPosY < 4)
    //    {
    //        if (TileStatus.tileStatus[thisPosX, thisPosY + 1, 1] == null)
    //        {
    //            goUp = true;
    //        }else if (TileStatus.tileStatus[thisPosX, thisPosY+1, 1].tag == "TeamUnit")
    //        {
    //            attackUp = true;
    //        }
    //    }
    //    if(attackDown||attackUp)
    //    {
    //        if (attackDown && attackUp)
    //        {
    //            if ((int)(Random.Range(0, 2)) == 0)
    //            {
    //                attackDown = false;
    //            }
    //            else
    //            {
    //                attackUp = false;
    //            }
    //        }
    //        if (attackDown)
    //        {
    //            enemyStatus = 2;
    //            this.transform.forward=Vector3.back;
    //            attackPosX = thisPosX;
    //            attackPosY = thisPosY - 1;
    //            return true;
    //        }
    //        if (attackUp)
    //        {
    //            enemyStatus = 2;
    //            this.transform.forward=Vector3.forward;
    //            attackPosX = thisPosX;
    //            attackPosY = thisPosY + 1;
    //            return true;
    //        }
    //    }
    //    if (!goforward&&enemyStatus!=2)
    //    {
    //        if (goUp || goDown)
    //        {
    //            if (goUp && goDown)
    //            {
    //                if ((int)(Random.Range(0, 2)) == 0)
    //                {
    //                    goUp = false;
    //                }
    //                else
    //                {
    //                    goDown = false;
    //                }
    //            }
    //            if (goDown)
    //            {
    //                goingPosY = thisPosY - 1;
    //                goingPosX = thisPosX;
    //                enemyStatus = 0;
    //                this.transform.forward = Vector3.back;
    //                TileStatus.tileStatus[goingPosX, goingPosY, 1] = this.gameObject;
    //                if (TileStatus.tileStatus[thisPosX, thisPosY, 1] == this.gameObject)
    //                    TileStatus.tileStatus[thisPosX, thisPosY, 1] = null;
    //                return false;
    //            }
    //            else if (goUp)
    //            {
    //                goingPosY = thisPosY + 1;
    //                goingPosX = thisPosX;
    //                enemyStatus = 0;
    //                this.transform.forward = Vector3.forward;
    //                TileStatus.tileStatus[goingPosX, goingPosY, 1] = this.gameObject;
    //                if (TileStatus.tileStatus[thisPosX, thisPosY, 1] == this.gameObject)
    //                    TileStatus.tileStatus[thisPosX, thisPosY, 1] = null;
    //                return false;
    //            }
    //        }
    //        else
    //        {
    //            enemyStatus = 1;
    //            TileStatus.tileStatus[thisPosX, thisPosY, 1] = this.gameObject;
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        goUp = false;
    //        goDown = false;
    //    }
    //    return false;

    //}
    //앞타일 검사-빈공간이면 다음타일 검사-
    int rangedAttackCheck(int minRange)
    {
        if (thisPosX - minRange >= 0)
        {
            if (TileStatus.tileStatus[thisPosX - minRange, thisPosY, 1] != null)
                if (TileStatus.tileStatus[thisPosX - minRange, thisPosY, 1].tag != "TeamUnit")
                {
                    return rangedAttackCheck(minRange + 1);
                }
                else
                {
                    attackPosX = thisPosX - minRange;
                    attackPosY = thisPosY;
                    return minRange;
                }
            else
            {
                return rangedAttackCheck(minRange + 1);
            }
        }
        else
        {
            return -1;
        }
    }

    public bool getDamage(float damage)
    {
        Current_health -= damage;
        redColorTimer = 0.2f;
        if (Current_health <= 0)
            Death();
        return true;
    }
    void Death()
    {
        //if (enemyType == 99)
        //{
        //    if (enemyStatus == 0)
        //    {

        //    }
        //    else
        //    {
        //        enemyStatus = 1;
        //    }
        //    memore = enemyStatus;
        //    enemyStatus = 3;
        //    ani.SetInteger("EnemyStatus", 3);
        //    this.gameObject.GetComponent<BoxCollider>().enabled = false;
        //    GameObject Boss = GameObject.Find("WizardBoss(Clone)");
        //    this.GetComponent<Golem>().GolemCheak();

        //}else
        if (enemyType != 0)
        {
            if (TileStatus.tileStatus[thisPosX, thisPosY, 1] == this.gameObject)//현제 위치 타일에 자기의 정보가 있으면 삭제
                TileStatus.tileStatus[thisPosX, thisPosY, 1] = null;
            if (TileStatus.tileStatus[goingPosX, goingPosY, 1] == this.gameObject)//현제 위치 타일에 자기의 정보가 있으면 삭제
                TileStatus.tileStatus[goingPosX, goingPosY, 1] = null;
            if (enemyType == 98)
            {
                if(TileStatus.tileStatus[thisPosX, thisPosY, 0]!=null)
                {
                    Destroy(TileStatus.tileStatus[thisPosX, thisPosY, 0]);
                }
                TileStatus.tileStatus[thisPosX, thisPosY, 0] = this.gameObject;

                enemyStatus = 3;
                ani.SetInteger("EnemyStatus", 3);
                StartCoroutine(unDeadDeath());
            }
            else
            {
                Destroy(this.gameObject.GetComponent<Collider>());
                enemyStatus = 3;
                ani.SetInteger("EnemyStatus", 3);
                Destroy(this.gameObject, 1.7f);
            }
           
        }
        else if (enemyType == 0)
        {
            if (bossType == 0)
            {
                Destroy(this.gameObject.GetComponent<BoxCollider>());
                this.transform.GetChild(0).GetComponent<Boss>().Death();
                this.transform.GetChild(1).GetComponent<MagicCircle>().DestroyThis();
                Destroy(this.gameObject, 8);
            }
            else if (bossType == 1)
            {
                Destroy(this.gameObject.GetComponent<BoxCollider>());
                this.GetComponent<WizardBoss>().Death();
            }
            else if (bossType == 2)
            {
                Destroy(this.gameObject.GetComponent<BoxCollider>());
                this.GetComponent<NecromancerBoss>().Death();
            }
        }
    }

    IEnumerator unDeadDeath()
    {
        yield return new WaitForSeconds(1.7f);
        this.gameObject.SetActive(false);
    }

    public void forcedDeath(float time)
    {
        if (enemyType != 0)
        {
            enemyStatus = 3;
            ani.SetInteger("EnemyStatus", 3);
            Debug.Log(this.gameObject.name);
            Destroy(this.gameObject, time);
            Destroy(this.gameObject.GetComponent<BoxCollider>());
           
            if (TileStatus.tileStatus[thisPosX, thisPosY, 1] == this.gameObject)//현제 위치 타일에 자기의 정보가 있으면 삭제
                TileStatus.tileStatus[thisPosX, thisPosY, 1] = null;
            if (TileStatus.tileStatus[goingPosX, goingPosY, 1] == this.gameObject)//현제 위치 타일에 자기의 정보가 있으면 삭제
                TileStatus.tileStatus[goingPosX, goingPosY, 1] = null;
            
        }
    }
    public void revive()
    {
        ani.SetInteger("EnemyStatus", 6);
        enemyStatus = 7;
        StartCoroutine(Revive());
    }

   public void Stun(float stunTime)
    {
        ani.SetInteger("EnemyStatus", 1);
        enemyStatus = 8;
        StartCoroutine(stun(stunTime));
    }

    IEnumerator stun(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        if(enemyStatus == 8)
        {
            enemyStatus = 1;
            ani.SetInteger("EnemyStatus", 1);
        }
    }

    IEnumerator Revive()
    {
        ani.Play("revive");
        yield return new WaitForSeconds(3f);
        if (enemyStatus == 7)
        {
            this.GetComponent<BoxCollider>().enabled = true;
            enemyStatus = 1;
            ani.SetInteger("EnemyStatus", enemyStatus);
            Current_health = Max_Health;
            goingPosX = thisPosX;
            goingPosY = thisPosY;
        }
        yield return 0;
    }
    IEnumerator attack()
    {
        attacked = true;
        ani.SetInteger("EnemyStatus", 2);
        yield return new WaitForSeconds(0.2f);
        
        if (enemyType==4||enemyType==5)
        {
            Instantiate(ParticleArr.Particles[3], AttackParticlePos.transform.position, AttackParticlePos.transform.rotation, ParticleArr.Particles[0].transform);
        }
        yield return new WaitForSeconds(0.15f);
        if (enemyType == 2 || enemyType == 1)
        {
            if (attackPosX!= -1)
            {
                if (thisPosX - attackPosX == 1)
                    Instantiate(ParticleArr.Particles[4], AttackParticlePos.transform.position, AttackParticlePos.transform.rotation, ParticleArr.Particles[0].transform);
                else if (thisPosX - attackPosX == 2)
                    Instantiate(ParticleArr.Particles[4], AttackParticlePos2.transform.position, AttackParticlePos2.transform.rotation, ParticleArr.Particles[0].transform);
            }else
            {
                if (thisPosX - attackPosX == 1)
                    Instantiate(ParticleArr.Particles[7], AttackParticlePos.transform.position, AttackParticlePos.transform.rotation, ParticleArr.Particles[0].transform);
                else if (thisPosX - attackPosX == 2)
                    Instantiate(ParticleArr.Particles[7], AttackParticlePos2.transform.position, AttackParticlePos2.transform.rotation, ParticleArr.Particles[0].transform);
            }
        }else if (enemyType == 3)
        {
            if (attackPosX != -1)
            {
                if (thisPosX - attackPosX == 1)
                    Instantiate(ParticleArr.Particles[6], AttackParticlePos.transform.position, AttackParticlePos.transform.rotation, ParticleArr.Particles[0].transform);
            }else
            {
                if (thisPosX - attackPosX == 1)
                    Instantiate(ParticleArr.Particles[7], AttackParticlePos.transform.position, AttackParticlePos.transform.rotation, ParticleArr.Particles[0].transform);
            }
        }
        yield return new WaitForSeconds(0.1f);
         if (enemyType == 99)
        {
            if (attackPosX != -1)
            {
                if (thisPosX - attackPosX == 1)
                    Instantiate(ParticleArr.Particles[6], AttackParticlePos.transform.position, AttackParticlePos.transform.rotation, ParticleArr.Particles[0].transform);
            }
            else
            {
                if (thisPosX - attackPosX == 1)
                    Instantiate(ParticleArr.Particles[7], AttackParticlePos.transform.position, AttackParticlePos.transform.rotation, ParticleArr.Particles[0].transform);
            }
        }
        yield return new WaitForSeconds(0.05f);

        if (enemyStatus == 2)
        {
            
            if (attackPosX != -1)
            {
                if (TileStatus.tileStatus[attackPosX, attackPosY, 1])
                {
                    TileStatus.tileStatus[attackPosX, attackPosY, 1].gameObject.GetComponent<TeamUnit>().getDamage(Damage);
                    if (enemyType == 98)
                    {
                                Instantiate(ParticleArr.Particles[8], AttackParticlePos.transform.position, AttackParticlePos.transform.rotation, ParticleArr.Particles[0].transform);
                    }
                    
                }
            }
            else
            {
                if (door != null)
                {
                    door.Hit(Damage / 10);
                    if (enemyType == 98)
                    {
                            if (thisPosX - attackPosX == 1)
                                Instantiate(ParticleArr.Particles[7], AttackParticlePos.transform.position, AttackParticlePos.transform.rotation, ParticleArr.Particles[0].transform);
                    }
                   
                }
            }
        }else if(enemyStatus == 1)
        {
            ani.SetInteger("EnemyStatus", 1);
            enemyStatus = 1;
        }
        else if (enemyStatus == 0)
        {
            ani.SetInteger("EnemyStatus", 0);
            enemyStatus = 0;
        }
        yield return new WaitForSeconds(0.5f);
        if (enemyStatus == 2)
        {
            ani.SetInteger("EnemyStatus", 4);
            enemyStatus = 4;
        }
        yield return new WaitForSeconds(Attack_Speed);
        if (enemyStatus == 4)
        {
            
            enemyStatus = 1;
        }
        attacked = false;
        yield return 0;
    }
    }