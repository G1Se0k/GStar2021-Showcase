using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleArr : MonoBehaviour
{
    [SerializeField]
    private GameObject[] particles;
    static public GameObject[] Particles;
    // Start is called before the first frame update
    void Start()
    {
        Particles = particles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
