using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    private int value = 1;
    GridSystem grid;
    public Vector3 player_pos;
    private Vector3 old_pos;
    private Vector3 direction;
    public Vector3 direction_fwd;
    public float speed = 1.0f;
    public float speed_falling = 1.0f;
    private GameObject go_camera;
    private GameObject go_pivot;
    private GM gm;
    private bool is_moving;
    private bool falling;
    private Vector3 dir_x = new Vector3(1, 0, 0);
    private Vector3 dir_y = new Vector3(0, 1, 0);
    private Vector3 dir_z = new Vector3(0, 0, 1);

    private float t;
    private float tf;

    private bool ending = false;

    void Start () {
        gm = GameObject.Find("/GM").GetComponent<GM>();
        grid = GameObject.Find("/GM").GetComponent<GridSystem>();
        go_camera = GameObject.Find("/camera");
        go_pivot = gameObject.transform.GetChild(0).gameObject;
        player_pos = new Vector3(grid.init_player_pos.x, grid.init_player_pos.y, grid.init_player_pos.z);
        old_pos = player_pos;
        transform.position = player_pos;
        is_moving = false;
        falling = false;
        ending = false;
    }
	
	void Update () {
        float angle = go_camera.transform.eulerAngles.y;

        if (!gm.end)
        {

            if (angle > 315 || angle < 45)
            {
                direction = dir_z;
                direction_fwd = new Vector3(0, 0, 0);
            }
            else if (angle > 45 && angle < 135)
            {
                direction = dir_x;
                direction_fwd = new Vector3(0, 90, 0);
            }
            else if (angle > 135 && angle < 225)
            {
                direction = -dir_z;
                direction_fwd = new Vector3(0, 180, 0);
            }
            else if (angle > 225 && angle < 315)
            {
                direction = -dir_x;
                direction_fwd = new Vector3(0, -90, 0);
            }
            go_pivot.transform.eulerAngles = direction_fwd;

            if (grid.is_cell_vacant(player_pos - dir_y) == true && falling == false) fall();

            if ((Input.GetButton("Fire1") || Input.GetButton("Jump")) && is_moving == false) move();

            if (is_moving)
            {
                if (t > 1) t = 1;
                transform.position = new Vector3(Mathf.Lerp(old_pos.x, player_pos.x, t), Mathf.Lerp(old_pos.y, player_pos.y, t), Mathf.Lerp(old_pos.z, player_pos.z, t));

                if (t == 1)
                {
                    is_moving = false;
                    t += 1;
                    grid.update_grid(old_pos, 0);
                    grid.update_grid(player_pos, value);
                    old_pos = player_pos;
                }
                t += speed * Time.deltaTime;
            }
            
            if (falling)
            {
                if (tf > 1) tf = 1;

                if (tf == 1)
                {
                    falling = false;
                    transform.position = new Vector3(player_pos.x, player_pos.y, player_pos.z);
                    tf += 1;
                    //if (old_pos.x == 1 || old_pos.x == grid.grid_size.x-1 || old_pos.y == 1 || old_pos.y == grid.grid_size.y - 1)
                    //{
                    //    grid.update_grid(old_pos, 3);
                    //}
                    //else grid.update_grid(old_pos, 0);
                    grid.update_grid(old_pos, 0);
                    grid.update_grid(player_pos, value);
                    old_pos = player_pos;
                }
                tf += speed_falling * Time.deltaTime;
            }
        }
        else {
            if (!ending)
            {
                ending = true;
                t = 0;
                gm.lock_colosse = true;
            }
            if (t > 1) t = 1;
            transform.position = new Vector3(Mathf.Lerp(old_pos.x, 8.0f, t), Mathf.Lerp(old_pos.y, 13, t), Mathf.Lerp(old_pos.z, 7.5f, t));

            t += 0.3f * Time.deltaTime;
        }
    
    }

    private void move()
    {
        bool can_translate = true;

        if (grid.is_cell_vacant_for_player(player_pos + direction) == false)
        {
            can_translate = false;
            if (grid.is_cell_vacant_for_player(player_pos + direction + dir_y) == true && grid.is_cell_vacant_for_player(player_pos + dir_y) == true) {

                direction = direction + dir_y;
                can_translate = true;
            }
            
            //if (grid.is_cell_vacant_for_player(player_pos + direction - dir_y) == false)
            //{
            //}
            //else
            //{
            //    can_translate = false;
            //}
        }
        //else
        //{
        //    if (grid.is_cell_vacant(player_pos + direction - dir_y) == true)
        //    {
        //        direction = direction - dir_y;
        //    }
        //}

        if (can_translate)
        {
            is_moving = true;
            t = 0;
            player_pos = player_pos + direction;
            if (grid.get_cell_content(player_pos + direction) == 5) gm.end = true;
        }
        //else Debug.Log("player can't translate");
    }

    private void fall()
    {
        falling = true;
        tf = 0;
        player_pos = player_pos - dir_y;
    }

}
