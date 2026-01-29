using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public  GameObject treat;
    public float offy = -0.6f;

    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //treat position below player
        if (currentTreat != null)
        {
            Vector3 playerPos = transform.position;
            Vector3 treatOffset = new Vector3(0.0f, offy, 0.0f);
            currentTreat.transform.position = playerPos +treatOffset;
        }
        //drop treat
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Rigidbody2D body = currentTreat.GetComponent<Rigidbody2D>();
            body.gravityScale = 1.0f;
            
            Collider2D collider = currentTreat.GetComponent<Collider2D>();
            collider.enabled = true;

            currentTreat = null;
        }

        //current player position
        float update = 0.0f;
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            Vector3 newPos = transform.position;
            newPos.x = newPos.x - speed;
            transform.position = newPos;
        }
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            Vector3 newPos = transform.position;
            newPos.x = newPos.x + speed;
            transform.position = newPos;
        }
            

    }
}
