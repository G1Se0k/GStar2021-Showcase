using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiratesBoss : MonoBehaviour
{
    [SerializeField]
    GameObject golem;
    GameObject enemyParent;
    [SerializeField]
    GameObject rifle;
    [SerializeField]
    GameObject pistol;
    [SerializeField]
    public GameObject bulletsParent;
    public GameObject door;
    int bossStatus = 0; ////0등장모션,1대기,2공격,3소환,4이동,5죽음,6스왑,7스킬
    [SerializeField]
    float attackDelay = 2;
    float attackDelayTime=2;
    Animator ani;
    public GameObject[] golems=new GameObject[5];
    public bool golemAlive=false;
    float weaponSwapTimer;
    int random;
    bool weaponRifle = false;
    int damage;

    int thisPosX;
    int thisPosY;

    Vector3 goingPos;
    bool goingPosIsUp = true;

    // Start is called before the first frame update
    void Start()
    {
        thisPosX = 10;
        thisPosY = 2;
        weaponSwapTimer = Random.Range(7f, 15f);
       enemyParent = GameObject.Find("enemyParent");
        ani = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossStatus==4)
        {
            
            if(goingPosIsUp)
            {
                if(goingPos.z-this.transform.position.z<0)
                {
                    this.transform.position = goingPos;
                }
                this.transform.position += (goingPos - this.transform.position).normalized * Time.deltaTime * 10;
            }
            else
            {
                if (goingPos.z - this.transform.position.z > 0)
                {
                    this.transform.position = goingPos;
                }
                this.transform.position += (goingPos - this.transform.position).normalized * Time.deltaTime * 10;
            }
        }

        if (weaponSwapTimer > 0)
        {
            weaponSwapTimer -= Time.deltaTime;
        }
        if (bossStatus != 5)
        {
            if (this.transform.position.y < 0 && bossStatus != 0)
            {
                this.transform.Translate(Vector3.up * -this.transform.position.y);
            }
        }
        if (ani == null)
        {
            ani = this.GetComponent<Animator>();
        }
        if (attackDelayTime >= 0)
        {
            attackDelayTime -= Time.deltaTime;
        }
        if(bossStatus==0)
        {
            this.transform.Translate(Vector3.up * Time.deltaTime*8);
            if (this.transform.position.y> 0.05f)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            }
            if (this.transform.position.y==0)
            {
                bossStatus = 1;
                ani.SetInteger("BossStatus", 1);
            }
        }
        if(bossStatus==1)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.transform.parent.transform.rotation, Time.deltaTime * 10f);
            if(attackDelayTime<0)
            {
                random = Random.Range(0, 3);
                if(weaponSwapTimer<0)
                {
                    random = 2;
                }
                if (random == 0)
                {
                    bossStatus = 2;
                    ani.SetInteger("BossStatus", 2);
                    StartCoroutine(attack());
                }
                else if (random == 1)
                {
                    bossStatus = 3;
                    ani.SetInteger("BossStatus", 3);
                    StartCoroutine(Summon());
                }
                else if (random == 2)
                {
                    bossStatus = 6;
                    ani.SetInteger("BossStatus", 6);
                    weaponSwapTimer = Random.Range(7f, 15f);
                    StartCoroutine(WeaponSwap());
                }else if(random==3)
                {
                    bossStatus = 7;
                    ani.SetInteger("BossStatus", 7);
                    if (weaponRifle)
                    {
                        StartCoroutine(PenetrateAttack());
                    }else
                    {
                        StartCoroutine(continuousAttack());
                    }
                }else if(random==4)
                {
                    //이동부분
                }    

            }
        }
    }

    void Summons()
    {
        int j = Random.Range(0, 5);
        GameObject Golem = Instantiate(golem, enemyParent.transform);
        TileStatus.tileStatus[11, j, 1] = Golem;
        golems[j] = Golem;
        Golem.GetComponent<Enemy>().door = door.GetComponent<Door>();
        Golem.GetComponent<Enemy>().thisPosX = 11;
        Golem.GetComponent<Enemy>().thisPosY = j;
        Golem.transform.position = new Vector3(-2.5f + 11 * 5, 0, 2.5f + j * 5);
        Instantiate(ParticleArr.Particles[1], new Vector3(ParticleArr.Particles[0].transform.position.x, ParticleArr.Particles[0].transform.position.y + 0.15f, -2.5f + ParticleArr.Particles[0].transform.position.z + (j * 5)), ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
        int i = Random.Range(0, 5);
        while(j==i)
        {
            i = Random.Range(0, 5);
        }
        j = i;
        Golem = Instantiate(golem, enemyParent.transform);
        TileStatus.tileStatus[11, j, 1] = Golem;
        golems[j] = Golem;
        Golem.GetComponent<Enemy>().door = door.GetComponent<Door>();
        Golem.GetComponent<Enemy>().thisPosX = 11;
        Golem.GetComponent<Enemy>().thisPosY = j;
        Instantiate(ParticleArr.Particles[1], new Vector3(ParticleArr.Particles[0].transform.position.x, ParticleArr.Particles[0].transform.position.y + 0.15f, -2.5f + ParticleArr.Particles[0].transform.position.z + (j * 5)), ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
        Golem.transform.position = new Vector3(-2.5f + 11 * 5, 0, 2.5f + j * 5);
    }

    public void Death()
    {

        ani.SetInteger("BossStatus", 5);
        bossStatus = 5;
        Destroy(this.gameObject, 3);
        for(int i=0;i<5;i++)
        {
            golems[i].GetComponent<Enemy>().forcedDeath(3);
        }
    }
    IEnumerator Summon()
    {
        yield return new WaitForSeconds(1);
        if (bossStatus == 3)
        {
            Summons();
        }
        yield return new WaitForSeconds(0.5f);
        if (bossStatus == 3)
        {
            ani.SetInteger("BossStatus", 1);
            bossStatus = 1;
            attackDelayTime = attackDelay;
        }
        yield return 0;
    }
    IEnumerator WeaponSwap()
    {
        yield return new WaitForSeconds(0.5f);
        if (bossStatus == 6)
        {
            if (weaponRifle)
            {
                weaponRifle = false;
                //화승총 엑티브 비활성화, 권총 활성화 코드 추가

            }
            else
            {
                weaponRifle = true;
                //화승총 엑티브 활성화, 권총 비활성화 코드 추가
            }

        }
        yield return new WaitForSeconds(0.5f);
        if (bossStatus == 6)
        {
            bossStatus = 1;
        }
            yield return 0;
    }
    IEnumerator attack()
    {
        yield return new WaitForSeconds(0.5f);
        if (bossStatus == 2)
        {
            while (true)
            {
                int range = 0;
                
                if ( thisPosX - range == -1)
                {
                    Door.DoorObj.GetComponent<Door>().Hit(damage / 10);
                    break;
                }
                else if (TileStatus.tileStatus[thisPosX - range, thisPosY, 1].tag == "TeamUnit")
                {
                    TileStatus.tileStatus[thisPosX - range, thisPosY, 1].GetComponent<TeamUnit>().getDamage(damage);
                    break;
                }

                range++;
            }
            attackDelayTime = attackDelay;
            bossStatus = 1;
            ani.SetInteger("BossStatus", 1);
        }
        yield return 0;
    }
     IEnumerator continuousAttack()
    {
        yield return new WaitForSeconds(0.5f);
        if (bossStatus == 2)
        {
            while (true)
            {
                int range = 0;
                
                if ( thisPosX - range == -1)
                {
                    Door.DoorObj.GetComponent<Door>().Hit(damage*0.1f*0.7f);
                    break;
                }
                else if (TileStatus.tileStatus[thisPosX - range, thisPosY, 1].tag == "TeamUnit")
                {
                    TileStatus.tileStatus[thisPosX - range, thisPosY, 1].GetComponent<TeamUnit>().getDamage(damage*0.7f);
                    break;
                }
                range++;
            }
            yield return new WaitForSeconds(0.5f);
            while (true)
            {
                int range = 0;

                if (thisPosX - range == -1)
                {
                    Door.DoorObj.GetComponent<Door>().Hit(damage * 0.1f * 0.7f);
                    break;
                }
                else if (TileStatus.tileStatus[thisPosX - range, thisPosY, 1].tag == "TeamUnit")
                {
                    TileStatus.tileStatus[thisPosX - range, thisPosY, 1].GetComponent<TeamUnit>().getDamage(damage * 0.7f);
                    break;
                }
                range++;
            }
            yield return new WaitForSeconds(0.5f);
            while (true)
            {
                int range = 0;

                if (thisPosX - range == -1)
                {
                    Door.DoorObj.GetComponent<Door>().Hit(damage * 0.1f * 0.7f);
                    break;
                }
                else if (TileStatus.tileStatus[thisPosX - range, thisPosY, 1].tag == "TeamUnit")
                {
                    TileStatus.tileStatus[thisPosX - range, thisPosY, 1].GetComponent<TeamUnit>().getDamage(damage * 0.7f);
                    break;
                }
                range++;
            }
            yield return new WaitForSeconds(0.5f);
            attackDelayTime = attackDelay;
            bossStatus = 1;
            ani.SetInteger("BossStatus", 1);
        }
        yield return 0;
    }
    IEnumerator PenetrateAttack()
    {
        yield return new WaitForSeconds(0.5f);
        if (bossStatus == 2)
        {
            while (true)
            {
                int range = 0;

                if (thisPosX - range <= -1)
                {
                    break;
                }
                else if (TileStatus.tileStatus[thisPosX - range, thisPosY, 1].tag == "TeamUnit")
                {
                    TileStatus.tileStatus[thisPosX - range, thisPosY, 1].GetComponent<TeamUnit>().getDamage(damage);
                }

                range++;
            }
            attackDelayTime = attackDelay;
            bossStatus = 1;
            ani.SetInteger("BossStatus", 1);
        }
        yield return 0;
    }
    IEnumerator blink()
    {
        yield return new WaitForSeconds(1f);
        if (bossStatus == 4)
        {
            TileStatus.tileStatus[10, thisPosY, 1] = null;
            int i = Random.Range(0, 5);
            if(i<thisPosY)
            {
                goingPosIsUp = false;
            }else
            {
                goingPosIsUp = true;
            }
            while (thisPosY == i)
            {
                i = Random.Range(0, 5);
                if (i < thisPosY)
                {
                    goingPosIsUp = false;
                }
                else
                {
                    goingPosIsUp = true;
                }
            }
            thisPosY = i;
            goingPos = new Vector3(this.transform.position.x, this.transform.position.y, 2.5f + thisPosY * 5);
            if (TileStatus.tileStatus[10, thisPosY, 1].tag == "TeamUnit")
            {
                TileStatus.tileStatus[10, thisPosY, 1].GetComponent<TeamUnit>().getDamage(999);
            }
            TileStatus.tileStatus[10, thisPosY, 1] = this.gameObject;
        }
        yield return new WaitForSeconds(0.5f);
        if (bossStatus == 4)
        {
            ani.SetInteger("BossStatus", 1);
            bossStatus = 1;
            attackDelayTime = attackDelay;
        }
        yield return 0;
    }
}
