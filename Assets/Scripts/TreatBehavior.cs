using UnityEngine;

public class TreatBehavior : MonoBehaviour
{
    public float timeout = 3f;
    private float timeStart = 0f;

    public GameObject[] treats;
    public int treatType;

    private bool isMerging = false;
    private PlayerBehavior player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        treats = player.treats;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Treat"))
            return;

        TreatBehavior otherTreat = other.gameObject.GetComponent<TreatBehavior>();
        if (otherTreat == null)
            return;

        if (isMerging || otherTreat.isMerging)
            return;

        if (otherTreat.treatType != treatType)
            return;

        if (treatType >= treats.Length - 1)
            return;

        // only one of the two treats handles the merge
        if (transform.position.x > other.transform.position.x)
            return;

        isMerging = true;
        otherTreat.isMerging = true;

        int nextType = treatType + 1;
        Vector3 mergePos = (transform.position + other.transform.position) / 2f;

        GameObject mergedTreat = Instantiate(treats[nextType], mergePos, Quaternion.identity);

        Rigidbody2D rb = mergedTreat.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f;
        }

        Collider2D col = mergedTreat.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = true;

        if (player != null)
            player.updateScore(treatType);

        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Top"))
        {
            timeStart = Time.time;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Top"))
        {
            if (timeStart == 0f)
                timeStart = Time.time;

            float timeSoFar = Time.time - timeStart;

            if (timeSoFar >= timeout)
            {
                if (player != null)
                    player.GameOver();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Top"))
        {
            timeStart = 0f;
        }
    }
}