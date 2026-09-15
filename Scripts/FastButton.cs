using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastButton : MonoBehaviour
{
    void Update()
    {
        if(Time.timeScale > 0)
        {
            this.GetComponent<Text>().text = "x" + Time.timeScale;
        }
    }
}
