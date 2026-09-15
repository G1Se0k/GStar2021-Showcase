using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    [SerializeField]
    private Text points;

    [SerializeField]
    private GameObject enemyP;

    [SerializeField]
    private AudioClip audioCoin;

    private AudioSource audioSource;

    private float point;

    private float currentPoint;
    public float CurrentPoint
    {
        get { return currentPoint; }
        set { currentPoint = value; }
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.25f;
        audioSource.clip = audioCoin;

        currentPoint = 1000;
        point = 1000;
        StartCoroutine(GiveSeconds());
    }

    private void Update()
    {
        point = Mathf.Lerp(point, CurrentPoint, 0.1f);
        points.text = Mathf.Round(point).ToString();
    }

    IEnumerator GiveSeconds(){
        while (this)
        {
            yield return new WaitForSeconds(1f);
            if (enemyP.transform.childCount > 0)
            {
                currentPoint += 1;
            }
        }
        yield return 0;
    }

    public void UsePoint(int cost)
    {
        currentPoint -= cost;
        audioSource.Play();
    }

}
