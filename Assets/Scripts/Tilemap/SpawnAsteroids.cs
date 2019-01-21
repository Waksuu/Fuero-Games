using UnityEngine;
using UnityEngine.Tilemaps;

#pragma warning disable 0649

public class SpawnAsteroids : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private GameObject asteroid;

    [SerializeField]
    private int width = 160;

    [SerializeField]
    private int height = 160;

    [SerializeField]
    private int seed = 1;

    private void Start()
    {
        InitializeGameWithSeed(seed);
        GenerateAsteroidsOnTilemap(width, height, tilemap, asteroid);
    }

    private void InitializeGameWithSeed(int seed) => Random.InitState(seed);

    private void GenerateAsteroidsOnTilemap(int width, int height, Tilemap tilemap, GameObject asteroid)
    {
        for (int x = width / -2; x < width / 2; x++)
        {
            for (int y = height / -2; y < height / 2; y++)
            {
                Vector3Int cellPosition = GetCellPosition(x, y);
                Vector3 centerPosition = GetCenterPosition(tilemap, cellPosition);
                CreateAsteroidInCellCenter(asteroid, centerPosition);
            }
        }
    }

    private Vector3Int GetCellPosition(int x, int y) => new Vector3Int(x, y, 0);

    private Vector3 GetCenterPosition(Tilemap tilemap, Vector3Int cellPosition) => tilemap.GetCellCenterLocal(cellPosition);

    private void CreateAsteroidInCellCenter(GameObject asteroid, Vector3 centerPosition) => Instantiate(asteroid, transform.position + centerPosition, CreateRandom2DRotation());

    private Quaternion CreateRandom2DRotation() => Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
}