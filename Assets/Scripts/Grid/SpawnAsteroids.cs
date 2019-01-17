using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    public Grid Grid;

    public GameObject Asteroid;

    [SerializeField]
    private int gridWidth = 4;

    [SerializeField]
    private int gridHeight = 4;

    private void Start()
    {
        for (int x = gridWidth / -2; x < gridWidth / 2; x++)
        {
            for (int y = gridHeight / -2; y < gridHeight / 2; y++)
            {
                Vector3Int cellPosition = GetCellPosition(x, y);
                Vector3 centerPosition = GetCenterPosition(cellPosition);
                CreateAsteroidInCellCenter(centerPosition);
            }
        }
    }

    private Vector3Int GetCellPosition(int x, int y) => new Vector3Int(x, y, 0);

    private Vector3 GetCenterPosition(Vector3Int cellPosition) => Grid.GetCellCenterLocal(cellPosition);

    private void CreateAsteroidInCellCenter(Vector3 centerPosition) => Instantiate(Asteroid, transform.position + centerPosition, transform.rotation);
}