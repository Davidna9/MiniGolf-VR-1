using UnityEngine;
using System.Collections;
using TMPro;

public class DetectorHoyo : MonoBehaviour
{
    [Header("Carteles de Resultado")]
    public GameObject pantallaGanaste; 
    public GameObject pantallaDerrota; 
    public TextMeshProUGUI golpesFinalText; 
    public TextMeshProUGUI tiempoFinalText; 

    [Header("Elementos de Arriba a Desaparecer")]
    public GameObject contadorGolpes;
    public GameObject golpesText;
    public GameObject contadorTiempo;
    public GameObject tiempoText;

    [Header("Condiciones de Derrota")]
<<<<<<< HEAD
    public int golpesMaximos = 30;
=======
    public int golpesMaximos = 10;
>>>>>>> e4d79ebc2cd5062650a44c22204a4c3928f85671
    public float tiempoMaximoSegundos = 90f; // 1 minuto y 30 segundos son 90 segundos

    private bool juegoTerminado = false;
    private float tiempoActual = 0f;

    void Start()
    {
        tiempoActual = 0f;
    }

    void Update()
    {
        if (juegoTerminado) return;

<<<<<<< HEAD
        // 1. Control de tiempo máximo (1:30)
=======
        // 1. CONTROL DE TIEMPO MÁXIMO
        // Sumamos el tiempo real de la partida
>>>>>>> e4d79ebc2cd5062650a44c22204a4c3928f85671
        tiempoActual += Time.deltaTime;
        if (tiempoActual >= tiempoMaximoSegundos)
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(false)); // Pierde por tiempo
        }

<<<<<<< HEAD
        // 2. Control de golpes máximos (30)
=======
        // 2. CONTROL DE GOLPES MÁXIMOS
        // Leemos los golpes que el palo va sumando en tus números de arriba
>>>>>>> e4d79ebc2cd5062650a44c22204a4c3928f85671
        if (golpesText != null)
        {
            TextMeshProUGUI textoUI = golpesText.GetComponent<TextMeshProUGUI>();
            if (textoUI != null)
            {
                int golpesActuales;
                if (int.TryParse(textoUI.text, out golpesActuales))
                {
                    if (golpesActuales >= golpesMaximos)
                    {
                        juegoTerminado = true;
                        StartCoroutine(MostrarResultadoConRetraso(false)); // Pierde por golpes
                    }
                }
            }
        }

<<<<<<< HEAD
        // Pruebas de teclado para testear rápido en el monitor sin el casco puesto
        if (Input.GetKeyDown(KeyCode.G))
=======
        // 3. TRUCOS DE TECLADO PARA PROBAR EN TU CASA SIN VISOR VR
        if (Input.GetKeyDown(KeyCode.G)) // Forzar Ganar
>>>>>>> e4d79ebc2cd5062650a44c22204a4c3928f85671
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(true));
        }
<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.P))
=======
        if (Input.GetKeyDown(KeyCode.P)) // Forzar Perder
>>>>>>> e4d79ebc2cd5062650a44c22204a4c3928f85671
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(false));
        }
    }

<<<<<<< HEAD
    // ESTE ES EL TRIGGER FÍSICO QUE SE ACTIVA CUANDO EMBOCÁS LA PELOTA
=======
>>>>>>> e4d79ebc2cd5062650a44c22204a4c3928f85671
    private void OnTriggerEnter(Collider other)
    {
        if (juegoTerminado) return;

<<<<<<< HEAD
        // Si lo que entra a la pelota negra se llama "pelota" o tiene el tag Player
        if (other.name.ToLower().Contains("pelota") || other.CompareTag("Player"))
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(true)); // ¡Victoria física real!
=======
        // Si la pelota entra físicamente al sensor del hoyo antes del límite
        if (other.name.ToLower().Contains("pelota") || other.CompareTag("Player"))
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(true)); // ¡Victoria!
>>>>>>> e4d79ebc2cd5062650a44c22204a4c3928f85671
        }
    }

    IEnumerator MostrarResultadoConRetraso(bool gano)
    {
        Debug.Log("Procesando fin de partida. Esperando 3 segundos de retraso...");
        yield return new WaitForSeconds(3f); 

<<<<<<< HEAD
        // Copia los números reales antes de borrar la interfaz de arriba
=======
        // Copia los datos finales en las casillas del cartel correspondiente
>>>>>>> e4d79ebc2cd5062650a44c22204a4c3928f85671
        if (golpesFinalText != null && golpesText != null)
            golpesFinalText.text = golpesText.GetComponent<TextMeshProUGUI>().text;

        if (tiempoFinalText != null && tiempoText != null)
            tiempoFinalText.text = tiempoText.GetComponent<TextMeshProUGUI>().text;

<<<<<<< HEAD
        // Hace desaparecer los marcadores neón flotantes de arriba
=======
        // Hace desaparecer por completo la interfaz flotante de arriba
>>>>>>> e4d79ebc2cd5062650a44c22204a4c3928f85671
        if (contadorGolpes != null) contadorGolpes.SetActive(false);
        if (golpesText != null) golpesText.SetActive(false);
        if (contadorTiempo != null) contadorTiempo.SetActive(false);
        if (tiempoText != null) tiempoText.SetActive(false);

<<<<<<< HEAD
        // Prende el sticker que corresponda en el centro del monitor
=======
        // Prende el cartel que corresponda según el resultado
>>>>>>> e4d79ebc2cd5062650a44c22204a4c3928f85671
        if (gano && pantallaGanaste != null)
        {
            pantallaGanaste.SetActive(true);
        }
        else if (!gano && pantallaDerrota != null)
        {
            pantallaDerrota.SetActive(true);
        }
    }
} 
