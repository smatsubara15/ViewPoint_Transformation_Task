using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class HeightAdjuster : MonoBehaviour {

    public static HeightAdjuster instance;

    [Header("Manual Adjustment")]
    [SerializeField] GameObject VRPlayer;
    [Tooltip("Set up the value for each manual adjustment. Unit: 1f = 1 Meter")]
    public float YoffsetStep = 0.05f;
    [Tooltip("Keyboard key to move eye height up for one step.")] 
    [SerializeField] KeyCode UpKey = KeyCode.UpArrow;
    [Tooltip("Keyboard key to move eye height down for one step.")] 
    [SerializeField] KeyCode DownKey = KeyCode.DownArrow;

    [Header("Auto Adjustment")]
    [Tooltip("If true, an automatic adjustment of eye height will be applied at the very beginning")] 
    [SerializeField] bool AutoAdjust = true;
    [Tooltip("The height offset to be automatically applied when AutoAdjust is on.")] 
    [SerializeField] float AutoHeightOffset = -0.9f;
    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
    void Start()
    {
        if (AutoAdjust) VRPlayer.transform.Translate(new Vector3(0, AutoHeightOffset, 0));
    }
    void Update()
    {
        if (Input.GetKeyDown(UpKey))
        {
            if (VRPlayer.activeSelf)
            {
                VRPlayer.transform.Translate(new Vector3(0, YoffsetStep, 0));
            }
            
        }
        if (Input.GetKeyDown(DownKey))
        {
            if (VRPlayer.activeSelf)
            {
                VRPlayer.transform.Translate(new Vector3(0, -YoffsetStep, 0));
            }
        }
        
    }
    public void HeadsetUp()
    {
        if (VRPlayer.activeSelf)
        {
            VRPlayer.transform.Translate(new Vector3(0, YoffsetStep, 0));
        }
    }
    public void HeadsetDown()
    {

        if (VRPlayer.activeSelf)
        {
            VRPlayer.transform.Translate(new Vector3(0, -YoffsetStep, 0));
        }

    }
}
