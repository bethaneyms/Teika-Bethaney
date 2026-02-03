using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerBehavior : MonoBehaviour
{
    public float speed;
    public GameObject currenttreat;
    //public GameObject treat;
    public GameObject[] treats;
    public float offY = -0.6f;
    public int[] numbers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // for (int i = 0; i < numbers.Length; i++)
        //   {
        //     print(numbers[int]);
        //}
    }

        // Update is called once per frame
        void Update()
        {
            // int choice = Random.Range(27, 60);
            // print(choice);


            if (currenttreat != null)
            {
                Vector3 playerPos = transform.position;
                Vector3 treatOffset = new Vector3(0.0f, offY, 0.0f);
                currenttreat.transform.position = playerPos + treatOffset;
            }
            else
            {
                int choice = Random.Range(0, treats.Length);
                currenttreat = Instantiate(treats[choice], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Rigidbody2D body = currenttreat.GetComponent<Rigidbody2D>();
                body.gravityScale = 1.0f;

                Collider2D collider = currenttreat.GetComponent<Collider2D>();
                collider.enabled = true;

                currenttreat = null;
            }

            //keyboard movement of player
            float offset = 0.0f;
            if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            {
                offset = -speed;
            }

            if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            {
                offset = speed;
            }

            Vector3 newPos = transform.position;
            newPos.x = newPos.x + offset;
            transform.position = newPos;
        }
}
