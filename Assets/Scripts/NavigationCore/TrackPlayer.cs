using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;
//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class TrackPlayer : MonoBehaviour
{
    static public TrackPlayer instance;
    NaviManager naviManager;
    [Header("Data Recording Setup")]
    [Tooltip("Setup the object(VR camera) that needs tracking and data recording")]
    public Transform PlayerTransform;
    [Tooltip("Setup recording interval. N = every N frames. Example: Every 20 frames " +
        "= 0.4 second based on 50 frames / second setup. " +
        "50 frames = One data entry every 1 second. Minimum value of interval is 1, " +
        "which means record data every frame (about 50 data entries per second)")]
    public uint RecordInterval; // every N frames

    [Header("TXT Output")]
    [Tooltip("If ON, a TXT file will be created with file name: Participant_ID_Name_Date.txt")]
    [SerializeField] bool TXTOutput = true;
    StreamWriter streamWriter;
    [Header("CSV Output")]
    [Tooltip("If ON, a CSV file will be created with file name: Participant_ID_Name_Date.csv")]
    [SerializeField] bool CSVOutput = true;
    StreamWriter CSVWriter;
    [HideInInspector] public bool ShouldRecord = false; // called by other manager
    [HideInInspector] public string FILE_NAME = "default.txt"; //file name will be set by function call
    
    private int counter;
    private string username;
    private string gender;
    private string date;
    private string participantID;

    private List<string[]> rowData = new List<string[]>();
    void Awake(){
        if (instance == null){
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        naviManager = NaviManager.instance;

        //make sure record interval is no less than 1
        RecordInterval = (RecordInterval < 1) ? 1 : RecordInterval;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ShouldRecord){
            RecordPose();
        }
    }
    private void OnDestroy()
    {
        if (CSVOutput)
        {
            WriteCSVData();
        }
    }
    private void RecordPose(){
        counter ++;
        if (counter > int.MaxValue / 2) {counter = 1;}
        
        if (counter % RecordInterval == 0){
            // code for data collection
            Vector3 playerPos = PlayerTransform.position;
            Quaternion playerRot = PlayerTransform.rotation;
            if (TXTOutput)
            {
                streamWriter.WriteLine(Time.time + ": " + playerPos.x + "," + playerPos.z + "," + playerPos.y + "," + playerRot.eulerAngles.y);
            }
            //record in CSV format
            string[] rowDataPose = new string[8];
            rowDataPose[0] = participantID;
            rowDataPose[1] = gender;
            rowDataPose[2] = (naviManager.CurrentLevel - 2).ToString();
            rowDataPose[3] = Time.time.ToString();
            rowDataPose[4] = playerPos.x.ToString();
            rowDataPose[5] = playerPos.y.ToString();
            rowDataPose[6] = playerPos.z.ToString();
            rowDataPose[7] = playerRot.eulerAngles.y.ToString();
            rowData.Add(rowDataPose);
            WriteCSVData();
            // Debug.Log("sss");
        }
    }
    //public methods, will be called by other managers
    public void SetFileName(){
        if (TXTOutput)
        {
            //initialize streamwriter
            streamWriter = File.AppendText(FILE_NAME + ".txt");
            streamWriter.AutoFlush = true;
            //write some meta info
            streamWriter.WriteLine("Participant Name: " + username);
            streamWriter.WriteLine("Participant Gender: " + gender);
            streamWriter.WriteLine("Participant ID: " + participantID);
            streamWriter.WriteLine("Test Date: " + date);
        }
        if (CSVOutput)
        {
            //create CSV file
            CSVWriter = File.AppendText(FILE_NAME + ".csv");
            CSVWriter.AutoFlush = true;
            string[] rowDataPose = new string[8];
            rowDataPose[0] = "Participant ID";
            rowDataPose[1] = "gender";
            rowDataPose[2] = "Level";
            rowDataPose[3] = "Time";
            rowDataPose[4] = "PosX";
            rowDataPose[5] = "PosY";
            rowDataPose[6] = "PosZ";
            rowDataPose[7] = "Facing Angle";
            rowData.Add(rowDataPose);
            WriteCSVData();
        }
    }

    //call by UI manager
    public void WriteModeInfo(){
        if (!TXTOutput)
        {
            return;
        }
        if (streamWriter == null){
            Debug.LogError("No stream writer found.");
        }
        streamWriter.WriteLine("Navi Mode: " + naviManager.CurrentMode);
    }
    //call by mazeManager during level preparation phase
    public void WriteLevelInfo(){
        if (!TXTOutput)
        {
            return;
        }
        if (streamWriter == null){
            Debug.LogError("No stream writer found.");
        }
        streamWriter.WriteLine("Trail No.:" + (naviManager.CurrentLevel - 2) + " Begins");
    }
    //call by starting confirmation
    public void WriteLevelStartingInfo(string objectname)
    {
        if (!TXTOutput)
        {
            return;
        }
        if (streamWriter == null)
        {
            Debug.LogError("No stream writer found.");
        }
        streamWriter.WriteLine("User confirmed on object: " + objectname + ", this trail officially starts.");
    }
    //call by time manager when level finishes
    public void WriteLevelFinishInfo(bool status, float timeSpent, string objectName){
        
        if (TXTOutput)
        {
            if (streamWriter == null)
            {
                Debug.LogError("No stream writer found.");
            }
            string message;
            if (status)
            {
                message = "Success object: " + objectName ;
            }
            else
            {
                message = "Fail at object: " + objectName;
            }
            streamWriter.WriteLine("Trail Result: " + message + " | Time Spent: " + timeSpent);

            streamWriter.WriteLine("Trail No.:" + (naviManager.CurrentLevel - 2) + " Ends");
        }
        //write a csv file at the end of each navi and reset row data
        if (CSVOutput) WriteCSVData();
    }
    private void WriteCustomCSVData(string message)
    {
        if (CSVOutput)
        {
            if (CSVWriter == null)
            {
                Debug.LogError("No CSV writer found.");
            }

            rowData.Add(new string[1] { message });
            WriteCSVData();
        }
    }
    private void WriteCustomCSVData(string[] messages)
    {
        if (CSVOutput)
        {
            if (CSVWriter == null)
            {
                Debug.LogError("No CSV writer found.");
            }
            rowData.Add(messages);
            WriteCSVData();
        }
    }
    private void WriteCSVData()
    {
        if (CSVWriter == null)
        {
            Debug.LogError("No CSV writer found.");
        }
        string[][] output = new string[rowData.Count][];
        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }
        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }
        CSVWriter.Write(sb);
        rowData.Clear();
    }
    public void WriteCustomInfo(string info){
        if (!TXTOutput)
        {
            return;
        }
        if (streamWriter == null){
            Debug.LogError("No stream writer found.");
        }
        streamWriter.WriteLine(info);
    }
    public void WritePlayerInfo(){
        if (!TXTOutput)
        {
            return;
        }
        if (streamWriter == null){
            Debug.LogError("No stream writer found.");
        }
        Vector3 playerPos = PlayerTransform.position;
        Quaternion playerRot = PlayerTransform.rotation;
        streamWriter.WriteLine(playerPos.x + "," + playerPos.z + "," + playerPos.y + "," + playerRot.eulerAngles.y);
    }
    public void Reset(){
        ShouldRecord = false;
        counter = 0;
    }
    public void Run(){
        Debug.Log("Data recording starts");
        ShouldRecord = true;
        counter = 0;
    }

    public void CreateFile(string idInput, string nameInput, string dateInput, string genderInput)
    {
        FILE_NAME = idInput + "_" + nameInput + "_" + dateInput;
        username = nameInput;
        gender = genderInput;
        date = dateInput;
        participantID = idInput;
        SetFileName();
        string logMsg = "Data file created at /YourProjectFolder/ " + participantID + "_" + name + "_" + date;
        if (TXTOutput)
        {
            Debug.Log(logMsg + ".txt");
        }
        if (CSVOutput)
        {
            Debug.Log(logMsg + ".csv");
        }
    }

}
