using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private float max_health;
    private float current_health;
    private float damage;
    private float current_move_speed;
    private float move_speed;
    private float attack_speed;
    private float attack_range;
    Animator ani;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public float Attack_Speed
    {
        get { return attack_speed; }
        set { attack_speed = value; }
    }
    public float Attack_range
    {
        get { return attack_range; }
        set { attack_range = value; }
    }
    public float Current_Move_Speed
    {
        get { return current_move_speed; }
        set { current_move_speed = value; }
    }
    public float Move_Speed
    {
        get {  return move_speed; }
        set {  move_speed = value; }
    }
    public float Max_Health
    {
        get { return max_health; }
        set { max_health = value; }
    }

    public float Current_health
    {
        get { return current_health; }
        set { current_health = value; }
    }

    private void Start()
    {
        max_health = 100f;
        current_health = 100f;
        damage = 10f;
        move_speed = 1f;
        attack_speed = 1f;
        attack_range = 1f;
    }


}
