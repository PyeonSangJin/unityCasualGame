using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CharacterStatus : MonoBehaviourPun, IPunObservable
{
    public const float maxHealth = 100f;
    public float currentHealth = 50f;

    public Slider hpBar;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentHealth);
        }
        else
        {
            currentHealth = (float)stream.ReceiveNext();
        }
    }

    void Update()
    {
 
        OnChangeHealth(currentHealth);

        if (!photonView.IsMine) return;
        if (currentHealth <= 0f)
        {
            GameManager.Instance.LeaveRoom();
        }
    }

    void OnChangeHealth(float currentHealth)
    {
        hpBar.value = currentHealth / maxHealth;
    }


    [PunRPC]
    public void TakeDamage(int amount)
    {  
        //deltatime 하면 이상해짐
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    [PunRPC]
    public void AddHealth(int amount)
    {
        //deltatime 하면 이상해짐
        currentHealth += amount;
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


}