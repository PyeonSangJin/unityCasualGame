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
            Debug.Log(photonView.InstantiationId);
            CmdDestroyItem(Item.gameObject);
        }
    }

    //클라이언트에서 피 주르륵 다르는거 잡아야함
    void CmdDestroyItem(GameObject item)
    {



        // if(photonView.IsMine)
        if (photonView.InstantiationId == 0)
            Destroy(item);
        else
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(item);
        }
        if( item == null) {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                if (player == this.gameObject) player.GetComponent<CharacterStatus>().photonView.RPC("AddHealth", RpcTarget.All, 10);
                else player.GetComponent<CharacterStatus>().photonView.RPC("TakeDamage", RpcTarget.All, 10);
            }
        }





    }




}
