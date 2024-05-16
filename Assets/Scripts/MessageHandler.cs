using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MessageHandler : MonoBehaviour
{
    [SerializeField] Timer Timer;
    public string GetMessage(string option)
    {
        string message = "";
        if (option == "direction")
        {
            Timer.activateGazeTimer();
            if(Timer.testGaze())
            {
                message = "Stop looking there. Focus on the work you need to do.";
            }
        }

        if (option == "input")
        {
            message = "I notice you aren't working. Focus on the work you need to do.";
        }
        if(option == "position")
        {
            message = "Please return to your work station.";
        }
        return message;
    }
}
