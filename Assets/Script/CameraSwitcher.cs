using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Ayarlar")]
    public float transitionSpeed = 10f; // Kafa çevirme hýzý
    private Transform targetTransform;

    [Header("Bakýþ Noktalarý")]
    public Transform frontPos;
    public Transform radarPos;
    public Transform rearPos;

    void Start()
    {
        if (frontPos != null)
        {
            targetTransform = frontPos;
            // Oyun baþýnda kamerayý direkt ön bakýþa ýþýnlayalým ki zýplama yapmasýn
            transform.localPosition = frontPos.localPosition;
            transform.localRotation = frontPos.localRotation;
        }
    }

    void Update()
    {
        // Tuþ giriþlerini Update içinde yakalamak en saðlýklýsýdýr
        if (Input.GetKeyDown(KeyCode.Alpha1)) targetTransform = frontPos;
        if (Input.GetKeyDown(KeyCode.Alpha2)) targetTransform = radarPos;
        if (Input.GetKeyDown(KeyCode.Alpha3)) targetTransform = rearPos;
    }

    void LateUpdate()
    {
        // KRÝTÝK: LateUpdate kullanarak kameranýn týrýn fizik hareketini 
        // (FixedUpdate) tamamlamasýný bekliyoruz. Bu delay sorununu çözer.

        if (targetTransform == null) return;

        // KRÝTÝK: position yerine localPosition kullanarak kamerayý týrýn içine sabitliyoruz.
        // Týr dünyada nereye giderse gitsin, kamera sadece týrýn içine göre yer deðiþtirir.
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetTransform.localPosition, Time.deltaTime * transitionSpeed);

        // Rotasyonu da yine týrýn içine göre (localRotation) yumuþatýyoruz.
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetTransform.localRotation, Time.deltaTime * transitionSpeed);
    }
}