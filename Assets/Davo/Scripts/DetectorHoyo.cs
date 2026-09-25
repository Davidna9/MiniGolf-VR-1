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
    public int golpesMaximos = 10;
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

        // 1. CONTROL DE TIEMPO MÁXIMO
        // Sumamos el tiempo real de la partida
        tiempoActual += Time.deltaTime;
        if (tiempoActual >= tiempoMaximoSegundos)
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(false)); // Pierde por tiempo
        }

        // 2. CONTROL DE GOLPES MÁXIMOS
        // Leemos los golpes que el palo va sumando en tus números de arriba
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

        // 3. TRUCOS DE TECLADO PARA PROBAR EN TU CASA SIN VISOR VR
        if (Input.GetKeyDown(KeyCode.G)) // Forzar Ganar
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(true));
        }
        if (Input.GetKeyDown(KeyCode.P)) // Forzar Perder
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(false));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (juegoTerminado) return;

        // Si la pelota entra físicamente al sensor del hoyo antes del límite
        if (other.name.ToLower().Contains("pelota") || other.CompareTag("Player"))
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(true)); // ¡Victoria!
        }
    }

    IEnumerator MostrarResultadoConRetraso(bool gano)
    {
        Debug.Log("Procesando fin de partida. Esperando 3 segundos de retraso...");
        yield return new WaitForSeconds(3f); 

        // Copia los datos finales en las casillas del cartel correspondiente
        if (golpesFinalText != null && golpesText != null)
            golpesFinalText.text = golpesText.GetComponent<TextMeshProUGUI>().text;

        if (tiempoFinalText != null && tiempoText != null)
            tiempoFinalText.text = tiempoText.GetComponent<TextMeshProUGUI>().text;

        // Hace desaparecer por completo la interfaz flotante de arriba
        if (contadorGolpes != null) contadorGolpes.SetActive(false);
        if (golpesText != null) golpesText.SetActive(false);
        if (contadorTiempo != null) contadorTiempo.SetActive(false);
        if (tiempoText != null) tiempoText.SetActive(false);

        // Prende el cartel que corresponda según el resultado
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
