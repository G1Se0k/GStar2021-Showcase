using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreatreArrangement : MonoBehaviour
{
    static public int[,,,] enemyCreatreArrangement = new int[4, 4, 12, 5];//에너미 생성 배치도 [스테이지,웨이브,x좌표(시간,나오는 타이밍),y좌표]저장값 에너미 타입,999는 빈공간
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 11; k++)
                {
                    for (int l = 0; l < 5; l++)
                    {
                        enemyCreatreArrangement[i, j, k, l] = 999;
                    }
                }
            }
        }
        {//테스트용 하나짜리
            enemyCreatreArrangement[1, 0, 0, 2] = 1;
        }
        {//1스테이지 1웨이브
            enemyCreatreArrangement[1, 1, 0, 1] = 1;
            enemyCreatreArrangement[1, 1, 0, 3] = 1;

            enemyCreatreArrangement[1, 1, 3, 0] = 1;
            enemyCreatreArrangement[1, 1, 3, 2] = 1;
            enemyCreatreArrangement[1, 1, 3, 4] = 1;

            enemyCreatreArrangement[1, 1, 6, 0] = 1;
            enemyCreatreArrangement[1, 1, 9, 1] = 1;
            enemyCreatreArrangement[1, 1, 6, 2] = 1;
            enemyCreatreArrangement[1, 1, 9, 3] = 1;
            enemyCreatreArrangement[1, 1, 6, 4] = 1;
        }
        {//1스테이지 2웨이브

            enemyCreatreArrangement[1, 2, 0, 1] = 1;
            enemyCreatreArrangement[1, 2, 0, 3] = 1;

            enemyCreatreArrangement[1, 2, 1, 0] = 2;
            enemyCreatreArrangement[1, 2, 1, 2] = 2;
            enemyCreatreArrangement[1, 2, 1, 4] = 2;

            enemyCreatreArrangement[1, 2, 4, 1] = 1;
            enemyCreatreArrangement[1, 2, 4, 3] = 1;

            enemyCreatreArrangement[1, 2, 5, 1] = 2;
            enemyCreatreArrangement[1, 2, 5, 3] = 2;


            enemyCreatreArrangement[1, 2, 7, 0] = 2;
            enemyCreatreArrangement[1, 2, 8, 0] = 2;
            enemyCreatreArrangement[1, 2, 9, 0] = 2;

            enemyCreatreArrangement[1, 2, 7, 4] = 2;
            enemyCreatreArrangement[1, 2, 8, 4] = 2;
            enemyCreatreArrangement[1, 2, 9, 4] = 2;
        }
        {//2스테이지 1웨이브

            enemyCreatreArrangement[2, 1, 0, 1] = 3;
            enemyCreatreArrangement[2, 1, 0, 2] = 3;
            enemyCreatreArrangement[2, 1, 0, 3] = 3;

            enemyCreatreArrangement[2, 1, 1, 0] = 3;
            enemyCreatreArrangement[2, 1, 1, 4] = 3;

            enemyCreatreArrangement[2, 1, 4, 1] = 4;
            enemyCreatreArrangement[2, 1, 4, 3] = 4;


            enemyCreatreArrangement[2, 1, 5, 1] = 4;
            enemyCreatreArrangement[2, 1, 5, 2] = 3;
            enemyCreatreArrangement[2, 1, 5, 3] = 4;

            enemyCreatreArrangement[2, 1, 8, 0] = 3;
            enemyCreatreArrangement[2, 1, 8, 1] = 3;
            enemyCreatreArrangement[2, 1, 8, 2] = 3;
            enemyCreatreArrangement[2, 1, 8, 3] = 3;
            enemyCreatreArrangement[2, 1, 8, 4] = 3;



        }
        {//2스테이지 2웨이브

            enemyCreatreArrangement[2, 2, 0, 1] = 3;

            enemyCreatreArrangement[2, 2, 1, 3] = 3;

            enemyCreatreArrangement[2, 2, 3, 1] = 3;
            enemyCreatreArrangement[2, 2, 3, 2] = 3;
            enemyCreatreArrangement[2, 2, 3, 3] = 3;


            enemyCreatreArrangement[2, 2, 5, 0] = 4;
            enemyCreatreArrangement[2, 2, 5, 1] = 4;
            enemyCreatreArrangement[2, 2, 5, 2] = 4;
            enemyCreatreArrangement[2, 2, 5, 3] = 4;
            enemyCreatreArrangement[2, 2, 5, 4] = 4;

            enemyCreatreArrangement[2, 2, 8, 1] = 5;
            enemyCreatreArrangement[2, 2, 8, 3] = 5;

            enemyCreatreArrangement[2, 2, 11, 0] = 3;
            enemyCreatreArrangement[2, 2, 11, 1] = 5;
            enemyCreatreArrangement[2, 2, 11, 2] = 3;
            enemyCreatreArrangement[2, 2, 11, 3] = 5;
            enemyCreatreArrangement[2, 2, 11, 4] = 3;


        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
