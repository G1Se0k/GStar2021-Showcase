using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isMouseOn;
    private bool colorUse;
    private GameObject unit;

    private GameObject gameManager;

    private int posX;
    private int posY;

    public int PosX { get { return posX; } set { posX = value; } }
    public int PosY { get { return posY; } set { posY = value; } }

    public GameObject Unit
    {
        get {  return unit; }
        set {  unit = value; }
    }

    public bool IsMouseOn
    {
        get {  return isMouseOn; }
        set {  isMouseOn = value; }
    }
    public bool ColorUse
    {
        get { return isMouseOn; }
        set { isMouseOn = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        isMouseOn = false;
        unit = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!colorUse)
        {
            if (isMouseOn == true)
            {
                isMouseOn = false;
            }
            else
            {
                this.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
            }
        }
    }

    public void Glow(Color color)
    {
        this.GetComponent<Renderer>().material.color = color;
        isMouseOn = true;
    }

    public void Glow2(Color color)
    {
        this.GetComponent<Renderer>().material.color = color;
        colorUse = true;
    }

    public void InitPos(int x, int y)
    {
        posX = x;
        posY = y;
    }
}
