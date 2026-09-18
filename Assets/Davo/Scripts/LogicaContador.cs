using UnityEngine;
using TMPro; // Obligatorio para cambiar tus números neón

public class LogicaContador : MonoBehaviour
{
    public TextMeshProUGUI textoGolpes; // Casilla para tu GolpesText
    private int cantidadGolpes = 0;

    void Start()
    {
        cantidadGolpes = 0;
        ActualizarTexto();
    }

    // Esta función la va a llamar el palo cada vez que choque la pelota
    public void SumarGolpe()
    {
        cantidadGolpes++;
        ActualizarTexto();
        Debug.Log("¡Golpe registrado! Total: " + cantidadGolpes);
    }

    void ActualizarTexto()
    {
        if (textoGolpes != null)
        {
            textoGolpes.text = cantidadGolpes.ToString("00"); // Mantiene el formato neón de dos dígitos
        }
    }
}
