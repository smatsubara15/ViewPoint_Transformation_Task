using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Mengyu Chen, 2018
//For questions: mengyuchenmat@gmail.com
public class ObjectActivate : MonoBehaviour {
	[SerializeField] Transform userCamera;
	[SerializeField] private GameObject[] objects;
	[SerializeField] private float activateTime;
	private float angle;
	private Vector3 targetDir;
	private bool activated = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!activated){
			targetDir = transform.position - userCamera.position;
            angle = Vector3.SignedAngle(targetDir, userCamera.rotation * Vector3.forward, Vector3.up);
			if (Time.timeSinceLevelLoad >= activateTime && (angle > 75 || angle < -75) ){
				for (int i = 0; i < objects.Length; i ++){
					objects[i].SetActive(true);
				}
				activated = true;
			}
		}
		
	}
}
