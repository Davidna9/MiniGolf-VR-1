using UnityEngine;
using TMPro; // Obligatorio para controlar TextMeshPro

public class Cronometro : MonoBehaviour
{
    // En este casillero vas a arrastrar tu TiempoText desde el Hierarchy
    public TextMeshProUGUI textoTiempo; 
    
    private float tiempoTranscurrido = 0f;

    void Update()
    {
        // Suma el tiempo real en segundos frame por frame
        tiempoTranscurrido += Time.deltaTime;

        // Calcula los minutos y segundos matemáticamente
        int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60F);
        int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60F);

        // Actualiza el cartel con el formato clásico de reloj (TIEMPO: 00:00)
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}
