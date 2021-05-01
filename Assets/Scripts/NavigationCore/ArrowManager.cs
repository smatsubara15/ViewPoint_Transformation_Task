using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class ArrowManager : MonoBehaviour
{
    //public properties
    public static ArrowManager instance;
    [Header("Setup Starting Points")]
    [Tooltip("Set up the starting points for each level here, the instruction avatar will show at the positions of these points")]
    public GameObject[] StartingPoints; // predefined starting point objects, with given position for each
    
    [Header("Setup Instruction Avatar")]
    [Tooltip("Reference to the Arrow Avatar object that will be used as instruction avatar")]
    [SerializeField] GameObject ArrowAvatar; // any prefab object that can represent the looking of an arrow
    [Tooltip("The text object that will be used to show instructions. Accessible to be manipulated in run time.")] 
    public GameObject InstructionText;
    [Tooltip("Reference to footprint object. Can be replaced.")] public GameObject FootPrint;

    //private properties
    private ArrowCollisionCheck arrowCollider; // a sub-script in charge of actual trigger check
    private TextMeshPro mText;
    [SerializeField] bool debug = false;
    void Awake(){
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        arrowCollider = ArrowAvatar.GetComponent<ArrowCollisionCheck>();
        ArrowAvatar.SetActive(false); //ArrowAvatar not visible at initialization
        SetTextStatus(false);
        mText = InstructionText.GetComponentInChildren<TextMeshPro>();
    }

    //level manager call "Activate(int)" function by giving an index value
    //to reference one of the start point objects that arrow avatar can teleport to
    public void Activate(int arrowPosIndex){
        if (StartingPoints[arrowPosIndex] != null){
            // Debug.Log(arrowPosIndex + " arrow pos");
            ArrowAvatar.transform.position = new Vector3(StartingPoints[arrowPosIndex].transform.position.x, 1.0f, StartingPoints[arrowPosIndex].transform.position.z);
            FootPrint.transform.position = new Vector3(StartingPoints[arrowPosIndex].transform.position.x, 0.005f, StartingPoints[arrowPosIndex].transform.position.z);
            FootPrint.transform.rotation = StartingPoints[arrowPosIndex].transform.rotation;
            
            // Debug.Log(ArrowAvatar.transform.position);
            ArrowAvatar.SetActive(true);
            SetTextStatus(true);
            mText.text = "Please stand on the footprint and click your trigger.";
        } else {
            Debug.Log("Arrow Activation Failed");
        }
    }
    public void Reset(){
        ArrowAvatar.transform.position = new Vector3(0, 100, 0);
        ArrowAvatar.SetActive(false); // turn off avatar to be not active in the scene
       
        // Debug.Log("arrow reset");
        SetTextStatus(false);
    }
    public void SetTextContent(string sentence){
        mText.text = sentence;
    }
    private void SetTextStatus(bool status){
        InstructionText.transform.position = ArrowAvatar.transform.position;
        InstructionText.SetActive(status);
    }
    private IEnumerator ResetFootPrint(float waitTime){
        yield return new WaitForSecondsRealtime(waitTime);
        FootPrint.transform.position = new Vector3(0, 100, 0);
    }
    public void TriggerFootPrint(){
        StartCoroutine(ResetFootPrint(3));
    }

}
