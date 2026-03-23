using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerBehavior : MonoBehaviour
{
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

    private QueueManager queue;

    void Start()
    {
        move = 0;
        total = 0;

        if (textField != null)
            textField.SetText("Score: " + total);

        GameObject queueObject = GameObject.FindGameObjectWithTag("QueueManager");
        if (queueObject != null)
            queue = queueObject.GetComponent<QueueManager>();
    }

    void Update()
    {
        if (currentTreat == null)
        {
            SpawnNewTreat();
        }

        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame &&
            currentTreat != null)
        {
            DropTreat();
        }

        float offset = 0.0f;

        if (Keyboard.current != null)
        {
            if ((Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) && move != 1)
                offset = -speed;

            if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
                offset = speed;
        }

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(newPos.x + offset, min, max);
        transform.position = newPos;
    }

  void SpawnNewTreat()
{
    if (queue == null)
    {
        Debug.Log("Queue is null");
        return;
    }

    if (treats == null || treats.Length == 0)
    {
        Debug.Log("No treats assigned");
        return;
    }

    int choice = queue.updateQueue();
    Debug.Log("Queue chose: " + choice);

    if (choice < 0 || choice >= treats.Length)
    {
        Debug.Log("Choice out of range: " + choice);
        return;
    }

    currentTreat = Instantiate(treats[choice], transform.position, Quaternion.identity);

    currentTreat.transform.SetParent(transform);
    currentTreat.transform.localPosition = new Vector3(0f, offY, 0f);

    Rigidbody2D rb = currentTreat.GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    Collider2D col = currentTreat.GetComponent<Collider2D>();
    if (col != null)
        col.enabled = false;
}

    void DropTreat()
    {
        currentTreat.transform.SetParent(null);

        Rigidbody2D rb = currentTreat.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f;
        }

        Collider2D col = currentTreat.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = true;

        currentTreat = null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("LB"))
            move = 1;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("LB"))
            move = 0;
    }

    public void GameOver()
{
    if (gameOverPanel != null)
        gameOverPanel.SetActive(true);

    Time.timeScale = 0;
}

    public void updateScore(int index)
    {
        total += points[index];

        if (textField != null)
            textField.SetText("Score: " + total);
    }
}