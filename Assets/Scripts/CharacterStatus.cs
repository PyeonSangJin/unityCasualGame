using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CharacterStatus : MonoBehaviourPun
{
    public const float maxHealth = 100f;
    public float health = 100f;
    public Slider hpBar;


    void FixedUpdate()
    {
        OnChangeHealth(health);

        if (!photonView.IsMine) return;
        if (health <= 0f)
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
        health -= amount;

        if (health <= 0)
        {
            health = 0;
        }
    }
    
    [PunRPC]
    public void AddHealth(int amount)
    {
        health += amount;
        
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}