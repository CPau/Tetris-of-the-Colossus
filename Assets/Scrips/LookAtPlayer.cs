using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    public GameObject billboard;
    GM gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("/GM").GetComponent<GM>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!gm.lock_colosse)
        {
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, billboard.transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
            //Debug.Log( transform.localEulerAngles);
        }
        else transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, Mathf.Lerp(transform.rotation.eulerAngles.y, 0, 0.5f), transform.rotation.eulerAngles.z);
    }
}
