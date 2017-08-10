using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

//Librarias para almacenamiento interno
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// Librerias para enviar correos
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class datos : MonoBehaviour {
    
    private datoUsuario usuario;

    public GameObject panelCargando;
    public AudioSource objAudio;

    public InputField txtNombre, txtCuenta, txtCorreo, txtValor, txtCiudad, txtTelefono;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        //StartCoroutine(DatosUsuario());
    }

    // Use this for initialization
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            usuario = GameControl.instance.loadFromDevice();
            StartCoroutine(CargarMenu());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DatosUsuario()
    {
        usuario = new datoUsuario();
        usuario.setCuenta(float.Parse(txtCuenta.text));
        usuario.setNombre(txtNombre.text);
        usuario.setCorreo(txtCorreo.text);
        usuario.setValor(float.Parse(txtValor.text));
        usuario.setCiudad(txtCiudad.text);
        usuario.setTelefono(txtTelefono.text);

        GameControl.instance.saveOnDevice(usuario);

        string datos = "&tipo=2" + "&cuenta=" + usuario.getCuenta() + "&nom=" + WWW.EscapeURL(usuario.getNombre()) + "&correo=" + usuario.getCorreo() + "&tel=" + usuario.getTelefono() +"&valor=" + usuario.getValor() + "&ciudad=" + WWW.EscapeURL(usuario.getCiudad());

        yield return null;

        Debug.Log(datos);

        // Subir a WebService

        WWW w = new WWW("http://api.powey.com.co/?cod=andres1234" + datos);
        yield return w;

        yield return new WaitForSeconds(1f);

        ExtractJSON(w.text);
    }

    private void ExtractJSON(string json)
    {
        JSONObject jo = new JSONObject(json);

        if (jo.type != JSONObject.Type.ARRAY)
        {
            if (jo["estado"].ToString() == "\"1\"")
            {
                Debug.Log("Guardo");
            }
            else
            {
                Debug.Log("No guardo");
            }
            return;
        }
        else
        {
            foreach (JSONObject item in jo.list)
            {
                if (item.type == JSONObject.Type.OBJECT)
                {
                    for (int i = 0; i < item.list.Count; i++)
                    {
                        Debug.Log(item.list[i]);
                    }
                }
            }
        }
    }

    IEnumerator CargarMenu()
    {
        objAudio.mute = true;
        panelCargando.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Scenes/menu");
    }

    public void enviar(Text val)
    {
        if(val.text == "enviar")
        {
            StartCoroutine("DatosUsuario");
        }
        StartCoroutine("CargarMenu");
    }
}
