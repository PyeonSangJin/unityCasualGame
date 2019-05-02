using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemSpawn : MonoBehaviour
{
    double[,,] mappos = new double[22, 12, 2];
    int[,] map = {
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
    private bool isin = true;


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

    private GameObject cube;

    void Awake()
    {
        cal();
        cube = Resources.Load<GameObject>("white");
    }


    IEnumerator SpawnCoroutine(int x, int y)
    {
        Vector3 vector3 = new Vector3((float)mappos[x, y, 0], (float)mappos[x, y, 1], 0);

        GameObject spawnCube = Instantiate(cube, vector3, Quaternion.identity);
        yield return new WaitForSeconds(5f); //1초동안 대기
        isin = true;
    }


        void Update()
    {
        //if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.Space)) CmdCubeSpawn();
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

    void CmdCubeSpawn()
    {

        //NetworkServer.Spawn(spawnCube);
    }

}