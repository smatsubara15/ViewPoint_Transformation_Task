using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Mengyu Chen, 2018
//For questions: mengyuchenmat@gmail.com
public class RingCentralPos : MonoBehaviour {

	[SerializeField] private int ObjectID = 0;
    [SerializeField] private int numObjects = 16;
    public Vector3 center = new Vector3(0, 0, 0);
	[SerializeField] private float distanceAway = 10;
    [SerializeField] private bool directMap = true;
    [SerializeField] private bool lockYaxis = false;
    [System.NonSerialized]public Quaternion objectRotation;
    [System.NonSerialized]public Vector3 objectPosition;


    // Update is called once per frame
    void Update()
    {
        objectRotation = Quaternion.Euler(0, (float)360 / numObjects * ObjectID, 0);
        objectPosition = center + Quaternion.Euler(0, (float)360 / numObjects * ObjectID, 0) * Vector3.forward * distanceAway;
        if (directMap)
        {
            if (lockYaxis){
                transform.rotation = objectRotation;
            }
            transform.position = objectPosition;
        }
    }

}
