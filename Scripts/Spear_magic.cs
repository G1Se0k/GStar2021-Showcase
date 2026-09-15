using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear_magic : MonoBehaviour
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
            this.transform.position = new Vector3(target.transform.position.x, current_y, target.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, current_y, this.transform.position.z);
        }
        current_y -= 0.2f;
        if(this.transform.position.y < -1)
        {
            Destroy(this.gameObject);
        }
    }

    public void init(Transform tar)
    {
        target = tar;
        current_y = 10;
        if(tar != null)
        {
            this.transform.position = target.transform.position + new Vector3(0, 10, 0);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.transform.GetComponent<Enemy>().getDamage(50f);
        }
    }
}
