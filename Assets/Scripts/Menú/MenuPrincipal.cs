using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

public class MenuPrincipal : MonoBehaviour
{
    //variables 
    public GameObject principal;
    public GameObject creditos;
    public GameObject panelConf;
    public GameObject audioObj;

    public AudioSource objAudio;

    public GameObject panelCargando;

    bool bandConf, bandAud, bandMus;

    /*AudioClip audioBotones;

    GameObject[] botones;

    void Awake()
    {
        audioBotones = (AudioClip)Resources.Load("Sonidos/botonAgua");
        botones = GameObject.FindGameObjectsWithTag("boton");
        principal.AddComponent<AudioSource>().clip = audioBotones;

        for(int i = 0; i < botones.Length; i++)
            botones[i].AddComponent<AudioSource>().clip = audioBotones;
    }*/

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    void Start()
    {
        //Debug.Log(Application.persistentDataPath);
        //Debug.Log(GameControl.control.objUsuario.getNombre());
        
        /*ThreadStart delegado = new ThreadStart(mensaje);
        //Creamos la instancia del hilo 
        Thread hilo = new Thread(delegado);
        //Iniciamos el hilo 
        hilo.Start();*/

        audioObj.GetComponent<Button>().onClick.AddListener(() => { cambiarEstadoAudio(); });
        iniciarAudio();
        bandMus = true;
        bandConf = false;
    }

    //funciones
    public void showPrincipal()
    {
        principal.SetActive(true);
        creditos.SetActive(false);
    }

    public void iniciarAudio()
    {
        Debug.Log(audioManager.instance.objAudio.getAudio());
        if (audioManager.instance.objAudio.getAudio())
        {
            audioObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Botones/sonido_On");
        }
        else
        {
            audioObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Botones/sonido_off");
        }
    }

    public void cambiarEstadoAudio()
    {
        if (audioManager.instance.objAudio.getAudio())
        {
            audioObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Botones/sonido_off");
            audioManager.instance.objAudio.setAudio(false);
        }
        else
        {
            audioObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Botones/sonido_On");
            audioManager.instance.objAudio.setAudio(true);
        }
        audioManager.instance.saveConfAudio(audioManager.instance.objAudio);
    }

    public void Musica(GameObject musicaObj)
    {
        if (bandMus)
        {
            musicaObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Botones/musica_NO");
            bandMus = false;
        }
        else
        {
            musicaObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Botones/musica_SI");
            bandMus = true;
        }
    }

    public void showCreditos()
    {
        //principal.GetComponent<AudioSource>().Play();
        creditos.SetActive(true);
        //credito.GetComponent<AudioSource>().Play();
        //principal.SetActive(false);
    }

    public void showConfiguracion()
    {
        if (bandConf)
        {
            panelConf.SetActive(false);
            bandConf = false;
        }
        else
        {
            panelConf.SetActive(true);
            bandConf = true;
        }
    }

    //función para comenzar el juego
    public void jugar()
    {
        //StartCoroutine("CargarMenu");
        SceneManager.LoadScene("Scenes/videoIntro");
    }

    public void pagar()
    {
        Application.OpenURL("https://www.zonapagos.com/t_chec/pagos.asp"); 
    }

    IEnumerator CargarMenu()
    {
        objAudio.mute = true;
        //Screen.orientation = ScreenOrientation.Landscape;
        //panelCargando.SetActive(true);
        yield return new WaitForSeconds(0f);
        if (true)
        {
            SceneManager.LoadScene("Scenes/videoIntro");
            
        }
    }

    public void mensaje()
    {
        MailMessage mail = new MailMessage();
        Attachment adjunto = new Attachment("Assets/Resources/img/powey.png");

        mail.From = new MailAddress("asalazarmarin@gmail.com");
        mail.To.Add("asalazarmarin@gmail.com");
        mail.Subject = "Bono de descuento Greenled";
        mail.IsBodyHtml = true;
        mail.Body = "<center>¡Felicitaciones!</center><br><br>"+
            "<p>Felicitaciones, adjunto se encuentra el bono de descuento que has conseguido.</p>"; 
        mail.Attachments.Add(adjunto);

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("asalazarmarin@gmail.com", "andresm1053827666") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);
    }

}
