using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using UnityEngine;

public class Ready2PlayScript_20240301 : MonoBehaviour
{
    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private TcpClient connectedTcpClient;

    public bool isReady2Play1stTime = false;
    public bool isReady2Play2ndTime = false;
    public float dataFromArduino_float = 0.0f;

    public GameObject xrOrigin;
    public GameObject TableZoneAnchor;
    public GameObject AnalyzeZoneAnchor;

    public GameObject GameObjectManager;
    //private SwC_CheckCueSpin _swCCheckCueSpin;
    private CheckCueSpinScript_20240301 _swCCheckCueSpin2;

    private const int SERVER_PORT = 6000;

    // Start is called before the first frame update
    void Start()
    {
        // Block of code for TCP/IP Server
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncomingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();

        _swCCheckCueSpin2 = GameObjectManager.GetComponent<CheckCueSpinScript_20240301>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Dynamic Bool: On Value Changed
    public void OnValueChanged_Ready2Play(bool toggleIsOn)
    {
        if (toggleIsOn && dataFromArduino_float == 0.0f && (!isReady2Play1stTime && !isReady2Play2ndTime))
        {
            SendMessage2Client("Server: Ready2Play");
            _swCCheckCueSpin2.isReceiveDataFromClient = true;
            _swCCheckCueSpin2.SendMsgFromExternalFunction("Server: CheckGripPosition");

            // dev mode in
            // SendMessage2Client("Server: Record Motion Data");
            // dev mode out

            return;
        }

        if (toggleIsOn && dataFromArduino_float == 0.0f && isReady2Play1stTime)
        {
            SendMessage2Client("Server: Record Motion Data");
            isReady2Play1stTime = false;
            isReady2Play2ndTime = false;

            return;
        }

        if (toggleIsOn && dataFromArduino_float == 0.0f && isReady2Play2ndTime)
        {
            SendMessage2Client("Server: Record Motion Data");
            isReady2Play1stTime = false;
            isReady2Play2ndTime = false;

            return;
        }

        if (toggleIsOn && dataFromArduino_float >= 100.0f && isReady2Play1stTime)
        {
            dataFromArduino_float = 0.0f;
            isReady2Play1stTime = false;

            SendMessage2Client("Server: Save Motion Data");
            // test mode in
            // _swCCheckCueSpin2.isReceiveDataFromClient = true;
            // test mode out
            _swCCheckCueSpin2.SendMsgFromExternalFunction("Server: SaveMotionData");
            // Move from current position to tableZoneAnchor position
            Vector3 tableZoneAnchor = TableZoneAnchor.transform.position;
            xrOrigin.transform.position = tableZoneAnchor;

            return;
        }

        if (toggleIsOn && dataFromArduino_float >= 100.0f && isReady2Play2ndTime)
        {
            dataFromArduino_float = 0.0f;
            isReady2Play2ndTime = false;

            SendMessage2Client("Server: Save Motion Data");
            // test mode in
            // _swCCheckCueSpin2.isReceiveDataFromClient = true;
            // test mode out
            _swCCheckCueSpin2.SendMsgFromExternalFunction("Server: SaveMotionData");
            // Reset
            dataFromArduino_float = 0.0f;
            // Move from current position to analyzeZoneAnchor position
            Vector3 analyzeZoneAnchor = AnalyzeZoneAnchor.transform.position;
            xrOrigin.transform.position = analyzeZoneAnchor;

            return;
        }
    }

    void ListenForIncomingRequests()
    {
        try
        {
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), SERVER_PORT);
            tcpListener.Start();
            Debug.Log("Server " + SERVER_PORT + " is listening: SwC_Ready2Play");
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incomingData = new byte[length];
                            Array.Copy(bytes, 0, incomingData, 0, length);
                            string clientMessage = Encoding.ASCII.GetString(incomingData);                            
                            if (clientMessage == "Client: Ready2Play")
                            {
                                Debug.Log("Client: Ready2Play");
                            }
                            else if (clientMessage == "Client: Save Motion Data")
                            {
                                Debug.Log("Client: Save Motion Data");
                            }
                            else if (clientMessage == "Client: Record Motion Data")
                            {
                                Debug.Log("Client: Record Motion Data");
                            }
                            else
                            {
                                Debug.Log("Client: " + clientMessage);
                            }
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }


    void SendMessage2Client(string serverMessage)
    {
        if (connectedTcpClient == null)
        {
            return;
        }

        try
        {
            NetworkStream stream = connectedTcpClient.GetStream();
            if (stream.CanWrite)
            {
                byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage);
                stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                Debug.Log("Server: " + serverMessage);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    private void OnApplicationQuit()
    {
        if (tcpListenerThread != null)
        {
            tcpListenerThread.Abort();
        }
        if (connectedTcpClient != null)
        {
            connectedTcpClient.Close();
        }
        if (tcpListener != null)
        {
            tcpListener.Stop();
        }
    }

    void OnDestroy()
    {
        if (tcpListenerThread != null)
        {
            tcpListenerThread.Abort();
        }
        if (connectedTcpClient != null)
        {
            connectedTcpClient.Close();
        }
        if (tcpListener != null)
        {
            tcpListener.Stop();
        }
    }
}

