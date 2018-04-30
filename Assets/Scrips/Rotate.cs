using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rot = new Vector3 (transform.rotation.eulerAngles.x , transform.rotation.eulerAngles.y + 4 * Time.deltaTime, transform.rotation.eulerAngles.z);
        transform.eulerAngles = rot;
    }
}
