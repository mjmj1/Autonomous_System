using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TcpSocket : MonoBehaviour
{
    TcpClient client;
    string serverIP = "10.101.36.73";
    int port = 60001;
    byte[] receivedBuffer;
    StreamReader reader;
    bool socketReady = false;
    NetworkStream stream;

    // Start is called before the first frame update
    void Start()
    {
        CheckReceive();
    }

    // Update is called once per frame
    void Update()
    {
        if(socketReady)
        {
            if(stream.DataAvailable)
            {
                receivedBuffer = new byte[100];
                stream.Read(receivedBuffer, 0, receivedBuffer.Length);  // stream에 있던 바이트배열 내려서 새로 선언한 바이트배열에 넣기
                string msg = Encoding.UTF8.GetString(receivedBuffer, 0, receivedBuffer.Length);
                if (msg.Equals("quit"))
                    UnityEditor.EditorApplication.isPlaying = false;
                    //Application.Quit();
                Debug.Log(msg);
            }
        }        
    }

    public void CheckReceive()
    {
        if (socketReady) return;
        try
        {
            client = new TcpClient(serverIP, port);
            if(client.Connected)
            {
                stream = client.GetStream();
                Debug.Log("Connect Success");
                socketReady = true;
            }
        }
        catch(Exception ex)
        {
            Debug.Log("On Client connect exception " + ex);
        }        
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    void CloseSocket()
    {
        if (!socketReady) return;

        reader.Close();
        client.Close();
        socketReady = false;
    }
}
