using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;

    [SerializeField]
    private float respawnTimer = 1f;
    private float _respawnTimer;

    private GameObject playerInstance;

    private void Start() => playerInstance = SpawnPlayer();

    private void Update()
    {
        if (PlayerExists(playerInstance))
        {
            return;
        }

        DecreaseRespawnTimer(ref _respawnTimer);
        if (RespawnIsReady(_respawnTimer))
        {
            playerInstance = SpawnPlayer();
        }
    }

    private GameObject SpawnPlayer()
    {
        _respawnTimer = respawnTimer;
        return Instantiate(PlayerPrefab, transform.position, transform.rotation);
    }

    private bool PlayerExists(GameObject playerInstance) => playerInstance != null;

    private void DecreaseRespawnTimer(ref float _respawnTimer) => _respawnTimer -= Time.deltaTime;

    private bool RespawnIsReady(float _respawnTimer) => _respawnTimer <= 0;
}