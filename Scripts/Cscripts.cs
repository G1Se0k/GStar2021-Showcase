using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cscripts : MonoBehaviour
{
    [SerializeField]
    private Transform defaultPos;
    [SerializeField]
    private Transform failPos;

    private int state;
    public int State
    {
        get { return state; }
        set { state = value; }
    }
    void Start()
    {
        state = 0;
    }

    void Update()
    {
        switch (state)
        {
            case 0:
                this.transform.position = Vector3.Lerp(this.transform.position, defaultPos.position, 0.1f * Time.deltaTime * 40);
                break;
            case 1:
                this.transform.position = Vector3.Lerp(this.transform.position, failPos.position, 0.02f * Time.deltaTime * 40);
                break;
        }
    }

    IEnumerator Shake()
    {
        for (int i = 0; i < 10; i++)
        {
            this.transform.position = defaultPos.transform.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), 0);
            yield return new WaitForSeconds(0.025f);
        }
        this.transform.position = defaultPos.transform.position;
        yield return 0;
    }

    public void Shake2()
    {
        StartCoroutine(Shake());
    }
}
