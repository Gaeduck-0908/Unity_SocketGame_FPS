using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.IO;
using Lib;

public class C_Client : Singleton<C_Client>
{
    // 서버에 연결되었는지 판별하는 변수
    public static bool is_connected = false;

    // 점속할 ip,port
    public const string server_ip = "127.0.0.1";
    public const int server_port = 1008;

    // 클라이언트 객체
    static TcpClient client = null;

    // 데이터를 전송,수신 할 변수들
    static NetworkStream ns = null;
    static StreamWriter w = null;
    static StreamReader r = null;

    // 서버에 연결하는 함수
    public void connect()
    {
        try
        {
            // server_ip,port 주소에 연결
            client = new TcpClient(server_ip, server_port);

            // 객체들을 받아옴
            ns = client.GetStream();
            w = new StreamWriter(ns);
            r = new StreamReader(ns);

            // 서버 연결 = 참
            is_connected = true;
            Debug.Log(is_connected);

        }
        // 오류
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    // 데이터를 받아오는 함수 recv()
    public void recv()
    {
        // 연결 되었는지 확인
        if(is_connected == true)
        {
            // 받아온 데이터가 있는지 확인
            if (ns.DataAvailable)
            {
                // ReadLine() 을 통해 데이터를 문자열로 가져옴
                string R_Data = r.ReadLine();

                // 받아온 데이터의 앞에서 7자리까지가 Postion 일시 좌표데이터인것을 판별
                if (R_Data.Substring(0, 7) == "Postion")
                {
                    // tmp 에 R_Data 값을 넣음
                    string tmp = R_Data.Replace("Postion","");
                    // pos 배열에 / 를 간격으로 저장
                    string[] pos = tmp.Split('/');
                    // C_Move enemy_move() 를 호출해 받아온 데이터를 Vector 값으로 변환하여 전송 [0] = X [1] Y
                    C_Move.Instance.enemy_move(new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2])));
                }

                // 받아온 데이터의 앞에서 8자리까지가 Rotation 일시 좌표데이터인것을 판별
                else if (R_Data.Substring(0, 8) == "Rotation")
                {
                    // tmp 에 R_Data 값을 넣음
                    string tmp = R_Data.Replace("Rotation", "");
                    // rot 배열에 / 를 간격으로 저장
                    string[] rot = tmp.Split('/');
                    Debug.Log(rot[0] + " " + rot[1] + " " + rot[2]);
                    // S_Move enemy_rot() 를 호출해 받아온 데이터를 Vecort 값으로 변환하여 전송 [0] = X [1] = Y [2] = Z
                    C_Move.Instance.enemy_rot(new Vector3(float.Parse(rot[0]), float.Parse(rot[1]), float.Parse(rot[2])));
                }
            }
        }
    }

    // 데이터를 보내는 함수
    public void send(string S_Data)
    {
        // 데이터를 보냄
        w.WriteLine(S_Data);
        // 버퍼 비움
        w.Flush();
        // 로그 출력
        Debug.Log("전송됨");
    }

    private void Start()
    {
        connect();
    }
}
