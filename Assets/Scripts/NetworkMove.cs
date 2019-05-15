using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkMove : MonoBehaviourPun, IPunObservable
{
    private Animator animator;
    Vector3 realPosition = Vector3.zero;
    Quaternion realRotation = Quaternion.identity;

    private GameObject canvus;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(animator.GetFloat("DirX"));
            stream.SendNext(animator.GetFloat("DirX"));
            stream.SendNext(animator.GetBool("Walking"));
        }
        else
        {
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
            animator.SetFloat("DirX", (float)stream.ReceiveNext());
            animator.SetFloat("DirY", (float)stream.ReceiveNext());
            animator.SetBool("Walking", (bool)stream.ReceiveNext());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
        }
        else {
            transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0, 0, 0,0);
            canvus = transform.FindChild("Canvas").gameObject;
            canvus.SetActive(false);
        }
    }
}
