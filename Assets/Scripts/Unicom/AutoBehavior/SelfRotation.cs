using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Mengyu Chen, 2018
//For questions: mengyuchenmat@gmail.com
public class SelfRotation : MonoBehaviour {

	[SerializeField] private float rotationSpeed = 2.0f;
	[SerializeField] private bool rotateX;
	[SerializeField] private bool rotateY;
	[SerializeField] private bool rotateZ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (rotateX){
			transform.Rotate(Time.deltaTime * rotationSpeed, 0, 0 );
		}
		if (rotateY){
			transform.Rotate(0, Time.deltaTime * rotationSpeed, 0 );
		}
		if (rotateZ){
			transform.Rotate(0, 0, Time.deltaTime * rotationSpeed );
		}
		

		// ...also rotate around the World's Y axis
		//transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
		
	}
}
