using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{

    public Text obj;
    bool cando;
    string msg = "";
    // Start is called before the first frame update
    void Start()
    {
        cando = false;
    }

    // Update is called once per frame
    void Update()
    {
         if (cando)
        {
            obj.text = msg;
        }
    }

    public void changeText()
    {
        Thread th = new Thread(change);
        th.Start();
    }

    public void change()
    {
        IPAddress ipAddress = IPAddress.Parse("192.168.1.108");
        Debug.Log(ipAddress.ToString());
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 12345);

        try
        {
            while (true)
            {
                cando = false;
                Socket handler = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                handler.Connect(remoteEP);
                Debug.Log("Connected.....");
                byte[] bytes = new byte[1024];
                int size = handler.Receive(bytes);
                msg = Encoding.ASCII.GetString(bytes);
                cando = true;
                Debug.Log("Received");
                handler.Close();
                Debug.Log("Disconnected");
            }
        }
        catch (Exception e)
        {
            Debug.Log("In Catch");
            Debug.Log("Excepption occured: " + e);
        }
    }

}
