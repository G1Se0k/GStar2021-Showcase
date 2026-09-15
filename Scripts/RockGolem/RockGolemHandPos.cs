using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemHandPos : MonoBehaviour
{
    [SerializeField]
    GameObject left;
    [SerializeField]
    GameObject right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp( right.transform.position, left.transform.position, 0.5f);
    }
}
