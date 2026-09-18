using UnityEngine;
using System.Collections;

public class DetectorHoyo : MonoBehaviour
{
    [Header("Cartel de Victoria")]
    public GameObject pantallaGanaste; 
    public TMPro.TextMeshProUGUI golpesFinalText; 
    public TMPro.TextMeshProUGUI tiempoFinalText; 

    [Header("Elementos de Arriba a Desaparecer")]
    public GameObject contadorGolpes;
    public GameObject golpesText;
    public GameObject contadorTiempo;
    public GameObject tiempoText;

    private void OnTriggerEnter(Collider other)
    {
        // Detecta el impacto de la pelota
        if (other.name.ToLower().Contains("pelota") || other.CompareTag("Player"))
        {
            StartCoroutine(MostrarVictoriaConRetraso());
        }
    }

    IEnumerator MostrarVictoriaConRetraso()
    {
        Debug.Log("¡Pelota en el hoyo! Esperando 3 segundos...");
        yield return new WaitForSeconds(3f); // Espera de festejo en VR

        // 1. Traspaso de datos automáticos al cartel transparente
        if (golpesFinalText != null && golpesText != null)
            golpesFinalText.text = golpesText.GetComponent<TMPro.TextMeshProUGUI>().text;

        if (tiempoFinalText != null && tiempoText != null)
            tiempoFinalText.text = tiempoText.GetComponent<TMPro.TextMeshProUGUI>().text;

        // 2. Hace desaparecer los 4 elementos neón de arriba
        if (contadorGolpes != null) contadorGolpes.SetActive(false);
        if (golpesText != null) golpesText.SetActive(false);
        if (contadorTiempo != null) contadorTiempo.SetActive(false);
        if (tiempoText != null) tiempoText.SetActive(false);

        // 3. Enciende el cartel de Ganaste solo en el centro
        if (pantallaGanaste != null) pantallaGanaste.SetActive(true);
    }
}
