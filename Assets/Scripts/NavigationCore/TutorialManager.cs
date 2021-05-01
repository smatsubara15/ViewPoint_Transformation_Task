using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class TutorialManager : MonoBehaviour
{
    TrackPlayer logManager;
    InteractionManager interactionManager;
    [Header("Object Setup")]
    [Tooltip("The arrival sphere will appear after user successfully test the controller feature")]
    [SerializeField] GameObject ArrivalSphere;
    [Tooltip("Setup Message for clicking tutorial")] 
    [SerializeField] string MessageBefore = "Please Click the Bubbles with your controller upon touching them.";
    [Tooltip("Setup Message for next phase")]
    [SerializeField] string MessageAfter = "Great. Please walk to the Sphere and click it to proceed to next level.";
    [Header("Reference")]
    [Tooltip("Reference to instruction text.")]
    [SerializeField] TextMeshPro InstructionText;
    private BubbleClick[] bubbles;
    private int activeBubbles;
    
    private void Start()
    {
        bubbles = FindObjectsOfType<BubbleClick>();
        ArrivalSphere.SetActive(false);
        InstructionText.text = MessageBefore;

        interactionManager = InteractionManager.instance;
        interactionManager.PinchClicked += PinchClickDetected;
    }
    private void OnDestroy()
    {
        interactionManager.PinchClicked -= PinchClickDetected;
    }

    void PinchClickDetected(bool state)
    {
        if (!state)
        {
            return;
        }
        Invoke("CheckBubbleStatus", 0.3f);
    }
    void CheckBubbleStatus()
    {
        activeBubbles = bubbles.Length;
        foreach (var bubble in bubbles)
        {
            if (bubble.transform.gameObject.activeSelf == false)
            {
                activeBubbles--;
            }
        }
        if (activeBubbles == 0)
        {
            InstructionText.text = MessageAfter;
            ArrivalSphere.SetActive(true);
        }
    }

}
