using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckCueSpinScript_20240301 : MonoBehaviour
{
    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private TcpClient connectedTcpClient;

    public bool isReady2CheckGripPosition = false;
    public bool isReady2CheckCueSpin = false;

    public int countPlayTimes = 0;
    public bool isReceiveDataFromClient = false;

    public GameObject GameObjectManager;
    private Ready2PlayScript_20240301 _swCReady2Play;
    public SerialPortCommunicationScript_F arduinoDevScript;
    private NetworkStream _stream;

    // Start is called before the first frame update
    void Start()
    {
        // Start TcpServer background thread
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncomingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();

        _swCReady2Play = GameObjectManager.GetComponent<Ready2PlayScript_20240301>();
    }

    void ListenForIncomingRequests()
    {
        try
        {
            tcpListener = new TcpListener(IPAddress.Any, 8052);
            tcpListener.Start();
            Debug.Log("Server 8052 is listening: SwC_CheckCueSpin");

            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    ProcessRequest(connectedTcpClient);
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }

    void ProcessRequest(TcpClient client)
    {
        using (NetworkStream stream = client.GetStream())
        {
            byte[] bytes = new byte[1024];
            int length;
            while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                var incomingData = new byte[length];
                Array.Copy(bytes, 0, incomingData, 0, length);
                string clientMessage = Encoding.ASCII.GetString(incomingData);
                Debug.Log("client message received as: " + clientMessage);

                if (countPlayTimes < 2 && clientMessage == "Client: isGrippingCorrectly" && isReceiveDataFromClient)
                {
                    // SendResponseToClient(stream, "Server: isGrippingCorrectly");
                    countPlayTimes = countPlayTimes + 1;

                    if (countPlayTimes == 1) 
                    {
                        _swCReady2Play.isReady2Play1stTime = true;
                        Debug.Log("isReady2Play1stTime: " + _swCReady2Play.isReady2Play1stTime);
                    }
                    else if (countPlayTimes == 2)
                    {
                        _swCReady2Play.isReady2Play2ndTime = true;
                        Debug.Log("isReady2Play2ndTime: " + _swCReady2Play.isReady2Play2ndTime);
                    }
                    
                    isReceiveDataFromClient = false;

                    _swCReady2Play.OnValueChanged_Ready2Play(true);
                }

                switch (clientMessage)
                {
                    case "Client: Top Spin":
                        if (isReceiveDataFromClient)
                        {
                            Debug.Log("Client: Top Spin");
                        }
                        break;
                    case "Client: No Spin":
                        if (isReceiveDataFromClient)
                        {
                            Debug.Log("Client: No Spin");
                        }
                        break;
                    case "Client: Back Spin":
                        if (isReceiveDataFromClient)
                        {
                            Debug.Log("Client: Back Spin");
                        }
                        break;
                }

                // if (!isReceiveDataFromClient)
                // {
                //     Debug.Log("Duplicate Message: " + clientMessage);
                //     SendResponseToClient(stream, "Server: Duplicate Message");
                // }
            }
        }
    }

    public void SendMsgFromExternalFunction(string msg)
    {
        if (connectedTcpClient == null)
        {
            return;
        }

        SendResponseToClient(_stream, msg);
    }

    void SendResponseToClient(NetworkStream stream, string serverResponse)
    {
        if (string.IsNullOrEmpty(serverResponse) || connectedTcpClient == null)
        {
            return;
        }
        try
        {
            stream = connectedTcpClient.GetStream();
            if (stream == null)
            {
                return;
            }
            byte[] msg = Encoding.ASCII.GetBytes(serverResponse);
            stream.Write(msg, 0, msg.Length);
            Debug.Log("Server: " + serverResponse);
        }
        catch (SocketException socketException)
        {
            Debug.LogFormat("SocketException {0}", socketException.ToString());
        }
    }

    private void OnDestroy()
    {
        if (connectedTcpClient != null)
        {
            connectedTcpClient.Close();
        }
        if (tcpListener != null)
        {
            tcpListener.Stop();
        }
        if (tcpListenerThread != null)
        {
            tcpListenerThread.Abort();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // A Button Pressed - Controller - Hit Cue Ball
    public void aButtonPressed(InputAction.CallbackContext context)
    {
        // if (context.started)
        // {
        //     Debug.Log("[Action: A Button Pressed] - Started");
        // }

        if (context.performed)
        {
            // Debug.Log("arduinoDevScript.OnValueChanged_SerialPort(true);");
            arduinoDevScript.OnValueChanged_SerialPort(true);
        }
        // if (context.canceled)
        // {
        //     Debug.Log("[Action: A Button Pressed] - Canceled");
        // }
    }
}
