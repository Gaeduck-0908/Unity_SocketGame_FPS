using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.IO;
using Lib;

public class C_Client : Singleton<C_Client>
{
    // ������ ����Ǿ����� �Ǻ��ϴ� ����
    public static bool is_connected = false;

    // ������ ip,port
    public const string server_ip = "127.0.0.1";
    public const int server_port = 1008;

    // Ŭ���̾�Ʈ ��ü
    static TcpClient client = null;

    // �����͸� ����,���� �� ������
    static NetworkStream ns = null;
    static StreamWriter w = null;
    static StreamReader r = null;

    // ������ �����ϴ� �Լ�
    public void connect()
    {
        try
        {
            // server_ip,port �ּҿ� ����
            client = new TcpClient(server_ip, server_port);

            // ��ü���� �޾ƿ�
            ns = client.GetStream();
            w = new StreamWriter(ns);
            r = new StreamReader(ns);

            // ���� ���� = ��
            is_connected = true;
            Debug.Log(is_connected);

        }
        // ����
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    // �����͸� �޾ƿ��� �Լ� recv()
    public void recv()
    {
        // ���� �Ǿ����� Ȯ��
        if(is_connected == true)
        {
            // �޾ƿ� �����Ͱ� �ִ��� Ȯ��
            if (ns.DataAvailable)
            {
                // ReadLine() �� ���� �����͸� ���ڿ��� ������
                string R_Data = r.ReadLine();

                // �޾ƿ� �������� �տ��� 7�ڸ������� Postion �Ͻ� ��ǥ�������ΰ��� �Ǻ�
                if (R_Data.Substring(0, 7) == "Postion")
                {
                    // tmp �� R_Data ���� ����
                    string tmp = R_Data.Replace("Postion","");
                    // pos �迭�� / �� �������� ����
                    string[] pos = tmp.Split('/');
                    // C_Move enemy_move() �� ȣ���� �޾ƿ� �����͸� Vector ������ ��ȯ�Ͽ� ���� [0] = X [1] Y
                    C_Move.Instance.enemy_move(new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2])));
                }

                // �޾ƿ� �������� �տ��� 8�ڸ������� Rotation �Ͻ� ��ǥ�������ΰ��� �Ǻ�
                else if (R_Data.Substring(0, 8) == "Rotation")
                {
                    // tmp �� R_Data ���� ����
                    string tmp = R_Data.Replace("Rotation", "");
                    // rot �迭�� / �� �������� ����
                    string[] rot = tmp.Split('/');
                    Debug.Log(rot[0] + " " + rot[1] + " " + rot[2]);
                    // S_Move enemy_rot() �� ȣ���� �޾ƿ� �����͸� Vecort ������ ��ȯ�Ͽ� ���� [0] = X [1] = Y [2] = Z
                    C_Move.Instance.enemy_rot(new Vector3(float.Parse(rot[0]), float.Parse(rot[1]), float.Parse(rot[2])));
                }
            }
        }
    }

    // �����͸� ������ �Լ�
    public void send(string S_Data)
    {
        // �����͸� ����
        w.WriteLine(S_Data);
        // ���� ���
        w.Flush();
        // �α� ���
        Debug.Log("���۵�");
    }

    private void Start()
    {
        connect();
    }
}
