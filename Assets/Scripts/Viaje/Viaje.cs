using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Viaje : MonoBehaviour {

    public GameObject panelCargando;
    public GameObject panelPrincipal;
    public string video;
    private bool parar;

    float t;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    // Use this for initialization
    void Start () {
        parar = false;
        StartCoroutine("continuar");
        Handheld.PlayFullScreenMovie(video, Color.black, FullScreenMovieControlMode.Minimal);
    }
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator continuar()
    {
        while (!parar)
        {
            if (!parar)
            {
                Handheld.StopActivityIndicator();
                parar = true;
            }
            yield return null;
        }
        StartCoroutine("nivel1");
        yield return null;
    }

    IEnumerator nivel1()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        panelCargando.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Scenes/nivel_1");
    }
}
