using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    private Color color;
    private int stage;
    public int Stage { get { return stage; } }

    public void stageSelect(int s)
    {
        stage = s;
        panel.SetActive(true);
        color = panel.GetComponent<Image>().color;
    }

    private void Start()
    {
        stage = 0;
    }

    private void Update()
    {
        if(stage != 0)
        {
            color.a += 0.01f * Time.deltaTime * 40;
            panel.GetComponent<Image>().color = color;
            if(color.a >= 1)
            {
                SceneManager.LoadScene("GameScene");
                DontDestroyOnLoad(this);
            }
        }
    }
}
