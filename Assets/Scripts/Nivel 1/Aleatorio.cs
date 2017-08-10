using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System;

public class Aleatorio : MonoBehaviour {

    //Definicion de variables
    public static Aleatorio aleatorio;
    private datoUsuario objUsuario;
    /*
     * pannelCiudad: panel donde se muestra todo el fondo de la ciudad sea de día o de noche.
     * panelInicial: panel donde se muestra el mensaje con las instrucciones del nivel 1 en el día.
     * panelConstruccion: panel donde se muestra mensaje donde se informa que hay niveles en costrucción.
     * panelInstrucciones: panel donde se mustra una animmación con la jugabilidad del nivel en el día.
     * panelNoche: panel donde se muestra el mensaje con instrucciones del nivel 1 en la noche.
     * panelIniciar: panel donde se muestra la animación del conteo antes de empezar a jugar.
     */
    public GameObject panelCiudad, panelInicial, panelConstruccion, panelInstrucciones, panelNoche, panelIniciar;
    /*
     * panelPerdio: panel que se muestra cuando el usuario no logra el objetivo del nivel 1.
     * panelGano: panel que se muestra cuando el usuario cumple el objetivo, y se informa sobre el ahorro 
     *            y las estrellas que se gano.
     * panelBono: panel donde se le muestra al usuario el bono que se gano, despues del nivel en la noche.
     * panelTrivia: panel  donde se muestra la pregunta del nivel 1.
     * panelIncorrecto: panel que se muestra cuando  el usuario elige una de las opciones erroneas en el panelTrivia.
     * panelCorrecto: panel que se muestra cuando  el usuario elige la opción correcta en el panelTrivia.
     * panelComentario: panel donde se le pide al usuario que deje comentario sobre el juego.
     * panelContenedor: panel donde se muestra la contidad de bombillos LED que se tienen.
     */
    public GameObject panelPerdio, panelGano, panelBono, panelTrivia, panelIncorrecto, panelCorrecto, panelComentario, panelContenedor;
    /*
     * cantLed: variable donde se almacena la cantidad de luces LED que se tiene.
     * cantLuzLedApagada: variable donde almacenamos las luces LED que se han apagado.
     * cantIncPren: variable donde se almacena las luces incandecentes encendidas.
     * cantLedPrend:  variable donde se almacena las luces LED encendidas.
     */
    public int cantLed, cantLuzLedApagada, cantIncPren, cantLedPrend ;
    /*
     * txtCantLed: variable que muestra la cantidad de luces LED que se tienen.
     */
    public Text txtCantLed;
    /*
     * txtCantDinero: variable que muestra la cantidad de dinero que se gana el ususario.
     */
    public Text txtCantDinero;
    /*
     * txtTiempo: variable que muestra el tiempo que le falta al usuario y se va disminuyendo.
     */
    public Text txtTiempo;
    /*
     * txtConsumo: variable que muestra el consumo inicial.
     * txtAhorro: variable que muestra el ahorro que se tuvo al terminar el nivel 1.
     * txtConsumoFinal: variable que muestra el consumo final al terminar el nivel 1.
     */
    public Text txtConsumo, txtAhorro, txtConsumoFinal;
    /*
     * cantLedCamb: variable que almacena los  bombillos LED cambiados y sirve para calcular resFinal.
     */
    public int cantLedCamb;
    /*
     * cantDinero: variable que almacena la cantidad de dinero que gana el usuario.
     */
    public int cantDinero;
    /*
     * tempTiempo: variable que almacena el tiempo que le resta al usuario.
     */
    string tempTiempo;
    /*
     * slider: variable que va almacenando el consumo para reflejarlo en la barra de consumo y se va actualizando a medida
     *         de como vaya avanzando el juego y de lo que haga el usuario.
     * slider1:  variable que que almacena el valor totaldel consumo despues de que el usuario termine de jugar, para
     *           reflejarlo en la barra.
     * slider2: variable que contiene el valor del consumo inicial, que se refleja en una barra.
     */
    public Slider slider, slider1, slider2, slider3;
    /*
     * MaxHealth: variable donde se almacena el valor del consumo inicial y sirve para saber el valor maximo del slider.
     */
    public float MaxHealth;
    /*
    * MinHealth: variable donde se almacena el valor minimo del consumo y sirve para saber el valor minimo del slider.
    */
    public float MinHealth;
    /*
     * Fill: variable donde se va actualizando el nivel de consumo.
     * Fill1: variable donde se muestra el  nivel de consumoTotal despues de completar el nivel 1.
     * Fill2: variable donde se muestra el nivel de consumoInicial del nivel 1.
     * 
     */
    public Image Fill, Fill1, Fill2, Fill3;
    /*
     * MaxHealthColor: variable donde se le asigna un color al valor maximo del consumo.
     */
    public Color MaxHealthColor;
    /*
     * MinHealthColor: variable donde se le asigna un color al valor maximo del consumo.
     */
    public Color MinHealthColor;
    /*
     * sonidoNivel1: variable que contiene el sonido de fondo del nivel 1.
     * corto: variable que contiene el sonido de un corto cuando el usuario coloca erroneamente el bombillo LED.
     * risaVillano: variable que contiene el sonido de la risa  del villano cuando enciende una luz.
     * sonidoDinero: variable que contiene el sonido de ganancia de dinero cuando el usuario coloca 
     *               correctamente el bombillo LED
     */
    public AudioSource sonidoNivel1, corto, risaVillano, sonidoDinero;
    /*
     * usuarios: variable de la instancia datosUsuario para obtener la informaion de los usuarios.
     */
    public datoUsuario usuarios;
    /*
     * txtComentario: campo de texto donde el usuario va poder colocar su commentario sobre el juego.
     */
    public InputField txtComentario;
    /*
     * btnLed: variable que se le asigna a los bombillos LED.
     */
    public Button btnLed;
    /*
     * estrella1, estrella2, estrella3: variables que se le asigna a las estrellas
     */
    public GameObject estrella1, estrella2, estrella3;

