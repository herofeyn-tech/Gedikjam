using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform avalanche; // Çýð objesini buraya sürükle
    public float maxShake = 0.5f;
    public float shakeDistance = 30f;

    Vector3 originalPos;

    void Start() => originalPos = transform.localPosition;

    void Update()
    {
        if (avalanche == null) return;

        float distance = Vector3.Distance(transform.position, avalanche.position);

        if (distance < shakeDistance)
        {
            // Mesafe azaldýkça sarsýntý artar
            float currentShake = (1 - (distance / shakeDistance)) * maxShake;
            transform.localPosition = originalPos + Random.insideUnitSphere * currentShake;
        }
        else
        {
            transform.localPosition = originalPos;
        }
    }
}