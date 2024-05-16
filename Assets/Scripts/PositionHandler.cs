using MixedReality.Toolkit.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PositionHandler : MonoBehaviour
{
    private Camera cam;
    private Vector3 position;
    [SerializeField] private TextToSpeechHandler handler;
    [SerializeField] private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        position = cam.transform.position;
        if(Mathf.Abs(position.x) > 1.5 || Mathf.Abs(position.z) > 1.5)
        {
            // For synchronizatrion purposes, gaze timer is not active when way from work station
            timer.resetGazeTimers();

            timer.activatePosTimer();
            if(timer.testPos())
            {
                handler.Speak();
            }
        }
        else
        {
            timer.resetPosTimers();
        }
    }
}
