using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {

    // grid : 0 free, 1 player, 2 blocks, 3 player only, 4 activate floor, 5 end

    public float tile_size = 1.0f;
    public Vector3 grid_size = new Vector3(10, 10, 10);
    public Vector3 init_player_pos = new Vector3(5, 1, 5);
    public int[,,] grid;

    void Awake()
    {
        grid = new int[(int)grid_size.x,(int)grid_size.y,(int)grid_size.z];
        for (int i = 0; i < grid_size.x; i++)
        {
            for (int j = 0; j < grid_size.y; j++)
            {
                for (int k = 0; k < grid_size.z; k++)
                {
                    grid[i,j,k] = 0;
                }
            }
        }
        Debug.Log(grid.Length);
        CreateFloor(0);

        for (int i = 6; i < 10; i++)
        {
            for (int j = 6; j < 10; j++)
            {
                CreateColumn(new Vector2(i,j),2);
            }
        }

        for (int i = 0; i < grid_size.x; i++)
        {
            CreateColumn(new Vector2(i, 0), 2);
        }
        for (int i = 0; i < grid_size.x; i++)
        {
            CreateColumn(new Vector2(i, grid_size.x - 1), 2);
        }
        for (int i = 0; i < grid_size.z; i++)
        {
            CreateColumn(new Vector2(0, i), 2);
        }
        for (int i = 0; i < grid_size.z; i++)
        {
            CreateColumn(new Vector2(grid_size.z - 1, i), 2);
        }

        //for (int i = 1; i < grid_size.x; i++)
        //{
        //    CreateColumn(new Vector2(i, 1), 3);
        //}
        //for (int i = 1; i < grid_size.x-1; i++)
        //{
        //    CreateColumn(new Vector2(i, grid_size.x - 2), 3);
        //}
        //for (int i = 1; i < grid_size.z-1; i++)
        //{
        //    CreateColumn(new Vector2(1, i), 3);
        //}
        //for (int i = 1; i < grid_size.z-1; i++)
        //{
        //    CreateColumn(new Vector2(grid_size.z - 2, i), 3);
        //}

        // end zone
        for (int i = 6; i < 10; i++) {
            grid[8, 12, i] = 5;
            grid[9, 12, i] = 5;
            grid[8, 13, i] = 5;
            grid[9, 13, i] = 5;
            grid[8, 14, i] = 5;
            grid[9, 14, i] = 5;
            grid[8, 15, i] = 5;
            grid[9, 15, i] = 5;
            grid[8, 16, i] = 5;
            grid[9, 16, i] = 5;
        }
        //grid[1, 1, 1] = 5;
    }

    void Start() {
        grid[(int)init_player_pos.x, (int)init_player_pos.y, (int)init_player_pos.z] = 1;
        //GameObject player = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
        //GameObject player = GameObject.Find("Player");
    }

    public int get_cell_content(Vector3 pos){
        int value;
        value = grid[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z)];
        return value;
    }

    public bool is_cell_vacant(Vector3 pos)
    {
        bool test;
        if (get_cell_content(pos) != 0)
            test = false;
        else test = true;
        return test;
    }

    public bool is_cell_vacant_for_player(Vector3 pos)
    {
        bool test;
        if (get_cell_content(pos) != 0 && get_cell_content(pos) != 3 && get_cell_content(pos) != 5)
            test = false;
        else test = true;
        return test;
    }

    public void update_grid(Vector3 pos, int value)
    {
        grid[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z)] = value;
    }

    public void CreateFloor(int h)
    {
        for (int i = 0; i < grid_size.x; i++)
        {
            for (int j = 0; j < grid_size.z; j++)
            {
                grid[i, h, j] = 2;
            }
        }
    }

    public void CreateColumn(Vector2 pos, int value)
    {
        for (int i = 0; i < grid_size.y; i++)
        {
            grid[Mathf.RoundToInt(pos.x), i, Mathf.RoundToInt(pos.y)] = value;
        }
    }

    public void PrintGrid()
    {
        for (int i = 0; i < grid_size.x; i++)
        {
            for (int j = 1; j < grid_size.y; j++)
            {
                for (int k = 0; k < grid_size.z; k++)
                {
                    if (grid[i, j, k] != 0)
                    {
                        Debug.Log("grid (" + i + "," + j + "," + k + ") " + grid[i, j, k]);
                    }
                }
            }
        }
    }
}
