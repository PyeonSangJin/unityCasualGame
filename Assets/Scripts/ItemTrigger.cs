using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemTrigger : MonoBehaviourPun
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
        PhotonNetwork.Destroy(item);
       // Destroy(item);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player == this.gameObject) player.GetComponent<CharacterStatus>().photonView.RPC("AddHealth", RpcTarget.All,10);
            else player.GetComponent<CharacterStatus>().photonView.RPC("TakeDamage", RpcTarget.All, 10);
        }

        
    }
}
