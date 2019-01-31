using Assets.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#pragma warning disable 0649

public class SpawnAsteroids : MonoBehaviour
{
    private const float _radius = 0.8f;

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

    private Dictionary<(int, int), Asteroid> grid = new Dictionary<(int, int), Asteroid>();

    //private float tilemapCellSizeX;
    //private float tilemapCellSizeY;
    //private Vector3 cameraSize;

    private void Start()
    {
        //tilemapCellSizeX = tilemap.cellSize.x;
        //tilemapCellSizeY = tilemap.cellSize.y;
        //cameraSize = Camera.main.ViewportToWorldPoint(new Vector3(1, 1)) * 2;

        InitializeGameWithSeed(seed);
        GenerateAsteroidsOnTilemap(width, height, tilemap, asteroid);
    }

    private void FixedUpdate()
    {
        //var cameraPosition = Camera.main.gameObject.transform.position;
        //Rect cameraFoV = new Rect(cameraPosition - (cameraSize * 0.5f), cameraSize);

        // I know coping grid is super slow
        Dictionary<(int, int), Asteroid> gridCopy = new Dictionary<(int, int), Asteroid>(grid);
        foreach (var item in gridCopy)
        {
            UpdateCoordinates(item);

            (int, int) topCellCoordinate = (item.Key.Item1, item.Key.Item2 + 1);
            (int, int) topRightCellCoordinate = (item.Key.Item1 + 1, item.Key.Item2 + 1);
            (int, int) rightCellCoordinate = (item.Key.Item1 + 1, item.Key.Item2);
            (int, int) downRightCellCoordinate = (item.Key.Item1 + 1, item.Key.Item2 - 1);
            (int, int) downCellCoordinate = (item.Key.Item1, item.Key.Item2 - 1);
            (int, int) downLeftCellCoordinate = (item.Key.Item1 - 1, item.Key.Item2 - 1);
            (int, int) leftCellCoordinate = (item.Key.Item1 - 1, item.Key.Item2);
            (int, int) topLeftCellCoordinate = (item.Key.Item1 - 1, item.Key.Item2 + 1);

            if (HandleCollison(item, topCellCoordinate) || HandleCollison(item, topRightCellCoordinate)
                || HandleCollison(item, rightCellCoordinate) || HandleCollison(item, downRightCellCoordinate)
                || HandleCollison(item, downCellCoordinate) || HandleCollison(item, downLeftCellCoordinate)
                || HandleCollison(item, leftCellCoordinate) || HandleCollison(item, topLeftCellCoordinate))
            {
                continue;
            }

            var newCellX = Mathf.FloorToInt(item.Value.NewPosition.x);
            var newCellY = Mathf.FloorToInt(item.Value.NewPosition.y);

            // if our asteroid moved out from grid cell
            if (item.Key.Item1 != newCellX || item.Key.Item2 != newCellY)
            {
                //Move asteroid to new cell
                Asteroid asteroidCoordinate = grid[item.Key];
                grid.Remove(item.Key);
                grid.Add((newCellX, newCellY), asteroidCoordinate);
            }

            ////It does not work as intended, also i would have to "cache" asteroids that are on screen
            /////and put them back into grid when they will be no longer visible
            //if (cameraFoV.Contains(item.Value.NewPosition))
            //{
            //    Instantiate(asteroid, item.Value.NewPosition, item.Value.Rotation);
            //    grid.Remove((newCellX, newCellY));
            //}
        }
    }

    #region Start

    private void InitializeGameWithSeed(int seed) => UnityEngine.Random.InitState(seed);

    private void GenerateAsteroidsOnTilemap(int width, int height, Tilemap tilemap, GameObject asteroid)
    {
        for (int y = width / -2; y < width / 2; y++)
        {
            for (int x = height / -2; x < height / 2; x++)
            {
                Vector3Int cellPosition = GetCellPosition(x, y);
                Vector3 centerPosition = GetCenterPosition(tilemap, cellPosition);

                Vector3 newPosition = centerPosition;
                Vector3 velocity = new Vector3(0, CreateRandomSpeed(), 0);
                Quaternion randomRotation = CreateRandom2DRotation();
                newPosition += randomRotation * velocity;

                grid.Add(((int)centerPosition.x, (int)centerPosition.y), new Asteroid
                {
                    Position = centerPosition,
                    NewPosition = newPosition,
                    Radius = _radius,
                    Rotation = randomRotation
                });

                //CreateAsteroidInCellCenter(asteroid, centerPosition);
            }
        }
    }

    private Vector3Int GetCellPosition(int x, int y) => new Vector3Int(x, y, 0);

    private Vector3 GetCenterPosition(Tilemap tilemap, Vector3Int cellPosition) => tilemap.GetCellCenterLocal(cellPosition);

    private void CreateAsteroidInCellCenter(GameObject asteroid, Vector3 centerPosition) => Instantiate(asteroid, transform.position + centerPosition, CreateRandom2DRotation());

    private Quaternion CreateRandom2DRotation() => Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));

    private float CreateRandomSpeed() => UnityEngine.Random.Range(0.1f, 1f);

    #endregion Start

    #region Update

    private void UpdateCoordinates(KeyValuePair<(int, int), Asteroid> item)
    {
        Vector3 position = item.Value.Position;

        Vector3 positionUpdate = item.Value.NewPosition;
        Vector3 newPositionUpdate = positionUpdate * 2 - position;

        grid[item.Key] = new Asteroid
        {
            Position = positionUpdate,
            NewPosition = newPositionUpdate,
            Radius = item.Value.Radius,
            Rotation = item.Value.Rotation,
        };
    }

    private bool HandleCollison(KeyValuePair<(int, int), Asteroid> item, (int, int) coordinate)
    {
        if (!grid.ContainsKey(coordinate))
        {
            return false;
        }

        Asteroid rightAsteroid = grid[coordinate];

        if (Mathf.Pow(rightAsteroid.Position.x - item.Value.NewPosition.x, 2) + Mathf.Pow(rightAsteroid.Position.y - item.Value.NewPosition.y, 2)
            <= Mathf.Pow(rightAsteroid.Radius + item.Value.Radius, 2))
        {
            grid.Remove(item.Key);
            grid.Remove(coordinate);
            return true;
        }

        return false;
    }

    #endregion Update
}