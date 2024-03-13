using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class SerialPortCommunicationScript : MonoBehaviour
{
    private const string PortName = "COM3";
    private const int BaudRate = 9600;
    private SerialPort _serialPort;
    private Ready2PlayScript_20240301 _swCReady2Play;
    private int countPlayTimes = 0;

    public GameObject GameObjectManager;
    public Toggle iconToggleElementsMotion;
    public Toggle iconToggleElementsReady2Play;

    // Start is called before the first frame update
    void Start()
    {
        InitializeSerialPort();
        InitializeSwCReady2Play();
    }

    private void InitializeSwCReady2Play()
    {
        _swCReady2Play = GameObjectManager.GetComponent<Ready2PlayScript_20240301>();
    }

    private void InitializeSerialPort()
    {
        _serialPort = new SerialPort(PortName, BaudRate);
        OpenSerialPort();
    }

    private void OpenSerialPort()
    {
        if (!_serialPort.IsOpen)
        {
            _serialPort.Open(); // Open the serial port
            _serialPort.ReadTimeout = 500; // Set the read timeout to 500 milliseconds
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_serialPort.IsOpen)
        {
            ReadDataFromSerialPort();
        }
    }

    private void ReadDataFromSerialPort()
    {
        try
        {
            string dataFromArduino = _serialPort.ReadLine();
            Debug.Log($"Data Received: {dataFromArduino}");

            if (float.TryParse(dataFromArduino, out float dataFromArduinoFloat) && dataFromArduinoFloat >= 100.0f)
            {
                if (countPlayTimes < 2)
                {
                    //Production Mode
                    //_swCReady2Play.dataFromArduino_float = dataFromArduinoFloat;
                    //Development Mode
                    _swCReady2Play.dataFromArduino_float = 101.0f;

                    if (countPlayTimes == 0)
                    {
                        _swCReady2Play.isReady2Play1stTime = true;
                    }

                    if (countPlayTimes == 1)
                    {
                        _swCReady2Play.isReady2Play2ndTime = true;
                    }

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
                }
            }
            // Process the data as needed
        }
        catch (System.TimeoutException)
        {
            // Handle timeout exceptions    
        }
    }

    void OnDisable()
    {
        CloseSerialPort();
    }

    private void CloseSerialPort()
    {
        if (_serialPort != null && _serialPort.IsOpen)
        {
            _serialPort.Close(); // Close the serial port when the script is disabled
        }
    }
}
