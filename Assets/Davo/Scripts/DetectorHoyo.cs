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
    public int golpesMaximos = 30;
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

        // 1. Control de tiempo máximo (1:30)
        tiempoActual += Time.deltaTime;
        if (tiempoActual >= tiempoMaximoSegundos)
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(false)); // Pierde por tiempo
        }

        // 2. Control de golpes máximos (30)
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

        // Pruebas de teclado para testear rápido en el monitor sin el casco puesto
        if (Input.GetKeyDown(KeyCode.G))
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(true));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(false));
        }
    }

    // ESTE ES EL TRIGGER FÍSICO QUE SE ACTIVA CUANDO EMBOCÁS LA PELOTA
    private void OnTriggerEnter(Collider other)
    {
        if (juegoTerminado) return;

        // Si lo que entra a la pelota negra se llama "pelota" o tiene el tag Player
        if (other.name.ToLower().Contains("pelota") || other.CompareTag("Player"))
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(true)); // ¡Victoria física real!
        }
    }

    IEnumerator MostrarResultadoConRetraso(bool gano)
    {
        Debug.Log("Procesando fin de partida. Esperando 3 segundos de retraso...");
        yield return new WaitForSeconds(3f); 

        // Copia los números reales antes de borrar la interfaz de arriba
        if (golpesFinalText != null && golpesText != null)
            golpesFinalText.text = golpesText.GetComponent<TextMeshProUGUI>().text;

        if (tiempoFinalText != null && tiempoText != null)
            tiempoFinalText.text = tiempoText.GetComponent<TextMeshProUGUI>().text;

        // Hace desaparecer los marcadores neón flotantes de arriba
        if (contadorGolpes != null) contadorGolpes.SetActive(false);
        if (golpesText != null) golpesText.SetActive(false);
        if (contadorTiempo != null) contadorTiempo.SetActive(false);
        if (tiempoText != null) tiempoText.SetActive(false);

        // Prende el sticker que corresponda en el centro del monitor
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
