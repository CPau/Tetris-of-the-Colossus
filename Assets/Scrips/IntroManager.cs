using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {

    Fading fader;

    private void Start()
    {
        fader = GameObject.Find("/Fader").GetComponent<Fading>();
        StartCoroutine("fadeIn");
    }

    public void Action()
    {
        StartCoroutine(GoToScene("Intro"));
    }

    public void PlayTetris()
    {
        StartCoroutine(GoToScene("TetrisColossus"));
    }

    public void Menu()
    {
        StartCoroutine(GoToScene("Menu"));
    }

    IEnumerator GoToScene(string name)
    {
        Debug.Log("FadeOut");
        float fadeTime = fader.BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public IEnumerator fadeIn()
    {
        Debug.Log("FadeIn");
        float fadeTime = fader.BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }

    public IEnumerator fadeOut()
    {
        Debug.Log("FadeOut");
        float fadeTime = fader.BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
    }
}
