using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : MonoBehaviour
{
    public Transform handPos;
    public GameObject Boss;
    float timer;
    public GameObject targetPos;
    // Start is called before the first frame update
    void Start()
    {
        timer = 1.5f;
        this.transform.position = new Vector3(44.5f, -5f, 12f);
        this.transform.forward = Vector3.left;
        this.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Boss != null)
        {
            timer -= Time.deltaTime;
            if (timer > 1f && this.transform.localScale.x < 15)
            {
                this.transform.position = handPos.position;
                this.transform.localScale += new Vector3(1, 1, 1).normalized * Time.deltaTime * 60;
            }
            if (timer > 0 && timer < 1f)
            {
                this.transform.position = handPos.position;
            }
            else
            {
                this.transform.position += (targetPos.transform.position - this.transform.position).normalized * Time.deltaTime * 40;
            }
            if (timer < -2)
            {
                Destroy(this.gameObject.GetComponent<Collider>());
                Destroy(this.gameObject);
            }
        }else
        {
            this.transform.localScale -= new Vector3(1, 1, 1).normalized * Time.deltaTime * 30;
            if(this.transform.localScale.x<0.05f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name=="Door")
        {
            other.gameObject.GetComponent<Door>().Hit(10);
            Destroy(this.gameObject.GetComponent<Collider>());
            Destroy(this.gameObject);
        }
    }
}
