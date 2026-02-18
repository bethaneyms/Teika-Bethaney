using UnityEngine;

public class BorderBehavior : MonoBehaviour
{
    public float timeout = 3f;
    public GameObject gameOver;
    
    private float timeStart; 
    private bool isGameOver = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Start the timer when a treat enters the top border zone
        if (other.gameObject.CompareTag("Treat") && !isGameOver)
        {
            timeStart = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Treat") && !isGameOver)
        {
            float timeThusfar = Time.time - timeStart;
            
            if (timeThusfar > timeout)
            {
                isGameOver = true;
                Debug.Log("Game Over!");
                
                if (gameOver != null)
                {
                    gameOver.SetActive(true); 
                    Time.timeScale = 0f;     
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Reset if the treat falls back down before the timeout
        if (other.gameObject.CompareTag("Treat"))
        {
            timeStart = Time.time;
        }
    }
}