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
    [Header("Start Platform")]
    [SerializeField] private Transform LevelStart;

    [Header("Components")]
    [SerializeField] private Toast leaderToast;
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
        GameObject chosenLevelPart;
        chosenLevelPart = PlatformPool.instance.GetEasyPlatform(); ;
        if (moveSpeed >= mediumLevelMark && moveSpeed < hardLevelMark) chosenLevelPart = PlatformPool.instance.GetMediumPlatform();
        if (moveSpeed >= hardLevelMark && moveSpeed < extremeLevelMark) chosenLevelPart = PlatformPool.instance.GetHardPlatform();
        if (moveSpeed >= extremeLevelMark && moveSpeed < hellLevelMark) chosenLevelPart = PlatformPool.instance.GetExtremePlatform();
        if (moveSpeed >= hellLevelMark) chosenLevelPart = PlatformPool.instance.GetHellPlatform();
        chosenLevelPart.transform.position = lastEndPosition.position;
        foreach (Transform child in chosenLevelPart.transform)
        {
            if (child.gameObject.CompareTag("CoinPattern"))
            {
                child.gameObject.SetActive(false);
                continue;
            }
            child.gameObject.SetActive(true);
        }
        chosenLevelPart.SetActive(true);
        lastEndPosition = chosenLevelPart.transform.Find("EndPosition");
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
}