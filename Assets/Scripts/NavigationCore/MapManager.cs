using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    NaviManager naviManager;
    TrackPlayer logManager;
    InteractionManager interactionManager;

    [Header("Reference Setup")]
    [SerializeField] Camera MapViewCamera;
    [SerializeField] GameObject Map;

    [Header("Map Reading Parameters")]
    [Tooltip("MapReadingTime decides on how long (in seconds) user will be given to read the map before they start walk around.")]
    [SerializeField] float MapReadingTime = 7.0f;
    [Tooltip("Static Map means the map will NOT update the facing direction of the user in real time " +
        "(only showing the initial facing direction when user first see the map)")]
    [SerializeField] bool StaticMap = true;

    private GameObject mainCamera;
    private Renderer floorRenderer;
    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    void Start()
    {
        naviManager = NaviManager.instance;
        logManager = TrackPlayer.instance;
        interactionManager = InteractionManager.instance;

        mainCamera = FindObjectOfType<CameraMarker>().gameObject;
        floorRenderer = GameObject.FindGameObjectWithTag("Floor").GetComponent<Renderer>();

        Map.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //make sure the map follows the main camera all the time
        Map.transform.SetPositionAndRotation(mainCamera.transform.position, mainCamera.transform.rotation);
    }
    public void Init()
    {
        StartCoroutine(MapReading(MapReadingTime));
    }

    IEnumerator MapReading(float duration)
    {
        Map.SetActive(true);
        floorRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        if (StaticMap)
        {
            MapViewCamera.enabled = false;
        }
        logManager.WriteCustomInfo("User starts reading map for " + MapReadingTime + " seconds.");
        yield return new WaitForSecondsRealtime(duration);
        Map.SetActive(false);
        floorRenderer.enabled = true;
        if (StaticMap)
        {
            MapViewCamera.enabled = true;
        }
        naviManager.ControlledInit();
    }
    public void RevealMap(float duration)
    {
        StartCoroutine(revealMap(duration));
    }
    IEnumerator revealMap(float duration)
    {
        Map.SetActive(true);
        floorRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        if (StaticMap)
        {
            MapViewCamera.enabled = false;
        }
        yield return new WaitForSecondsRealtime(duration);
        Map.SetActive(false);
        floorRenderer.enabled = true;
        if (StaticMap)
        {
            MapViewCamera.enabled = true;
        }
    }
}
