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
    // 클라이언트가 연결되었는지 확인
    public static bool client_connected = false;

    // 서버를 열 변수
    TcpListener server;

    // 서버의 ip,port
    public const string ip = "127.0.0.1";
    public const int port = 1007;

    // 클라이언트 객체
    static TcpClient client = null;

    // 데이터를 전송,수신 할 변수들
    public static NetworkStream ns = null;
    public static StreamWriter w = null;
    public static StreamReader r = null;

    // 서버를 여는 함수
    public void server_open()
    {
        try
        {
            // 서버 설정
            server = new TcpListener(IPAddress.Parse(ip), port);
            // 서버 시작
            server.Start();
            // 로그 출력
            Debug.Log("서버 시작됨");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        // 클라이언트 접속을 받는 listen() 호출
        listen();
    }

    public void recv()
    {
        if (client_connected == true)
        {
            // 받아온 데이터가 있는지 확인
            if (ns.DataAvailable)
            {
                // ReadLine() 을 통해 데이터를 문자열로 가져옴
                string R_Data = r.ReadLine();
                // 로그 출력
                Debug.Log(R_Data);

                // 받아온 데이터의 앞에서 3자리까지가 Pos 일시 좌표데이터인것을 판별
                if (R_Data.Substring(0, 7) == "Postion")
                {
                    // tmp 에 R_Data 값을 넣음
                    string tmp = R_Data.Replace("Postion", "");
                    // pos 배열에 / 를 간격으로 저장
                    string[] pos = tmp.Split('/');
                    Debug.Log(pos[0] + " " + pos[1] + " " + pos[2]);

                    // C_Move enemy_move() 를 호출해 받아온 데이터를 Vector 값으로 변환하여 전송 [0] = X [1] = Y [2] = Z
                    S_Move.Instance.enemy_move(new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2])));
                }
            }
        }
    }

    // 데이터를 보내는 함수
    public void send(string S_Data)
    {
        if(client_connected == true)
        {
            // 데이터를 보냄
            w.WriteLine(S_Data);
            // 버퍼 비움
            w.Flush();
            // 로그 출력
            Debug.Log("전송됨");
        }
    }

    void listen()
    {
        // 연결 시도를 비동기로 잡아줌
        server.BeginAcceptTcpClient(accept, server);
    }

    // 연결을 받아주는 함수 (비동기)
    void accept(IAsyncResult iar)
    {
        TcpListener listener = (TcpListener)iar.AsyncState;

        client = listener.EndAcceptTcpClient(iar);

        // 클라이언트에 데이터를 보내기 위한 객체 호출
        ns = client.GetStream();
        w = new StreamWriter(ns);
        r = new StreamReader(ns);

        // 클라이언트 연결 = 참
        client_connected = true;
        Debug.Log("클라이언트 연결 : " + client_connected);
    }

    private void Start()
    {
        server_open();
    }
}
