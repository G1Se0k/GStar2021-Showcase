using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCard : MonoBehaviour
{
    [SerializeField]
    private int unitType;
    public int Type
    {
        get { return unitType; }
    }

    [SerializeField]
    private Transform unitP;
    public Transform UnitP
    {
        get { return unitP; }
        set { unitP = value; }
    }

    [SerializeField]
    public GameObject[] unitPrefabs;

    private bool isClick;

    private Vector2 startPos;
    public Vector2 StartPos
    {
        get { return startPos; }
        set { startPos = value; }
    }

    [SerializeField]
    private GameObject gameManager;
    public GameObject GameManager
    {
        get { return gameManager; }
        set{ gameManager = value; }
    }

    private int cost;
    private Text costText;

    [SerializeField]
    private Sprite[] source_image;


    private void Start()
    {

        unitType = Random.Range(1,6);
        isClick = false;
        this.transform.localPosition = new Vector3(550,-50,0);
        switch (unitType)
        {
            case 1:
                cost = 10;
                break;
            case 2:
                cost = 20;
                break;
            case 3:
                cost = 30;
                break;
            case 4:
                cost = 40;
                break;
            case 5:
                cost = 50;
                break;
        }
        this.transform.GetChild(1).GetComponent<Text>().text = cost.ToString();
        this.transform.GetChild(0).GetComponent<Image>().sprite = source_image[unitType - 1];
    }

    private void Update()
    {
        if (isClick)
        {
            this.transform.position = Input.mousePosition;
        }
        else
        {
            this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, startPos, 0.1f * Time.deltaTime * 40);
        }
    }

    public void Click()
    {
        if (gameManager.GetComponent<Point>().CurrentPoint >= cost)
        {
            isClick = true;
            Color color = this.GetComponent<Image>().color;
            color.a = 0.5f;
            this.GetComponent<Image>().color = color;
            gameManager.transform.GetComponent<MouseManager>().MouseItem = this.gameObject;
        }
    }

    public void UnClick()
    {
        isClick = false;
        Color color = this.GetComponent<Image>().color;
        color.a = 1;
        this.GetComponent<Image>().color = color;
        gameManager.transform.GetComponent<MouseManager>().MouseItem = null;
        if(gameManager.transform.GetComponent<MouseManager>().MouseTile != null)
        {
            if (gameManager.transform.GetComponent<MouseManager>().MouseTile.GetComponent<Tile>().Unit == null)
            {
                if(TileStatus.tileStatus[(int)(gameManager.transform.GetComponent<MouseManager>().MouseTile.transform.position.x/5),(int)(gameManager.transform.GetComponent<MouseManager>().MouseTile.transform.position.z / 5),1]==null)
                {
                    gameManager.transform.GetComponent<MouseManager>().MouseTile.GetComponent<Tile>().Unit = Instantiate(unitPrefabs[unitType - 1], unitP);
                    gameManager.transform.GetComponent<MouseManager>().MouseTile.GetComponent<Tile>().Unit.transform.position = gameManager.transform.GetComponent<MouseManager>().MouseTile.transform.position - new Vector3(2.5f, 0, -2.5f);
                    gameManager.GetComponent<Point>().UsePoint(cost);

                    for (int i = 0; i < this.GetComponentInParent<UnitBox>().Length; i++)
                    {
                        if (this.transform.parent.GetComponent<UnitBox>().Units[i] == this.gameObject)
                        {
                            for (int j = 0; j < this.transform.parent.GetComponent<UnitBox>().Length - i - 1; j++)
                            {
                                this.transform.parent.GetComponent<UnitBox>().Units[i + j] = this.transform.parent.GetComponent<UnitBox>().Units[i + j + 1];
                                this.transform.parent.GetComponent<UnitBox>().Units[i + j + 1] = null;
                                this.transform.parent.GetComponent<UnitBox>().Units[i + j].GetComponent<UnitCard>().StartPos = new Vector2(50 + ((i + j) * 100), -50);
                            }
                        }
                    }

                    this.GetComponentInParent<UnitBox>().Length--;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
