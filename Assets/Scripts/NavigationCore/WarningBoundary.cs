using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class WarningBoundary : MonoBehaviour
{
    Renderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        meshRenderer.enabled = false;
    }

    void OnTriggerEnter(Collider collider){
        if (collider.tag == "Player"){
            meshRenderer.enabled = true;

        }
    }
    void OnTriggerExit(Collider collider){
        if (collider.tag == "Player"){
            meshRenderer.enabled = false;
        }
    }

}
