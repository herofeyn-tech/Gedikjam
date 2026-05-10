using UnityEngine;

public class TruckController : MonoBehaviour
{
    [Header("Referanslar")]
    public Rigidbody rb;
    public Transform steeringWheel; // Buraya pTorus1 objesini sürükle
    public Transform centerOfMassObject; // Buraya COM_Helper objesini sürükle

    [Header("Görsel Ayarlar")]
    public float steeringSmoothness = 5f;

    private float steerInput;
    private float visualWheelRotation;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        
        // Ağırlık merkezini COM_Helper'ın olduğu yerde sabitliyoruz
        if (centerOfMassObject != null)
            rb.centerOfMass = centerOfMassObject.localPosition;
    }

    void Update()
    {
        // Sadece pTorus1'in animasyonu için girişi alıyoruz
        steerInput = Input.GetAxis("Horizontal");
        AnimateSteering();
    }

    void AnimateSteering()
    {
        if (steeringWheel != null)
        {
            // Direksiyon görseli girişe göre 500 dereceye kadar yumuşakça döner
            float targetVisual = steerInput * 500f; 
            visualWheelRotation = Mathf.Lerp(visualWheelRotation, targetVisual, Time.deltaTime * steeringSmoothness);
            steeringWheel.localRotation = Quaternion.Euler(0, 0, -visualWheelRotation);
        }
    }
}