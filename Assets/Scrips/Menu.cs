using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    
    public Camera cam;
    IntroManager intro_m;
    GlobalGM ggm;

    void Start()
    {
        intro_m = GameObject.Find("/IntroManager").GetComponent<IntroManager>();
        ggm = GameObject.Find("/GlobalGM").GetComponent<GlobalGM>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Clicked();
    }

    void Clicked()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.name == "Hard")
            {
                ggm.mode = 3;
                intro_m.Action();
            }
            if (hit.collider.gameObject.name == "Medium")
            {
                ggm.mode = 2;
                intro_m.Action();
            }
            else if (hit.collider.gameObject.name == "Easy")
            {
                ggm.mode = 1;
                intro_m.Action();
            }
            else if (hit.collider.gameObject.name == "Quit")
            {
                Application.Quit();
            }
        }
    }

    IEnumerator WaitAnimation()
    {
        Debug.Log("wait for action");
        yield return new WaitForSeconds(14);
        intro_m.Action();
    }

}
