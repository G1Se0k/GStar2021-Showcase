using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [SerializeField]
    private Transform door;
    [SerializeField]
    private Transform enemyP;
    [SerializeField]
    private Transform particleP;

    [SerializeField]
    private GameObject[] particle;

    [SerializeField]
    private Text[] text;

    [SerializeField]
    private Point point;

    private int[] count;

    private void Start()
    {
        count = new int[2] { 3, 3 };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (count[0] > 0 && point.CurrentPoint >= 100)
            {
                count[0]--;
                point.CurrentPoint -= 100;
                GameObject part = Instantiate(particle[0], particleP);
                part.transform.position = door.position + new Vector3(3.5f,7.5f,-1.5f);
                door.GetComponent<Door>().Repair(30);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (count[1] > 0 && point.CurrentPoint >= 100)
            {
                count[1]--;
                point.CurrentPoint -= 100;
                for (int i = 0; i < enemyP.childCount; i++)
                {
                    enemyP.GetChild(i).GetComponent<Enemy>().getDamage(100f);

                    GameObject part = Instantiate(particle[1], particleP);
                    part.transform.position = enemyP.GetChild(i).position;
                }
                Camera.main.GetComponent<Cscripts>().Shake2();
            }
        }
        text[0].text = count[0].ToString();
        text[1].text = count[1].ToString();
    }
    public void Init()
    {
        count = new int[2] { 3, 3 };
    }
}
