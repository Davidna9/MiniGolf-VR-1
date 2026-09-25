using UnityEngine;

public class PaloMiniGolf : MonoBehaviour
{
    public float fuerza = 10f;
    private Rigidbody rbPalo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbPalo = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pelota"))
        {
            Rigidbody rbPelota = other.GetComponent<Rigidbody>();
            
            if (rbPelota != null)
            {
                Vector3 direccion = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;

                float velocidadMando = rbPalo.linearVelocity.magnitude;

                rbPelota.linearVelocity = direccion * (velocidadMando * fuerza);
            }
        }
    }
}
