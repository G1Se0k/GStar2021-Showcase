using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject[] Enemys;//0 보스골렘,1검,2창,3해머,4권총,5총,10소환사보스
    [SerializeField]
    Transform line1;
    [SerializeField]
    Transform enemyParent;
    [SerializeField]
    public int count_enemy1;
    [SerializeField]
    GameObject but;
    [SerializeField]
    Transform BossGolemCreatPos;
    [SerializeField]
    GameObject door;
    [SerializeField]
    GameObject bulletsParent;
    [SerializeField]
    int stage=1;
    [SerializeField]
    int wave = 3;
    [SerializeField]
    GameObject WinWindow;
    [SerializeField]
    GameObject wizardCreateMagicCircles;

    [SerializeField]
    GameObject SpawnLight;
    float createTimer=0;
    int line;
    int enemyType;
    bool gameStartOn=false;
    int createEnemyXNum=0;
    // Start is called before the first frame update

    void Start()
    {
        if (GameObject.Find("StageNum"))
        {
            stage = GameObject.Find("StageNum").GetComponent<StageController>().Stage;
            wave = 0;
            Destroy(GameObject.Find("StageNum"));
        }
    }

    // Update is called once per frame
    void Update()
    {
       
         createTimer += Time.deltaTime;
        if (gameStartOn)
        {


            if (wave != 3)
            {
                if (count_enemy1 > 0)
                {
                    if (createTimer > 1)
                    {

                        if (TileStatus.tileStatus[11, 0, 1] == null && TileStatus.tileStatus[11, 1, 1] == null && TileStatus.tileStatus[11, 2, 1] == null && TileStatus.tileStatus[11, 3, 1] == null && TileStatus.tileStatus[11, 4, 1] == null)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                if (EnemyCreatreArrangement.enemyCreatreArrangement[stage, wave, createEnemyXNum, j] != 999)
                                {
                                    Instantiate(ParticleArr.Particles[1], new Vector3(ParticleArr.Particles[0].transform.position.x, ParticleArr.Particles[0].transform.position.y + 0.15f, -2.5f + ParticleArr.Particles[0].transform.position.z + (j * 5)), ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
                                    GameObject enemy = Instantiate(Enemys[EnemyCreatreArrangement.enemyCreatreArrangement[stage, wave, createEnemyXNum, j]], enemyParent);
                                    enemy.transform.position = new Vector3(line1.position.x, line1.position.y, -2.5f + line1.position.z + (j * 5));
                                    TileStatus.tileStatus[11, j, 1] = enemy;
                                    enemy.GetComponent<Enemy>().thisPosX = 11;
                                    enemy.GetComponent<Enemy>().thisPosY = j;
                                    enemy.GetComponent<Enemy>().door = door.GetComponent<Door>();
                                    count_enemy1--;
                                }
                            }
                            createEnemyXNum += 1;
                            createTimer = 0;
                        }
                    }

                }
                else if (enemyParent.childCount == 0)
                {
                    createEnemyXNum = 0;
                    gameStartOn = false;
                    but.SetActive(true);
                }
            }
            if (wave == 3)
            {
                if (count_enemy1 > 0)
                {
                    if (stage == 2)
                    {
                        GameObject Boss = Instantiate(Enemys[11], enemyParent);
                        GameObject magic = Instantiate(wizardCreateMagicCircles);
                        magic.transform.position = new Vector3(line1.position.x - 5, line1.position.y + 0.15f, -2.5f + line1.position.z + (2 * 5));

                        Boss.transform.position = new Vector3(line1.position.x - 5, line1.position.y - 13, -2.5f + line1.position.z + (2 * 5));
                        Boss.GetComponent<NecromancerBoss>().door = door;
                        Boss.GetComponent<NecromancerBoss>().bulletsParent = bulletsParent;
                        Boss.GetComponent<NecromancerBoss>().tile = this.GetComponent<TileMaker>();
                        Boss.GetComponent<Enemy>().thisPosX = 10;
                        Boss.GetComponent<NecromancerBoss>().gamemanager = this.gameObject;
                        if (TileStatus.tileStatus[10, 2, 1] != null)
                        {
                            Boss.GetComponent<Enemy>().thisPosY = 2; if (TileStatus.tileStatus[10, 2, 1].tag == "TeamUnit")
                            {
                                TileStatus.tileStatus[10, 2, 1].GetComponent<TeamUnit>().getDamage(10000);//보스 나올 위치에 있는 유닛 즉사
                            }
                        }
                        TileStatus.tileStatus[10, 2, 1] = Boss;

                        count_enemy1--;

                    }
                    else if (stage == 1)
                    {

                        GameObject Boss = Instantiate(Enemys[10], enemyParent);
                        GameObject magic= Instantiate(wizardCreateMagicCircles);
                        magic.transform.position = new Vector3(line1.position.x - 5, line1.position.y+0.15f, -2.5f + line1.position.z + (2 * 5));

                        Boss.transform.position = new Vector3(line1.position.x - 5, line1.position.y - 13, -2.5f + line1.position.z + (2 * 5));
                        Boss.GetComponent<WizardBoss>().door = door;
                        Boss.GetComponent<WizardBoss>().bulletsParent = bulletsParent;
                        Boss.GetComponent<Enemy>().thisPosX = 10;
                        if (TileStatus.tileStatus[10, 2, 1] != null)
                        {
                            Boss.GetComponent<Enemy>().thisPosY = 2; if (TileStatus.tileStatus[10, 2, 1].tag == "TeamUnit")
                            {
                                TileStatus.tileStatus[10, 2, 1].GetComponent<TeamUnit>().getDamage(10000);//보스 나올 위치에 있는 유닛 즉사
                            }
                        }
                        TileStatus.tileStatus[10, 2, 1] = Boss;

                        count_enemy1--;

                    }
                }
                else if (enemyParent.childCount == 0)
                {
                    gameStartOn = false;
                    but.SetActive(true);
                    wave = 0;
                    WinWindow.SetActive(true);
                }
            }
                    
            
            
        }
}
    public void gameStart()
    {
        gameStartOn = true;
        wave++;
        if (stage==1)
        {
            if (wave == 0)
            {

                count_enemy1 = 1;
            }
            if (wave==1)
            {

                count_enemy1 = 10;
            }
            if (wave == 2)
            {

                count_enemy1 = 15;
            }

        }
        if (stage == 2)
        {
            
            if (wave == 1)
            {

                count_enemy1 = 15;
            }
            if (wave == 2)
            {

                count_enemy1 = 17;
            }

        }
        if (stage == 3)
        {
            
            if (wave == 1)
            {

                count_enemy1 = 17;
            }
            if (wave == 2)
            {

                count_enemy1 = 20;
            }

        }
        if (wave==3)
        {
            count_enemy1 = 1;
        }
        
    }
    //추가 내용 - 기석
    public void NextStage()
    {
        wave = 0;
        if(stage < 3)
        {
            stage += 1;
        }
    }
}
