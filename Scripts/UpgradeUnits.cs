using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUnits : MonoBehaviour
{
    [SerializeField]
    private TeamStats stats;
    [SerializeField]
    private int type;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.GetChild(1).GetComponent<Text>().text = stats.Cost[type].ToString();
    }
}
