using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Mengyu Chen, 2018
//For questions: mengyuchenmat@gmail.com
public class SelfHovering : MonoBehaviour {

	[SerializeField]private Vector3 coefficient = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField]private Vector3 freq = new Vector3(0.0f, 0.0f, 0.0f);

    private void Start()
    {
        // coefficient.x = Random.Range(-0.004f, 0.004f);
        // coefficient.y = Random.Range(0.005f, 0.01f);
        // coefficient.z = Random.Range(-0.004f, 0.004f);

        // freq = Random.Range(-2, 2);
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(new Vector3(Mathf.Sin(freq.x *Time.time) * coefficient.x * transform.localScale.x, 
                                        Mathf.Sin(freq.y * Time.time) * coefficient.y * transform.localScale.y,
                                        Mathf.Sin(freq.z *Time.time) * coefficient.z * transform.localScale.z));
	}

}
