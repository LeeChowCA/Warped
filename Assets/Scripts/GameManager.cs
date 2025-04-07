using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject playerPrefab;
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
    }

    public void PlayerDied()
    {
        // Handle player death (e.g., show game over screen, reduce lives, etc.)
        Invoke("RespawnPlayer", 2f); // Respawn after a delay
    }
}
