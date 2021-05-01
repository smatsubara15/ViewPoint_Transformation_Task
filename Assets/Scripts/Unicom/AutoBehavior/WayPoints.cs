using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Mengyu Chen, 2018
//For questions: mengyuchenmat@gmail.com
public class WayPoints : MonoBehaviour {

	private GameObject[] waypoints;
	[SerializeField] GameObject[] MoveObjects;
	[HideInInspector] public int[] num;
	public float[] minDist;
	[Tooltip("speed must be less than minDist")]
	public float[] speed;
	public bool rand = false;
	[HideInInspector]public bool Cruising = true;
	private bool[] go;
	// private Vector3[] nextRotation;
	private Vector3[] rotationAngles;
	private Vector3[] originPos;

	void Start()
	{
		originPos = new Vector3[MoveObjects.Length];
		rotationAngles = new Vector3[MoveObjects.Length];
		num = new int[MoveObjects.Length];
		go = new bool[MoveObjects.Length];

		for (int i = 0; i < MoveObjects.Length; i ++){
			originPos[i] = MoveObjects[i].transform.position;
		}
		
		waypoints = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; ++i){
			waypoints[i] = transform.GetChild(i).gameObject;
		}
	}

	void Update ()
	{
		for (int i = 0; i < MoveObjects.Length; i ++){
			float dist = Vector3.Distance(MoveObjects[i].transform.position, waypoints[num[i]].transform.position);
			if (go[i]){
				if (dist > minDist[i]){
					Move (i);
				}else{
					if (!rand){
						if (num[i] + 1 == waypoints.Length) {
							go[i] = false;
						} else {
							num[i]++;
						}
					} else {
						num[i] = Random.Range (0, waypoints.Length);
					}
				}
			} else {
				MoveObjects[i].transform.position = originPos[i];
				num[i] = 0;
				go[i] = true;
			}
		}
	}
	public void Move(int i){
		if (Cruising){
			Vector3 temPos = waypoints[num[i]].transform.position - MoveObjects[i].transform.position;
			Quaternion lastPos = Quaternion.LookRotation(temPos);
			MoveObjects[i].transform.rotation = Quaternion.Slerp(MoveObjects[i].transform.rotation,lastPos, Time.deltaTime * 1.0f);
			//MoveObject.transform.LookAt(waypoints[num].transform.position);
			MoveObjects[i].transform.position += MoveObjects[i].transform.forward * speed[i] * Time.deltaTime;
		}
	}
	public void RandomTarget(){
		for (int i = 0; i < MoveObjects.Length; i ++){
			num[i] = Random.Range (0, waypoints.Length);
		}
	}
}
