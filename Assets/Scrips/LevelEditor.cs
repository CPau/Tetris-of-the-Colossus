using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour {

    GridSystem grid;
    private List<Vector3> blocks = new List<Vector3>();

    void Start () {
        grid = GameObject.Find("/GM").GetComponent<GridSystem>();
        // Read child to construct level
        foreach (Transform child in this.transform)
        {
            blocks.Add(child.transform.position);
            grid.update_grid(child.transform.position, int.Parse(child.name));
        }

    }
	
	
	void Update () {
		
	}


}
