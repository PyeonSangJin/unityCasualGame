using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkAnimator : MonoBehaviourPun, IPunObservable
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(animator.GetFloat("DirX"));
            stream.SendNext(animator.GetFloat("DirX"));
            stream.SendNext(animator.GetBool("Walking"));
        }
        else
        {
            animator.SetFloat("DirX", (float)stream.ReceiveNext());
            animator.SetFloat("DirY", (float)stream.ReceiveNext());
            animator.SetBool("Walking", (bool)stream.ReceiveNext());
        }
    }
}
