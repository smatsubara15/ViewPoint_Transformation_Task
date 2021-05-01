using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class CanvasDirection : MonoBehaviour {

	Transform Target;
	[SerializeField] bool lockY = false;
	[SerializeField] bool lockX = false;
	[SerializeField] bool lockZ = true;
	// Canvas canvas;
	void Start () {
		// canvas = GetComponent<Canvas>();
		//Target = CameraMarker.instance.transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(transform.position + CameraMarker.instance.transform.rotation * Vector3.forward,
            CameraMarker.instance.transform.rotation * Vector3.up);
		Vector3 eulerAngles = transform.eulerAngles;
		if (lockZ) eulerAngles.z = 0;
		if (lockX) eulerAngles.x = 0;
		if (lockY) eulerAngles.y = 0;
		transform.eulerAngles = eulerAngles;
	}
}
