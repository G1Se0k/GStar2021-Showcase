using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerBoss : MonoBehaviour
{
    [SerializeField]
    public GameObject gamemanager;
    [SerializeField]
    GameObject enemyParent;
    [SerializeField]
    GameObject fireBoll;
    [SerializeField]
    GameObject fireBollPos;
    [SerializeField]
    GameObject undead;
    [SerializeField]
    public GameObject bulletsParent;
    public GameObject door;
    int bossStatus = 0; ////0등장모션,1대기,2공격,3소환,4이동,5죽음,6부활스킬,7공격스킬2
    [SerializeField]
    float attackDelay = 2;
    [SerializeField]
    public float summonTimer = 0;
    float attackDelayTime = 2;
    Animator ani;
    [SerializeField]
    public GameObject[] unDeads = new GameObject[10];
    public GameObject[] unDeads2 = new GameObject[10];
    int random;
    public TileMaker tile;
    int thisPosX;
    int thisPosY;
    Vector3 correction;
    Vector3 correction2;

    // Start is called before the first frame update
    void Start()
    {
        unDeads = new GameObject[10];
        unDeads2 = new GameObject[10];
        correction = new Vector3(-2.5f, -2f ,2.5f);
        correction2 = new Vector3(-2.5f, 3.5f, 2.5f);
        thisPosX = 10;
        thisPosY = 2;
        enemyParent = GameObject.Find("enemyParent");
        ani = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (summonTimer >= 0)
        {
            summonTimer -= Time.deltaTime;
        }
        if (attackDelayTime >= 0)
        {
            attackDelayTime -= Time.deltaTime;
        }
        if (bossStatus == 0)
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * 8);
            if (this.transform.position.y > 0.05f)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            }
            if (this.transform.position.y == 0)
            {
                bossStatus = 1;
                ani.SetInteger("BossStatus", 1);
            }
        }
        if (bossStatus == 1)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.transform.parent.transform.rotation, Time.deltaTime * 10f);
            if (attackDelayTime < 0)
            {
                random = Random.Range(0, 4);
                if (random == 0)
                {
                    for (int i = 0; i < this.transform.parent.transform.childCount; i++)
                    {

                        if (!this.transform.parent.GetChild(i).gameObject.activeSelf)
                        {
                            break;
                        }
                        if(i ==this.transform.parent.transform.childCount-1)
                        {
                            bossStatus = 3;
                            ani.SetInteger("BossStatus", 3);
                            StartCoroutine(Summon());
                        }
                    }
                    if (bossStatus != 3)
                    {
                        bossStatus = 6;
                        ani.SetInteger("BossStatus", 6);
                        StartCoroutine(Revive());
                    }
                }
                else if (random == 1)
                {

                    bossStatus = 3;
                    ani.SetInteger("BossStatus", 3);
                    StartCoroutine(Summon());
                }
                else if (random == 2)
                {
                    bossStatus = 4;
                    ani.SetInteger("BossStatus", 4);
                    StartCoroutine(blink());
                }
                else if (random == 3)
                {
                    int count = 0;
                    bool undeadaLive=false;
                    for(int i=0; i<10;i++)
                    {
                        if (unDeads[i] != null)
                        {
                            count++;
                            undeadaLive = true;
                        }
                    }
                    if (undeadaLive)
                    {
                        if (count <= 4)
                        {
                            bossStatus = 7;
                            ani.SetInteger("BossStatus", 7);
                            StartCoroutine(UndeadExplosion());
                        }else
                        {
                            bossStatus = 3;
                            ani.SetInteger("BossStatus", 3);
                            StartCoroutine(Summon());
                        }
                    }else
                    {
                        bossStatus = 3;
                        ani.SetInteger("BossStatus", 3);
                        StartCoroutine(Summon());
                    }
                }
                else if (random == 4)
                {
                    bossStatus = 4;
                    ani.SetInteger("BossStatus", 4);
                    StartCoroutine(blink());
                }

            }
        }
    }

    void Summons()
    {
        if(unDeads[9] !=null)
        if (unDeads[9].activeSelf == false)
            unDeads[9] = null;

            if (unDeads[9] == null)
        {
            
                int j = Random.Range(0, 5);
            GameObject Golem = Instantiate(undead, enemyParent.transform);
            TileStatus.tileStatus[11, j, 1] = Golem;
            for (int a = 0; a < 10; a++)
            {
                if (unDeads[a] == null)
                {
                    unDeads[a] = Golem;
                    break;
                }else if(unDeads[a].activeSelf==false)
                    {
                    unDeads[a] = Golem;
                    break;
                }

            }
            Golem.GetComponent<Enemy>().door = door.GetComponent<Door>();
            Golem.GetComponent<Enemy>().thisPosX = 11;
            Golem.GetComponent<Enemy>().thisPosY = j;
            Golem.transform.position = new Vector3(-2.5f + 11 * 5, 0, 2.5f + j * 5);
            Instantiate(ParticleArr.Particles[5], new Vector3(ParticleArr.Particles[0].transform.position.x, ParticleArr.Particles[0].transform.position.y + 0.15f, -2.5f + ParticleArr.Particles[0].transform.position.z + (j * 5)), ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
           
            Golem.GetComponent<Enemy>().enemyStatus = 6;
            if (unDeads[9] == null)
            {
                int i = Random.Range(0, 5);
                while (j == i)
                {
                    i = Random.Range(0, 5);
                }
                j = i;
                Golem = Instantiate(undead, enemyParent.transform);
                TileStatus.tileStatus[11, j, 1] = Golem;
                for (int a = 0; a < 10; a++)
                {
                    if (unDeads[a] == null)
                    {
                        unDeads[a] = Golem;
                        break;
                    }
                    else if (unDeads[a].activeSelf == false)
                    {
                        unDeads[a] = Golem;
                        break;
                    }

                }
                Golem.GetComponent<Enemy>().door = door.GetComponent<Door>();
                Golem.GetComponent<Enemy>().thisPosX = 11;
                Golem.GetComponent<Enemy>().thisPosY = j;
                Instantiate(ParticleArr.Particles[5], new Vector3(ParticleArr.Particles[0].transform.position.x, ParticleArr.Particles[0].transform.position.y + 0.15f, -2.5f + ParticleArr.Particles[0].transform.position.z + (j * 5)), ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
                
                Golem.transform.position = new Vector3(-2.5f + 11 * 5, 0, 2.5f + j * 5);
                Golem.GetComponent<Enemy>().enemyStatus = 6;
            }
        }
    }
    public void Blink()
    {
        StartCoroutine(blink());
    }

    public void Death()
    {

        ani.SetInteger("BossStatus", 5);
        bossStatus = 5;
        Destroy(this.gameObject, 3);
        for(int i =0; i<this.transform.parent.transform.childCount;i++)
        {
            
            if (!this.transform.parent.GetChild(i).gameObject.activeSelf)
            {
                
                Destroy(this.transform.parent.GetChild(i).gameObject);
            }else
            {
                this.transform.parent.GetChild(i).GetComponent<Enemy>().forcedDeath(1.7f);
            }
        }
    }

    void revive()
    {
        for(int i=0;i<12;i++)
        {
            for(int j=0;j<5;j++)
            {
                if(TileStatus.tileStatus[i,j,0]!=null)
                {
                    Destroy(TileStatus.tileStatus[i, j, 0]);
                    if (TileStatus.tileStatus[i, j, 1] == null)
                    {
                        if (unDeads[9] == null)
                        {
                            GameObject _undead = Instantiate(undead, tile.Tiles[i, j].transform.position + correction2, enemyParent.transform.rotation, enemyParent.transform);

                            _undead.GetComponent<Enemy>().thisPosX = i;
                            _undead.GetComponent<Enemy>().thisPosY = j;
                            _undead.GetComponent<Enemy>().enemyStatus = 6;
                            _undead.GetComponent<Enemy>().goingPosX = i;
                            _undead.GetComponent<Enemy>().goingPosY = j;
                            _undead.GetComponent<Enemy>().door = door.GetComponent<Door>();
                            for (int a = 0; a < 10; a++)
                            {
                                if (unDeads[a] == null)
                                {
                                    unDeads[a] = _undead;
                                    break;
                                }
                                else if (unDeads[a].activeSelf == false)
                                {
                                    unDeads[a] = _undead;
                                    break;
                                }

                            }
                            TileStatus.tileStatus[i, j, 1] = _undead;
                            Instantiate(ParticleArr.Particles[5], tile.Tiles[i, j].transform.position + correction2, ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
                            Instantiate(ParticleArr.Particles[5], tile.Tiles[i, j].transform.position + correction2, ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
                            Instantiate(ParticleArr.Particles[5], tile.Tiles[i, j].transform.position + correction2, ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
                        }
                    }
                }
            }
        }
    }

    void undeadStun()
    {
        for(int i=0;i<10;i++)
        {
            if (unDeads[i] != null)
            {
                if(unDeads[i].activeSelf)
                    if(unDeads[i].GetComponent<Enemy>().enemyStatus !=3)
                unDeads[i].GetComponent<Enemy>().Stun(2);

            }
            unDeads2[i] = unDeads[i];
        }
        
    }
    void undeadExplosion()
    {
        for (int i = 0; i < 10; i++)
        {
            if (unDeads2[i] != null)
            {
                if (unDeads2[i].activeSelf == true)
                {
                    int x = unDeads[i].GetComponent<Enemy>().thisPosX;
                    if (unDeads2[i].transform.position.x - (-2.5f + x * 5) < -2.5)
                    {
                        x -= 1;
                    }
                    Instantiate(ParticleArr.Particles[2], tile.Tiles[x, unDeads[i].GetComponent<Enemy>().thisPosY].transform.position + correction2, ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
                }
            }
        }
    }
    IEnumerator UndeadExplosion()
    {
        undeadStun();
        yield return new WaitForSeconds(1.4f);
        if (bossStatus == 7)
        {
            undeadExplosion();
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 10; i++)
        {
            if (unDeads[i] != null)
            {
                if (unDeads[i].activeSelf == true)
                {
                    unDeads2[i].GetComponent<Enemy>().forcedDeath(1.7f);
                    
                    int x = unDeads2[i].GetComponent<Enemy>().thisPosX;
                    if(unDeads2[i].transform.position.x-(-2.5f + x*5)<-2.5)
                    {
                        x -= 1;
                    }
                    int y = unDeads2[i].GetComponent<Enemy>().thisPosY;
                    for (int k = 0; k< 3; k++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (x - 1 + k ==-1)
                            {
                                if(j==0)
                                {
                                    Door.DoorObj.GetComponent<Door>().Hit(10);
                                }
                            }
                               if (x - 1 + k < 12 && x - 1 + k > -1)
                                if (y - 1 + j < 5 && y - 1 + j > -1)
                                    if (TileStatus.tileStatus[x - 1 + k, y - 1 + j, 1] != null)
                                        if (TileStatus.tileStatus[x - 1 + k, y - 1 + j, 1].tag == "TeamUnit")
                                        {
                                            TileStatus.tileStatus[x - 1 + k, y - 1 + j, 1].GetComponent<TeamUnit>().getDamage(100);
                                            //StartCoroutine(TileGlow(x - 1 + k, y - 1 + j));
                                        }
                        }
                    }

                }
            }
        }

        
            yield return new WaitForSeconds(0.5f);
        if (bossStatus == 7)
        {
            ani.SetInteger("BossStatus", 1);
            bossStatus = 1;
            attackDelayTime = attackDelay;

        }
        yield return 0;
    }

    IEnumerator TileGlow(int x, int y)
    {
        gamemanager.GetComponent<TileMaker>().Tiles[x, y].GetComponent<Tile>().Glow2(Color.red);
        yield return new WaitForSeconds(0.5f);
        gamemanager.GetComponent<TileMaker>().Tiles[x, y].GetComponent<Tile>().ColorUse=false;
        yield return 0;
    }


    IEnumerator Revive()
    {
        yield return new WaitForSeconds(0.5f);
        if (bossStatus == 6)
        {
            revive();
        }
        yield return new WaitForSeconds(1.5f);
        if (bossStatus == 6)
        {
            ani.SetInteger("BossStatus", 1);
            bossStatus = 1;
            attackDelayTime = attackDelay;
        }
        yield return 0;

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
    IEnumerator blink()
    {
        yield return new WaitForSeconds(1f);
        if (bossStatus == 4)
        {
            Instantiate(ParticleArr.Particles[1], new Vector3(this.transform.position.x, ParticleArr.Particles[0].transform.position.y + 0.15f, this.transform.position.z), ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
           
            TileStatus.tileStatus[10, thisPosY, 1] = null;
            int i = Random.Range(0, 5);
            while (thisPosY == i)
            {
                i = Random.Range(0, 5);
            }
            thisPosY = i;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 2.5f + thisPosY * 5);
            Instantiate(ParticleArr.Particles[1], new Vector3(this.transform.position.x, ParticleArr.Particles[0].transform.position.y + 0.15f, this.transform.position.z), ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
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
    IEnumerator attack()
    {
        yield return new WaitForSeconds(0.5f);
        if (bossStatus == 2)
        {
            GameObject fire = Instantiate(fireBoll, bulletsParent.transform);
            fire.transform.position = fireBollPos.transform.position;
            fire.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            attackDelayTime = attackDelay;
            bossStatus = 1;
            ani.SetInteger("BossStatus", 1);
        }
        yield return 0;
    }

}