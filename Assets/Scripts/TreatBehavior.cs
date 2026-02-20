using UnityEngine;

public class TreatBehavior : MonoBehaviour {
    public int treatType;
    public float timeout = 3.0f;
    private float timeStart;

    void Start() {
        timeStart = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Top")) {
            timeStart = Time.time;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Top")) {
            float elapsed = Time.time - timeStart;
            if (elapsed > timeout) {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null) {
                    player.GetComponent<PlayerBehavior>().GameOver();
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Top")) {
            timeStart = Time.time;
        }
    }
}
