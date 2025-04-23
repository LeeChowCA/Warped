using UnityEngine;

public class WinEffect : MonoBehaviour
{

    [SerializeField] private ParticleSystem winEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the trigger is the player
        if (collision.CompareTag("Player"))
        {
            // Play the particle system
            if (winEffect != null)
            {
                winEffect.Play();
                Debug.Log("Win effect triggered!");
            }
        }
    }
}
