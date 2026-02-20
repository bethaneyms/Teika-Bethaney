using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerBehavior : MonoBehaviour {
    public float speed;
    private GameObject currentTreat;
    public float offY = -0.6f;
    public float min; 
    public float max;
    public int move;

    public int[] points;
    public int total;
    public TMP_Text textField;
    public GameObject[] treats;
    public GameObject gameOverPanel;

    void Start() {
        move = 0;
    }

    void Update() {
        if (currentTreat != null) {
            Vector3 playerPos = transform.position;
            Vector3 treatOffset = new Vector3(0.0f, offY, 0.0f);
            currentTreat.transform.position = playerPos + treatOffset;
        }
        else {

            if (treats.Length > 0) {
                int choice = Random.Range(0, treats.Length);
                currentTreat = Instantiate(treats[choice], transform.position, Quaternion.identity);
                currentTreat.GetComponent<Rigidbody2D>().gravityScale = 0;
                currentTreat.GetComponent<Collider2D>().enabled = false;
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && currentTreat != null) {
            Rigidbody2D body = currentTreat.GetComponent<Rigidbody2D>();
            body.gravityScale = 1.0f;

            Collider2D collider = currentTreat.GetComponent<Collider2D>();
            collider.enabled = true;

            currentTreat = null;
        }

        float offset = 0.0f;
        if (Keyboard.current != null) {
            if ((Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) && move != 1) offset = -speed;
            if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) offset = speed;
        }

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(newPos.x + offset, min, max);
        transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("LB")) move = 1; 
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("LB")) move = 0;
    }

    public void GameOver() {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void updateScore(int index) {
        total = total + points[index];
        textField.SetText("Score: " + total);
    }
}