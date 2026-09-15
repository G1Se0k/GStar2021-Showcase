using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    Color color;
    private void Start()
    {
        color = GetComponent<Image>().color;
        color.a = 1;
        GetComponent<Image>().color = color;
    }

    private void Update()
    {
        color.a -= 0.01f * Time.deltaTime * 40;
        GetComponent<Image>().color = color;
        if(color.a <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
