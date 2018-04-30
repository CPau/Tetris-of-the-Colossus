using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisManager : MonoBehaviour {

    GameObject player;
    BeatCtrl beat;
    Tetris last_tetris;
    public List<string> blocks_name = new List<string>();
    public int beats_between_spawns = 6;
    public int beat_offset = 4;
    public int number_objects_per_spawn = 1;
    public int random_objects_between_spawn = 1;

    private bool init;

    private List<Vector4> zones = new List<Vector4>();

    void Start () {
		player = GameObject.Find("/Player");
        beat = GameObject.Find("/GM").GetComponent<BeatCtrl>();
        //last_tetris = GameObject.Find("/Z").GetComponent<Tetris>();
        blocks_name.Add("O");
        blocks_name.Add("L");
        blocks_name.Add("I");
        blocks_name.Add("Z");
        blocks_name.Add("T");
        blocks_name.Add("J");
        blocks_name.Add("M");
        blocks_name.Add("O");
        blocks_name.Add("P");
        blocks_name.Add("R");
        blocks_name.Add("S");
        blocks_name.Add("U");
        blocks_name.Add("X");
        blocks_name.Add("Y");

        zones.Add(new Vector4(2, 2, 5, 5));
        zones.Add(new Vector4(2, 6, 5, 9));
        zones.Add(new Vector4(2, 10, 5, 13));
        zones.Add(new Vector4(5, 2, 9, 5));
        zones.Add(new Vector4(10, 2, 13, 5));
        zones.Add(new Vector4(10, 6, 13, 9));
        zones.Add(new Vector4(6, 10, 9, 13));
        zones.Add(new Vector4(9, 9, 13, 13));

        init = true;
    }
	
	void Update () {
        if (!beat.preupdate) beat.PreUpdate();

        if (beat.beat)
        {
            // Do something

            if (beat.beat_time % beats_between_spawns == 0) InstantiateTetris();
        }
    }

    void InstantiateTetris()
    {
        if (init)
        {
            int tetromino = Random.Range(0, blocks_name.Count);
            GameObject tetris = Instantiate(Resources.Load("Prefabs/" + blocks_name[tetromino])) as GameObject;
            last_tetris = tetris.GetComponent<Tetris>();
            tetris.transform.position = new Vector3(2, 23, 2);
            init = false;
        }
        else if (!last_tetris.init)
        {
            int tetromino = Random.Range(0, blocks_name.Count);
            GameObject tetris = Instantiate(Resources.Load("Prefabs/" + blocks_name[tetromino])) as GameObject;
            last_tetris = tetris.GetComponent<Tetris>();
            tetris.transform.position = new Vector3(2, 23, 2);
        }
        
    }
}
