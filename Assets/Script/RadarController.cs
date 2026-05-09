using UnityEngine;

public class RadarController : MonoBehaviour
{
    [Header("Ayarlar")]
    public Camera radarCamera; // RadarCam objesini buraya sürükle
    public float guncellemeSuresi = 2f; // Kaç saniyede bir yenilensin?

    [Header("Ses (Opsiyonel)")]
    public AudioSource radarTýkSesi; // O mekanik "týk" sesi (istersen ekle)

    void Start()
    {
        // Eðer kamera atanmadýysa uyarý ver
        if (radarCamera == null)
        {
            Debug.LogError("Radar Kamerasý atanmadý!");
            return;
        }

        // Oyun baþladýðýnda kamerayý kapat (önlem olarak)
        radarCamera.enabled = false;

        // "RadariYenile" fonksiyonunu, 0. saniyeden baþlayarak 'guncellemeSuresi'nde bir tekrarla
        InvokeRepeating("RadariYenile", 0f, guncellemeSuresi);
    }

    void RadariYenile()
    {
        // 1. Kamerayý BÝR KARELÝÐÝNE çalýþtýr ve RenderTexture'a görüntüyü bas
        radarCamera.Render();

        // 2. Eðer bir ses dosyasý koyduysan onu çal
        if (radarTýkSesi != null)
        {
            radarTýkSesi.Play();
        }

        // Debug.Log("Radar Güncellendi!"); // Test için açabilirsin
    }
}