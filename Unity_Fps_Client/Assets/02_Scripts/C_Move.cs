using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib;

public class C_Move : Singleton<C_Move>
{
    public GameObject Player; // �÷��̾�
    public GameObject Enemy; // �޾ƿ� �����ͷ� ������ ������Ʈ

    private Vector3 lastPos; // ������ ��ġ
    private Quaternion lastRot; // ������ ����

    private void Start()
    {
        lastPos = Enemy.gameObject.transform.position;
        lastRot = Enemy.gameObject.transform.GetChild(0).rotation;
    }
    void Update()
    {
        Player_Move_send(); // move() ȣ��
        C_Client.Instance.recv(); // C_Client �ȿ� �ִ� recv() ȣ��
    }

    public void Player_Move_send() // ������ �Լ�
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

    // ������ ������ �Լ�
    public void enemy_move(Vector3 position)
    {
        // �޾ƿ� �������� ��ǥ������ ����
        if (lastPos != Enemy.gameObject.transform.localPosition)
        {
            Enemy.gameObject.transform.position = position;
        }
        lastPos = Enemy.gameObject.transform.position;
    }

    // ������ ������ ������ �Լ�
    public void enemy_rot(Vector3 rotation)
    {
        if (lastRot != Enemy.gameObject.transform.GetChild(0).rotation)
        {
            Enemy.gameObject.transform.GetChild(0).localEulerAngles = rotation;
        }
        lastRot = Enemy.gameObject.transform.GetChild(0).rotation;
    }
}
