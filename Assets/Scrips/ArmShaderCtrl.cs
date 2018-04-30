using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmShaderCtrl : MonoBehaviour {

    private Material mat;

    public Color arm_color;
    public Color emit_color;
    private Color mat_arm;
    private Color mat_emit;
    private Color validated_arm;
    private Color validated_emit;
    public Color r_color;
    [Range(0.0f,1.0f)]
    public float alpha;
    public int arm_mode;

    private float speed;

    private bool changing_color;
    private float t;

    void Start () {
        foreach (Transform child in this.transform)
        {
            if (child.name != "C")
            {
                this.mat = child.GetComponent<Renderer>().material;
            }
        }
        //arm_color = new Vector4(arm_color.r, arm_color.g, arm_color.b, alpha);
        this.mat.SetColor("_Color", this.arm_color);
        this.mat.SetColor("_EmissionColor", this.emit_color);
        this.validated_arm = new Vector4(1,1,1, 0.8f);
        this.validated_emit = new Vector4(0, 0, 0, 0);
        this.arm_mode = 0;
        this.changing_color = true;
        this.speed = 1;
        this.t = 0;
    }
	
	void Update ()
    {
        if (this.changing_color)
        {
            if (this.arm_mode == 1)
            {
                Color tmp = new Color(1, 1, 1, 1);
                tmp = Color.Lerp(this.arm_color, this.validated_arm, Mathf.PingPong(Time.time, 1));
                this.mat_arm = tmp;
                this.r_color = tmp;
                tmp = new Color(1, 1, 1, 1);
                tmp = Color.Lerp(this.emit_color, this.validated_emit, Mathf.PingPong(Time.time, 1));
                this.mat_emit = tmp;
                this.mat.SetColor("_Color", this.mat_arm);
                this.mat.SetColor("_EmissionColor", this.mat_emit);
                //Debug.Log("color mode 1 ");
            }
            else if (arm_mode == 2)
            {
                if (this.t > 1) this.t = 1;
                Color tmp = new Color(1, 1, 1, 1);
                tmp = Color.Lerp(this.arm_color, this.validated_arm, this.t);
                this.mat_arm = tmp;
                this.r_color = tmp;
                tmp = new Color(1, 1, 1, 1);
                tmp = Color.Lerp(this.emit_color, this.validated_emit, this.t);
                this.mat_emit = tmp;
                this.mat.SetColor("_Color", this.mat_arm);
                this.mat.SetColor("_EmissionColor", this.mat_emit);
                //Debug.Log("color mode 2");
                if (this.t == 1)
                {
                    this.t += 1;
                    this.changing_color = false;
                }
                this.t += speed * Time.deltaTime;
            }
        }
    }
}
