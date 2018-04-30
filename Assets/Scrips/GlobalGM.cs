using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGM : MonoBehaviour {

    public Fading fader;
    public int mode = 2;

    void Start () {
		
	}
	
	void Update () {
		
	}

    public IEnumerator fadeIn()
    {
        Debug.Log("FadeIn");
        float fadeTime = fader.BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }

    public IEnumerator fadeOut()
    {
        Debug.Log("FadeOut");
        float fadeTime = fader.BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
    }
}
