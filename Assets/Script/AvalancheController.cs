using UnityEngine;
using UnityEngine.SceneManagement;

public class NewAvalancheController : MonoBehaviour
{
    [Header("Takip Ayarlarý")]
    public Transform hedefKamyon;
    public float temelHiz = 18f;
    public float yakalamaHiz = 35f;

    [Header("Atmosfer")]
    public float olumSisiYogunlugu = 0.7f;

    void FixedUpdate()
    {
        if (hedefKamyon == null) return;

        float mesafe = Vector3.Distance(transform.position, hedefKamyon.position);
        float suAnkiHiz = (mesafe > 30f) ? yakalamaHiz : temelHiz;

        Vector3 hedefPos = new Vector3(hedefKamyon.position.x, transform.position.y, hedefKamyon.position.z);
        transform.position = Vector3.MoveTowards(transform.position, hedefPos, suAnkiHiz * Time.fixedDeltaTime);

        if (mesafe > 3f)
        {
            transform.LookAt(hedefPos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. KONTROL: Çarptýðýmýz objenin kendisi, root'u (en üstü) veya baðlý olduðu Rigidbody "Player" mý?
        // Bu sayede týrýn hangi parçasýna çarparsa çarpsýn ölüm tetiklenir.
        bool oyuncuyaCarpti = other.CompareTag("Player") ||
                             (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player")) ||
                             other.transform.root.CompareTag("Player");

        if (oyuncuyaCarpti)
        {
            Debug.Log("ÇIÐ YAKALADI: " + other.name); // Konsoldan kontrol etmek için

            // Sisi bembeyaz yap
            UnityEngine.RenderSettings.fogDensity = olumSisiYogunlugu;

            // Sahneyi yeniden yükle
            RestartGame();
        }
        else
        {
            // Eðer bir þeye çarpýyor ama ölmüyorsan, konsolda neye çarptýðýný gör:
            Debug.Log("Çýð bir þeye çarptý ama 'Player' etiketi bulamadý: " + other.name);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}