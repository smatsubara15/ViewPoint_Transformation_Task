using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class CameraMarker : MonoBehaviour {
	public static CameraMarker instance;
	void Start () {
		if (instance == null){
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
