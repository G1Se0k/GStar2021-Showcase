using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    Transform forwardRot;
    [SerializeField]
    Transform downRot;
    [SerializeField]
    Transform upRot;
    [SerializeField]
    Transform pos;
    [SerializeField]
    Transform rockPos;
    [SerializeField]
    public GameObject doorPos;
    [SerializeField]
    GameObject rockPrefab;
    GameObject rock = null;
    [SerializeField]
    GameObject parent;

    private GameObject magicZone;
    private float magicZoneScale;

    int bossStatus;//0등장모션,1대기,2문공격,3위공격,4밑공격,5죽음
    float timer;
    Animator ani;
    float upDownAttackTime = 1f;
    float upDownAttackCoolDown = 5f;
    float rockAttackTime = 1f;
    float rockAttackCoolDown = 5f;
    float coolTime = 0;
    bool attackEnd;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShakeCamera());
        ani = this.GetComponent<Animator>();
        timer = 5;
        bossStatus = 0;
        for (int i = 0; i < 5; i++)
        {
            for(int j=9;j<12;j++)
            TileStatus.tileStatus[j, i, 1] = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ani==null)
        {
            ani = this.GetComponent<Animator>();
        }

        if (bossStatus != 2)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 12.5f);
        }
        timer -= Time.deltaTime;
        coolTime -= Time.deltaTime;
        if (timer < 3.8 && timer > 3.4f && bossStatus == 0)
        {
            this.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 43);
        }
        if (bossStatus == 0 && timer > -0.3f && timer <= 3.4f)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, pos.position, Time.deltaTime*1);
        }
        if (bossStatus == 0 && timer < -0.3f)
        {
            bossStatus = 1;
            ani.SetInteger("BossStatus", 1);
            parent.GetComponent<BoxCollider>().enabled = true;
        }
        if(bossStatus==3||bossStatus==4)//상하 공격 실행
        {
            if(timer < 0.1f&&!attackEnd)
            {
                if (bossStatus == 3)
                {
                    attack(3);
                    attackEnd = true;
                }
                else
                {
                    attack(4);
                    attackEnd = true;
                }
                
            }
        }
        if (bossStatus == 3 || bossStatus == 4 || bossStatus == 2)//공격 후 딜레이(대기시간)
        {
            if (timer < 0)
            {
                bossStatus = 1;
                ani.SetInteger("BossStatus", 1);
            }
        }

        if (bossStatus == 1)//공격 가능해지면 공격 검사
        {
            if (coolTime < 0)
            {
                attackCheck();
                attackEnd = false;
            }
        }

        if (bossStatus == 3)
        {
            ani.SetInteger("BossStatus", 3);

        } else if (bossStatus == 4)
        {
            ani.SetInteger("BossStatus", 4);
        }
        else if (bossStatus == 2)
        {
            ani.SetInteger("BossStatus", 2);

        }
        if (bossStatus == 2)
        {
            if(rock==null)
            {
                
                rock= Instantiate(rockPrefab);
                rock.GetComponent<BossRock>().Boss = rockPos.gameObject;
                rock.GetComponent<BossRock>().handPos = rockPos;
                rock.GetComponent<BossRock>().targetPos = doorPos ;

            }
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, forwardRot.rotation, 3f* Time.deltaTime);
            //this.transform.position = Vector3.Lerp(this.transform.position, pos.position, 100f*Time.deltaTime);
            this.transform.position = pos.position;
        }
        if (bossStatus == 1)
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, forwardRot.rotation, 3f * Time.deltaTime);
                this.transform.position = Vector3.Lerp(this.transform.position, pos.position, 3f*Time.deltaTime);
            this.transform.position = pos.position;
        }
        if (bossStatus == 3)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, upRot.rotation, 0.01f);
            this.transform.position = pos.position;
        }
        if (bossStatus == 4)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, downRot.rotation, 0.01f);
            this.transform.position = pos.position;
        }
        if(bossStatus==5)
        {
            if(timer<0f)
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(pos.position.x-7, pos.position.y-10, pos.position.z), 1f*Time.deltaTime);
            else
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(pos.position.x, pos.position.y, pos.position.z), 2f * Time.deltaTime);
        }
        


    }

    void attackCheck()
    {
        if ((int)Random.Range(0, 2) == 0)
        {
            if (TeamUnitCheck(0))
            {
                bossStatus = 3;
                timer = upDownAttackTime;
                coolTime = upDownAttackCoolDown;
                return;
            }
            else if (TeamUnitCheck(1))
            {
                bossStatus = 4;
                timer = upDownAttackTime;
                coolTime = upDownAttackCoolDown;
                return;
            }

        }
        else
        {
            if (TeamUnitCheck(1))
            {
                bossStatus = 4;
                timer = upDownAttackTime;
                coolTime = upDownAttackCoolDown;
                return;
            }
            else if (TeamUnitCheck(0))
            {
                bossStatus = 3;
                timer = upDownAttackTime;
                coolTime = upDownAttackCoolDown;
                return;
            }
        }
        bossStatus = 2;
        timer = rockAttackTime;
        coolTime = rockAttackCoolDown;
    }
    bool TeamUnitCheck(int num)//0=위쪽검사,1=밑쪽검사
    {
        switch (num)
        {
            case 0:
                for (int i = 4; i < 8; i++)
                {
                    for (int j = 3; j < 5; j++)
                    {
                        if (TileStatus.tileStatus[i, j, 1] != null)
                        {
                            if (TileStatus.tileStatus[i, j, 1].tag == "TeamUnit")
                            {
                                bossStatus = 3;
                                return true;
                            }
                        }
                    }
                }
                break;
            case 1:
                for (int i = 4; i < 8; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (TileStatus.tileStatus[i, j, 1] != null)
                        {
                            if (TileStatus.tileStatus[i, j, 1].tag == "TeamUnit")
                            {
                                bossStatus = 4;
                                return true;
                            }
                        }
                    }
                }
                break;
        }
        return false;
    }

    void attack(int num)
    {
        switch(num)
        {
            case 3:
                for (int i = 4; i < 8; i++)
                {
                    for (int j = 3; j < 5; j++)
                    {
                        if (TileStatus.tileStatus[i, j, 1] != null)
                        {
                            if (TileStatus.tileStatus[i, j, 1].tag == "TeamUnit")
                            {
                                TileStatus.tileStatus[i, j, 1].GetComponent<TeamUnit>().getDamage(50);
                               Camera.main.transform.GetComponent<Cscripts>().Shake2();
                            }
                        }
                    }
                }
                break;
            case 4:
                Debug.Log(4);
                for (int i = 4; i < 8; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (TileStatus.tileStatus[i, j, 1] != null)
                        {
                            if (TileStatus.tileStatus[i, j, 1].tag == "TeamUnit")
                            {
                                TileStatus.tileStatus[i, j, 1].GetComponent<TeamUnit>().getDamage(50);
                                Camera.main.transform.GetComponent<Cscripts>().Shake2();
                            }
                        }
                    }
                }
                break;
        }
    }









    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tile")
        {
            switch (bossStatus)
            {
                case 0:
                    break;
                case 1:
                    Debug.Log(1);
                    break;
                case 2:
                    Debug.Log(2);
                    break;
                case 3:
                    Debug.Log(3);
                    
                    break;
                case 4:
                   
                    break;
            }
            Debug.Log(5);
        }
    }
    public void Death()
    {
        bossStatus = 5;
        ani.SetInteger("BossStatus", 5);
        Destroy(this.gameObject,6);
        timer = 2;
        Destroy(rockPos.gameObject);
        StartCoroutine(ShakeCameraDeath());
        
    }

    private Vector3 magiczoneAngle = new Vector3(90,360,0);

    //IEnumerator initMagicZone()
    //{
    //    while (magicZoneScale < 2.9999f)
    //    {
    //        magicZoneScale = Mathf.Lerp(magicZoneScale, 3, 0.1f);
    //        magicZone.transform.localScale = new Vector3(magicZoneScale, magicZoneScale, 0);
    //        magicZone.transform.localEulerAngles = Vector3.Lerp(magicZone.transform.localEulerAngles, magiczoneAngle, 0.1f);
    //        yield return new WaitForSeconds(0.01f);
    //    }
    //    magicZone.transform.localScale = new Vector3(3, 3, 0);
    //    yield return 0;
    //}

    IEnumerator ShakeCamera()
    {
        yield return new WaitForSeconds(1.5f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();
        yield return new WaitForSeconds(1.5f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();
        yield return new WaitForSeconds(0.25f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();
        yield return new WaitForSeconds(0.25f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();
        yield return new WaitForSeconds(0.25f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();
        yield return new WaitForSeconds(0.25f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();
        yield return new WaitForSeconds(0.25f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();
        yield return new WaitForSeconds(0.25f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();
        yield return 0;
    }
    IEnumerator ShakeCameraDeath()
    {
        yield return new WaitForSeconds(2.5f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();
        yield return new WaitForSeconds(0.25f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();
        yield return new WaitForSeconds(0.25f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();

        yield return new WaitForSeconds(0.25f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();

        yield return new WaitForSeconds(0.25f);
        Camera.main.transform.GetComponent<Cscripts>().Shake2();
        yield return 0;
    }

}




