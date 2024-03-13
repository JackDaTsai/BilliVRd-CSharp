using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SerialPortCommunicationScript_F : MonoBehaviour
{
    // Block of code for TCP/IP Server
    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private TcpClient connectedTcpClient;

    // Block of code for Script, GameObject, Script, and UI Toggle Elements
    public CueTrackerScript cueTrackerScript;
    public GameObject GameObjectManager;
    private Ready2PlayScript_20240301 _swCReady2Play;
    public Toggle iconToggleElementsMotion;
    public Toggle iconToggleElementsReady2Play;
    public Toggle iconToggleElementsArduinoDev;

    // Development Mode -> is accessible from Unity Editor and CueTrackerScript
    public int countPlayTimes = 0;

    // Start is called before the first frame update
    void Start()
    {
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncomingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
        
        _swCReady2Play = GameObjectManager.GetComponent<Ready2PlayScript_20240301>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Dynamic Bool: On Value Chsanged
    public void OnValueChanged_SerialPort(bool toggleIsOn)
    {
        if (countPlayTimes < 2)
        {
            cueTrackerScript.isStandOnHittingZone = false;
            if (toggleIsOn && countPlayTimes == 0) // 1st time
            {
                _swCReady2Play.dataFromArduino_float = 101.0f;
                _swCReady2Play.isReady2Play1stTime = true;
                _swCReady2Play.OnValueChanged_Ready2Play(true);

                if (iconToggleElementsMotion.isOn == true)
                {
                    iconToggleElementsMotion.isOn = false;
                }

                if (iconToggleElementsReady2Play.isOn == true)
                {
                    iconToggleElementsReady2Play.isOn = false;
                }

                countPlayTimes = countPlayTimes + 1;

                iconToggleElementsArduinoDev.isOn = false;

                return;
            }

            // Controller Production Mode
            if (toggleIsOn && countPlayTimes == 1) // 2nd time
            {
                _swCReady2Play.dataFromArduino_float = 101.0f;
                _swCReady2Play.isReady2Play2ndTime = true;
                _swCReady2Play.OnValueChanged_Ready2Play(true);

                if (iconToggleElementsMotion.isOn == true)
                {
                    iconToggleElementsMotion.isOn = false;
                }

                if (iconToggleElementsReady2Play.isOn == true)
                {
                    iconToggleElementsReady2Play.isOn = false;
                }

                countPlayTimes = countPlayTimes + 1;

                iconToggleElementsArduinoDev.isOn = false;

                return;
            }

            // Hand Tracking Production Mode
            // if (!toggleIsOn)
            // {
            //     _swCReady2Play.dataFromArduino_float = 101.0f;
            //     _swCReady2Play.isReady2Play2ndTime = true;
            //     _swCReady2Play.OnValueChanged_Ready2Play(true);

            //     if (iconToggleElementsMotion.isOn == true)
            //     {
            //         iconToggleElementsMotion.isOn = false;
            //     }

            //     if (iconToggleElementsReady2Play.isOn == true)
            //     {
            //         iconToggleElementsReady2Play.isOn = false;
            //     }

            //     countPlayTimes = countPlayTimes + 1;
            // }
        }
    }

    void ListenForIncomingRequests()
    {
        try
        {
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
            tcpListener.Start();
            Debug.Log("Server 8000 is listening: SerialPortCommunicationScript_F");
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
                            Debug.Log("client message received as: " + clientMessage);

                            if (float.TryParse(clientMessage, out float dataFromArduinoFloat) && dataFromArduinoFloat >= 100.0f)
                            {
                                _swCReady2Play.dataFromArduino_float = 100.0f;
                                _swCReady2Play.isReady2Play1stTime = true;

                                if (iconToggleElementsMotion.isOn == true)
                                {
                                    iconToggleElementsMotion.isOn = false;
                                }

                                if (iconToggleElementsReady2Play.isOn == true)
                                {
                                    iconToggleElementsReady2Play.isOn = false;
                                }
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
