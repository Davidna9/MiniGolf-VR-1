using UnityEngine;
using System.Collections;
using TMPro;

public class DetectorHoyo : MonoBehaviour
{
    [Header("Carteles de Resultado (Stickers)")]
    public GameObject pantallaGanaste; 
    public GameObject pantallaDerrota; 
    public TextMeshProUGUI golpesFinalText; 
    public TextMeshProUGUI tiempoFinalText; 

    [Header("Elementos de Arriba a Desaparecer")]
    public GameObject contadorGolpes;
    public GameObject golpesText;
    public GameObject contadorTiempo;
    public GameObject tiempoText;

    [Header("Condiciones del Juego (Modificables)")]
    public int golpesMaximos = 30; // Podés cambiar este número en el Inspector
    public float tiempoMaximoSegundos = 90f; // 1:30 minutos = 90 segundos

    private bool juegoTerminado = false;
    private float tiempoActual = 0f;

    void Update()
    {
        if (juegoTerminado) return;

        // 1. CONTROL DE TIEMPO LÍMITE
        tiempoActual += Time.deltaTime;
        if (tiempoActual >= tiempoMaximoSegundos)
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(false)); // Pierde por tiempo
        }

        // 2. CONTROL DE GOLPES MÁXIMOS MODIFICABLES
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
                        StartCoroutine(MostrarResultadoConRetraso(false)); // Pierde por superar golpes
                    }
                }
            }
        }

        // Teclas de prueba rápida para testear en el monitor de tu casa sin VR
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

    // DETECTOR FÍSICO REAL DEL HOYO (RECORDÁ MARCAR EL "IS TRIGGER" EN EL COLLIDER)
    private void OnTriggerEnter(Collider other)
    {
        if (juegoTerminado) return;

        // Lee si lo que entra se llama pelota o tiene el tag Player
        if (other.name.ToLower().Contains("pelota") || other.CompareTag("Player"))
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(true)); // ¡Victoria física real!
        }
    }

    IEnumerator MostrarResultadoConRetraso(bool gano)
    {
        Debug.Log("Fin de partida detectado. Esperando 1 segundo de retraso...");
        yield return new WaitForSeconds(1f); // ESPERA CAMBIADA A 1 SEGUNDO EXACTO

        // Copia los récords reales que el jugador sumó arriba en los carteles de frente
        if (golpesFinalText != null && golpesText != null)
            golpesFinalText.text = golpesText.GetComponent<TextMeshProUGUI>().text;

        if (tiempoFinalText != null && tiempoText != null)
            tiempoFinalText.text = tiempoText.GetComponent<TextMeshProUGUI>().text;

        // Hace desaparecer por completo los marcos flotantes de la interfaz de arriba
        if (contadorGolpes != null) contadorGolpes.SetActive(false);
        if (golpesText != null) golpesText.SetActive(false);
        if (contadorTiempo != null) contadorTiempo.SetActive(false);
        if (tiempoText != null) tiempoText.SetActive(false);

        // Prende el sticker correspondiente adelante de todo el mapa
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
