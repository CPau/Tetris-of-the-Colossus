using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIntro : MonoBehaviour {

    IntroManager intro_m;

    void Start () {
        intro_m = GameObject.Find("/IntroManager").GetComponent<IntroManager>();
        StartCoroutine("WaitAnimation");
    }


    IEnumerator WaitAnimation()
    {
        Debug.Log("wait for action");
        yield return new WaitForSeconds(15);
        intro_m.PlayTetris();
    }
}
