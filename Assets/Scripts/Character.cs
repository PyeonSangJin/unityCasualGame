using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Character : MonoBehaviourPun
{
    public float speed = 5.0f;
    private Vector3 vector;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag;


    //타일단위
    public int walkCount;
    private int currentWalkCount;
    //2.4를 20픽셀씩 옮긴다.
    //while currentcnt = +1 20까지;

    private bool canMove = true;

    private Animator animator;

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    //함수 실해하다 호출하면 다중처리되는것처럼 진행
    IEnumerator MoveCoroutine() {
        while (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }
            
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0) vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;

            Vector2 start = transform.position; //캐릭터
            Vector2 end = start + new Vector2(vector.x * speed * walkCount * Time.deltaTime, vector.y * speed * walkCount * Time.deltaTime); // 이동하고자 하는곳

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            if (hit.transform != null) break; 

            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed) * Time.deltaTime, 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed) * Time.deltaTime, 0);
                }
                if (applyRunFlag) currentWalkCount++;

                currentWalkCount++;
                yield return new WaitForSeconds(0.01f); //1초동안 대기
            }
            currentWalkCount = 0;


        }
        animator.SetBool("Walking", false);
        canMove = true;
    }


    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (canMove) {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
