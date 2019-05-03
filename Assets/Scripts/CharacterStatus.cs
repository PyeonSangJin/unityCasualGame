using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CharacterStatus : MonoBehaviourPunCallbacks, IPunObservable
{
    public const float maxHealth = 100f;

    //동기화 변수 값변경시 자동으로 동기화(int, float만 가능)24개까지
    //hook는 해당 변수 값이 변경시 함수 호출
    //[SyncVar(hook = "OnChangeHealth")]
    public float currentHealth;

    public Slider hpBar;
    //    private NetworkStartPosition[] spawnPoints;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentHealth);
        }
        else
        {
            this.currentHealth = (float)stream.ReceiveNext();
        }
    }


    private void Awake()
    {
//        if (isLocalPlayer)
//        {
//            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
 //       }
    }

    void Update()
    {
        //ProcessInputs();
        // trigger Beams active state
        //if (beams != null && IsFiring != beams.activeSelf)
        //{
        //    beams.SetActive(IsFiring);
        //}
        if (currentHealth <= 0f)
        {
            GameManager.Instance.LeaveRoom();
        }
    }


    void OnChangeHealth(float currentHealth)
    {
        hpBar.value = currentHealth / maxHealth;
    }


    //   [ServerCallback]
    public void TakeDamage(int amount)
    {
        if (!photonView.IsMine) return;

        currentHealth -= amount * Time.deltaTime;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
    //        RpcRespawn();
        }

    }

    //   [ServerCallback]
    public void AddHealth(int amount)
    {
        if (!photonView.IsMine) return;

        currentHealth += amount * Time.deltaTime;


        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }

    private void FixedUpdate()
    {
        OnChangeHealth(currentHealth);
    }

    //클라이언트가 연결되면 실행하는 콜백함수
    //snycVar가 먼저 설정된 뒤 실행
    //public override void OnStartClient()
    //{
    //    base.OnStartClient();
    //    currentHealth = 50;
    //}

    //command와 반대 서버에서 클라이언트에게 실행하라고
    //[ClientRpc]
    //void RpcRespawn()
    //{
    //    if (isLocalPlayer)
    //    {
    //        Vector3 spawnPoint = Vector3.zero;

    //        if (spawnPoints != null && spawnPoints.Length > 0)
    //        {

    //            spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
    //        }

    //        CmdPlayerReset(spawnPoint);
    //    }
    // }

    //서버에게 클라이언트 위치 바꿔달라고 요청

    //    [Command]
    //void CmdPlayerReset(Vector3 vector3)
    //{
    //    currentHealth = 50;
    //    transform.position = vector3;
    //}


}