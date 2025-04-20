using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CinemachineCamera virtualCamera; // Updated to use CinemachineCamera  
    private GameObject player;

    [SerializeField] GameObject copPrefab;
    [SerializeField] private Transform[] copSpawnPoints;
    [SerializeField] private int numOfCops = 5;



    private void Awake()
    {
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

    private void Start()
    {
        RespawnPlayer();
        SpawnCops();
    }

    public void RespawnPlayer()
    {
        if (player != null)
        {
            Destroy(player);
        }
        player = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        virtualCamera.Follow = player.transform; // Set the camera to follow the new player instance
        virtualCamera.LookAt = player.transform; // Set the camera to look at the new player instance
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
}
