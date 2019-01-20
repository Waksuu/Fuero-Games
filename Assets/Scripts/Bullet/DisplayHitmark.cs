#pragma warning disable 0649

using UnityEngine;

public class DisplayHitmark : MonoBehaviour
{
    [SerializeField]
    private GameObject hitmarkPrefab;

    [SerializeField]
    private float disappearDelay = 0.25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddScore();
        QuciklyDisplayHitmark(hitmarkPrefab, disappearDelay);
    }

    private void AddScore()
    {
        ScoreManager.Score += 10;
        ScoreManager.UpdateScore = true;
    }

    private void QuciklyDisplayHitmark(GameObject hitmarkPrefab, float disappearDelay)
    {
        GameObject hitmark = CreateHitmarkAtCollisionPosition(hitmarkPrefab);
        Destroy(hitmark, disappearDelay);
    }

    private GameObject CreateHitmarkAtCollisionPosition(GameObject hitmarkPrefab) => Instantiate(hitmarkPrefab, transform.position, transform.rotation);
}