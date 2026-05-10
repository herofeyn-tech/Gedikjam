using UnityEngine;

public class KamyonHareket : MonoBehaviour
{
    [Header("Referanslar")]
    public Rigidbody rb;
    public Transform driverCamera; // Kamyon içindeki kamerayı buraya sürükle

    [Header("Hareket Ayarları")]
    public float moveSpeed = 45f;       
    public float acceleration = 5f;    // Hızlanma softluğu
    public float rotationSpeed = 4f;    // 45 dereceye geçiş softluğu

    private float moveInputX;
    private float moveInputZ;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        
        // Eğer kamera atanmadıysa otomatik olarak sahnedeki ana kamerayı bul
        if (driverCamera == null && Camera.main != null)
            driverCamera = Camera.main.transform;
    }

    void FixedUpdate()
    {
        // Girdiler
        moveInputX = Input.GetAxis("Vertical");   // W (1) / S (-1) -> X Ekseni
        moveInputZ = Input.GetAxis("Horizontal"); // D (1) / A (-1) -> Z Ekseni

        ApplyCameraRelativeMovement();
        ApplySoftRotation();
    }

    void ApplyCameraRelativeMovement()
    {
        if (driverCamera == null) return;

        // Kameranın ileri ve sağ yönlerini alıyoruz (Y eksenini sıfırlıyoruz ki yere batmasın)
        Vector3 forward = driverCamera.forward;
        Vector3 right = driverCamera.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Senin istediğin eksen mantığı: 
        // W/S ile kameranın ileri hattında (X gibi düşün), 
        // A/D ile kameranın yan hattında (Z gibi düşün) hareket.
        Vector3 moveDirection = (forward * moveInputX) + (right * moveInputZ);
        
        Vector3 targetVelocity = moveDirection * moveSpeed;
        targetVelocity.y = rb.linearVelocity.y; // Yerçekimini koru

        // SOFT HAREKET: Mevcut hızı hedef hıza Lerp ile yumuşakça yaklaştır
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, Time.fixedDeltaTime * acceleration);
    }

    void ApplySoftRotation()
    {
        // A/D basılınca max 45 derece soft rotasyon (Şerit değiştirme hissi)
        float targetYRotation = moveInputZ * 45f;
        
        // Mevcut rotasyonu hedef açıya Slerp ile yumuşat
        Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);
        
        // transform.rotation yerine localRotation kullanarak tırın genel yönünü bozmadan 
        // sadece o anki "açısını" yumuşatıyoruz.
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
    }
}