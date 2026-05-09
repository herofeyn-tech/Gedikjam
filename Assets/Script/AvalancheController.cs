using UnityEngine;
using UnityEngine.SceneManagement;

public class AvalancheController : MonoBehaviour
{
    public Transform kamyon; // Inspector'dan týrýný buraya sürükle
    public float temelHiz = 12f;
    public float yakalamaMesafesi = 30f;

    void Update()
    {
        if (kamyon == null) return;

        // 1. HIZ HESABI
        float suAnkiHiz = temelHiz;
        float mesafe = Vector3.Distance(transform.position, kamyon.position);

        // Oyuncu çok açýlýrsa çýð "hadi lan yakalayayým" diyip hýzlanýr
        if (mesafe > yakalamaMesafesi)
        {
            suAnkiHiz += 4f;
        }

        // 2. HEDEFE KÝLÝTLENME (Mýknatýs Mantýðý)
        // Kamyonun olduðu yöne doðru bir yön vektörü oluþturuyoruz
        Vector3 yon = (kamyon.position - transform.position).normalized;

        // Çýðýn havaya uçmamasý veya yere girmemesi için Y (yükseklik) farkýný siliyoruz
        yon.y = 0;

        // Çýðý her karede direkt senin olduðun yöne doðru itiyoruz
        transform.position += yon * suAnkiHiz * Time.deltaTime;

        // 3. GÖRSEL DÜZELTME
        // Çýðýn sana "bakmasýný" saðlar, böylece yan yan gelmez, yüzü sana dönük olur
        transform.LookAt(new Vector3(kamyon.position.x, transform.position.y, kamyon.position.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        // Kamyonun Tag'inin "Player" olduðundan %100 emin ol
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}