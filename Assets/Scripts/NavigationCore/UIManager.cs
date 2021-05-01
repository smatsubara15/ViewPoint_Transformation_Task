using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;
//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class UIManager : MonoBehaviour {
    HeightAdjuster heightAdjuster;
    LevelLauncher levelLauncher;
    TimeManager timeManager;
    NaviManager naviManager;
	TrackPlayer logManager;
    InteractionManager interactionManager;

    [Header("Input Fields")]
	[Tooltip("Input field for participant name")]
    [SerializeField]private InputField nameInput;
    [Tooltip("Input field for participant gender")] 
    [SerializeField]private InputField genderInput;
    [Tooltip("Input field for participant id")]
    [SerializeField]private InputField idInput;
    [Tooltip("Input field for test date")]
    [SerializeField]private InputField dateInput;
    [Header("Launching UI Elements")]
    [Tooltip("Setup the UI elements to be turned off after selecting maze mode")]
    [SerializeField]private GameObject[] LaunchingUIElements;

    [Header("Basic UI display params")]
    [SerializeField] float ButtonSpacing = 8.0f;

    //private properties
    private bool spatialObjectSwitch = false;
    private bool distanceModeSwitch = false;
    private bool fogModeSwitch = false;
    private bool dummyModeSwitch = false;
    Player player;
	void Start () {
        if (logManager == null) logManager = TrackPlayer.instance;
        if (player == null) player = Player.instance;
        if (heightAdjuster == null) heightAdjuster = HeightAdjuster.instance;
        if (levelLauncher == null) levelLauncher = LevelLauncher.instance;
        if (timeManager == null) timeManager = TimeManager.instance;
        if (naviManager == null) naviManager = NaviManager.instance;
        if (interactionManager == null) interactionManager = InteractionManager.instance;

        
	}

	void LateUpdate()
	{
		
	}
	
	public void ConfirmFileName(){
        logManager.CreateFile(idInput.text, nameInput.text, dateInput.text, genderInput.text);
    }
	public void DeactivateInfoRect(){
        foreach (var obj in LaunchingUIElements)
        {
            obj.SetActive(false);
        }
	}
	public void ActivateInfoRect(){
        foreach (var obj in LaunchingUIElements)
        {
            obj.SetActive(true);
        }
    }
    public void OnGUI()
    {
        
        if (timeManager.learningPhasePause)
        {
            GUILayout.BeginArea(new Rect(50, 40, 300, 500));
            if (GUILayout.Button("Click Here to Proceed to Next Level"))
            {
                timeManager.ProceedLearningPause();
            }
            GUILayout.EndArea();
            return;
        }
        
        if (NaviManager.instance.CurrentLevel >= 1)
        {
            GUILayout.BeginArea(new Rect(50, 40, 300, 500));
            if (GUILayout.Button("Headset Higher +" + heightAdjuster.YoffsetStep))
            {
                heightAdjuster.HeadsetUp();
            }
            GUILayout.Space(ButtonSpacing);
            if (GUILayout.Button("Headset Lower -" + heightAdjuster.YoffsetStep))
            {
                heightAdjuster.HeadsetDown();
            }
            GUILayout.Space(ButtonSpacing);
            
            GUILayout.Space(ButtonSpacing);
            GUILayout.Space(ButtonSpacing);
            GUILayout.Space(ButtonSpacing);
            GUILayout.Space(ButtonSpacing);

            if (interactionManager.Debug)
            {
                if (GUILayout.Button("(Debug) Controller Click"))
                {
                    if (interactionManager.PinchClicked != null)
                    {
                        interactionManager.PinchClicked(true);
                        StartCoroutine(PinchReset());
                    }
                }
            }

            GUILayout.EndArea();
        }
        
    }
    IEnumerator PinchReset()
    {
        yield return new WaitForSeconds(0.15f);
        interactionManager.PinchClicked(false);
    }
}
