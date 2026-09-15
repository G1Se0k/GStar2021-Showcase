using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoll : MonoBehaviour
{
    [SerializeField]
    GameObject boom;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.forward = Vector3.left;
        Destroy(this.gameObject, 7);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * 30f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="TeamUnit")
        {
            other.GetComponent<TeamUnit>().getDamage(100);
            GameObject Boom= Instantiate(boom);
            Boom.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }else if( other.tag == "Door")
        {
            other.GetComponent<Door>().Hit(10);
            GameObject Boom = Instantiate(boom);
            Boom.transform.position = this.transform.position;
            Destroy(this.gameObject);

        }
    }
}
