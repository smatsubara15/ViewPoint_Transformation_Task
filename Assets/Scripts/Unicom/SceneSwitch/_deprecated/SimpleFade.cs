using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class SimpleFade : MonoBehaviour {
	public static SimpleFade instance { set; get; }
	public bool fadingNeeded;
	[SerializeField] Image fadeImage;
	[SerializeField] float fadeInTime = 0.01f;
	[SerializeField] float fadeOutTime = 0.2f;
	[SerializeField] Color fadeColor;
	[Range(0.0f,1.0f)][SerializeField] float fadeIntensity = 0.75f;
    private Color defaultColor;
	private bool isInTransition;
    private float transition;
    private bool isShowing;
    private float duration;
    private bool fading = false;
	 private void Awake()
    {
        instance = this;
        transition = 0.0f;
		//Debug.Log(transition);
		fadeImage.color = new Color(1,1,1,0);
		defaultColor = new Color(1,1,1,0);
		//Fade(false, 2.0f);
    }
	public void Fade(bool showing, float duration)
    {
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        //transition = (isShowing) ? 0 : 1;
    }
	private void Update()
    {
		
		if	(fadingNeeded && transition < 0.05f)
		{
				// Debug.Log("fading now " + transition);
				Fade(true, fadeInTime);
				// Debug.Log(isShowing + " needed ");
				fading = true;
				//StartCoroutine(reset_fading());
		} else if (!fadingNeeded && transition > 0.95f)
		{
				// Debug.Log("fading out " + transition);
				Fade(false, fadeOutTime);
				// Debug.Log(isShowing + " not needed");
				fading = true;
		}

        if (!isInTransition)
        {
			// Debug.Log(isInTransition + " returning");
            return;
        }
        transition += (isShowing) ? 0.02f * (1 / duration) : - 0.02f * (1 / duration);
		fadeColor.a = fadeIntensity;
		fadeImage.color = Color.Lerp(defaultColor, fadeColor, transition);
		
        if(transition >= 0.95 || transition <= 0.05)
        {
            isInTransition = false;
			fading = false;
        } 

        // if(fadingNeeded)
        // {
           
        //     if (fading == false)
        //     {
        //         Debug.Log("fading now");
        //         Fade(true, fadeInTime);
        //         fading = true;
        //         //StartCoroutine(reset_fading());
        //     }
        // } else if (fadingNeeded == false)
        // {
        //     if (fading == false){
		// 		Fade(true, fadeOutTime);
		// 		fading = true;
		// 	}
        // }

        // if (!isInTransition)
        // {
        //     return;
        // }
        // transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        // fadeImage.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 0, 0, 0.9f), transition);

        // if(transition > 1 || transition < 0)
        // {
        //     isInTransition = false;
		// 	fading = false;
        // }
        
       
    }
    public IEnumerator SceneSwitchReset(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        fading = false;
    }
    public IEnumerator Unfade(float waitTime){
        yield return new WaitForSecondsRealtime(waitTime);
        fadingNeeded = false;
    }
}
