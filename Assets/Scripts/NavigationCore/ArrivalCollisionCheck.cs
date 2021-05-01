using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class ArrivalCollisionCheck : MonoBehaviour {

	NaviManager naviManager;
    FadeManager fadeManager;
    TrackPlayer logManager;
    InteractionManager interactionManager;

    [Header("Arrival Settings")]
    [Tooltip("Setup the arrival point name. It left empty, its object name will be used.")]
    [SerializeField] string ArrivalPointName = "";
    [Tooltip("Distance threshold that decides how close the user need to be with the target can count as a successful trial. " +
        "For example, if the value is 0.5 then the user needs to be inside the 0.5 meter radius of the target object to be considered a successful trial")]
    [SerializeField] float ArrivalDistance = 0.5f;
    //[Tooltip("Typically, we only allow one true target, which will result in a successful trial.")]
    //[SerializeField] bool TrueTarget = false;
    //[Tooltip("A starting object may not be the arrival target.")] 
    //[SerializeField] bool StartingObject = false;

    [Header("Debug")]
    [Tooltip("For debug purpose.")] 
    [SerializeField] bool debug = false;

    private GameObject target;
    private GameObject user;
    private bool triggered = false;
    private bool controllerTouching = false;
    private bool pinchClicked = false;
    void Start(){
        naviManager = NaviManager.instance;
        fadeManager = FadeManager.instance;
        logManager = TrackPlayer.instance;
        interactionManager = InteractionManager.instance;

        //check if it has starting confirmation or not
        //if (GetComponent<StartingConfirmation>())
        //{
        //    StartingObject = true;
        //}

        target = GameObject.FindGameObjectWithTag("Target");
        user = FindObjectOfType<CameraMarker>().gameObject;

        //event subscription from interaction manager
        interactionManager.PinchClicked += PinchClickDetected;
    }
    private void OnDestroy()
    {
        interactionManager.PinchClicked -= PinchClickDetected;
    }
    void Update(){
        if (!naviManager.LevelStartConfirmed)
        {
            return;
        }
        if (controllerTouching && pinchClicked){
            //level start confirmation is required to be able to trigger arrival
            if (triggered == false) {
                //fadeManager.QuickFade();
                if (ArrivalPointName == "")
                {
                    ArrivalPointName = transform.name;
                }
                Debug.Log(ArrivalPointName + " arrival triggered");

                var dist = Vector3.Distance(target.transform.position, user.transform.position);
                Debug.Log("Distance to Expected Destination: " + dist);
                logManager.WriteCustomInfo("Distance to Expected Destination: " + dist);
                if (dist <= ArrivalDistance)
                {
                    naviManager.CompleteMaze(true, ArrivalPointName);
                } else
                {
                    naviManager.CompleteMaze(false, ArrivalPointName);
                }
                triggered = true; //extra safe to make sure if doesn't accidentally trigger twice
            }
        }
    }
	void OnTriggerEnter(Collider collider){
        if (collider.tag == "GameController"){
            controllerTouching = true;
        }
    }
    void OnTriggerExit(Collider collider){
        if (collider.tag == "GameController"){
            controllerTouching = false;
        }
    }
    void PinchClickDetected(bool state)
    {
        pinchClicked = state;
    }
}
