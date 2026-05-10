using UnityEngine;

public class RadioManager : MonoBehaviour
{
    [Header("Referanslar")]
    public AudioSource radioSource; // Filtreleri eklediðin AudioSource
    public AudioClip[] sarkilar;    // Akira Yamaoka þarký listesi

    private int mevcutSarkiIndex = 0;

    void Start()
    {
        if (sarkilar.Length > 0 && radioSource != null)
        {
            SarkiyiCal();
        }
    }

    void Update()
    {
        // "U" tuþuna basýldýðýnda sýradaki þarkýya geç
        if (Input.GetKeyDown(KeyCode.U))
        {
            SiradakiSarkiyaGec();
        }

        // Þarký bittiðinde otomatik sýradakine geçmesi için
        if (!radioSource.isPlaying)
        {
            SiradakiSarkiyaGec();
        }
    }

    void SarkiyiCal()
    {
        radioSource.clip = sarkilar[mevcutSarkiIndex];
        radioSource.Play();
        Debug.Log("Radyoda Çalan: " + sarkilar[mevcutSarkiIndex].name);
    }

    public void SiradakiSarkiyaGec()
    {
        if (sarkilar.Length == 0) return;

        // Index'i bir artýr, liste sonuna gelince baþa dön (Mod alma mantýðý)
        mevcutSarkiIndex = (mevcutSarkiIndex + 1) % sarkilar.Length;
        SarkiyiCal();
    }
}