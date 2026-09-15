using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JjangDol : MonoBehaviour
{

    private Vector3 startPos;
    private int type;
    public int Type
    {
        get { return type; }
        set { type = value; }
    }

    private float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void Start()
    {
        startPos = this.transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(startPos, this.transform.position) > 22.5f)
        {
            Destroy(this.gameObject);
        }
        this.transform.position += this.transform.forward * 0.2f * Time.timeScale * Time.deltaTime * 40;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.transform.GetComponent<Enemy>().getDamage(damage);
            if(type == 2)
            {
                other.gameObject.transform.GetComponent<Enemy>().Move_Speed = 0;
                Instantiate(ParticleArr.Particles[10], this.transform.position, ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
            }
            else
            {
                Instantiate(ParticleArr.Particles[9],this.transform.position, ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
            }
            Destroy(this.gameObject);
        }
    }
}