    /*
     * resFinal: variable que sirve para calcular la cantidad de estrellas ganadas.
     * tiempo: variable que actualiza el estado del tiempo en el nivel.
     * puntuacion: varible que define el puntaje que logro el usuario en el día esto nos servira para calcular la
     *             puntuacionFinal.
     * puntuacionNoche: varible que define el puntaje que logro el usuario en la noche esto nos servira para calcular la
     *                  puntuacionFinal.
     * puntuacionFinal: varible que define el puntaje que logro el usuario en total esto nos servira para calcular la
     *                  tabla de posiciones.
     * consumoInc: variable que calcula el consumo con las luces incandecentes, tambien sirve para calcular el consumoTotal.
     * consumoLed: variable que calcula el consumo con las luces LED, tambien sirve para clacular el consumoTotal.
     * consumoTotal: variable que sirve para calcular el consumo total con la sumatoria de consumoInc + consumoLed, y 
     *               sirve para calcular el ahorro.
     * t: variable que contiene, almacena y compara el tiempo de la animacion de las instrucciones.
     * t1: variable que contiene, almacena y compara el tiempo de la animacion de las conteo.
     * consumoInicial: variable que calcula el consumo incial que lo calcula al principio del nivel, 
     *                 y sirve para calcular el ahorro.
     * ahorro: variable que calcula el ahorro basandose en el consumoInicial - consumoTotal.
     */
    float resFinal, tiempo, puntuacion, puntuacionNoche, puntuacionFinal, consumoInc, consumoLed, consumoTotal, t, t1, consumoInicial, ahorro;

    Texture frame;

    /*
     * img: variable que guarda imagen de la luz incandecente encendida.
     */
    private Sprite img;
    /*
     * img1: variable que guarda imagen de el villano encendiendo la luz incandecente.
     * img2: variable que guarda imagen de el villano encendiendo la luz LED.
     * img3: variable que guarda imagen de la luz LED encendida.
     */
    private Sprite img1, img2, img3;
    /*
     * spawnPoints: vector que contiene el nuemro de ventanas que se estan usando
     */
    GameObject[] spawnPoints;
    /*
     * currentPoint: variable que identifica en que posicion exacta estan las ventanas.
     */
    GameObject currentPoint;
    /*
     * index: variable que recorre las posiciones del vector spawnPoints.
     */
    int index;
    /*
     * band: variable 
     */
    bool band, dia, gano;

