using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] BGM;

    private AudioSource BGMSource;
    private int randnum;


    void Start()
    {
        randnum = Random.RandomRange(0, BGM.Length);

        BGMSource = gameObject.AddComponent<AudioSource>();
        BGMSource.loop = true;
        BGMSource.clip = BGM[randnum];
        BGMSource.volume = 0.25f;
        BGMSource.Play();
    }
}
