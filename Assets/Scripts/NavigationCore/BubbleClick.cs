using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class BubbleClick : MonoBehaviour
{
    TrackPlayer logManager;
    InteractionManager interactionManager;
    [Header("Logging Setup")]
    [SerializeField] bool LogClickingActivity = false;
    private bool triggered = false;
    private bool controllerTouching = false;
    private bool pinchClicked = false;
    void Start()
    {
        logManager = TrackPlayer.instance;
        interactionManager = InteractionManager.instance;

        interactionManager.PinchClicked += PinchClickDetected;
    }
    private void OnDestroy()
    {
        interactionManager.PinchClicked -= PinchClickDetected;
    }

    // Update is called once per frame
    void Update()
    {
        if (controllerTouching && pinchClicked)
        {
            //level start confirmation is required to be able to trigger arrival
            if (triggered == false)
            {
                if (LogClickingActivity)
                {
                    logManager.WriteCustomInfo("Tutorial Bubble: " + transform.name + " Clicked");
                }
                triggered = true; //extra safe to make sure if doesn't accidentally trigger twice
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "GameController")
        {
            controllerTouching = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "GameController")
        {
            controllerTouching = false;
        }
    }
    void PinchClickDetected(bool state)
    {
        pinchClicked = state;
    }
}
