using UnityEngine;

public class TreatBehavior : MonoBehaviour {

    public GameObject[] treats;
    public int treatType;
    public GameObject gameOverText; 

    void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            treats = player.GetComponent<PlayerBehavior>().treats;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Treat")) {
            TreatBehavior otherScript = other.gameObject.GetComponent<TreatBehavior>();
            
            // Only merge if types match AND we aren't at the last treat
            if (otherScript.treatType == treatType && treatType < treats.Length - 1) {
                // ID check prevents double-spawning
                if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID()) {
                    int nextIndex = treatType + 1;
                    Vector3 spawnPos = Vector3.Lerp(transform.position, other.transform.position, 0.5f);
                    
                    GameObject newTreat = Instantiate(treats[nextIndex], spawnPos, Quaternion.identity);
                    newTreat.GetComponent<Collider2D>().enabled = true;
                    newTreat.GetComponent<Rigidbody2D>().gravityScale = 1.0f;

                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }

    // This handles the Game Over when they hit the purple bar
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Finish")) {
            if (gameOverText != null) {
                gameOverText.SetActive(true);
                // Time.timeScale = 0; // Uncomment this to freeze the game on lose
            }
        }
    }
}