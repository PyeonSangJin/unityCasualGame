using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemTrigger : MonoBehaviourPun
{
    GameObject obj;
    private int faraway = -1;

    void OnTriggerEnter2D(Collider2D Item)
    {
        if (Item.CompareTag("white") && faraway != Item.GetInstanceID())
        {
            faraway = Item.GetInstanceID();

            obj = Item.gameObject;
            CmdDestroyItem(obj);
        }
    }
    
    void CmdDestroyItem(GameObject item)
    {
        PhotonNetwork.Destroy(item);
        //photonView.RPC("destroy", RpcTarget.All, null);

        if (!PhotonNetwork.IsMasterClient) return;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player == this.gameObject) player.GetComponent<CharacterStatus>().photonView.RPC("AddHealth", RpcTarget.All, 10);
            else player.GetComponent<CharacterStatus>().photonView.RPC("TakeDamage", RpcTarget.All, 10);
        }
    }

    [PunRPC]
    void destroy()
    {
        Destroy(obj);
    }
}
