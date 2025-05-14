using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Stage")]
    public int stageLevel = 1;
    [SerializeField] private float stageLevelUpTime = 5f, stageLevelUpTimer;

    [Header("Enemies Settings")]
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform enemySpawnPos;
    [SerializeField] private float enemySpawnTime, baseEnemySpawnTime = 5f, enemySpawnTimer;
    [SerializeField] private float enemyMoveSpeed, baseEnemyMoveSpeed = 5f;
    [SerializeField] private int enemyHealth = 10;

    [Space] [Header("Obstacle Settings")]
    [SerializeField] private Obstacle obstaclePrefab;
    [SerializeField] private Transform[] obstascleSpawnPos;
    [SerializeField] private float obstacleSpawnTime, baseObstacleSpawnTime = 5f, obstacleSpawnTimer;
    
    [Space]
    [SerializeField] private Gem gemPrefab;
    [SerializeField] private float gemSpawnTime, baseGemSpawnTime = 1f, gemSpawnTimer;
    [SerializeField] private float minY, maxY;
    
    [Space]
    [SerializeField] private PowerUp[] powerUpPrefab;
    [SerializeField] private float powerUpSpawnTime, basePowerUpSpawnTime = 1f, powerUpSpawnTimer;
    
    public static EnemySpawner instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        HandleStageLevel();
        HandleSpawnEnemy();
        HandleSpawnObstacle();
        HandleSpawnGem();
        HandleSpawnPowerUp();
    }

    private void HandleStageLevel()
    {
        if (stageLevelUpTimer < stageLevelUpTime) stageLevelUpTimer += Time.deltaTime;
        else
        {
            stageLevel++;
            stageLevelUpTimer = 0;
        }
    }
    private void HandleSpawnObstacle()
    {
        if (obstacleSpawnTimer < obstacleSpawnTime) obstacleSpawnTimer += Time.deltaTime;
        else
        {
            enemyMoveSpeed = baseEnemyMoveSpeed * (1 + ((float)stageLevel / 100));

            Obstacle newObstacle = Instantiate(obstaclePrefab,
                obstascleSpawnPos[Random.Range(0, obstascleSpawnPos.Length)].transform.position, quaternion.identity);
            newObstacle.Init(enemyMoveSpeed);
            
            obstacleSpawnTimer = 0;
            obstacleSpawnTime = Random.Range(0,
                Mathf.Clamp(baseObstacleSpawnTime - ((float)stageLevel / 100), 0, baseObstacleSpawnTime));
        }
    }
    private void HandleSpawnEnemy()
    {
        if (enemySpawnTimer < enemySpawnTime) enemySpawnTimer += Time.deltaTime;
        else
        {
            enemyMoveSpeed = baseEnemyMoveSpeed * (1 + ((float)stageLevel / 100));
            enemyHealth = (int)(10 * (1 + ((float)stageLevel / 100)));                             
            
            Enemy newEnemy = Instantiate(enemyPrefab, enemySpawnPos.transform.position,quaternion.identity);
            newEnemy.Init(enemyHealth, enemyMoveSpeed);
            
            enemySpawnTimer = 0;
            enemySpawnTime = Random.Range(0,
                Mathf.Clamp(baseEnemySpawnTime - ((float)stageLevel / 100), 0, baseEnemySpawnTime));
        }
    }
    private void HandleSpawnGem()
    {
        if (gemSpawnTimer < gemSpawnTime) gemSpawnTimer += Time.deltaTime;
        else
        {
            Gem newGem = Instantiate(gemPrefab, new Vector3(8,Random.Range(minY,maxY)),quaternion.identity);
            newGem.Init(enemyMoveSpeed);
            
            gemSpawnTimer = 0;
            gemSpawnTime = Random.Range(0,
                Mathf.Clamp(baseGemSpawnTime - ((float)stageLevel / 100), 0, baseGemSpawnTime));
        }
    }
    private void HandleSpawnPowerUp()
    {
        if (powerUpSpawnTimer < powerUpSpawnTime) powerUpSpawnTimer += Time.deltaTime;
        else
        {
            PowerUp newPowerUp = Instantiate(powerUpPrefab[Random.Range(0, powerUpPrefab.Length)], new Vector3(8,Random.Range(minY,maxY)),quaternion.identity);
            newPowerUp.Init(enemyMoveSpeed);
            
            powerUpSpawnTimer = 0;
            powerUpSpawnTime = Random.Range(0,
                Mathf.Clamp(basePowerUpSpawnTime - ((float)stageLevel / 100), 0, basePowerUpSpawnTime));
        }
    }
}
