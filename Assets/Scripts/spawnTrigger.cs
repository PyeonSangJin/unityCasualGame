using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTrigger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("white"))
        {
            Debug.Log(other.ToString());
            CmdDestroyOther(other.gameObject);
        }
    }

    
    void CmdDestroyOther(GameObject other)
    {
       // NetworkServer.Destroy(cube);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player == this.gameObject) player.GetComponent<CharacterStatus>().AddHealth(10);
            else player.GetComponent<CharacterStatus>().TakeDamage(10);
        }

        Destroy(other);
    }
}
