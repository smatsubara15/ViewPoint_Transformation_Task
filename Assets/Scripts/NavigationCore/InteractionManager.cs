using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance;

    [Header("Controller Setup")]
    [Tooltip("Reference to SteamVR Hand Component (Right)")] 
    [SerializeField] Hand RightHand;
    [Tooltip("Reference to SteamVR Hand Component (Left)")] 
    [SerializeField] Hand LeftHand;
    [Tooltip("If true, we will use controller click as a confirmation upon collision based triggers")] 
    public bool ControllerConfirm = true;

    [Header("Debug Mode")]
    public bool Debug = true;
    bool ControllerMovement = true;
    
    Player player;

    public delegate void PinchClickedEvent(bool state);
    public PinchClickedEvent PinchClicked; //subscribed by ArrivalCollisionCheck, ArrowCollisionCheck, StartingConfirmation
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        if (player == null) player = Player.instance;
        ControllerMovement = Debug;
    }

    // Update is called once per frame
    void Update()
    {
        if (RightHand == null || !RightHand.enabled) return;
        if (LeftHand == null || !LeftHand.enabled) return;

        if (XRTK.ControllerStats.getPinch(RightHand))
        {
            if (PinchClicked != null) PinchClicked(true);
        }
        if (XRTK.ControllerStats.getPinchUp(RightHand))
        {
            if (PinchClicked != null) PinchClicked(false);
        }
        if (XRTK.ControllerStats.getPinch(LeftHand)){
            if (PinchClicked != null) PinchClicked(true);
        }
        if (XRTK.ControllerStats.getPinchUp(LeftHand))
        {
            if (PinchClicked != null) PinchClicked(false);
        }
        if (ControllerMovement && Debug)
        {
            if (XRTK.ControllerStats.getGrip(RightHand))
            {
                var dir = XRTK.ControllerStats.getControllerRotation(RightHand) * Vector3.forward;
                var XZdir = new Vector3(dir.x, 0, dir.z);
                // Debug.Log(dir);
                player.transform.Translate(XZdir * 0.01f);
            }
            if (XRTK.ControllerStats.getGrip(LeftHand))
            {
                var dir = XRTK.ControllerStats.getControllerRotation(LeftHand) * Vector3.forward;
                var XZdir = new Vector3(dir.x, 0, dir.z);
                // Debug.Log(dir);
                player.transform.Translate(XZdir * 0.01f);
            }
        }
    }

}
