using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : Unit
{
    private void Start()
    {
        this.GetComponent<Unit>().Move_Speed = 10;
    }
    void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            this.transform.LookAt(this.transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            this.GetComponent<Animator>().Play("Sword And Shield Run");
            this.GetComponent<Animator>().speed = this.GetComponent<Unit>().Move_Speed/10;
            this.transform.Translate(Vector3.forward*Move_Speed * Time.deltaTime);
        }
        else
        {
            this.GetComponent<Animator>().Play("Breathing Idle");
        }

        if(this.transform.position.x > 10f)
        {
            this.transform.position = new Vector3(10f, this.transform.position.y, this.transform.position.z);
        }
        if(this.transform.position.x < -5f)
        {
            this.transform.position = new Vector3(-5f, this.transform.position.y, this.transform.position.z);
        }
        if(this.transform.position.z > 25f)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 25f);
        }
        if(this.transform.position.z < 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        }
    }
}
