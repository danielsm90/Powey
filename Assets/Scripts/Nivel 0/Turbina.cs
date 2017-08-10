using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Turbina : MonoBehaviour {

    public static Turbina instance;
    public int vueltas;

    public GameObject panelCargando;
    public AudioSource sonidoNivel0;
    public datoUsuario usuario;

    //Ventana de inicio
    public Button btIniciar;
    public GameObject panelInicial;
    public GameObject panelVictoria;
    public GameObject panelDerrota;
    public GameObject panelPausa;
    public GameObject panelTrivia;
    public GameObject panelTriviaCorrecta;
    public GameObject panelTriviaIncorrecta;
    public GameObject panelInstruccion;
    public GameObject panelCorreo, panelRequerido;
    public InputField correou;

    //Control de tiempo
    public Text tiempo;
    public float t;

    //Variables para Turbina
    public GameObject turbina;
    public HingeJoint2D fisTurbina;
    private JointMotor2D motorTurbina;

    //Variables para Vueltas
    public GameObject controlVuelta;
    public BoxCollider2D cantVuelta;

    //Barra de progreso
    public Slider slider;
    private int counter;
    public int MaxHealth = 1000;
    public int MinHealth = 0;
    public Image Fill;
    public Color MaxHealthColor = Color.yellow;
    public Color MinHealthColor = Color.red;

    public float speed = 0.1F;

    bool pausa;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        instance = this;
        vueltas = 0;
        counter = MinHealth;
        pausa = false;
        
    }

    // Use this for initialization
    void Start () {

        btIniciar.onClick.AddListener(() => { iniciarJuego(); });
        panelInicial.SetActive(true);

        t = 31;

        //slider.wholeNumbers = true;
        slider.minValue = MinHealth;
        slider.maxValue = MaxHealth;
        slider.value = MaxHealth;

        fisTurbina = turbina.GetComponent<HingeJoint2D>();
        fisTurbina.useMotor = true;

        controlVuelta.AddComponent<Rigidbody2D>();
        controlVuelta.GetComponent<Rigidbody2D>().gravityScale = 0;
        controlVuelta.AddComponent<BoxCollider2D>();
        controlVuelta.AddComponent<ControlVuelta>();
        cantVuelta.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (!panelInicial.activeSelf && !pausa && !panelInstruccion.activeSelf)
        {
            if (t <= 1)
            {
                motorTurbina.motorSpeed = 0;
                motorTurbina.maxMotorTorque = 1;
                fisTurbina.motor = motorTurbina;
                panelDerrota.SetActive(true);
            }
            else
            {

                if (counter != MaxHealth)
                {
                    t -= Time.deltaTime;
                    tiempo.text = t.ToString().Split('.')[0] + "";

                    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        // Get movement of the finger since last frame
                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                        motorTurbina.motorSpeed = -200;
                        motorTurbina.maxMotorTorque = 0.3f;
                        fisTurbina.motor = motorTurbina;
                        UpdateHealthBar(counter);
                        counter++;
                        // Move object across XY plane
                        //turbina.transform.Rotate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
                    }else
                    {
                        motorTurbina.motorSpeed = 0;
                        motorTurbina.maxMotorTorque = 5;
                        fisTurbina.motor = motorTurbina;
                        UpdateHealthBar(counter);
                        counter--;
                    }

                    /*if (move > 0)
                    {
                        motorTurbina.motorSpeed = -move * 200;
                        motorTurbina.maxMotorTorque = 0.3f;
                        fisTurbina.motor = motorTurbina;
                        UpdateHealthBar(counter);
                        counter++;

                    }
                    else if (move == 0)
                    {
                        motorTurbina.motorSpeed = 0;
                        motorTurbina.maxMotorTorque = 5;
                        fisTurbina.motor = motorTurbina;
                        UpdateHealthBar(counter);        // just for testing purposes
                        counter--;
                    }*/
                }
                else
                {
                    motorTurbina.motorSpeed = 0;
                    motorTurbina.maxMotorTorque = 5;
                    fisTurbina.motor = motorTurbina;
                    panelVictoria.SetActive(true);

                }
            }
        }
    }
    public void UpdateHealthBar(int val)
    {
        slider.value = val;
        Fill.color = Color.Lerp(MinHealthColor, MaxHealthColor, (float)val / MaxHealth);
    }

    public void iniciarJuego()
    {
        sonidoNivel0.Play();
        tiempo.gameObject.SetActive(true);
        panelInstruccion.SetActive(false);
        panelInicial.SetActive(false);
        pausa = false;
    }

    public void iniciarInstruccion()
    {
        panelInstruccion.SetActive(true);
        panelInicial.SetActive(false);
        panelInstruccion.GetComponent<Animation>().Play();
    }


    public void pausarJuego()
    {
        if (pausa)
        {
            pausa = false;
            panelPausa.SetActive(false);
        }
        else
        {
            pausa = true;
            panelPausa.SetActive(true);
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Scenes/menu");
    }

    public void Reiniciar()
    {
        panelInicial.SetActive(true);
        vueltas = 0;
        counter = MinHealth;
        t = 31;
        tiempo.text = "30";
        panelPausa.SetActive(false);
        panelDerrota.SetActive(false);
        panelVictoria.SetActive(false);
    }

    IEnumerator CargarMenu()
    {
        //panelCargando.SetActive(true);
        //Screen.orientation = ScreenOrientation.Landscape;
        yield return new WaitForSeconds(0f);
        if (true)
        {
            SceneManager.LoadScene("Scenes/videoViaje");
        }
    }

    public void Continuar()
    {
        //StartCoroutine("CargarMenu");
        SceneManager.LoadScene("Scenes/videoViaje");
    }

    public void showTrivia()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            usuario = GameControl.instance.loadFromDevice();
            panelTrivia.SetActive(true);
          
        }
        else
        {
            panelCorreo.SetActive(true);
        }

    }

   public void calificar(Text txtResp)
    {
        if(txtResp.name == "agua")
        {
            StartCoroutine(mandarCalificacion("1"));
        }
        else
        {
            StartCoroutine(mandarCalificacion("0"));
        }
    }

    private IEnumerator mandarCalificacion(string resp)
    {
        if (AccesoInternet())
        {
            string correo = usuario.getCorreo();
            string param = "?cod=andres1234&tipo=3&correo=" + correo + "&t_id=0&resp=" + resp;
            WWW w = new WWW("http://api.powey.com.co/" + param);
            yield return w;
            yield return new WaitForSeconds(1f);
            ExtractJSON(w.text, resp);
        }
        else
        {
            yield return null;
        }
    }

    private void ExtractJSON(string json, string resp)
    {
        JSONObject jo = new JSONObject(json);

        if (jo.type != JSONObject.Type.ARRAY)
        {
            if (jo["estado"].ToString() == "0")
            {
                Debug.Log(jo["msj"]);
            }
            else
            {
                if (resp == "1")
                {
                    panelTriviaCorrecta.SetActive(true);
                }
                else
                {
                    panelTriviaIncorrecta.SetActive(true);
                }
            }
            return;
        }
    }

    private bool AccesoInternet()
    {
        try
        {
            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
            return true;
        }
        catch (Exception es)
        {
            return false;
        }

    }

    public void agregCorreo()
    {
        if (correou.text!= "")
        {
            usuario = new datoUsuario();
            usuario.setCorreo(correou.text);
            GameControl.instance.saveOnDevice(usuario);
            panelVictoria.SetActive(false);
            panelCorreo.SetActive(false);
            panelRequerido.SetActive(false);
            showTrivia();
           
        }
        else
        {
            Debug.Log(correou);
            panelRequerido.SetActive(true);
        }
    }

}
