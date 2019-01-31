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
        foreach (var cell in gridCopy)
        {
            UpdateCoordinates(cell);

            (int, int) topCellCoordinate = (cell.Key.Item1, cell.Key.Item2 + 1);
            (int, int) topRightCellCoordinate = (cell.Key.Item1 + 1, cell.Key.Item2 + 1);
            (int, int) rightCellCoordinate = (cell.Key.Item1 + 1, cell.Key.Item2);
            (int, int) downRightCellCoordinate = (cell.Key.Item1 + 1, cell.Key.Item2 - 1);
            (int, int) downCellCoordinate = (cell.Key.Item1, cell.Key.Item2 - 1);
            (int, int) downLeftCellCoordinate = (cell.Key.Item1 - 1, cell.Key.Item2 - 1);
            (int, int) leftCellCoordinate = (cell.Key.Item1 - 1, cell.Key.Item2);
            (int, int) topLeftCellCoordinate = (cell.Key.Item1 - 1, cell.Key.Item2 + 1);

            if (HandleCollison(cell, topCellCoordinate) || HandleCollison(cell, topRightCellCoordinate)
                || HandleCollison(cell, rightCellCoordinate) || HandleCollison(cell, downRightCellCoordinate)
                || HandleCollison(cell, downCellCoordinate) || HandleCollison(cell, downLeftCellCoordinate)
                || HandleCollison(cell, leftCellCoordinate) || HandleCollison(cell, topLeftCellCoordinate))
            {
                continue;
            }

            (int, int) newCell = (Mathf.FloorToInt(cell.Value.NewPosition.x), Mathf.FloorToInt(cell.Value.NewPosition.y));

            if (AsteroidChangedCell(cell.Key, newCell))
            {
                MoveAsteroidToNewCell(cell.Key, newCell);
            }
        }
    }

    private void UpdateCoordinates(KeyValuePair<(int, int), Asteroid> cell)
    {
        Vector3 position = cell.Value.Position;

        Vector3 positionUpdate = cell.Value.NewPosition;
        Vector3 newPositionUpdate = positionUpdate * 2 - position;

        spawnAsteroids.Grid[cell.Key] = new Asteroid
        {
            Position = positionUpdate,
            NewPosition = newPositionUpdate,
            Radius = cell.Value.Radius,
            Rotation = cell.Value.Rotation,
        };
    }

    private bool HandleCollison(KeyValuePair<(int, int), Asteroid> cell, (int, int) coordinate)
    {
        if (CoordinateIsEmpty(coordinate))
        {
            return false;
        }

        Asteroid neighbourAsteroid = spawnAsteroids.Grid[coordinate];

        if (Collision(cell.Value, neighbourAsteroid))
        {
            DestroyCollidedAsteroids(cell.Key, coordinate);
            return true;
        }

        return false;
    }

    private bool CoordinateIsEmpty((int, int) coordinate) => !spawnAsteroids.Grid.ContainsKey(coordinate);

    private bool Collision(Asteroid currentAsteroid, Asteroid neighbourAsteroid) => Mathf.Pow(neighbourAsteroid.Position.x - currentAsteroid.NewPosition.x, 2) + Mathf.Pow(neighbourAsteroid.Position.y - currentAsteroid.NewPosition.y, 2) <= Mathf.Pow(neighbourAsteroid.Radius + currentAsteroid.Radius, 2);

    private void DestroyCollidedAsteroids((int, int) key, (int, int) coordinate)
    {
        spawnAsteroids.Grid.Remove(key);
        spawnAsteroids.Grid.Remove(coordinate);
    }

    private bool AsteroidChangedCell((int, int) cell, (int, int) newCell) => cell != newCell;

    private void MoveAsteroidToNewCell((int, int) cell, (int, int) newCell)
    {
        Asteroid asteroidCoordinate = spawnAsteroids.Grid[cell];
        spawnAsteroids.Grid.Remove(cell);
        spawnAsteroids.Grid.Add(newCell, asteroidCoordinate);
    }
}