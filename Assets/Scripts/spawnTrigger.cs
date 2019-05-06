using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawnTrigger : MonoBehaviourPun
{
    void OnTriggerEnter2D(Collider2D Item)
    {
        if (Item.CompareTag("white"))
        {
            CmdDestroyItem(Item.gameObject);
        }
    }
    

    void CmdDestroyItem(GameObject item)
    {


        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player == this.gameObject) player.GetComponent<CharacterStatus>().AddHealth(10);
            else player.GetComponent<CharacterStatus>().TakeDamage(10);
        }

        PhotonNetwork.Destroy(item);
        Destroy(item);
    }
}
