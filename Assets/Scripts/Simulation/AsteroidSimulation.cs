using Assets.Model;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSimulation : MonoBehaviour
{
    private SpawnAsteroids spawnAsteroids;

    private void Awake() => spawnAsteroids = SpawnAsteroids.Instance;

    private void FixedUpdate()
    {
        Dictionary<(int, int), Asteroid> gridCopy = new Dictionary<(int, int), Asteroid>(spawnAsteroids.Grid);
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

            if (item.Key.Item1 != newCellX || item.Key.Item2 != newCellY)
            {
                Asteroid asteroidCoordinate = spawnAsteroids.Grid[item.Key];
                spawnAsteroids.Grid.Remove(item.Key);
                spawnAsteroids.Grid.Add((newCellX, newCellY), asteroidCoordinate);
            }
        }
    }

    private void UpdateCoordinates(KeyValuePair<(int, int), Asteroid> item)
    {
        Vector3 position = item.Value.Position;

        Vector3 positionUpdate = item.Value.NewPosition;
        Vector3 newPositionUpdate = positionUpdate * 2 - position;

        spawnAsteroids.Grid[item.Key] = new Asteroid
        {
            Position = positionUpdate,
            NewPosition = newPositionUpdate,
            Radius = item.Value.Radius,
            Rotation = item.Value.Rotation,
        };
    }

    private bool HandleCollison(KeyValuePair<(int, int), Asteroid> item, (int, int) coordinate)
    {
        if (!spawnAsteroids.Grid.ContainsKey(coordinate))
        {
            return false;
        }

        Asteroid rightAsteroid = spawnAsteroids.Grid[coordinate];

        if (Mathf.Pow(rightAsteroid.Position.x - item.Value.NewPosition.x, 2) + Mathf.Pow(rightAsteroid.Position.y - item.Value.NewPosition.y, 2)
            <= Mathf.Pow(rightAsteroid.Radius + item.Value.Radius, 2))
        {
            spawnAsteroids.Grid.Remove(item.Key);
            spawnAsteroids.Grid.Remove(coordinate);
            return true;
        }

        return false;
    }
}