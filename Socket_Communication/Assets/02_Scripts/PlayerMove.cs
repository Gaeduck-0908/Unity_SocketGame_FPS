using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //움직임 속도
    private float MoveSpeed = 10.0f;
    //중력가속도
    private float GravitySpeed = -20.0f;
    //중력
    private float ThisGravity;
    //점프 속도
    private int JumpPower = 5;
    //현재 점프 횟수
    private int JumpCount = 0;
    //최대 점프 횟수
    private int MaxJumpCount = 1;
    //CharacterController컴포넌트 이용
    CharacterController cc;

    //초기설정
    private void Start()
    {
        //CharacterController 가져옴
        cc = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        //입력
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        //점프초기화 (땅에 닿아있다면)
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            JumpCount = 0;
            ThisGravity = 0;
        }
        //점프 기능
        if (Input.GetButtonDown("Jump"))
        {
            //점프를 뛰었다면
            if (JumpCount == 0 && cc.collisionFlags != CollisionFlags.Below)
            {
                return;
            }
            //점프를 뛰지 않았다면
            else if (JumpCount < MaxJumpCount)
            {
                ThisGravity = JumpPower;
                JumpCount++;
            }
        }

        
        //X,Z 축만 이동
        Vector3 dir = new Vector3(Horizontal, 0, Vertical);
        //균일이동을 위해 정규화
        dir.Normalize();
        
        //카메라 방향으로 돌림
        dir = Camera.main.transform.TransformDirection(dir);
        
        //중력 적용
        ThisGravity += GravitySpeed * Time.deltaTime;
        dir.y = ThisGravity;
        //움직임 적용
        cc.Move(dir * MoveSpeed * Time.deltaTime);
    }
}
