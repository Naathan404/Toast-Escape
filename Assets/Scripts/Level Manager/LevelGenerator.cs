using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class LevelGenerator : MonoBehaviour
{
    [Header("Scroll Speed Settings")]
    private const float DISTANCE_SPAWN_LEVEL = 10;
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float increasingMoveSpeedFactor;
    [SerializeField] private float moveSpeed;

    [Header("Platform Chunk Spawn Settings")]
    [SerializeField] private float easyLevelMark;
    [SerializeField] private float mediumLevelMark;
    [SerializeField] private float hardLevelMark;
    [SerializeField] private float extremeLevelMark;
    [SerializeField] private float hellLevelMark;
    [Header("Chunk Lists")]
    [SerializeField] private Transform LevelStart;
    [SerializeField] private List<Transform> EasyLevelList;
    [SerializeField] private List<Transform> MediumLevelList;
    [SerializeField] private List<Transform> HardLevelList;
    [SerializeField] private List<Transform> ExtremeLevelList;
    [SerializeField] private List<Transform> HellLevelList;

    [Header("Components")]
    [SerializeField] private Cat leaderCat;
    [SerializeField] private Camera mainCamera;
    private Transform lastEndPosition;

    public static LevelGenerator instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        // Initialize the level generator
        InitLevelStart();
        moveSpeed = baseSpeed;
    }

    private void InitLevelStart()
    {
        lastEndPosition = LevelStart.Find("EndPosition");
        for (int i = 0; i < 1; i++)
        {
            SpawnLevel();
        }
    }

    private void Update()
    {
        InitNewLevelPart();
        IncreasingMoveSpeed();
    }

    private void InitNewLevelPart()
    {
        float cameraX = mainCamera.transform.position.x;
        if (lastEndPosition.position.x < cameraX + DISTANCE_SPAWN_LEVEL)
            SpawnLevel();
    }

    private void SpawnLevel()
    {
        Transform chosenLevelPart;
        chosenLevelPart = EasyLevelList[Random.Range(0, EasyLevelList.Count)];
        if (moveSpeed >= mediumLevelMark && moveSpeed < hardLevelMark) chosenLevelPart = MediumLevelList[Random.Range(0, MediumLevelList.Count)];
        if (moveSpeed >= hardLevelMark && moveSpeed < extremeLevelMark) chosenLevelPart = HardLevelList[Random.Range(0, HardLevelList.Count)];
        if (moveSpeed >= extremeLevelMark && moveSpeed < hellLevelMark) chosenLevelPart = ExtremeLevelList[Random.Range(0, ExtremeLevelList.Count)];
        if (moveSpeed >= hellLevelMark) chosenLevelPart = HellLevelList[Random.Range(0, HellLevelList.Count)];

        Transform levelPartTransform = Instantiate(chosenLevelPart, lastEndPosition.position, Quaternion.identity);
        lastEndPosition = levelPartTransform.Find("EndPosition");
    }

    private void IncreasingMoveSpeed()
    {
        moveSpeed += Time.deltaTime * increasingMoveSpeedFactor;
        moveSpeed = Mathf.Clamp(moveSpeed, baseSpeed, maxSpeed);
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    //     private Transform SpawnLevelPart(Vector3 spawnPos)
    //     {
    //         Transform levelPartTransform = Instantiate(Level01, spawnPos, Quaternion.identity);
    //         return levelPartTransform;
    //     }
}
