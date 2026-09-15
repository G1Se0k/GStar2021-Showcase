using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;

    private float timeFast;

    private void Start()
    {
        Time.timeScale = 1;
        timeFast = 1f;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = timeFast;
                pausePanel.SetActive(false);
            }
        }
    }
    public void Fast()
    {
        if (timeFast == 1)
        {
            timeFast = 1.5f;
        }
        else if(timeFast == 1.5f)
        {
            timeFast = 2;
        }
        else
        {
            timeFast = 1;
        }
        Time.timeScale = timeFast;
    }
}
