using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusArr : MonoBehaviour
{
    static public float[] MoveSpeed=new float[20];
    static public float[] MaxHp = new float[20];
    static public float[] AttackSpeed = new float[20];
    static public float[] AttackRange = new float[20];
    static public float[] Damage = new float[20];

    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            MoveSpeed[i] = 0;
        }
        for (int i = 0; i < 20; i++)
        {
            MaxHp[i] = 0;
        }
        for (int i = 0; i < 20; i++)
        {
            AttackSpeed[i] = 0;
        }
        for (int i = 0; i < 20; i++)
        {
            AttackRange[i] = 0;
        }
        for (int i = 0; i < 20; i++)
        {
            Damage[i] = 0;
        }

        {//검 할버드 망치 권총 화승총 순서
            MoveSpeed[1] = 5;
            MoveSpeed[2] = 4;
            MoveSpeed[3] = 6;
            MoveSpeed[4] = 4;
            MoveSpeed[5] = 3;
        }
        {
            MaxHp[1] = 120;
            MaxHp[2] = 100;
            MaxHp[3] = 160;
            MaxHp[4] = 80;
            MaxHp[5] = 60;
        }
        {
            AttackSpeed[1] = 2;
            AttackSpeed[2] = 3;
            AttackSpeed[3] = 3.5f;
            AttackSpeed[4] = 4;
            AttackSpeed[5] = 5;
        }
        {
            AttackRange[1] = 1;
            AttackRange[2] = 2;
            AttackRange[3] = 1;
            AttackRange[4] = 3;
            AttackRange[5] = 5;
        }
        {
            Damage[1] = 75;
            Damage[2] = 85;
            Damage[3] = 200;
            Damage[4] = 75;
            Damage[5] = 140;
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
