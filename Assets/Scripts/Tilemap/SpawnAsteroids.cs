using Assets.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#pragma warning disable 0649

public class SpawnAsteroids : MonoBehaviour
{
    private const float _radius = 0.8f;

    [System.NonSerialized]
    public Dictionary<(int, int), Asteroid> Grid = new Dictionary<(int, int), Asteroid>();

    [Header("Tilemap")]
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

    [Header("Asteroid speed")]
    [Range(0f,1f)]
    [SerializeField]
    private float minSpeed = 0.1f;

    [Range(0f, 1f)]
    [SerializeField]
    private float maxSpeed = 0.2f;

    #region Singleton

    public static SpawnAsteroids Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion Singleton

    private void Start()
    {
        InitializeGameWithSeed(seed);
        SpawnAsteroidsInGrid(width, height, tilemap, asteroid);
    }

    #region Start

    private void InitializeGameWithSeed(int seed) => UnityEngine.Random.InitState(seed);

    private void SpawnAsteroidsInGrid(int width, int height, Tilemap tilemap, GameObject asteroid)
    {
        for (int y = width / -2; y < width / 2; y++)
        {
            for (int x = height / -2; x < height / 2; x++)
            {
                Vector3Int cellPosition = GetCellPosition(x, y);
                Vector3 centerPosition = GetCenterPosition(tilemap, cellPosition);

                Vector3 newPosition = centerPosition;
                Quaternion randomRotation = CreateAsteroidNewPosition(ref newPosition);

                Grid.Add(((int)centerPosition.x, (int)centerPosition.y), new Asteroid
                {
                    Position = centerPosition,
                    NewPosition = newPosition,
                    Radius = _radius,
                    Rotation = randomRotation
                });
            }
        }
    }

    private Vector3Int GetCellPosition(int x, int y) => new Vector3Int(x, y, 0);

    private Vector3 GetCenterPosition(Tilemap tilemap, Vector3Int cellPosition) => tilemap.GetCellCenterLocal(cellPosition);

    private Quaternion CreateAsteroidNewPosition(ref Vector3 newPosition)
    {
        Vector3 velocity = new Vector3(0, CreateRandomSpeed(), 0);
        Quaternion randomRotation = CreateRandom2DRotation();
        newPosition += randomRotation * velocity;
        return randomRotation;
    }

    private Quaternion CreateRandom2DRotation() => Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));

    private float CreateRandomSpeed() => UnityEngine.Random.Range(minSpeed, maxSpeed);

    #endregion Start
}