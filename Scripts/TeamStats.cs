using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamStats : MonoBehaviour
{
    private int upgradeCoin;
    private float[] damage;
    private float[] hP;
    private int[] level;
    private int[] cost;
    private int[] range;

    public float[] Damage { get { return damage; } }
    public float[] HP { get { return hP; } }
    public int[] Cost { get { return cost; } }
    public int[] Range { get { return range; } }

    void Start()
    {
        damage = new float[5] { 10, 10, 100, 0, 50 };
        hP = new float[5] { 100, 100, 300, 500, 50 };
        level = new int[5] { 1, 1, 1, 1, 1 };
        cost = new int[5] { 100, 100, 100, 100, 100 };
        range = new int[5] { 5, 5, 1, 0, 9 };
    }

    public void LevelUp(int type)
    {
        if(this.GetComponent<Point>().CurrentPoint >= cost[type])
        {
            this.GetComponent<Point>().UsePoint(cost[type]);
            damage[type] += 20f;
            hP[type] += 50;
            level[type] += 1;
            cost[type] += 100;
        }
    }
}
