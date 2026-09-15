using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D crossHair;
    [SerializeField]
    private Texture2D defaultCursor;
    [SerializeField]
    private AudioClip audioShoot;

    private AudioSource audioSource;

    private RaycastHit hit;

    private GameObject mouseItem;
    public GameObject MouseItem
    {
        get { return mouseItem; }
        set { mouseItem = value; }
    }


    private GameObject mouseTile;
    public GameObject MouseTile
    {
        get {  return mouseTile; }
        set {  mouseTile = value; }
    }

    private void Start()
    {
        Cursor.SetCursor(defaultCursor, new Vector2(crossHair.width >> 1, crossHair.height >> 1), CursorMode.Auto);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.25f;
        audioSource.clip = audioShoot;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("Tiles");
        int layerMask2 = 1 << LayerMask.NameToLayer("Enemys");
        if (mouseItem != null)
        {
            if (Physics.Raycast(ray, out hit, 150,layerMask))
            {
                if(hit.collider.gameObject.tag == "Tile")
                {
                    mouseTile = hit.collider.gameObject;

                    if ((hit.collider.gameObject.GetComponent<Tile>().Unit == null) && (TileStatus.tileStatus[(int)(this.transform.GetComponent<MouseManager>().MouseTile.transform.position.x / 5), (int)(this.transform.GetComponent<MouseManager>().MouseTile.transform.position.z / 5), 1] == null))
                    {
                        int type = mouseItem.GetComponent<UnitCard>().Type;
                        int x = hit.collider.gameObject.GetComponent<Tile>().PosX;
                        int y = hit.collider.gameObject.GetComponent<Tile>().PosY;
                        hit.collider.gameObject.GetComponent<Tile>().Glow(new Color(1, 1, 0));
                        for (int i = 0; i < this.transform.GetComponent<TeamStats>().Range[type-1] && x + i + 1 < 12; i++)
                        {
                            this.GetComponent<TileMaker>().Tiles[x + i + 1, y].GetComponent<Tile>().Glow(new Color(1, 1, 0));
                        }
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<Tile>().Glow(new Color(1, 0, 0));
                    }

                    //Debug.Log(mouseItem.GetComponent<UnitCard>().Type);
                }
                else
                {
                    mouseTile = null;
                }
            }
            else
            {
                mouseTile = null;
            }
        }
        else
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(ray, out hit, 150, layerMask))
                {
                    if (hit.transform.tag == "DoorFrontTile")
                    {
                        Cursor.SetCursor(crossHair, new Vector2(crossHair.width >> 1, crossHair.height >> 1), CursorMode.Auto);
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (Physics.Raycast(ray, out hit, 100, layerMask2))
                            {
                                audioSource.Play();
                                hit.transform.GetComponent<Enemy>().getDamage(10f);
                            }
                        }
                    }
                    else
                    {
                        Cursor.SetCursor(defaultCursor, new Vector2(crossHair.width >> 1, crossHair.height >> 1), CursorMode.Auto);
                    }
                }
                else
                {
                    Cursor.SetCursor(defaultCursor, new Vector2(crossHair.width >> 1, crossHair.height >> 1), CursorMode.Auto);
                }
            }
            else
            {
                Cursor.SetCursor(defaultCursor, new Vector2(crossHair.width >> 1, crossHair.height >> 1), CursorMode.Auto);
            }
        }
    }

    public void setDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, new Vector2(crossHair.width >> 1, crossHair.height >> 1), CursorMode.Auto);
    }
}
