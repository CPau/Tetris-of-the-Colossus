using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour {

    public GameObject player;

    void Update()
    {
        transform.LookAt(player.transform.position, -Vector3.up);
    }
}
