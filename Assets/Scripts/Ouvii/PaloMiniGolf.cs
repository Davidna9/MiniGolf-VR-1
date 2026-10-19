using UnityEngine;

public class PaloMiniGolf : MonoBehaviour
{
    [Header("Configuración Física")]
    public float multiplicadorFuerza = 1.5f;
    public float fuerzaMinimaParaGolpe = 0.2f;
    public float radioDeteccionPalo = 0.15f;
    public float cooldownGolpe = 0.2f; 

    [Header("Efectos de Satisfacción")]
    public AudioSource audioSource;
    public GameObject efectoParticulasPrefab;

    private Vector3 posicionAnterior;
    private Vector3 vectorVelocidadPalo;
    private float tiempoUltimoGolpe;

    void Start()
    {
        posicionAnterior = transform.position;
        tiempoUltimoGolpe = -cooldownGolpe;
    }

    void FixedUpdate()
    {
        vectorVelocidadPalo = (transform.position - posicionAnterior) / Time.fixedDeltaTime;
        ProcesarImpactoRaycast();
        posicionAnterior = transform.position;
    }

    private void ProcesarImpactoRaycast()
    {
        if (Time.time < tiempoUltimoGolpe + cooldownGolpe) return;

        Vector3 direccionMovimiento = transform.position - posicionAnterior;
        float distanciaMovimiento = direccionMovimiento.magnitude;

        if (distanciaMovimiento > 0.001f)
        {
            RaycastHit hit;
            Vector3 direccionNormalizada = direccionMovimiento / distanciaMovimiento;
            bool impactoDetectado = false;

            if (Physics.SphereCast(posicionAnterior, radioDeteccionPalo, direccionNormalizada, out hit, distanciaMovimiento))
            {
                if (hit.collider.CompareTag("Pelota"))
                {
                    impactoDetectado = true;
                }
            }
            
            if (!impactoDetectado)
            {
                if (Physics.SphereCast(transform.position, radioDeteccionPalo, -direccionNormalizada, out hit, distanciaMovimiento))
                {
                    if (hit.collider.CompareTag("Pelota"))
                    {
                        impactoDetectado = true;
                    }
                }
            }

            if (impactoDetectado)
            {
                Rigidbody rbPelota = hit.collider.GetComponent<Rigidbody>();
                if (rbPelota != null)
                {
                    float velocidadMando = vectorVelocidadPalo.magnitude;
                    
                    if (velocidadMando < fuerzaMinimaParaGolpe) return;

                    Vector3 direccionFisica = new Vector3(vectorVelocidadPalo.x, 0f, vectorVelocidadPalo.z).normalized;
                    
                    if (direccionFisica == Vector3.zero)
                    {
                        direccionFisica = new Vector3(direccionNormalizada.x, 0f, direccionNormalizada.z).normalized;
                    }

                    float fuerzaFinal = velocidadMando * multiplicadorFuerza;
                    
                    rbPelota.AddForce(direccionFisica * fuerzaFinal, ForceMode.VelocityChange);
                    tiempoUltimoGolpe = Time.time;

                    DispararFeedbackSensorial(velocidadMando, hit.point);
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
            GameObject particulas = Instantiate(efectoParticulasPrefab, puntoImpacto, Quaternion.identity);
            Destroy(particulas, 1f);
        }
    }
}
