using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCtrl : MonoBehaviour {

    public bool beat;
    public bool preupdate;
    public int beat_time;
    public float tempo;

    private float t;


    void Start () {
        beat = false;
        preupdate = false;
        beat_time = 0;
        tempo = 0.5f;
        t = 0;
    }

    public void PreUpdate()
    {
        beat = false;
        if (Input.GetKeyDown("t"))
        {
            beat = true;
            beat_time += 1;
        }
        if (t > tempo)
        {
            beat = true;
            beat_time += 1;
            t = 0;
        }

        t += Time.deltaTime;
        preupdate = true;
    }

    void LateUpdate()
    {
        preupdate = false;
    }
}
