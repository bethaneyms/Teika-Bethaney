using UnityEngine;
using UnityEngine.InputSystem;

//whatever bro
//follow the way the sprites folder to make it easier 

public class PlayerBehavior : MonoBehaviour{
    public float speed;
    private GameObject currentTreat;
    public float offY  = -0.6f;
    public float min; 
    public float max;
    public int move;

    public GameObject[] treats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){

        move =0; // 0 means you can move both ways
    }

    //int choice =  

    // Update is called once per frame
    void Update(){

        if(currentTreat != null){
            Vector3 playerPos = transform.position;
            Vector3 treatOffset = new Vector3(0.0f, offY, 0.0f);
            currentTreat.transform.position = playerPos + treatOffset;
        }
        else{
            int choice = Random.Range(0, treats.Length);
            currentTreat  = Instantiate(treats[choice], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }

        if(Keyboard.current.spaceKey.wasPressedThisFrame){
            Rigidbody2D body = currentTreat.GetComponent<Rigidbody2D>();
            body.gravityScale= 1.0f;

            Collider2D collider  = currentTreat.GetComponent<Collider2D>();
            collider.enabled = true;

            currentTreat = null;
        }

        //keyboard movement of player
        float offset = 0.0f;
        bool left = (Keyboard.current.leftArrowKey.isPressed|| Keyboard.current.aKey.isPressed) && move != 1;
        if(left == true){
            offset = -speed;
        }

        if(Keyboard.current.rightArrowKey.isPressed|| Keyboard.current.dKey.isPressed){
            offset = speed;
        }

        Vector3 newPos = transform.position;
        newPos.x = newPos.x + offset;
        
        //float startTime = 0.0f;
        if(transform.position.x > max){
            //startTime  = Time.time;
            newPos.x = max;
        }
        transform.position = newPos;


        if(transform.position.x < min){
            newPos.x = min;
        }
        transform.position = newPos;


    }
    private void OnCollisionEnter2D(Collision2D other){
    print("you touched " + other.gameObject.name);
    if (other.gameObject.CompareTag("LB")){
            move = 1; // cannot move left
        }
    }

    private void OnCollisionStay2D(Collision2D other){
    print("you are touching " + other.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D other) {
    print("you stopped " + other.gameObject.name);
    if (other.gameObject.CompareTag("LB")){
        move = 0; // can move left again
        }
    }


}