    //Metodo en el que se define con que elementos debe empezar la escena y su orientación
    void Awake() {
        Screen.orientation = ScreenOrientation.Landscape;
        panelInicial.SetActive(true);
        objUsuario = new datoUsuario();
    }

    //Metodo en el que se incializan las variables
    void Start()
    {
        t1 = 0f;
        t = 0f;
        consumoInc = 0f;
        consumoLed = 0f;
        consumoTotal = 0f;
        ahorro = 0f;
        cantLuzLedApagada = 0;
        puntuacionNoche = 0f;
        puntuacionFinal = 0f;
        resFinal = 0f;
        tiempo = 60f;
        cantLedCamb = 0;
        cantDinero = 0;
        aleatorio = this;
        cantLed = 30;
        band = true;
        dia = true;
        gano = false;
        img = Resources.Load<Sprite>("img/Nivel 1/Ciudad_Final-02");
        img1 = Resources.Load<Sprite>("img/Nivel 1/ventanas-06");
        img2 = Resources.Load<Sprite>("img/Nivel 1/elementos-07");
        img3 = Resources.Load<Sprite>("img/Nivel 1/Ciudad_Final-04");
        spawnPoints = GameObject.FindGameObjectsWithTag("ventana");
        cantIncPren = spawnPoints.Length;
        consumoInc = cantIncPren * 60;
        consumoLed = cantLedPrend * 9;
        consumoTotal = consumoInc + consumoLed;
        consumoInicial = cantIncPren * 60;
        barraConsumoTotal(consumoInicial);
        MaxHealth = consumoInc;
        MinHealth = 0f;
        slider.minValue = MinHealth;
        slider.maxValue = MaxHealth;
        slider.value = MaxHealth;
        slider1.minValue = MinHealth;
        slider1.maxValue = MaxHealth;
        slider1.value = MaxHealth;
        slider2.minValue = MinHealth;
        slider2.maxValue = MaxHealth;
        slider2.value = MaxHealth;
        slider3.minValue = MinHealth;
        slider3.maxValue = MaxHealth;
        slider3.value = MinHealth;
        ahorro = consumoInicial - consumoTotal;
        txtAhorro.text = "" + ahorro + "W";
        txtCantLed.text = "" + cantLed;
        txtCantDinero.text = "" + cantDinero;
        txtTiempo.text = "" + tiempo;
        txtConsumo.text = "" + consumoInicial +" W";
        txtConsumoFinal.text = "" + consumoTotal + "W";
    }

    //Metodo donde se hacen las actuizaciones del juego y sus variables
    void FixedUpdate()
    {
        if (!panelGano.activeSelf && !panelPerdio.activeSelf && !panelInicial.activeSelf && !panelInstrucciones.activeSelf && !panelIniciar.activeSelf)
        {
            txtCantLed.text = "" + cantLed;
            txtCantDinero.text = "" + cantDinero;
            consumoInc = cantIncPren * 60;

            if (dia)
            {

                if (cantLedCamb == spawnPoints.Length)
                    gano = true;
            }
            else
            {
                consumoLed = cantLedPrend * 9;
            }
            consumoTotal = consumoInc + consumoLed;
            txtConsumoFinal.text = "" + consumoTotal + "W";
            ahorro = consumoInicial - consumoTotal;
            txtAhorro.text = "" + ahorro + "W";

            if (tiempo <= 1 || gano)
                band = false;
            else
            {
                tiempo -= Time.deltaTime;
                tempTiempo = "" + tiempo;
                txtTiempo.text = tempTiempo.Split('.')[0];
            }
            UpdateHealthBar(consumoTotal);
            barraWey(ahorro);
        }

        
    }

    public void Jugar()
    {
        panelIniciar.SetActive(false);
        StartCoroutine("villano");
    }

