using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFX : MonoBehaviour
{
    [SerializeField]
    private GameObject stageWindow;

    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void exit()
    {
        Application.Quit();
    }

    public void aespa()
    {
        GameObject gameManager;
        GameObject skill;
        GameObject victory;
        gameManager = GameObject.Find("GameManager");
        skill = GameObject.Find("Skills");
        victory = GameObject.Find("Victory");
        skill.GetComponent<Skills>().Init();
        gameManager.GetComponent<CreateEnemy>().NextStage();
        victory.SetActive(false);
    }

    public void SwitchStageWindow()
    {
        stageWindow.GetComponent<StageWindow>().Switch();
    }
}
