using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CinemachineCamera virtualCamera; // Updated to use CinemachineCamera  
    private GameObject player;

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
}
