using UnityEngine;

public class PaloMiniGolf : MonoBehaviour
{
    [Header("Configuracion Fisica")]
    public float multiplicadorFuerza = 1.5f;
    public float fuerzaMinimaParaGolpe = 0.2f;
    public float radioDeteccionPalo = 0.15f;

    [Header("Efectos de Satisfaccion")]
    public AudioSource audioSource;
    public GameObject efectoParticulasPrefab;

    private Vector3 posicionAnterior;
    private Vector3 vectorVelocidadPalo;

    void Start()
    {
        posicionAnterior = transform.position;
    }

    void Update()
    {
        vectorVelocidadPalo = (transform.position - posicionAnterior) / Time.deltaTime;
        
        ProcesarImpactoRaycast();

        posicionAnterior = transform.position;
    }

    private void ProcesarImpactoRaycast()
    {
        Vector3 direccionMovimiento = transform.position - posicionAnterior;
        float distanciaMovimiento = direccionMovimiento.magnitude;

        if (distanciaMovimiento > 0.001f)
        {
            RaycastHit hit;
            if (Physics.SphereCast(posicionAnterior, radioDeteccionPalo, direccionMovimiento.normalized, out hit, distanciaMovimiento))
            {
                if (hit.collider.CompareTag("Pelota"))
                {
                    Rigidbody rbPelota = hit.collider.GetComponent<Rigidbody>();
                    if (rbPelota != null)
                    {
                        float velocidadMando = vectorVelocidadPalo.magnitude;

                        if (velocidadMando < fuerzaMinimaParaGolpe) return;

                        Vector3 direccionFisica = new Vector3(vectorVelocidadPalo.x, 0f, vectorVelocidadPalo.z).normalized;
                        float fuerzaFinal = velocidadMando * multiplicadorFuerza;

                        rbPelota.AddForce(direccionFisica * fuerzaFinal, ForceMode.VelocityChange);

                        DispararFeedbackSensorial(velocidadMando, hit.point);
                    }
                }
            }
        }
    }

    private void DispararFeedbackSensorial(float intensidadGolpe, Vector3 puntoImpacto)
    {
        if (audioSource != null)
        {
            audioSource.volume = Mathf.Clamp(intensidadGolpe / 5f, 0.2f, 1.0f);
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.Play();
        }

        if (efectoParticulasPrefab != null)
        {
            Destroy(Instantiate(efectoParticulasPrefab, puntoImpacto, Quaternion.identity), 1f);
        }
    }
}
