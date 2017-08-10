using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public GameObject panelAnimacion;
    public GameObject panelCargando;
    public string video;

    private bool parar;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    // Use this for initialization
    void Start () {
        parar = false;
        StartCoroutine("animacion");

        Handheld.PlayFullScreenMovie(video, Color.black, FullScreenMovieControlMode.Minimal);
    }

    // Update is called once per frame
    void Update () {
       
    }

    IEnumerator animacion()
    {
        while (!parar)
        {
            if (!parar)
            {
                //Handheld.StopActivityIndicator();
                parar = true;
            }
            yield return null;
        }
        StartCoroutine("nivel0");
        yield return null;
    }

    IEnumerator nivel0()
    {
        
        panelCargando.SetActive(true);
        Screen.orientation = ScreenOrientation.Landscape;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Scenes/nivel_0");
    }
}
