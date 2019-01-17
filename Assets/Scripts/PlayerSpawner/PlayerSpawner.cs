using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;

    [SerializeField]
    private float respawnTimer = 1f;
    private float _respawnTimer;

    private GameObject playerInstance;

    private void Start()
    {
        playerInstance = SpawnPlayer();
    }

    private void Update()
    {
        if (PlayerExists())
        {
            return;
        }

        DecreaseRespawnTimer();
        if (RespawnIsReady())
        {
            playerInstance = SpawnPlayer();
        }
    }

    private GameObject SpawnPlayer()
    {
        _respawnTimer = respawnTimer;
        return playerInstance = Instantiate(PlayerPrefab, transform.position, transform.rotation);
    }

    private bool PlayerExists() => playerInstance != null;

    private void DecreaseRespawnTimer() => _respawnTimer -= Time.deltaTime;

    private bool RespawnIsReady() => _respawnTimer <= 0;
}