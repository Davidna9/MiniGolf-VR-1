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
    public float tiempoMaximoSegundos = 90f; 

    private bool juegoTerminado = false;
    private float tiempoActual = 0f;

    void Update()
    {
        if (juegoTerminado) return;

        // Control de tiempo máximo (1:30)
        tiempoActual += Time.deltaTime;
        if (tiempoActual >= tiempoMaximoSegundos)
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(false));
        }

        // Control de golpes máximos (30)
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
                        StartCoroutine(MostrarResultadoConRetraso(false));
                    }
                }
            }
        }

        // Teclas de prueba para testear rápido en casa
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

    private void OnTriggerEnter(Collider other)
    {
        if (juegoTerminado) return;

        if (other.name.ToLower().Contains("pelota") || other.CompareTag("Player"))
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(true));
        }
    }

    IEnumerator MostrarResultadoConRetraso(bool gano)
    {
        Debug.Log("Fin de partida detectado. Esperando 3 segundos...");
        yield return new WaitForSeconds(3f); 

        // Copia los números reales a los carteles
        if (golpesFinalText != null && golpesText != null)
            golpesFinalText.text = golpesText.GetComponent<TextMeshProUGUI>().text;

        if (tiempoFinalText != null && tiempoText != null)
            tiempoFinalText.text = tiempoText.GetComponent<TextMeshProUGUI>().text;

        // Apaga los marcadores de arriba
        if (contadorGolpes != null) contadorGolpes.SetActive(false);
        if (golpesText != null) golpesText.SetActive(false);
        if (contadorTiempo != null) contadorTiempo.SetActive(false);
        if (tiempoText != null) tiempoText.SetActive(false);

        // Enciende el sticker que corresponda
        if (gano && pantallaGanaste != null) pantallaGanaste.SetActive(true);
        else if (!gano && pantallaDerrota != null) pantallaDerrota.SetActive(true);
    }
}
