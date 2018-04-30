using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour {

    GameObject arm;
    ArmShaderCtrl arm_ctrl;
    PlayerCtrl player;
    GridSystem grid;
    BeatCtrl beat;
    GlobalGM ggm;

    private int value = 2;
    public float speed = 1.0f;
    public bool follow;
    public string type;
    private bool project;
    private Vector3 pos;
    private Vector3 rot;
    private int beat_after_spawn;
    public int beat_before_falling;
    public bool init;
    private bool can_fall;
    private bool to_destroy;

    private int holding_time;

    private List<Vector3> blocks = new List<Vector3>();
    private bool move;
    private Vector3 under = new Vector3(0, -1, 0);
    

    void Start()
    {
        player = GameObject.Find("/Player").GetComponent<PlayerCtrl>();
        grid = GameObject.Find("/GM").GetComponent<GridSystem>();
        beat = GameObject.Find("/GM").GetComponent<BeatCtrl>();
        ggm = GameObject.Find("/GlobalGM").GetComponent<GlobalGM>();
        this.arm = Instantiate(Resources.Load("Prefabs/" + transform.name[0] + "_arm")) as GameObject;
        this.arm_ctrl = arm.GetComponent<ArmShaderCtrl>();
        //transform.position = new Vector3(5, 5, 5);

        //this.pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.follow = true;
        //InitPosGrid();
        this.can_fall = true;
        this.project = true;
        this.beat_after_spawn = 0;
        this.beat_before_falling = 4;
        this.init = true;
        this.to_destroy = false;
        if (ggm.mode == 2) this.holding_time = 30;
        if (ggm.mode == 3) this.holding_time = 20;
        this.arm_ctrl.arm_mode = 0;
    }

    void InitPosPhase ()
    {
        //this.pos = new Vector3(Mathf.Clamp(player.player_pos.x, 2, 13), 23, Mathf.Clamp(player.player_pos.z, 2, 13));
        this.pos = new Vector3(player.player_pos.x, 23, player.player_pos.z);
        this.transform.position = this.pos;
        this.rot = new Vector3(transform.eulerAngles.x, player.direction_fwd.y, transform.eulerAngles.z);
        this.transform.eulerAngles = this.rot;
    }

    void Update () {
        if (!beat.preupdate) beat.PreUpdate();

        if (this.follow) InitPosPhase();
        else
        {
            if (beat.beat)
            {
                Fall();
                if (!this.init && !this.can_fall && this.beat_after_spawn <= this.beat_before_falling + 2)
                    this.to_destroy = true;
                if (this.init && beat_after_spawn > this.beat_before_falling) this.init = false;
                //grid.PrintGrid();
            }
        }

        if (this.follow)
        {
            //if (Input.GetKeyDown("f"))
            //{
            //    this.follow = false;
            //    InitPosGrid();
            //}
            //if (beat_before_falling == beat_after_spawn)
            if (ggm.mode == 1)
            {
                if (Input.GetButton("Fire2"))
                {
                    this.follow = false;
                    InitPosGrid();
                    this.beat_after_spawn = 0;
                    this.arm_ctrl.arm_mode = 2;
                }
            }
            else
            {
                if (Input.GetButton("Fire2") || this.beat_after_spawn >= this.holding_time)
                {
                    this.follow = false;
                    InitPosGrid();
                    this.beat_after_spawn = 0;
                    this.arm_ctrl.arm_mode = 2;
                }
            }
            
            if (this.beat_after_spawn >= this.holding_time - 10)
            {
                this.arm_ctrl.arm_mode = 1;
            }
        }

        if (beat.beat)
        {
            this.beat_after_spawn += 1;
        }

        if (this.project)
        {
            ArmatureProjection();
        }

        if (this.to_destroy)
        {
            Destroy(this.arm);
            for (int i = 0; i < 4; i++)
            {
                grid.update_grid(this.blocks[i], 0);
            }
            Destroy(this.gameObject);
        }
    }

    void ArmatureProjection()
    {
        if (!this.follow)
        {
            //this.arm.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 2, 13), TestUnder(), Mathf.Clamp(transform.position.z, 2, 13));
            this.arm.transform.position = new Vector3(transform.position.x, TestUnder(), transform.position.z);

        }
        else
        {
            //this.arm.transform.position = new Vector3(Mathf.Clamp(player.player_pos.x, 2, 13), player.player_pos.y, Mathf.Clamp(player.player_pos.z, 2, 13));
            this.arm.transform.position = new Vector3(player.player_pos.x, player.player_pos.y, player.player_pos.z);
            this.arm.transform.transform.eulerAngles = new Vector3(arm.transform.transform.eulerAngles.x, player.direction_fwd.y, arm.transform.transform.eulerAngles.z);
        }
    }

    int TestUnder()
    {
        int project_h = 0;

        // Test blocks under each and if not itself
        foreach (Vector3 block in this.blocks)
        {
            bool to_test = true;
            for (int i = 0; i < 4; i++)
            {
                if ((block + this.under) == this.blocks[i]) to_test = false;
            }
            if (to_test)
            {
                bool test_pos = true;
                int under_pos = Mathf.RoundToInt(transform.position.y) - 1;
                while (test_pos)
                {
                    if (grid.is_cell_vacant(new Vector3(Mathf.RoundToInt(block.x), under_pos, Mathf.RoundToInt(block.z))) || grid.get_cell_content(new Vector3(Mathf.RoundToInt(block.x), under_pos, Mathf.RoundToInt(block.z))) == 1)
                    {
                        under_pos = under_pos - 1;
                    }
                    else
                    {
                        test_pos = false;
                        if (grid.get_cell_content(new Vector3(Mathf.RoundToInt(block.x), under_pos, Mathf.RoundToInt(block.z)))==1) project_h = Mathf.Max(project_h, under_pos);
                        else project_h = Mathf.Max(project_h, under_pos + 1);
                    }
                }
            }
        }
        return project_h;
    }

    void InitPosGrid()
    {
        foreach (Transform child in this.transform)
        {
            if (child.name == "C")
            {
                this.blocks.Add(child.transform.position);
                grid.update_grid(child.transform.position, value);
            }
        }
    }

    //void Translate()
    //{
    //    bool can_translate = true;

    //    // Test blocks under each and if not itself
    //    foreach (Vector3 block in this.blocks)
    //    {
    //        bool to_test = true;
    //        for (int i = 0; i < 4; i++)
    //        {
    //            if ((block + this.under) == this.blocks[i]) to_test = false;
    //        }
    //        if (to_test)
    //        {
    //            if (grid.is_cell_vacant(block + under) == false) can_translate = false;
    //        }
    //    }


    //}

    void Fall()
    {
        can_fall = true;
        bool is_player = false;
        // Test blocks under each and if not itself
        foreach (Vector3 block in this.blocks)
        {
            bool to_test = true;
            for (int i = 0; i < 4; i++)
            {
                if ((block + this.under) == this.blocks[i]) to_test = false;
            }
            if (to_test)
            {
                if (grid.is_cell_vacant(block + this.under) == false)
                {
                    if (grid.get_cell_content(block + this.under) == 1) is_player = true;
                    else this.can_fall = false;
                }

            }
        }

        if (this.can_fall && !is_player)
        {
            for (int i = 0; i < 4; i++)
            {
                grid.update_grid(this.blocks[i], 0);
                this.blocks[i] = this.blocks[i] + this.under;
            }
            foreach (Vector3 block in this.blocks)
            {
                grid.update_grid(block, this.value);
            }
            Vector3 new_pos = this.transform.position + this.under;
            //Debug.Log(new_pos);
            this.transform.position = new_pos;
        }
        else
        {
            if (this.project)
            {
                Debug.Log("can't translate");
                this.project = false;
                Destroy(this.arm);
            }
        }

        
    }
}