    //Metodo para quitar la animacion de las instrucciones y empezar con el juego
    public void quitar()
    {
        panelInstrucciones.SetActive(false);
        panelIniciar.SetActive(true);
        t = panelInstrucciones.GetComponent<Animation>().clip.length;
   
    }

    //Metodo que inicia las instrucciones del nivel 1
    public void instrucciones()
    {
        
        panelInicial.SetActive(false);
        panelInstrucciones.SetActive(true);
        StartCoroutine("tutorial");
    }

    //Corrutina que contiene la animacion de las instrucciones
    IEnumerator tutorial()
    {
        while (t < panelInstrucciones.GetComponent<Animation>().clip.length)
        {
            t += Time.deltaTime;
            yield return null;
        }
        panelInstrucciones.SetActive(false);
        panelIniciar.SetActive(true);
        StartCoroutine("conteo");
        yield return null;
    }

    //Corrutina que contiene la animacion de la ventana ganaste
    IEnumerator ganador()
    {
        while (t < panelGano.GetComponent<Animation>().clip.length)
        {
            t += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    //Corrutina que ejecuta un conteo antes de empezar con la jugabilidad del nivel 1
    IEnumerator conteo()
    {
        while (t1 < panelIniciar.GetComponent<Animation>().clip.length)
        {
            t1 += Time.deltaTime;
            yield return null;
        }
        sonidoNivel1.Play();
        panelIniciar.SetActive(false);
        StartCoroutine("villano");
        yield return null;
    }

    /*Corrutina que contiene los movimientos del villano en forma 
    aleatoria tanto en el día como en la noche*/
    IEnumerator villano()
    {

        while (band)
        {
            yield return null;
            index = UnityEngine.Random.Range(0, spawnPoints.Length - 1);
            currentPoint = spawnPoints[index];
            
            if (dia)
            {

                if (currentPoint.GetComponent<Image>().sprite.name != "Ciudad_Final-02" && currentPoint.GetComponent<Image>().sprite.name != "Ciudad_Final-05")
                {
                    currentPoint.GetComponent<Image>().sprite = img1;
                    risaVillano.Play();
                    yield return new WaitForSeconds(1.5f);
                    currentPoint.GetComponent<Image>().sprite = img;
                    cantIncPren++;

                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                panelCiudad.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Nivel 1/noche");
                panelContenedor.SetActive(false);
                btnLed.GetComponent<Image>().color = Color.clear;
                txtCantLed.color = Color.clear;
                if (currentPoint.GetComponent<Image>().sprite.name == "Ciudad_Final-03")
                {
                    currentPoint.GetComponent<Image>().sprite = img1;
                    yield return new WaitForSeconds(0.4f);
                    currentPoint.GetComponent<Image>().sprite = img;
                    cantLuzLedApagada--;
                    cantIncPren++;
                }
                else if (currentPoint.GetComponent<Image>().sprite.name == "Ciudad_Final-05")
                {
                    currentPoint.GetComponent<Image>().sprite = img2;
                    yield return new WaitForSeconds(0.4f);
                    currentPoint.GetComponent<Image>().sprite = img3;
                    cantLuzLedApagada--;
                    cantLedPrend++;
                }
                
                else
                {
                    yield return null;
                }
                yield return null;
            }
        }
        if (dia)
        {
            resFinal = ((float)cantLedCamb / spawnPoints.Length) * 100f;

            puntuacion = (resFinal * 100) + (tiempo * 50);

            Debug.Log(puntuacion);

            if (resFinal >= 70)
            {
                if (resFinal >= 70 && resFinal <= 75)
                {
                    estrella1.GetComponent<Image>().color = Color.white;
                    Debug.Log("1 Estrella - " + puntuacion);

                }
                else if (resFinal >= 76 && resFinal <= 90)
                {
                    estrella1.GetComponent<Image>().color = Color.white;
                    estrella2.GetComponent<Image>().color = Color.white;
                    Debug.Log("2 Estrella - " + puntuacion);
                }
                else
                {
                    estrella1.GetComponent<Image>().color = Color.white;
                    estrella2.GetComponent<Image>().color = Color.white;
                    estrella3.GetComponent<Image>().color = Color.white;
                    Debug.Log("3 Estrella - " + puntuacion);
                }
                
                panelNoche.SetActive(true);
            }
            else
            {
                panelPerdio.SetActive(true);
            }


            yield return null;
        }
        else
        {
            puntuacionNoche = cantLuzLedApagada * 20;
            puntuacionFinal = puntuacionNoche + puntuacion;
            panelGano.SetActive(true);
            StartCoroutine("ganador");
            yield return null;
        }
    }

    //Metodo que sirve para que el usuario apage las luces en el día y la noche
    public void usuario(GameObject go)
    {
        if (dia)
        {
            if (go.GetComponent<Image>().sprite.name == "Ciudad_Final-02")
            {
                go.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Nivel 1/Ciudad_Final-03");
                cantIncPren--;
            }
        }
        else
        {
            if (go.GetComponent<Image>().sprite.name == "Ciudad_Final-02")
            {
                go.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Nivel 1/Ciudad_Final-03");
                cantLuzLedApagada++;
                cantIncPren--;
            }
            else if (go.GetComponent<Image>().sprite.name == "Ciudad_Final-04")
            {
                go.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Nivel 1/Ciudad_Final-05");
                cantLuzLedApagada++;
                cantLedPrend--;
            }
            
        }

    }

    /*Metodo que organiza el escenario en la noche para la jugabilidad e
     iniciliza variables y trae la corrutina del villano*/ 
    public void noche()
    {
        band = true;
        dia = false;
        gano = false;
        tiempo = 20f;
        txtTiempo.text = "" + tiempo;
        StartCoroutine("villano");

        panelNoche.SetActive(false);

        cantIncPren = spawnPoints.Length - cantLedCamb;
        cantLedPrend = 0;
        //Recorrido a las ventanas para saber cuales estan apagadas y prenderlas
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            currentPoint = spawnPoints[i];
            if (currentPoint.GetComponent<Image>().sprite.name == "Ciudad_Final-03")
            {
                currentPoint.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Nivel 1/Ciudad_Final-02");
                
            }
            else if (currentPoint.GetComponent<Image>().sprite.name == "Ciudad_Final-05")
            {
                currentPoint.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Nivel 1/Ciudad_Final-04");
                cantLedPrend++;
            }
        }
    }

    //Metodo para activar la ventana de trivia
    public void irTrivia()
    {
        panelGano.SetActive(false);
        panelTrivia.SetActive(true);
    }

    //Metodo para activar la ventana del bono
    public void irBono()
    {
        panelGano.SetActive(false);
        panelBono.SetActive(true);
    }

    //Metodo para actualizar la barra de consumo
    public void UpdateHealthBar(float val)
    {
        slider.value = val;
        Fill.color = Color.Lerp(MinHealthColor, MaxHealthColor, val);
        slider1.value = val;
        Fill1.color = Color.Lerp(MinHealthColor, MaxHealthColor, val);
        
    }

    public void barraWey(float val)
    {
        slider3.value = val;
        Fill3.color = Color.Lerp(MinHealthColor, MaxHealthColor, val);
    }

    //Metodo para mantener la barra de consumo de antes sin variar
    public void barraConsumoTotal(float val)
    {
        slider2.value = val;
        Fill2.color = Color.Lerp(MinHealthColor, MaxHealthColor, val);
    }

    //Metodo que califica la respuesta de la trivia
    public void bueno()
    {
        StartCoroutine(mandarCalificacion("1"));
    }

    //Metodo que califica la respuesta de la trivia
    public void malo()
    {
        StartCoroutine(mandarCalificacion("0"));
    }

    //Corrutina que  envia la calificación al web service y su respuesta
    private IEnumerator mandarCalificacion(string resp)
    {
        if (AccesoInternet())
        {
            usuarios = GameControl.instance.loadFromDevice();
            string correo = usuarios.getCorreo();
            string param = "?cod=andres1234&tipo=3&correo=" + correo + "&t_id=1&resp=" + resp;
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
                    panelTrivia.SetActive(false);
                    panelCorrecto.SetActive(true);
                }
                else
                {
                    panelTrivia.SetActive(false);
                    panelIncorrecto.SetActive(true);
                }
            }
            return;
        }
    }

