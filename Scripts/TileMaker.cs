using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaker : MonoBehaviour
{
    [SerializeField]
    private Transform tileP;
    [SerializeField]
    private GameObject[] tilePrefabs;

    private GameObject[,] tiles = new GameObject[12, 5];

    public GameObject[,] Tiles
    {
        get { return tiles; }
    }

    private void Start()
    {
        IintializeTiles();
    }

    private void Update()
    {
        
    }

    private void IintializeTiles()
    {
        for(int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (i < 3)
                {
                    tiles[i, j] = Instantiate(tilePrefabs[3], tileP);
                }
                else if(i == 11)
                {
                    tiles[i, j] = Instantiate(tilePrefabs[j+4], tileP);
                }
                else
                {
                    tiles[i, j] = Instantiate(tilePrefabs[Random.Range(0, 3)], tileP);
                }
                tiles[i, j].transform.localPosition = new Vector3(i * 5, 0, j * 5);
                tiles[i, j].GetComponent<Tile>().InitPos(i, j);
            }
        }
    }

}
