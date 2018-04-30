using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentSpawn : MonoBehaviour {

    private bool active;
    private float t;
    private Color transp = new Color(1, 1, 1, 0);

    void Awake()
    {
        active = false;
        SetMaterialTransparent();
    }

    void OnEnable()
    {
        active = true;
    }
    
    void Update () {
        if (active)
        {
            if (t > 1) t = 1;
            Color tmp = new Color(1, 1, 1, t);
            foreach (Material m in gameObject.GetComponent<Renderer>().materials)
            {
                m.SetColor("_Color", tmp);
            }
            if (t == 1)
            {
                active = false;
                SetMaterialOpaque();
            }
            t += 1 * Time.deltaTime;
        }
    }

    private void SetMaterialTransparent()
    {
        foreach (Material m in gameObject.GetComponent<Renderer>().materials)
        {
            m.SetFloat("_Mode", 2);
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetInt("_ZWrite", 0);
            m.DisableKeyword("_ALPHATEST_ON");
            m.EnableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = 3000;
            m.SetColor("_Color", transp);
        }
    }



    private void SetMaterialOpaque()
    {
        foreach (Material m in gameObject.GetComponent<Renderer>().materials)
        {
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            m.SetInt("_ZWrite", 1);
            m.DisableKeyword("_ALPHATEST_ON");
            m.DisableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = -1;
        }
    }
}
