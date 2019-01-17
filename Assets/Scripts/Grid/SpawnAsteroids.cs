using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    public int GridWidth = 4;
    public int GridHeight = 4;
    public Grid Grid;
    public GameObject Asteroid;

    private void Start()
    {
        for (int x = GridWidth / -2; x < GridWidth / 2; x++)
        {
            for (int y = GridHeight / -2; y < GridHeight / 2; y++)
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