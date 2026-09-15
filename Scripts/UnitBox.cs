using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBox : MonoBehaviour
{
    [SerializeField]
    private GameObject unitCard;

    private GameObject[] units;
    public GameObject[] Units
    {
        get { return units; }
        set { units = value; }
    }

    [SerializeField]
    private GameObject gameManager;

    [SerializeField]
    private Transform unitP;

    private int length = 0;
    public int Length
    {
        get { return length; }
        set { length = value; }
    }

    void Start()
    {
        units = new GameObject[5];
        for(int i = 0; i < 5; i++)
        {
            if(this.gameObject.transform.GetChild(i).gameObject != null)
            {
                units[i] = this.gameObject.transform.GetChild(i).gameObject;
                units[i].GetComponent<UnitCard>().StartPos = new Vector2(50 + (i * 100), -50);
                length++;
            }
        }
        StartCoroutine(CreateCard());
    }

    void Update()
    {
    }

    IEnumerator CreateCard()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            if (length < 5)
            {
                if(units[length] == null)
                {
                    units[length] = Instantiate(unitCard, this.transform);
                    units[length].GetComponent<UnitCard>().StartPos = new Vector2(50 + ((length) * 100), -50);
                    units[length].transform.localPosition = new Vector2(650, -50);
                    units[length].GetComponent<UnitCard>().GameManager = gameManager;
                    units[length].GetComponent<UnitCard>().UnitP = unitP;
                    length++;
                }
            }
        }

        yield return 0;
    }
}
