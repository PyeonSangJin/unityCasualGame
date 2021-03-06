﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class ItemSpawn : MonoBehaviourPunCallbacks
{
    private GameObject item;
    private bool isin = true;
    private double[,,] mappos = new double[22, 12, 2];
    private int[,] map = {
          {2,2,2,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0 },
          {2,2,2,2,1,1,1,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0 },
          {2,2,1,2,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,0,0 },
          {2,2,1,2,2,2,1,1,1,1,0,0,0,1,1,1,1,1,1,1,0,0 },
          {1,1,1,1,1,2,2,1,1,1,1,1,1,1,2,2,2,2,1,1,0,1 },
          {1,1,1,1,1,1,2,2,1,1,1,1,1,1,2,1,1,2,1,0,0,1 },
          {1,1,1,1,0,1,1,2,1,1,1,2,2,2,2,1,1,2,1,0,1,1 },
          {1,1,1,0,0,1,1,2,2,2,2,2,1,2,2,1,1,1,1,1,1,1 },
          {0,0,1,0,1,1,1,1,1,1,1,1,1,1,2,2,2,1,1,2,1,1 },
          {0,0,0,0,1,0,0,1,1,1,1,1,0,1,2,2,1,1,1,2,2,1 },
          {0,0,0,0,0,0,1,1,1,1,0,0,0,1,1,2,2,2,2,2,2,2 },
          {0,0,0,0,0,1,1,1,1,0,0,0,0,1,1,2,2,1,1,1,1,2 }
   };
    public float spawnTime = 5f;

    public void cal()
    {
        for (int i = 0; i < 22; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                mappos[i, j, 1] = 8.7 - (1.33 * j);
                mappos[i, j, 0] = -14 + (1.33 * i);
            }
        }
    }

    void Awake()
    {
        cal();
        item = Resources.Load<GameObject>("white");
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        int x = Random.Range(0, 22);
        int y = Random.Range(0, 12);

        if (map[y, x] != 0)
        {
            if (isin)
            {
                isin = false;
                StartCoroutine(SpawnCoroutine(x, y));
            }
        }
    }

    IEnumerator SpawnCoroutine(int x, int y)
    {
        Vector3 vector = new Vector3((float)mappos[x, y, 0], (float)mappos[x, y, 1], 0);
        
        PhotonNetwork.Instantiate(this.item.name, vector, Quaternion.identity, 0);

        yield return new WaitForSeconds(spawnTime);
        isin = true;
    }
}