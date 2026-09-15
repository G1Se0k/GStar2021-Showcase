using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStatus : MonoBehaviour
{
    static public GameObject[,,] tileStatus=new GameObject[12,5,2];
    //[게임판좌표x,게임판좌표x,(0=해당타일 위에 있는 개체,1=해당 타일로 이동중인 개체)]
    void Start()
    {
        for (int i = 0; i < 12; i++)
            for (int j = 0; j < 5; j++)
                for (int k = 0; k < 2; k++) {
                    tileStatus[i, j, k] = null;
                }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
