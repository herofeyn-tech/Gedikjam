using UnityEngine;

public class TruckController : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movement Settings")]
    public float downhillForce = 500f; // Sürekli ileri (5v)
    public float brakeForce = 300f;    // Frenleme gücü (3v)
    public float turnSpeed = 100f;     // Dönüþ hýzý

    void FixedUpdate()
    {
        // 1. DAÝMA ÝLERÝ (Downhill Momentum)
        // Yerçekimi ve yokuþ etkisiyle araç sürekli ileri itilir
        rb.AddForce(transform.forward * downhillForce);

        // 2. FRENLEME (Sadece yavaþlatýr, durdurmaz)
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(-transform.forward * brakeForce);
        }

        // 3. DÖNÜÞ (A / D)
        float turnInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(turnInput) > 0.1f)
        {
            // Araç hareket halindeyken daha iyi döner
            transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.fixedDeltaTime);
        }
    }
}