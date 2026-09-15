using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul_Attack : MonoBehaviour
{
    private Transform target;

    private float current_y;

    private float damage;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    void Update()
    {
        if (target != null)
        {
            this.transform.position = target.transform.position + new Vector3(0, 0.5f, 0);
        }
    }

    public void init(Transform tar)
    {
        target = tar;
        current_y = 10;
        if (tar != null)
        {
            this.transform.position = target.transform.position + new Vector3(0, 0.5f, 0);
            target.GetComponent<Enemy>().getDamage(50f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
