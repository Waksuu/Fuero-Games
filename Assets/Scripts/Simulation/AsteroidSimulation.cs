using Assets.Model;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSimulation : MonoBehaviour
{
    private SpawnAsteroids spawnAsteroids;

    private void Start() => spawnAsteroids = SpawnAsteroids.Instance;

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

            (int, int) newCell = (Mathf.FloorToInt(item.Value.NewPosition.x), Mathf.FloorToInt(item.Value.NewPosition.y));

            if (AsteroidChangedCell(item, newCell))
            {
                MoveAsteroidToNewCell(item, newCell);
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
        if (CoordinateIsEmpty(coordinate))
        {
            return false;
        }

        Asteroid neighbourAsteroid = spawnAsteroids.Grid[coordinate];

        if (Collision(item.Value, neighbourAsteroid))
        {
            DestroyCollidedAsteroids(item, coordinate);
            return true;
        }

        return false;
    }

    private bool CoordinateIsEmpty((int, int) coordinate) => !spawnAsteroids.Grid.ContainsKey(coordinate);

    private bool Collision(Asteroid itemAsteroid, Asteroid gridAsteroid) => Mathf.Pow(gridAsteroid.Position.x - itemAsteroid.NewPosition.x, 2) + Mathf.Pow(gridAsteroid.Position.y - itemAsteroid.NewPosition.y, 2) <= Mathf.Pow(gridAsteroid.Radius + itemAsteroid.Radius, 2);

    private void DestroyCollidedAsteroids(KeyValuePair<(int, int), Asteroid> item, (int, int) coordinate)
    {
        spawnAsteroids.Grid.Remove(item.Key);
        spawnAsteroids.Grid.Remove(coordinate);
    }

    private bool AsteroidChangedCell(KeyValuePair<(int, int), Asteroid> item, (int, int) newCell) => item.Key != newCell;

    private void MoveAsteroidToNewCell(KeyValuePair<(int, int), Asteroid> item, (int, int) newCell)
    {
        Asteroid asteroidCoordinate = spawnAsteroids.Grid[item.Key];
        spawnAsteroids.Grid.Remove(item.Key);
        spawnAsteroids.Grid.Add(newCell, asteroidCoordinate);
    }
}