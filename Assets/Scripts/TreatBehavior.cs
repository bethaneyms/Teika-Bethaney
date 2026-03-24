using UnityEngine;

public class TreatBehavior : MonoBehaviour
{
    public float timeout = 3f;
    private float timeStart = 0f;

    public GameObject[] treats;
    public int treatType;

    private bool isMerging = false;
    public bool canMerge = true;
    private PlayerBehavior player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerBehavior>();
            if (player != null && treats == null)
                treats = player.treats;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!canMerge)
            return;

        if (!other.gameObject.CompareTag("Treat"))
            return;

        TreatBehavior otherTreat = other.gameObject.GetComponent<TreatBehavior>();
        if (otherTreat == null)
            return;

        if (!otherTreat.canMerge)
            return;

        if (isMerging || otherTreat.isMerging)
            return;

        if (otherTreat.treatType != treatType)
            return;

        if (treats == null || treatType >= treats.Length - 1)
            return;

        if (gameObject.GetInstanceID() > other.gameObject.GetInstanceID())
            return;

        isMerging = true;
        otherTreat.isMerging = true;

        int nextType = treatType + 1;
        Vector3 mergePos = (transform.position + other.transform.position) / 2f;

        GameObject mergedTreat = Instantiate(treats[nextType], mergePos, Quaternion.identity);

        TreatBehavior mergedBehavior = mergedTreat.GetComponent<TreatBehavior>();
        if (mergedBehavior != null)
        {
            mergedBehavior.treatType = nextType;
            mergedBehavior.treats = treats;
            mergedBehavior.canMerge = false;
            mergedBehavior.Invoke(nameof(EnableMerge), 0.2f);
        }

        Rigidbody2D rb = mergedTreat.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.WakeUp();
        }

        Collider2D col = mergedTreat.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = true;

        if (player != null)
            player.updateScore(treatType);

        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    void EnableMerge()
    {
        canMerge = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Top"))
            timeStart = Time.time;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Top"))
        {
            float timeSoFar = Time.time - timeStart;
            if (timeSoFar >= timeout && player != null)
                player.GameOver();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Top"))
            timeStart = 0f;
    }
}