using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UdpReceiveScript : MonoBehaviour
{
    private Thread _receiveThread;
    private UdpClient _udpClient;
    private const int Port = 5054;
    public bool IsReceivingEnabled = true;
    public bool IsConsolePrintingEnabled = false;
    public string ReceivedData;

    // Start is called before the first frame update
    void Start()
    {
        InitializeUdpClient();
        StartReceivingThread();
    }
    private void InitializeUdpClient()
    {
        _udpClient = new UdpClient(Port);
    }

    private void StartReceivingThread()
    {
        _receiveThread = new Thread(new ThreadStart(ReceiveData));
        _receiveThread.IsBackground = true;
        _receiveThread.Start();
    }

    private void ReceiveData()
    {
        while (IsReceivingEnabled)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataBytes = _udpClient.Receive(ref anyIP);
                ReceivedData = Encoding.UTF8.GetString(dataBytes);

                if (IsConsolePrintingEnabled)
                {
                    Debug.Log(ReceivedData);
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
            }
        }
    }
    private void OnDestroy()
    {
        _receiveThread.Abort();
        _udpClient.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
