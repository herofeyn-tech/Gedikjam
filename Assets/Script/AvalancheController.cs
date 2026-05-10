using UnityEngine;
using UnityEngine.SceneManagement;

public class AvalancheController : MonoBehaviour
{
    public Transform kamyon;
    public float temelHiz = 15f; // Týrýn hýzýndan biraz daha yüksek tut
    public float maxHiz = 25f;   // Uzaktayken yetiþme hýzý

    void Update()
    {
        if (kamyon == null) return;

        float mesafe = Vector3.Distance(transform.position, kamyon.position);

        // Hýz ayarý: Eðer çok uzaktaysa hýzlý gelsin, 
        // Yakýndaysa yavaþlamasýn, en az 'temelHiz' ile devam etsin.
        float suAnkiHiz = (mesafe > 30f) ? maxHiz : temelHiz;

        // Yön hesabý (Sadece Yatayda - Z ve X ekseninde)
        Vector3 hedefPos = new Vector3(kamyon.position.x, transform.position.y, kamyon.position.z);
        Vector3 yon = (hedefPos - transform.position).normalized;

        // HAREKET: Ýçinden geçmesi için direkt pozisyonu güncelliyoruz
        transform.position += yon * suAnkiHiz * Time.deltaTime;

        // Çýðýn týrýn yönüne bakmasý
        transform.LookAt(hedefPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Kamyonun Tag'i "Player" olmalý
        if (other.CompareTag("Player"))
        {
            Debug.Log("ÇIÐ YAKALADI!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}