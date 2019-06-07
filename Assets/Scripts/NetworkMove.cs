using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkMove : MonoBehaviourPun, IPunObservable
{
    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;

    private GameObject canvus;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
    
    void Update()
    {
        if (photonView.IsMine)
        {
        }
        else {
            transform.position = Vector3.Lerp(transform.position, currPos, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, currRot, 0.1f);

            gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0, 0, 0,0);
            canvus = transform.Find("Canvas").gameObject;
            canvus.SetActive(false);
        }
    }
}
