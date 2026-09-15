using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private float hP;
    private float maxHp;

    static public  GameObject DoorObj;

    private Transform door1;
    private Transform door2;

    private Vector3 startPos;

    [SerializeField]
    private Cscripts camera;

    [SerializeField]
    private ParticleSystem[] explosion;

    [SerializeField]
    private GameObject defeatWindow;

    [SerializeField]
    private AudioClip audioHit;

    private AudioSource audioSource;

    private bool isAlive;

    public float HP
    {
        get{return hP;}
    }
    public float MaxHp
    {
        get{return maxHp; }
    }

    private void Start()
    {
        maxHp = 100;
        hP = maxHp;
        door1 = this.transform.GetChild(0);
        door2 = this.transform.GetChild(1);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = 0.25f;
        audioSource.clip = audioHit;

        startPos = this.transform.position;
        isAlive = true;
        DoorObj = this.gameObject;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Hit(10f);
        //}
    }

    public void Hit(float damage)
    {
        if (isAlive)
        {
            hP -= damage;
            if (hP <= 0)
            {
                Death();
                isAlive = false;
            }
            else
            {
                audioSource.Play();
                StartCoroutine(Shake());
            }
        }
    }
    public void Repair(float damage)
    {
        if (isAlive)
        {
            hP += damage;
            if (hP > maxHp)
            {
                hP = maxHp;
            }
        }
    }

    private void Death()
    {
        if(door1!=null)
        Destroy(door1.gameObject);
        if (door2 != null)
            Destroy(door2.gameObject);
        camera.State = 1;
        foreach(ParticleSystem ex in explosion)
        {
            ex.Play();
        }
        StartCoroutine(Defeat());
    }

    IEnumerator Shake()
    {
        for(int i = 0; i < 10; i++)
        {
            this.transform.position = startPos + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), 0);
            yield return new WaitForSeconds(0.025f);
        }
        this.transform.position = startPos;
        yield return 0;
    }

    IEnumerator Defeat()
    {
        yield return new WaitForSeconds(3f);
        defeatWindow.SetActive(true);
        yield return 0;
    }
}
