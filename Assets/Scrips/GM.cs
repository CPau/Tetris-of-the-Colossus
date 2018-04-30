using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

    PlayerCtrl player;
    GridSystem grid;
    public GameObject stage;
    public bool end;
    Fading fader;
    public bool lock_colosse;

    private bool start;

	void Start () {
        fader = GameObject.Find("/Fader").GetComponent<Fading>();
        player = GameObject.Find("/Player").GetComponent<PlayerCtrl>();
        grid = GameObject.Find("/GM").GetComponent<GridSystem>();
        //stage = GameObject.Find("/palier");
        start = false;
        end = false;
        lock_colosse = false;
        StartCoroutine("fadeIn");
    }

	void Update () {
        if (player.player_pos.y == 1) start = true;

        if (player.player_pos.y >= 6 && start)
        {
            grid.CreateFloor(5);
            stage.SetActive(true);
        }

        if (player.player_pos.y >= 10 && start)
        {
            lock_colosse = true;
        }

        if (end) StartCoroutine(GoToScene("EndMenu"));
    }

    IEnumerator fadeIn()
    {
        Debug.Log("FadeIn");
        float fadeTime = fader.BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }

    IEnumerator fadeOut()
    {
        Debug.Log("FadeOut");
        float fadeTime = fader.BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
    }

    IEnumerator GoToScene(string name)
    {
        yield return new WaitForSeconds(5);
        Debug.Log("FadeOut");
        float fadeTime = fader.BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
