using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkStatus : MonoBehaviourPun, IPunObservable
{
    private float health;

    private void Awake()
    {
        health = GetComponent<CharacterStatus>().health;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (float)stream.ReceiveNext();
        }
    }
    

    void Update()
    {
        if (photonView.IsMine)
        {
        }
        else
        {
          //  transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
          //  transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
        }
    }
}
