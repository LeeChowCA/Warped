using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CinemachineCamera virtualCamera; // Updated to use CinemachineCamera  
    private GameObject player;

    [SerializeField] GameObject copPrefab;
    [SerializeField] private Transform[] copSpawnPoints;
    [SerializeField] private int numOfCops = 12;

    [SerializeField] GameObject eggTurretPrefab;
    [SerializeField] private Transform[] eggTurretSpawnPoints;
    [SerializeField] private int numOfEggTurrets = 13;

    public static event Action<Transform> OnPlayerRespawned;

    [SerializeField] private UIManager ui;
    private int score = 0;

    //[SerializeField] private GameObject spawnEffectPrefab;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_DEAD, OnEnemyDead);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
    }

    private void Start()
    {
        ui.UpdateScore(score);

        RespawnPlayer();
        SpawnCops();
        SpawnEggTurret();
    }

    public void RespawnPlayer()
    {
        if (player != null)
        {
            Destroy(player);
        }
        player = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        virtualCamera.Follow = player.transform; // Set the camera to follow the new player instance

        //if (spawnEffectPrefab != null)
        //{
        //    GameObject spawnEffect = Instantiate(spawnEffectPrefab, respawnPoint.position, Quaternion.identity);
        //    Destroy(spawnEffect, 200f); // Destroy the particle system after 2 seconds
        //    Debug.Log("Spawn effect instantiated at: " + respawnPoint.position);
        //}
        
        //virtualCamera.LookAt = player.transform; // Set the camera to look at the new player instance

        OnPlayerRespawned?.Invoke(player.transform);
    }

    public void PlayerDied()
    {
        // Handle player death (e.g., show game over screen, reduce lives, etc.)  
        Invoke("RespawnPlayer", 2f); // Respawn after a delay  
    }

    public Transform GetPlayerTransform()
    {
        return player.transform;
    }

    private void SpawnCops()
    {

        if (copSpawnPoints.Length == 0)
        {
            Debug.LogWarning("No cop spawn points assigned!");
            return;
        }
        if (numOfCops > copSpawnPoints.Length)
        {
            Debug.LogWarning("Number of cops exceeds available spawn points. Adjusting to maximum available.");
            numOfCops = copSpawnPoints.Length;
        }

        if (copPrefab == null)
        {
            Debug.LogWarning("Cop prefab is not assigned!");
            return;
        }

        for (int i = 0; i < numOfCops; i++)
        {
            Transform spawnPoint = copSpawnPoints[i];
            Instantiate(copPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private void SpawnEggTurret()
    {
        if (eggTurretSpawnPoints.Length == 0)
        {
            Debug.LogWarning("No egg turret spawn points assigned!");
            return;
        }

        if (numOfEggTurrets > eggTurretSpawnPoints.Length)
        {
            Debug.LogWarning("Number of egg turrets exceeds available spawn points. Adjusting to maximum available.");
            numOfEggTurrets = eggTurretSpawnPoints.Length;
        }

        if (eggTurretPrefab == null)
        {
            Debug.LogWarning("Egg turret prefab is not assigned!");
            return;
        }

        for (int i = 0; i < numOfEggTurrets; i++)
        {
            Transform spawnPoint = eggTurretSpawnPoints[i];
            Instantiate(eggTurretPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private void OnEnemyDead()
    {
        score++;
        ui.UpdateScore(score);
    }
}