    //Metodo para activar la ventana de envio de commentarios
    public void comentario()
    {
        StartCoroutine("mandarComentario");
    }

    //Corrutina que envia el comentario del usuario al web service
    private IEnumerator mandarComentario()
    {
        if (AccesoInternet())
        {
            string comen = WWW.EscapeURL(txtComentario.text);
            usuarios = GameControl.instance.loadFromDevice();
            string correo = usuarios.getCorreo();

            string param = "?cod=andres1234&tipo=4&correo=" + correo + "&comentario=" + comen;
            WWW w = new WWW("http://api.powey.com.co/" + param);
            yield return w;
            yield return new WaitForSeconds(1f);
            ExtractJSONComen(w.text, comen);
        }
        else
        {
            yield return null;
        }
    }
    private void ExtractJSONComen(string json, string comen)
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
                panelComentario.SetActive(false);
                panelConstruccion.SetActive(true);
            }
            return;
        }
    }

    //Metodo que verifica la conxion a internet
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

    //Metodo que envia el mensaje del bono a el usuario y a la empresa conexa
    public void bono()
    {
        ThreadStart delegado = new ThreadStart(mensajeUsuario);
        //Creamos la instancia del hilo 
        Thread hilo = new Thread(delegado);
        //Iniciamos el hilo 
        hilo.Start();

        ThreadStart conexa = new ThreadStart(mensajeConexa);
        //Creamos la instancia del hilo 
        Thread hiloConexa = new Thread(conexa);
        //Iniciamos el hilo 
        hiloConexa.Start();

        panelTrivia.SetActive(true);
        panelBono.SetActive(false);
    }

    //Metodo que contiene la informacón que le llega al usuario
    public void mensajeUsuario()
    {
        MailMessage mail = new MailMessage();
        //Attachment adjunto = new Attachment("Assets/Resources/img/powey.png");

        mail.From = new MailAddress("asalazarmarin@gmail.com");
        mail.To.Add(objUsuario.getCorreo());
        mail.Subject = "Bono de descuento Greenled";
        mail.IsBodyHtml = true;
        mail.Body = "<center>¡Felicitaciones!</center><br><br>" +
            "<p>Adjunto se encuentra el bono de descuento que has conseguido.</p>";
        //mail.Attachments.Add(adjunto);

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("asalazarmarin@gmail.com", "andresm1053827666") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);
    }

    //Metodo que contiene la información que le llega a la empresa conexa
    public void mensajeConexa()
    {
        MailMessage mail = new MailMessage();
        //Attachment adjunto = new Attachment("Assets/Resources/img/powey.png");

        mail.From = new MailAddress("asalazarmarin@gmail.com");
        mail.To.Add("asalazarmarin@gmail.com");
        mail.Subject = "Bono de descuento Greenled";
        mail.IsBodyHtml = true;
        mail.Body = "<center>¡Bono de Descuento!</center><br><br>" +
            "<p>El siguiente usuario ha adquirido un bono de descuento.</p><br>"+
            "Nombre: " + objUsuario.getNombre() + "<br>Ciudad: " + objUsuario.getCiudad();
        //mail.Attachments.Add(adjunto);

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("asalazarmarin@gmail.com", "andresm1053827666") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);
    }

    //Metodo  para salir de las ventana bueno o malo
    public void cerrar()
    {
        panelCiudad.SetActive(true);
        panelCorrecto.SetActive(false);
        panelIncorrecto.SetActive(false);
        panelComentario.SetActive(true);
    }

    //Metodo para volver a iniciar el nivel 1
    public void reintentar()
    {
        SceneManager.LoadScene("nivel_1");

    }

    //Metodo para volver al menu principal
    public void Menu()
    {
        SceneManager.LoadScene("Scenes/menu");
    }

    //Metodo para salir del juego
    public void salirJuego()
    {
        Application.Quit();
    }
}
