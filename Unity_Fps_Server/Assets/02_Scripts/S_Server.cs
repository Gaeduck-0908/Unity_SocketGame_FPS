using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Lib;

public class S_Server : Singleton<S_Server>
{
    // Ŭ���̾�Ʈ�� ����Ǿ����� Ȯ��
    public static bool client_connected = false;

    // ������ �� ����
    TcpListener server;

    // ������ ip,port
    public const string ip = "127.0.0.1";
    public const int port = 1008;

    // Ŭ���̾�Ʈ ��ü
    static TcpClient client = null;

    // �����͸� ����,���� �� ������
    public static NetworkStream ns = null;
    public static StreamWriter w = null;
    public static StreamReader r = null;

    // ������ ���� �Լ�
    public void server_open()
    {
        try
        {
            // ���� ����
            server = new TcpListener(IPAddress.Parse(ip), port);
            // ���� ����
            server.Start();
            // �α� ���
            Debug.Log("���� ���۵�");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        // Ŭ���̾�Ʈ ������ �޴� listen() ȣ��
        listen();
    }

    public void recv()
    {
        if (client_connected == true)
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
                    string tmp = R_Data.Replace("Postion", "");
                    // pos �迭�� / �� �������� ����
                    string[] pos = tmp.Split('/');
                    // S_Move enemy_move() �� ȣ���� �޾ƿ� �����͸� Vector ������ ��ȯ�Ͽ� ���� [0] = X [1] = Y [2] = Z
                    S_Move.Instance.enemy_move(new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2])));
                }

                // �޾ƿ� �������� �տ��� 8�ڸ������� Rotation �Ͻ� ��ǥ�������ΰ��� �Ǻ�
                else if(R_Data.Substring(0, 8) == "Rotation")
                {
                    // tmp �� R_Data ���� ����
                    string tmp = R_Data.Replace("Rotation", "");
                    // rot �迭�� / �� �������� ����
                    string[] rot = tmp.Split('/');
                    // S_Move enemy_rot() �� ȣ���� �޾ƿ� �����͸� Vecort ������ ��ȯ�Ͽ� ���� [0] = X [1] = Y [2] = Z
                    S_Move.Instance.enemy_rot(new Vector3(float.Parse(rot[0]), float.Parse(rot[1]), float.Parse(rot[2])));
                }
            }
        }
    }

    // �����͸� ������ �Լ�
    public void send(string S_Data)
    {
        if(client_connected == true)
        {
            // �����͸� ����
            w.WriteLine(S_Data);
            // ���� ���
            w.Flush();
            // �α� ���
            Debug.Log("���۵�");
        }
    }

    void listen()
    {
        // ���� �õ��� �񵿱�� �����
        server.BeginAcceptTcpClient(accept, server);
    }

    // ������ �޾��ִ� �Լ� (�񵿱�)
    void accept(IAsyncResult iar)
    {
        TcpListener listener = (TcpListener)iar.AsyncState;

        client = listener.EndAcceptTcpClient(iar);

        // Ŭ���̾�Ʈ�� �����͸� ������ ���� ��ü ȣ��
        ns = client.GetStream();
        w = new StreamWriter(ns);
        r = new StreamReader(ns);

        // Ŭ���̾�Ʈ ���� = ��
        client_connected = true;
        Debug.Log("Ŭ���̾�Ʈ ���� : " + client_connected);
    }

    private void Start()
    {
        server_open();
    }
}
