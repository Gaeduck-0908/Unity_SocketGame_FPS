using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib;

public class S_Move : Singleton<S_Move>
{
    public GameObject Player; // 플레이어
    public GameObject Enemy; // 받아온 데이터로 움직일 오브젝트

    void Update()
    {
        Player_Move_send(); // move() 호출
        S_Server.Instance.recv(); // C_Client 안에 있는 recv() 호출
    }

    public void Player_Move_send() // 움직임 함수
    {
        S_Server.Instance.send("Postion" + 
            Player.transform.position.x.ToString() + "/" + 
            Player.transform.position.y.ToString() + "/" + 
            Player.transform.position.z.ToString());
    }

    // 상대방을 움직일 함수
    public void enemy_move(Vector3 position)
    {
        // 받아온 데이터의 좌표값으로 설정
        Enemy.gameObject.transform.position = position;
    }
}
