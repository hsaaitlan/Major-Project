using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO;
using System.Net;
using System;
using System.Net.Sockets;
using System.Text;

public class updateTexture : MonoBehaviour
{
    private Renderer rend;
    //private int count = 0;
    private static byte[] rcv;
    private bool cando = false;
    private static Texture2D tex;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        Thread thread = new Thread(StartServer);
        thread.Start();
        
    }

    void StartServer()
    {
        /*TcpListener listener = new TcpListener(IPAddress.Parse("192.168.1.108"), 12345);
        listener.Start();*/
        try
        {
            while (true)
            {
                TcpClient client = new TcpClient("192.168.1.108", 12345);
                NetworkStream ns = client.GetStream();

                byte[] r = new byte[16];
                int s = ns.Read(r, 0, 16);
                String leng = Encoding.ASCII.GetString(r);
                int len = Convert.ToInt32(leng);
                Debug.Log("Len: " + leng);
                //rcv = new byte[len];
                //int size = ns.Read(rcv, 0, len);    
                rcv = ReadStream(ns, len);
                if (rcv.Length == len)
                    cando = true;
                else
                    Debug.Log("Incomplete Data");
                Debug.Log("Received: " + rcv.Length);
                ns.Flush();
                client.Close();
            }
        }
        catch (Exception ee)
        {
            Debug.LogError("Exception : " + ee);
        }
    }

    byte[] ReadStream(NetworkStream ns, int len)
    {
        byte[] buffer = null;
        buffer = new byte[len];
        int byte_read = 0;
        int byte_offset = 0;
        while (byte_offset < len)
        {
            byte_read = ns.Read(buffer, byte_offset, len - byte_offset);
            int prev = byte_offset;
            byte_offset += byte_read;
            if (prev >= byte_offset)
                break;
        }
        return buffer;
    }

    /*void StartServer()
    {
        IPAddress ipAddress = IPAddress.Parse("192.168.1.108");
        Debug.Log(ipAddress.ToString());
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 12345);

       

        //Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            
            while (true)
            {
                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(remoteEP);
                Debug.Log("Connected.....");
                String data = null;
                byte[] bytes = null;
                bytes = new byte[1024];
                int size = sender.Receive(bytes);

                //Debug.Log(size);
                int val = BitConverter.ToInt32(bytes, 0);
                cando = false;
                Debug.Log(val);
                //Debug.Log("Receiving Image Array");
                rcv = new byte[val];
                //int rcvd = Receive(sender, rcv, 0, val, 10000);
                int num = val / 10;
                int rem = val % 10;
                int rcvd = 0;
                for (int i = 0; i < num; i++)
                {
                    rcvd += sender.Receive(rcv, i * 10, 10, SocketFlags.None);
                }
                rcvd += sender.Receive(rcv, num * 10, rem, SocketFlags.None);
                Debug.Log("Received: " + rcvd);
                if (rcvd == val)
                    cando = true;
                byte[] s = Encoding.ASCII.GetBytes(rcvd + "");
                sender.Send(s);
                //Debug.Log("Returned");
                //tex.LoadImage(rcv);
                //Debug.Log("Texture: " + tex.width);
                //rend.material.SetTexture("_MainTex", tex);
                *//*try
                {
                    int dataSize = 0;
                    byte[] b = new byte[1024 * 10000];
                    dataSize = sender.Receive(b);
                    if (dataSize > 0)
                    {
                        MemoryStream ms = new MemoryStream(b);
                        rcv = ms.ToArray();
                    }
                
                } catch (Exception ee)
                {
                    Debug.Log(ee);
                }
                for (int i = val - 10; i < val + 30; i++)
                {
                    Debug.Log(rcv[i] + " ");
                }*//*


                //Debug.Log("Size: "+ size + " Value " + val);

                *//*while (true)
                {
                    bytes = new byte[1024];
                    int size = sender.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, size);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }*/
                /*Debug.Log("MSG: " + data);

                String msg = "bd";
                byte[] buffer = Encoding.ASCII.GetBytes(msg);

                int sentSize = sender.Send(buffer);*//*
                sender.Close();
                Debug.Log("Disconnected");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e);
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if (cando)
        {
            cando = false;
            Debug.Log("Changing");
            rend.material.SetTexture("_MainTex", GetTexture());
        }
        /*if (startThrd)
        {
            startThrd = false;
            Thread th = new Thread(StartServer);
            th.Start();
        }*/
        /*if (Input.GetMouseButtonDown(0))
        {
            if (count == 0) {
                count = 1;
                rend.material.SetTexture("_MainTex", getTexture("Assets/image/VR_Scene/img.jpeg"));
            } else
            {
                count = 0;
                rend.material.SetTexture("_MainTex", getTexture("Assets/image/VR_Scene/imgg.jpeg"));
            }
        }*/
    }

    /*public static int Receive(Socket socket, byte[] buffer, int offset, int size, int timeout)
    {
        //Debug.Log("Entered");
        int startTickCount = Environment.TickCount;
        int received = 0;  // how many bytes is already received
        do
        {
            //Debug.Log("Do While");
            if (Environment.TickCount > startTickCount + timeout)
                throw new Exception("Timeout.");
            //Debug.Log("Try");
            try
            {
                int rcvd = socket.Receive(buffer, offset + received, size - received, SocketFlags.None);
                received += rcvd;
                byte[] send = Encoding.ASCII.GetBytes("" + rcvd);
                Debug.Log("Rcvd: " + rcvd);
                int sent = socket.Send(send);
            }
            catch (SocketException ex)
            {
                //Debug.Log("In Catch");
                if (ex.SocketErrorCode == SocketError.WouldBlock ||
                    ex.SocketErrorCode == SocketError.IOPending ||
                    ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                {
                    // socket buffer is probably empty, wait and try again
                    Thread.Sleep(30);
                }
                else
                {
                    Debug.Log(ex);
                    throw ex;
                }  // any serious error occurr
            }
        } while (received < size);
        Debug.Log("Received: " + received);
        return received;
        *//*for (int i = 0; i < 10; i++)
            Debug.Log(buffer[i]);*//*
    }*/

    public static Texture2D GetTexture()
    {
        if (rcv == null)
            Debug.Log("Null Array");
        tex = new Texture2D(1, 1);
        tex.LoadImage(rcv);
        return tex;
    }
    /*public static Texture getTexture(string filePath)
    {
        byte[] filedata;
        if (File.Exists(filePath))
        {
            filedata = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            //tex.LoadImage(filedata);
        }
        return tex;
    }*/
}
