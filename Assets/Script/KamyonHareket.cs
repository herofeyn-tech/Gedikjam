using UnityEngine;
using UnityEngine.SceneManagement;

public class KamyonHareket : MonoBehaviour
{
    [Header("Referanslar")]
    public Rigidbody rb;
    public Transform driverCamera;

    [Header("Hareket Ayarları")]
    public float moveSpeed = 45f;
    public float acceleration = 5f;
    public float rotationSpeed = 4f;

    [Header("Hasar ve Ses Ayarları")]
    public AudioSource motorSesiNormal;   // Harıl harıl çalışan temiz ses
    public AudioSource motorSesiHasarli; // Traktör gibi hırlayan ses
    public AudioSource metalHasarSesi;   // Çarpma anında çalacak efekt

    [Range(0, 1)] public float hasarHizKesintisi = 0.5f;

    private float moveInputX;
    private float moveInputZ;
    private float currentMoveSpeed;
    private int carpanEngelSayisi = 0;
    private bool tırCalisiyorMu = true;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        currentMoveSpeed = moveSpeed;

        if (driverCamera == null && Camera.main != null)
            driverCamera = Camera.main.transform;

        if (motorSesiNormal) motorSesiNormal.Play();
        if (motorSesiHasarli) motorSesiHasarli.Stop();
    }

    void FixedUpdate()
    {
        if (!tırCalisiyorMu)
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.fixedDeltaTime * acceleration);
            return;
        }

        moveInputX = Input.GetAxis("Vertical");
        moveInputZ = Input.GetAxis("Horizontal");

        ApplyCameraRelativeMovement();
        ApplySoftRotation();
    }

    void ApplyCameraRelativeMovement()
    {
        if (driverCamera == null) return;

        Vector3 forward = driverCamera.forward;
        Vector3 right = driverCamera.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * moveInputX) + (right * moveInputZ);
        Vector3 targetVelocity = moveDirection * currentMoveSpeed;
        targetVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, Time.fixedDeltaTime * acceleration);
    }

    void ApplySoftRotation()
    {
        float targetYRotation = moveInputZ * 45f;
        Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // "Obstacle" tag'ine sahip bir şeye çarptığımızda
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // METAL HASAR SESİNİ ÇAL
            // PlayOneShot kullanıyoruz ki ses bitmeden tekrar çarpılırsa üst üste binebilsin
            if (metalHasarSesi != null)
            {
                metalHasarSesi.PlayOneShot(metalHasarSesi.clip);
            }

            carpanEngelSayisi++;
            HasarDurumunuGuncelle();
        }
    }

    void HasarDurumunuGuncelle()
    {
        if (carpanEngelSayisi == 1)
        {
            currentMoveSpeed = moveSpeed * hasarHizKesintisi;

            if (motorSesiNormal) motorSesiNormal.Stop();
            if (motorSesiHasarli) motorSesiHasarli.Play();
        }
        else if (carpanEngelSayisi >= 2)
        {
            tırCalisiyorMu = false;
            currentMoveSpeed = 0;

            if (motorSesiHasarli) motorSesiHasarli.Stop();
            if (motorSesiNormal) motorSesiNormal.Stop();
        }
    }
}