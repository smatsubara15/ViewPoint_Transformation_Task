using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class TimeManager : MonoBehaviour {
    public static TimeManager instance;
    
    [Header("Timer Setup")]
	[Tooltip("Unit: Seconds. Set up time limit for navi test. Not valid during tutorial and learning phase")]
    public float TimeLimit = 45.0f;
    [Header("Display Setup")]
    [Tooltip("Whether or not to show time on monitor (not in VR headset, participant won't see it)")]
    public bool DisplayTime = true;
    [Tooltip("Reference text object")] public Text TimeText;
	[System.NonSerialized]public bool learningPhasePause = false;

    private float timeCount;
    private bool completed = false;
    private bool shouldCount = false;
    private int successStatus = 0; //0 for unknown, 1 for success, 2 for fail;
    private string objectName = "";
    NaviManager naviManager;
    TrackPlayer logManager;
    //IntermissionManager intermissionManager;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start () {
		timeCount = TimeLimit;
		naviManager = NaviManager.instance;
		logManager = TrackPlayer.instance;
        //intermissionManager = IntermissionManager.instance;
	}
	void Update () {
		if (shouldCount){
			CountDown();
		}
	}
	private void CountDown(){
		TimeText.text = "Current Level = " + (naviManager.CurrentLevel - 2) + " | Time left: " + timeCount.ToString();
		if(timeCount > 0)
		{
			timeCount -= Time.deltaTime;

			if(completed)
			{
                if (successStatus == 1) {
                    Debug.Log("Trial successful with correct destination");
                    Debug.Log("Time Remaining: " + timeCount);
                    logManager.WriteLevelFinishInfo(completed, TimeLimit - timeCount, objectName);
                } else if (successStatus == 2)
                {
                    Debug.Log("Trial unsuccessful with wrong destination");
                    Debug.Log("Time Remaining: " + timeCount);
                    logManager.WriteLevelFinishInfo(!completed, TimeLimit - timeCount, objectName);
                } else
                {
                    Debug.Log("Error. SuccessStatus unknown " + successStatus);
                }

                if (naviManager.CurrentLevel == 2)
                {
                    Reset();
                    naviManager.ChooseLevel();
                    naviManager.UnloadLevel();
                    learningPhasePause = true;
                    WriteTimeDisplay("Click button above to officially start navi test.");
                    Debug.Log("pause");
                }
                else if (naviManager.CurrentLevel < 2) {
                    Reset();
                    naviManager.ChooseLevel();
                    naviManager.UnloadLevel();
                    StartCoroutine(NextLevel(3.0f));
                }
                else { 
					Reset();
					naviManager.ChooseLevel();
                    naviManager.UnloadLevel();

                    naviManager.PrepareLevel();
                    //intermissionManager.ActivatePoints();
                }
			}
		}
		else
		{
			Debug.Log("Trial unsuccessful. Time Out");
			logManager.WriteLevelFinishInfo(completed, TimeLimit, objectName);
			Reset();
			naviManager.ChooseLevel();
			naviManager.UnloadLevel();

            naviManager.PrepareLevel();
            //intermissionManager.ActivatePoints();
        }
	}
	public void Reset(){
		shouldCount = false;
		completed = false;
        successStatus = 0;
		timeCount = TimeLimit;
        objectName = "";
		DeactivateDisplay();
	}
	public void Run(){
		// timeCount = TimeLimit;
		shouldCount = true;
        Debug.Log("Timer starts");
        if (DisplayTime)
        {
            ActivateDisplay();
        }
	}
    public void Complete(bool success, string name)
    {
        completed = true;
        if (success)
        {
            successStatus = 1;
        } else
        {
            successStatus = 2;
        }
        objectName = name;
    }
    public void SetTimeLimit(int limit)
    {
        timeCount = limit;
    }
    public void ProceedLearningPause()
    {
        StartCoroutine(NextLevel(3.0f));
        learningPhasePause = false;
        DeactivateDisplay();
    }
	public void WriteTimeDisplay(string message){
		TimeText.text = message;
	}

    #region private methods
    private void ActivateDisplay(){
		TimeText.text = timeCount.ToString("N3");
	}
	private void DeactivateDisplay(){
		TimeText.text = "";
	}
	private IEnumerator NextLevel(float waitTime)
    {

        yield return new WaitForSecondsRealtime(waitTime);
		naviManager.PrepareLevel();
    }
    #endregion private methods
}
