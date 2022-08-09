using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib;

public class C_Move : Singleton<C_Move>
{
    public GameObject Player; // 플레이어
    public GameObject Enemy; // 받아온 데이터로 움직일 오브젝트

    private Vector3 lastPos; // 마지막 위치
    private Quaternion lastRot; // 마지막 각도

    private void Start()
    {
        lastPos = Enemy.gameObject.transform.position;
        lastRot = Enemy.gameObject.transform.GetChild(0).rotation;
    }
    void Update()
    {
        Player_Move_send(); // move() 호출
        C_Client.Instance.recv(); // C_Client 안에 있는 recv() 호출
    }

    public void Player_Move_send() // 움직임 함수
    {
        C_Client.Instance.send("Postion" +
        Player.transform.position.x.ToString() + "/" +
        Player.transform.position.y.ToString() + "/" +
        Player.transform.position.z.ToString());

        C_Client.Instance.send("Rotation" +
        Player.transform.GetChild(0).rotation.x.ToString() + "/" +
        Player.transform.GetChild(0).rotation.y.ToString() + "/" +
        Player.transform.GetChild(0).rotation.z.ToString());
    }

    // 상대방을 움직일 함수
    public void enemy_move(Vector3 position)
    {
        // 받아온 데이터의 좌표값으로 설정
        if (lastPos != Enemy.gameObject.transform.localPosition)
        {
            Enemy.gameObject.transform.position = position;
        }
        lastPos = Enemy.gameObject.transform.position;
    }

    // 상대방의 각도를 움직일 함수
    public void enemy_rot(Vector3 rotation)
    {
        if (lastRot != Enemy.gameObject.transform.GetChild(0).rotation)
        {
            Enemy.gameObject.transform.GetChild(0).localEulerAngles = rotation;
        }
        lastRot = Enemy.gameObject.transform.GetChild(0).rotation;
    }
}
