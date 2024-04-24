using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ACalcular : MonoBehaviour
{
    int primerValor, segundoValor, Temporal, RespuestaFinal, Alternativa1, Alternativa2, puntaje;
    int respuestasCorrectas = 0;
    int respuestasIncorrectas = 0;
    public Animator AnimatorFondo, AnimatorRespuesta;
    public Text PrimerDigito, SegundoDigito, Signo, Alt_1, Alt_2, Alt_3, Respuesta_contiene;
    public Text contadorCorrectas, contadorIncorrectas, estadoJuego;
    public Text correctasModal;
    public Sprite SpriteSi, SpriteNo, SpriteTransparente;
    public Texture TexturaSuma, TexturaResta, TexturaMultiple, TexturaDivide;
    private string TempoOp, VarSigno;
    public GameObject Canvas, Bien_o_Mal_1, Bien_o_Mal_2, Bien_o_Mal_3;
    public Transform ElNuevoGlobo;
    public GameObject panelOperaciones;
    public GameObject panelOptions;

    public GameOverScreen gameOverScreen;

    public AudioSource ElcomponenteAudio;
    public AudioClip SonidoSi, SonidoNo;
    public Timer timer;

    private string estadillojuego;

    public Button option_1;
    public Button option_2;
    public Button option_3;
    private List<Button> botones = new List<Button>(); //Lista para almacenar las opciones 

    void Start()
    {
        Canvas.SetActive(false);
        GameObject.Find("Main Camera").transform.position = new Vector3(-0.43f, 0f, -10f);
        puntaje = 0;
        ActualizarContadores();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // para sumar
        {
            ACalcularFn("suma");
        }
        if (Input.GetKeyDown(KeyCode.R)) // para restar
        {
            ACalcularFn("resta");
        }
        if (Input.GetKeyDown(KeyCode.M)) // para multiplicar
        {
            ACalcularFn("multiplica");
        }
        if (Input.GetKeyDown(KeyCode.D)) // para dividir
        {
            ACalcularFn("divide");
        }
    }

    public void FuncionParaSumar()
    {
        TempoOp = "suma";
        SalenBloques();
    }
    
    public void FuncionParaRestar()
    {
        TempoOp = "resta";
        SalenBloques();
    }
    
    public void FuncionParaDividir()
    {
        TempoOp = "divide";
        SalenBloques();
    }
    
    public void FuncionParaMultiplicar()
    {
        TempoOp = "multiplica";
        SalenBloques();
    }

    public void SalenBloques()
    {
        AnimatorFondo = GameObject.Find("Fondo").GetComponent<Animator>();
        AnimatorFondo.Play("Salida");

        Debug.Log("Agreguemos lo botone");
        //agregando los botones al Array, todos están en interactive True
        botones.Add(option_1);
        botones.Add(option_2);
        botones.Add(option_3);
        //Para la prueba de esto se llamará a la función para contar que los bótones están en interactive
        Debug.Log("Aquí llamaré a la función que revisa los interactable");
        revisarInteractive();
    }

    public void AmoverCamara()
    {
        GameObject.Find("Main Camera").transform.position = new Vector3(22.00f, 0f, -10f);
        if (TempoOp == "suma")
        {
            ACalcularFn("suma");
            GameObject.Find("FondoOperaciones").GetComponent<Renderer>().material.mainTexture = TexturaSuma;
            timer.IniciarTemporizador(); // Aseg�rate de que el objeto Timer est� asignado en el inspector.
        }
        if (TempoOp == "resta")
        {
            ACalcularFn("resta");
            GameObject.Find("FondoOperaciones").GetComponent<Renderer>().material.mainTexture = TexturaResta;
            timer.IniciarTemporizador(); // Aseg�rate de que el objeto Timer est� asignado en el inspector.
        }
        if (TempoOp == "multiplica") { ACalcularFn("multiplica"); GameObject.Find("FondoOperaciones").GetComponent<Renderer>().material.mainTexture = TexturaMultiple; }
        if (TempoOp == "divide") { ACalcularFn("divide"); GameObject.Find("FondoOperaciones").GetComponent<Renderer>().material.mainTexture = TexturaDivide; }

        Canvas.SetActive(true);
    }

    public void RetornaMenu()
    {
        Canvas.SetActive(false);
        GameObject.Find("Main Camera").transform.position = new Vector3(-0.43f, 0f, -10f);
        AnimatorFondo = GameObject.Find("Fondo").GetComponent<Animator>();
        AnimatorFondo.Play("Inicia");
    }

    public void CambiarAOptions()
    {
        panelOperaciones.SetActive(false); // Desactiva el panel de operaciones
        panelOptions.SetActive(true); // Activa el panel de opciones
    }

    public void CambiarAOperaciones()
    {
        panelOptions.SetActive(false); // Desactiva el panel de opciones
        panelOperaciones.SetActive(true); // Activa el panel de operaciones
    }

    public void ACalcularFn(string operacion)
    {
        estadillojuego = operacion;
        Debug.Log("en acalcular operación que se pasa es "+ estadillojuego);

        ResetearValores();
        primerValor = Random.Range(1, 10);
        segundoValor = Random.Range(1, 10);

        if (primerValor - segundoValor < 0)
        {
            Temporal = segundoValor;
            segundoValor = primerValor;
            primerValor = Temporal;
        }

        if (operacion == "suma")
        {
            RespuestaFinal = primerValor + segundoValor;
            VarSigno = "suma";
        }

        if (operacion == "resta")
        {
            RespuestaFinal = primerValor - segundoValor;
            VarSigno = "resta";
        }

        if (operacion == "multiplica")
        {
            RespuestaFinal = primerValor * segundoValor;
            VarSigno = "multiplica";
        }

        if (operacion == "divide")
        {
            RespuestaFinal = primerValor / segundoValor;
            VarSigno = "divide";
        }

        PrimerDigito.text = primerValor.ToString();
        SegundoDigito.text = segundoValor.ToString();

        if (VarSigno == "suma")
        {
            Signo.text = " + ";
        }

        if (VarSigno == "resta")
        {
            Signo.text = " - ";
        }

        if (VarSigno == "multiplica")
        {
            Signo.text = " * ";
        }

        if (VarSigno == "divide")
        {
            Signo.text = " � ";
        }

        // PRIMERA ALTERNATIVA
        Temporal = Random.Range(2, 20);
        while (Temporal == RespuestaFinal)
        {
            Temporal = Random.Range(2, 20);
        }
        Alternativa1 = Temporal;

        // SEGUNDA ALTERNATIVA
        Temporal = Random.Range(2, 20);
        while ((Temporal == RespuestaFinal) || (Temporal == Alternativa1))
        {
            Temporal = Random.Range(2, 20);
        }
        Alternativa2 = Temporal;

        // Ordenando Las Alternativas
        Temporal = Random.Range(1, 7);
        if (Temporal == 1)
        {
            Alt_1.text = RespuestaFinal.ToString(); Alt_2.text = Alternativa1.ToString(); Alt_3.text = Alternativa2.ToString();
        }
        if (Temporal == 2)
        {
            Alt_1.text = RespuestaFinal.ToString(); Alt_2.text = Alternativa2.ToString(); Alt_3.text = Alternativa1.ToString();
        }
        if (Temporal == 3)
        {
            Alt_1.text = Alternativa1.ToString(); Alt_2.text = RespuestaFinal.ToString(); Alt_3.text = Alternativa2.ToString();
        }
        if (Temporal == 4)
        {
            Alt_1.text = Alternativa1.ToString(); Alt_2.text = Alternativa2.ToString(); Alt_3.text = RespuestaFinal.ToString();
        }
        if (Temporal == 5)
        {
            Alt_1.text = Alternativa2.ToString(); Alt_2.text = RespuestaFinal.ToString(); Alt_3.text = Alternativa1.ToString();
        }
        if (Temporal == 6)
        {
            Alt_1.text = Alternativa2.ToString(); Alt_2.text = Alternativa1.ToString(); Alt_3.text = RespuestaFinal.ToString();
        }
    }

    public void Alt_1_accion()
    {
        if (Alt_1.text == RespuestaFinal.ToString())
        {
            Bien_o_Mal_1.GetComponent<Image>().sprite = SpriteSi;
            respuestasCorrectas++;
            ActualizarContadores();
            Acertaste();
        }
        else
        {
            Bien_o_Mal_1.GetComponent<Image>().sprite = SpriteNo;
            respuestasIncorrectas++;
            ActualizarContadores();
            Fallaste();
        }
    }

    public void Alt_2_accion()
    {
        if (Alt_2.text == RespuestaFinal.ToString())
        {
            Bien_o_Mal_2.GetComponent<Image>().sprite = SpriteSi;
            respuestasCorrectas++;
            ActualizarContadores();
            Acertaste();
        }
        else
        {
            Bien_o_Mal_2.GetComponent<Image>().sprite = SpriteNo;
            respuestasIncorrectas++;
            ActualizarContadores();
            Fallaste();
        }
    }

    public void Alt_3_accion()
    {
        if (Alt_3.text == RespuestaFinal.ToString())
        {
            Bien_o_Mal_3.GetComponent<Image>().sprite = SpriteSi;
            respuestasCorrectas++;
            ActualizarContadores();
            Acertaste();
        }
        else
        {
            Bien_o_Mal_3.GetComponent<Image>().sprite = SpriteNo;
            respuestasIncorrectas++;
            ActualizarContadores();
            Fallaste();
        }
    }

    public void ResetearValores()
    {
        Bien_o_Mal_1.GetComponent<Image>().sprite = SpriteTransparente;
        Bien_o_Mal_2.GetComponent<Image>().sprite = SpriteTransparente;
        Bien_o_Mal_3.GetComponent<Image>().sprite = SpriteTransparente;
        Respuesta_contiene.text = "?";
    }

    public void Fallaste()
    {
        ElcomponenteAudio.clip = SonidoNo;
        ElcomponenteAudio.Play();
        timer.DisminuirTiempo(40);
        Debug.Log("el tiempo de timer es de: " + timer.timerSlider.value);
        ActualizarEstadoJuego();
        if(timer.timerSlider.value <= 0.0){
            MostrarMensajePerdida();
        }
    }

    public void Acertaste()
    {
        desactivarBotones();

        Respuesta_contiene.text = RespuestaFinal.ToString();
        puntaje = puntaje + 1;
        AnimatorRespuesta = GameObject.Find("Respuesta").GetComponent<Animator>();
        AnimatorRespuesta.Play("Respuesta_Correcta");

        // Reproduce el sonido de acierto
        ElcomponenteAudio.clip = SonidoSi;
        ElcomponenteAudio.Play();

        ActualizarEstadoJuego();
        StartCoroutine(SiguientePregunta());
        timer.IniciarTemporizador();
    }

    IEnumerator SiguientePregunta()
    {
        reactivarBotones();

        yield return new WaitForSeconds(0.7f);
        ACalcularFn(VarSigno);
    }

    public void ActualizarContadores()
    {
        contadorCorrectas.text = "Correctas: " + respuestasCorrectas.ToString();
        correctasModal.text = contadorCorrectas.text;
        contadorIncorrectas.text = "Incorrectas: " + respuestasIncorrectas.ToString();
    }

    void ActualizarEstadoJuego()
    {
        int totalPreguntas = respuestasCorrectas + respuestasIncorrectas;

        if (totalPreguntas == 0)
        {
            estadoJuego.text = "Progreso: 0%";
        }
        else
        {
            float porcentajeCorrectas = (float)respuestasCorrectas / (float)totalPreguntas * 100f;

            if (porcentajeCorrectas >= 90f)
            {
                estadoJuego.text = "Excelente";
            }
            else
            {
                estadoJuego.text = "Progreso: " + porcentajeCorrectas.ToString("F2") + "%";
            }
        }
    }

    public void MostrarMensajePerdida()
    {
        // Aqu� puedes desactivar el juego o mostrar un panel con el mensaje "Perdiste"
        estadoJuego.text = "�Perdiste!";
        // Puedes a�adir m�s l�gica aqu�, como detener el juego, etc.
        FinalizarJuego();
    }

    void FinalizarJuego()
    {
        gameOverScreen.Setup(respuestasCorrectas, estadillojuego);
        Debug.Log("se està enviando a gameOverScreen "+ respuestasCorrectas + " respuestas correctas y el estado de juego que envio es: " + estadillojuego);
        Debug.Log("El puntaje lo vamos a reiniciar");
        puntaje = puntaje - puntaje;
        Debug.Log("Reinializaremos las respuestas a 0");
        respuestasCorrectas = 0;
        respuestasIncorrectas = 0;
        ActualizarEstadoJuego();
        Debug.Log("Pasemos a la siguiente pregunta luego de la perdida, llamando a la corrutina");
        StartCoroutine(SiguientePregunta());
        Debug.Log("Observando el Array de botones: " + botones);
    }

    //Método para calcular que las opciones seleccionadas seán maximo 2
    void revisarInteractive(){
        Debug.Log("Aquí confirmo que se llamó a revisarInteractive()");
        for (int i = 0; i < botones.Count; i++){
            Debug.Log("El interactable del botón en posición " + i + " es " + botones[i].interactable);
        }
    }

    void desactivarBotones(){
        for (int i = 0; i < botones.Count; i++){
           botones[i].interactable = false;
           Debug.Log("Se ha puesto el botón de id " + i + "en false");
        }
        revisarInteractive();
    }

    void reactivarBotones(){
        for (int i = 0; i < botones.Count; i++){
           botones[i].interactable = true;
           Debug.Log("Se ha puesto el botón de id " + i + " en true");
        }
        revisarInteractive();
    }

}
