using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using PimDeWitte.UnityMainThreadDispatcher;
using TMPro;


public class TCPClient : MonoBehaviour
{
    [SerializeField] private Timer timer;
   
    private TcpClient client;
    private NetworkStream stream;
    private bool isConnected = false;
    public TouchScreenKeyboard keyboard;
    private string ipString = "";
    [SerializeField] private TMP_Text ip;
    [SerializeField] private GameObject canvas;

    private bool done;


    void Start()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.PhonePad, false, false, false, false);
        done = false;
    }

    private void Update()
    {
        if(!keyboard.done)
        {
            ip.text = keyboard.text;    
            ipString = keyboard.text;
        }

        if(!done && keyboard.done)
        {
            ConnectToServer(ipString);
            done = true;
            keyboard = null;
        }
    }


    void OnDestroy()
    {
        DisconnectFromServer();
    }

    void ConnectToServer(string serverIP)
    {
        try
        {
            int serverPort = 32401;
            client = new TcpClient(serverIP, serverPort);
            stream = client.GetStream();
            isConnected = true;
            ip.text = "Connected to server.";
            canvas.SetActive(false);

            // Start a new thread to continuously receive data from the server
            Thread receiveThread = new Thread(ReceiveDataFromServer);
            receiveThread.Start();
        }
        catch (Exception e)
        {
            ip.text = "Error connecting to server " + ipString + ": " + e.Message + ". Please restart app and try again.";
        }
    }

    void DisconnectFromServer()
    {
        if (isConnected)
        {
            isConnected = false;
            stream.Close();
            client.Close();
            Debug.Log("Disconnected from server.");
        }
    }

    void SendMessageToServer(string message)
    {
        try
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Debug.Log("Sent message to server: " + message);
        }
        catch (Exception e)
        {
            Debug.LogError("Error sending message to server: " + e.Message);
        }
    }

    void ReceiveDataFromServer()
    {
        try
        {
            while (isConnected)
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    // Convert byte array to string
                    string receivedMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    // Handle received data on the main thread using UnityMainThreadDispatcher

                    UnityMainThreadDispatcher.Instance().Enqueue(() => HandleReceivedData(receivedMessage));

                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error receiving data from server: " + e.Message);
        }
    }

    void HandleReceivedData(string receivedMessage)
    {
        // Split the string by whitespace to get individual tokens
        string[] tokens = receivedMessage.Split(' ');

        // Variables to store float values
        
        

        // Loop through each token to find the ones containing "MouseTime" and "KBTime"
        foreach (string token in tokens)
        {
            if (token.StartsWith("MouseTime:"))
            {
                float mouseTimeFloat = 0f;
                // Extract the substring after "MouseTime:"
                string mouseTimeString = token.Substring("MouseTime:".Length);
                // Convert the substring to a float
                float.TryParse(mouseTimeString, out mouseTimeFloat);
                timer.SetMouseTime(mouseTimeFloat);
            }
            else if (token.StartsWith("KBTime:"))
            {
                float kbTimeFloat = 0f;
                // Extract the substring after "KBTime:"
                string kbTimeString = token.Substring("KBTime:".Length);
                // Convert the substring to a float
                float.TryParse(kbTimeString, out kbTimeFloat);
                timer.SetKBTime(kbTimeFloat);

                timer.UpdateGazeAndPosTimer();
            }
        }

        
        

      
        Debug.Log("Received data from server: " + receivedMessage);
        // Process received data here
        // Split the string, parse values, update UI, etc.
    }
}
