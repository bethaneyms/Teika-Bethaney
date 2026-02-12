using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 0.1f;
    public GameObject currenttreat;
    public GameObject[] treats;
    public float offY = -0.6f;
    public int[] numbers;
    public float min;
    public float max;
    public int move;
    public GameObject gameOver;
    private float startTime; 

    void Start()
    {
        startTime = 0.0f;
        move = 0; 
    }

    void Update()
    {
        // 1. Handle the "Held" Item
        if (currenttreat != null)
        {
            Vector3 playerPos = transform.position;
            Vector3 treatOffset = new Vector3(0.0f, offY, 0.0f);
            currenttreat.transform.position = playerPos + treatOffset;
        }
        else
        {
            int choice = Random.Range(0, treats.Length);
            currenttreat = Instantiate(treats[choice], Vector3.zero, Quaternion.identity);
        }

        // 2. Drop Logic
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Rigidbody2D body = currenttreat.GetComponent<Rigidbody2D>();
            body.gravityScale = 1.0f;

            Collider2D col = currenttreat.GetComponent<Collider2D>();
            if (col != null) col.enabled = true;

            currenttreat = null;
        }

        // 3. Movement Logic
        float offset = 0.0f;
        
        // Simplified movement check
        bool moveLeft = (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) && move != 1;
        bool moveRight = (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) && move != 2;

        if (moveLeft)
        {
            offset = -speed;
        }
        else if (moveRight)
        {
            offset = speed;
        }

        Vector3 newPos = transform.position;
        newPos.x += offset;

        // 4. Clamping (Boundary Checks)
        // We clamp the value BEFORE applying it to the transform
        if (newPos.x > max) {
            newPos.x = max;            
        }
        if (newPos.x < min) {
            newPos.x = min;
        }

        transform.position = newPos;
    }

    // Merged the duplicate methods into one
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("player touched " + other.gameObject.name);
        // Add logic here if needed
    }
}
