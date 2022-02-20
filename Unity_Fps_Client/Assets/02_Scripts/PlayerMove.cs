using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //������ �ӵ�
    private float MoveSpeed = 10.0f;
    //�߷°��ӵ�
    private float GravitySpeed = -20.0f;
    //�߷�
    private float ThisGravity;
    //���� �ӵ�
    private int JumpPower = 5;
    //���� ���� Ƚ��
    private int JumpCount = 0;
    //�ִ� ���� Ƚ��
    private int MaxJumpCount = 1;
    //CharacterController������Ʈ �̿�
    CharacterController cc;

    //�ʱ⼳��
    private void Start()
    {
        //CharacterController ������
        cc = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        //�Է�
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        //�����ʱ�ȭ (���� ����ִٸ�)
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            JumpCount = 0;
            ThisGravity = 0;
        }
        //���� ���
        if (Input.GetButtonDown("Jump"))
        {
            //������ �پ��ٸ�
            if (JumpCount == 0 && cc.collisionFlags != CollisionFlags.Below)
            {
                return;
            }
            //������ ���� �ʾҴٸ�
            else if (JumpCount < MaxJumpCount)
            {
                ThisGravity = JumpPower;
                JumpCount++;
            }
        }

        
        //X,Z �ุ �̵�
        Vector3 dir = new Vector3(Horizontal, 0, Vertical);
        //�����̵��� ���� ����ȭ
        dir.Normalize();
        
        //ī�޶� �������� ����
        dir = Camera.main.transform.TransformDirection(dir);
        
        //�߷� ����
        ThisGravity += GravitySpeed * Time.deltaTime;
        dir.y = ThisGravity;
        //������ ����
        cc.Move(dir * MoveSpeed * Time.deltaTime);
    }
}
