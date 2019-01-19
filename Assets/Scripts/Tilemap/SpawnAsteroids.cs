using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnAsteroids : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private GameObject asteroid;

    [SerializeField]
    private int gridWidth = 4;

    [SerializeField]
    private int gridHeight = 4;

    [SerializeField]
    private int seed = 1;

    private void Start()
    {
        InitializeGameWithSeed(seed);
        GenerateAsteroidsOnTilemap(gridWidth, gridHeight, tilemap, asteroid);
    }

    private void InitializeGameWithSeed(int seed) => Random.InitState(seed);

    private void GenerateAsteroidsOnTilemap(int gridWidth, int gridHeight, Tilemap tilemap, GameObject asteroid)
    {
        for (int x = gridWidth / -2; x < gridWidth / 2; x++)
        {
            for (int y = gridHeight / -2; y < gridHeight / 2; y++)
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

    private Quaternion CreateRandom2DRotation() => Quaternion.Euler(0f, 0f, Random.Range(0.0f, 360.0f));
}