using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerBehavior : MonoBehaviour
{
    public float speed;
    public float offY = -1.0f;
    public float min;
    public float max;
    public int move;

    public int[] points;
    public int total;
    public TMP_Text textField;
    public GameObject[] treats;
    public GameObject gameOverPanel;

    private GameObject currentTreat;
    private QueueManager queue;
    private bool canSpawnNext = true;

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

        if (currentTreat != null)
        {
            currentTreat.transform.position = transform.position + new Vector3(0f, offY, 0f);
        }

        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame &&
            currentTreat != null)
        {
            DropTreat();
        }

        if (currentTreat == null && canSpawnNext)
        {
            SpawnNewTreat();
        }
    }

    void SpawnNewTreat()
    {
        if (queue == null || treats == null || treats.Length == 0)
            return;

        int choice = queue.updateQueue();
        Debug.Log("Queue chose: " + choice);

        if (choice < 0 || choice >= treats.Length || treats[choice] == null)
            return;

        currentTreat = Instantiate(
            treats[choice],
            transform.position + new Vector3(0f, offY, 0f),
            Quaternion.identity
        );

        TreatBehavior tb = currentTreat.GetComponent<TreatBehavior>();
        if (tb != null)
        {
            tb.treatType = choice;
            tb.treats = treats;
        }

        Rigidbody2D rb = currentTreat.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.WakeUp();
        }

        Collider2D col = currentTreat.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }

    void DropTreat()
    {
        if (currentTreat == null)
            return;

        GameObject droppedTreat = currentTreat;
        currentTreat = null;
        canSpawnNext = false;

        droppedTreat.transform.position += new Vector3(0f, -0.2f, 0f);

        Rigidbody2D rb = droppedTreat.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.WakeUp();
            rb.AddForce(Vector2.down * 2f, ForceMode2D.Impulse);
        }

        Collider2D col = droppedTreat.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = true;

        Invoke(nameof(AllowNextSpawn), 0.15f);
    }

    void AllowNextSpawn()
    {
        canSpawnNext = true;
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
        if (points != null && index >= 0 && index < points.Length)
            total += points[index];

        if (textField != null)
            textField.SetText("Score: " + total);
    }
}