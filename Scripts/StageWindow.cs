using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageWindow : MonoBehaviour
{
    private bool isOpen;

    void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        if (isOpen)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(1, 1, 1), 0.1f * Time.deltaTime * 40);
        }
        else
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(0, 0, 0), 0.1f * Time.deltaTime * 40);
        }
    }

    public void Switch()
    {
        isOpen = !isOpen;
    }
}
