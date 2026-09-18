using UnityEngine;

public class DetectorGolpePalo : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Si chocamos contra la pelota...
        if (collision.gameObject.name.ToLower().Contains("pelota"))
        {
            // Buscamos tu script del marcador y sumamos el golpe
            LogicaContador contador = FindObjectOfType<LogicaContador>();
            if (contador != null)
            {
                contador.SumarGolpe();
            }
        }
    }
}
