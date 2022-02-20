using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib;

public class S_Move : Singleton<S_Move>
{
    public GameObject Player; // �÷��̾�
    public GameObject Enemy; // �޾ƿ� �����ͷ� ������ ������Ʈ

    void Update()
    {
        Player_Move_send(); // move() ȣ��
        S_Server.Instance.recv(); // C_Client �ȿ� �ִ� recv() ȣ��
    }

    public void Player_Move_send() // ������ �Լ�
    {
        S_Server.Instance.send("Postion" + 
            Player.transform.position.x.ToString() + "/" + 
            Player.transform.position.y.ToString() + "/" + 
            Player.transform.position.z.ToString());
    }

    // ������ ������ �Լ�
    public void enemy_move(Vector3 position)
    {
        // �޾ƿ� �������� ��ǥ������ ����
        Enemy.gameObject.transform.position = position;
    }
}
