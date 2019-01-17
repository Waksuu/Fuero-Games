using UnityEngine;

public class DisplayHitmark : MonoBehaviour
{
    public GameObject HitmarkPrefab;
    public float DisappearDelay = 0.25f;

    private void OnTriggerEnter2D(Collider2D collision) => QuciklyDisplayHitmark(HitmarkPrefab, DisappearDelay);

    private void QuciklyDisplayHitmark(GameObject hitmarkPrefab, float disappearDelay)
    {
        GameObject hitmark = CreateHitmarkAtCollisionPosition(hitmarkPrefab);
        Destroy(hitmark, disappearDelay);
    }

    private GameObject CreateHitmarkAtCollisionPosition(GameObject hitmarkPrefab) => Instantiate(hitmarkPrefab, transform.position, transform.rotation);
}