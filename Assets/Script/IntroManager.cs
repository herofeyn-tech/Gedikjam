using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string anaOyunSahnesiAdi = "SampleScene"; // Asıl oyun sahnesinin adını buraya yaz

    void Start()
    {
        // Video bittiğinde çalışacak fonksiyonu kaydediyoruz
        videoPlayer.loopPointReached += SahneyiDegistir;
    }

    void SahneyiDegistir(VideoPlayer vp)
    {
        // Video bittiğinde asıl oyun sahnesini yükle
        SceneManager.LoadScene(anaOyunSahnesiAdi);
    }

    void Update()
    {
        // Eğer oyuncu videoyu geçmek isterse (Örn: Boşluk tuşu)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(anaOyunSahnesiAdi);
        }
    }
}