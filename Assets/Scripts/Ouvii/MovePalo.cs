using UnityEngine;

public class MovePalo : MonoBehaviour
{ 
    public Transform mandoVR; 
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(mandoVR.position);
        rb.MoveRotation(mandoVR.rotation);
    }
}
